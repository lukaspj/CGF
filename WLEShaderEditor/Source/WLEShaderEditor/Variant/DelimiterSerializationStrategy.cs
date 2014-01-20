using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Graph;
using System.IO;
using System.Drawing;
using ShaderModuleAPI;
using WLEShaderEditor.Utility;
using WLEShaderEditor.Model;

namespace WLEShaderEditor.Variant
{
   class DelimiterSerializationStrategy : WLEShaderEditor.Framework.ISerializationStrategy
   {
      #region Serialization

      public string SerializeModel(GraphModel model)
      {
         string output = "";
         foreach (Node node in model.NodeIdentifierDict.Keys)
            output += SerializeNode(node, model) + "|~|";
         output += "~|";
         foreach (NodeConnection NC in model.NodeConnections)
            output += SerializeConnection(NC, model) + "|~|";
         if (model.NodeConnections.Count > 0)
            output = output.Substring(0, output.Length - 3);
         return output;
      }

      private string SerializeNode(Node n, GraphModel model)
      {
         string retStr = "";
         retStr += String.Format("{0};{1}|", n.Location.X, n.Location.Y);
         retStr += ((IModule)n.ParentModule).Serialize(n) + "|";
         retStr += ((IModule)n.ParentModule).GetNodeName() + "|";
         retStr += model.NodeIdentifierDict[n];
         return retStr;
      }

      private string SerializeConnection(NodeConnection NC, GraphModel model)
      {
         string retStr = "";
         retStr += model.NodeIdentifierDict[NC.From.Node] + "|";
         retStr += NC.From.Item.Tag + "|";
         retStr += model.NodeIdentifierDict[NC.To.Node] + "|";
         retStr += NC.To.Item.Tag;
         return retStr;
      }

      #endregion

      #region Deserialization

      public void DeserializeFile(string file, 
         ref Dictionary<Node, int> nodeIdDict,
         ref Dictionary<int, Node> nodeIdReverseDict,
         ref List<NodeConnection> connections)
      {
         StreamReader SR = new StreamReader(file);
         string content = SR.ReadToEnd();
         SR.Close();
         string nodes = content.Split(new[] { "|~|~|" }, StringSplitOptions.None)[0];
         string conns = content.Split(new[] { "|~|~|" }, StringSplitOptions.None)[1];

         string[] nodeSplit = nodes.Split(new[] { "|~|" }, StringSplitOptions.None);
         foreach (string s in nodeSplit)
            DeserializeNode(s, ref nodeIdDict, ref nodeIdReverseDict);

         string[] connSplit = conns.Split(new[] { "|~|" }, StringSplitOptions.None);
         foreach (string s in connSplit)
            DeserializeConnection(s, ref connections, ref nodeIdReverseDict);
      }

      private Node DeserializeNode(string s, 
         ref Dictionary<Node, int> nodeIdDict, 
         ref Dictionary<int, Node> nodeIdReverseDict)
      {
         Node node;
         string[] data = s.Split('|');
         string[] splitLocation = data[0].Split(';');
         float x = float.Parse(splitLocation[0]);
         float y = float.Parse(splitLocation[1]);
         PointF loc = new PointF(x, y);
         string serializedData = data[1];
         string nodeName = data[2];
         int id = int.Parse(data[3]);
         IModule module = null;
         foreach (IModule mod in GlobalData.Modules)
         {
            if (mod.GetNodeName().Equals(nodeName))
            {
               module = mod;
               break;
            }
         }
         if (module == null)
            return null;
         node = module.Deserialize(serializedData);
         node.Location = loc;
         nodeIdDict[node] = id;
         nodeIdReverseDict[id] = node;
         return node;
      }

      private NodeConnection DeserializeConnection(string s,
         ref List<NodeConnection> connections,
         ref Dictionary<int, Node> nodeIdReverseDict)
      {
         string[] splitData = s.Split('|');
         if (splitData.Length != 4)
            return null;
         int fromID = int.Parse(splitData[0]);
         int toID = int.Parse(splitData[2]);
         Node fromNode = nodeIdReverseDict[fromID];
         Node toNode = nodeIdReverseDict[toID];
         NodeItem fromItem = null, toItem = null;
         foreach (NodeItem item in fromNode.Items)
         {
            if (item.Tag != null && item.Tag.ToString().Equals(splitData[1]))
               fromItem = item;
         }
         foreach (NodeItem item in toNode.Items)
         {
            if (item.Tag.ToString().Equals(splitData[3]))
               toItem = item;
         }

         NodeConnector fromConnector = new NodeOutputConnector(fromItem, true);
         NodeConnector toConnector = new NodeInputConnector(toItem, true);

         NodeConnection NC = new NodeConnection()
         {
            From = fromConnector,
            To = toConnector
         };

         connections.Add(NC);

         return NC;
      }
      #endregion
   }
}

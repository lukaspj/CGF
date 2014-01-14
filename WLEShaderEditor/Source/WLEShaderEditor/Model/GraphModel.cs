using System;
using System.Collections.Generic;
using System.Linq;
using Graph;
using ShaderModuleAPI;
using System.IO;
using WLEShaderEditor.Utility;
using System.Drawing;
using System.Globalization;

namespace WLEShaderEditor.Model
{
   public class GraphModel
   {
      Dictionary<Node, int> _nodeIdentifierDict;
      Dictionary<int, Node> _nodeIdentifierReverseDict;
      List<NodeConnection> _nodeConnections;

      public Dictionary<Node, int> NodeIdentifierDict
      {
         get
         {
            return _nodeIdentifierDict;
         }
      }
      public Dictionary<int, Node> NodeIdentifierReverseDict
      {
         get
         {
            return _nodeIdentifierReverseDict;
         }
      }
      public List<NodeConnection> NodeConnections
      {
         get
         {
            return _nodeConnections;
         }
      }
      
      public GraphModel()
      {
         _nodeIdentifierDict = new Dictionary<Node, int>();
         _nodeIdentifierReverseDict = new Dictionary<int, Node>();
         _nodeConnections = new List<NodeConnection>();
      }

      public GraphModel(Dictionary<Node, int> nodeIDdict, Dictionary<int, Node> nodeIdReversedDict, List<NodeConnection> conns)
      {
         _nodeIdentifierDict = nodeIDdict;
         _nodeIdentifierReverseDict = nodeIdReversedDict;
         _nodeConnections = conns;
      }

      private string SerializeNode(Node n)
      {
         string retStr = "";
         retStr += String.Format("{0};{1}|", n.Location.X, n.Location.Y);
         retStr += ((IModule)n.ParentModule).Serialize(n) + "|";
         retStr += ((IModule)n.ParentModule).GetNodeName() + "|";
         retStr += NodeIdentifierDict[n];
         return retStr;
      }

      private Node DeserializeNode(string s)
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
         node = module.Deserialize(serializedData);
         node.Location = loc;
         NodeIdentifierDict[node] = id;
         NodeIdentifierReverseDict[id] = node;
         return node;
      }

      private string SerializeConnection(NodeConnection NC)
      {
         string retStr = "";
         retStr += NodeIdentifierDict[NC.From.Node] + "|";
         retStr += NC.From.Item.Tag + "|";
         retStr += NodeIdentifierDict[NC.To.Node] + "|";
         retStr += NC.To.Item.Tag;
         return retStr;
      }

      private NodeConnection DeserializeConnection(string s)
      {
         string[] splitData = s.Split('|');
         if (splitData.Length != 4)
            return null;
         int fromID = int.Parse(splitData[0]);
         int toID = int.Parse(splitData[2]);
         Node fromNode = NodeIdentifierReverseDict[fromID];
         Node toNode = NodeIdentifierReverseDict[toID];
         NodeItem fromItem = null, toItem = null;
         foreach (NodeItem item in fromNode.Items)
         {
            if ( item.Tag != null && item.Tag.ToString().Equals(splitData[1]))
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

         NodeConnections.Add(NC);
         
         return NC;
      }

      public static GraphModel FromControls(List<Node> nodes)
      {
         Dictionary<Node, int> nodeIDdict = new Dictionary<Node, int>();
         Dictionary<int, Node> nodeIDReversedDict = new Dictionary<int, Node>();
         
         int i = 0;
         foreach (Node n in nodes)
         {
            nodeIDdict.Add(n, i);
            nodeIDReversedDict.Add(i++, n);
         }

         List<NodeConnection> connections = new List<NodeConnection>();

         foreach (Node node in nodes)
         {
            foreach (NodeConnection conn in node.Connections)
            {
               if (!connections.Contains(conn))
                  connections.Add(conn);
            }
         }
         
         GraphModel _Model = new GraphModel(nodeIDdict, nodeIDReversedDict, connections);
         return _Model;
      }

      public void SaveToFile(string file)
      {
         StreamWriter SW = new StreamWriter(file);
         string output = "";
         foreach (Node node in NodeIdentifierDict.Keys)
            output += SerializeNode(node) + "|~|";
         output += "~|";
         foreach (NodeConnection NC in NodeConnections)
            output += SerializeConnection(NC) + "|~|";
         if(NodeConnections.Count > 0)
            output = output.Substring(0, output.Length - 3);
         SW.Write(output);
         SW.Close();
      }

      public static GraphModel LoadFromFile(string file)
      {
         GraphModel model = new GraphModel();
         StreamReader SR = new StreamReader(file);
         string content = SR.ReadToEnd();
         SR.Close();
         string nodes = content.Split(new[] { "|~|~|" }, StringSplitOptions.None)[0];
         string conns = content.Split(new[] { "|~|~|" }, StringSplitOptions.None)[1];

         string[] nodeSplit = nodes.Split(new[] { "|~|" }, StringSplitOptions.None);
         foreach (string s in nodeSplit)
            model.DeserializeNode(s);

         string[] connSplit = conns.Split(new[] { "|~|" }, StringSplitOptions.None);
         foreach (string s in connSplit)
            model.DeserializeConnection(s);

         return model;
      }

      public void FillInToGraphControl(GraphControl gc)
      {
         gc.AddNodes(NodeIdentifierDict.Keys);
         foreach (NodeConnection NC in NodeConnections)
         {
            gc.Connect(NC.From.Item, NC.To.Item);
         }
      }
   }
}

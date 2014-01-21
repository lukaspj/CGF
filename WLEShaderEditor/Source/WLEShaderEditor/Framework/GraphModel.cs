using System;
using System.Collections.Generic;
using System.Linq;
using Graph;
using ShaderModuleAPI;
using System.IO;
using WLEShaderEditor.Utility;
using System.Drawing;
using System.Globalization;

using WLEShaderEditor.Framework;

namespace WLEShaderEditor.Framework
{
   public class GraphModel
   {
      Dictionary<Node, int> _nodeIdentifierDict;
      Dictionary<int, Node> _nodeIdentifierReverseDict;
      List<NodeConnection> _nodeConnections;
      List<object> _dependencyList;

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
      public List<object> DependencyList
      {
         get
         {
            return _dependencyList;
         }
      }

      IDependencyParserStrategy mDependencyParserStrategy;
      ISerializationStrategy mSerializationStrategy;
      
      public GraphModel(IDependencyParserStrategy dps, ISerializationStrategy iss)
      {
         _nodeIdentifierDict = new Dictionary<Node, int>();
         _nodeIdentifierReverseDict = new Dictionary<int, Node>();
         _nodeConnections = new List<NodeConnection>();
         _dependencyList = new List<object>();
         mDependencyParserStrategy = dps;
         mSerializationStrategy = iss;
      }

      public GraphModel(IDependencyParserStrategy dps, ISerializationStrategy iss, List<Node> nodes)
      {
         mDependencyParserStrategy = dps;
         mSerializationStrategy = iss;

         _nodeIdentifierDict = new Dictionary<Node, int>();
         _nodeIdentifierReverseDict = new Dictionary<int, Node>();
         _dependencyList = new List<object>();

         int i = 0;
         foreach (Node n in nodes)
         {
            _nodeIdentifierDict.Add(n, i);
            _nodeIdentifierReverseDict.Add(i++, n);
            object dependency = mDependencyParserStrategy.ParseDependencies(((IModule)n.ParentModule).GetDependencyObject());
            if(dependency != null && !_dependencyList.Exists(elem => elem.Equals(dependency)))
               _dependencyList.Add(dependency);
         }

         _nodeConnections = new List<NodeConnection>();

         foreach (Node node in nodes)
         {
            foreach (NodeConnection conn in node.Connections)
            {
               if (!_nodeConnections.Contains(conn))
                  _nodeConnections.Add(conn);
            }
         }
      }

      public GraphModel(IDependencyParserStrategy dps, ISerializationStrategy iss, string file)
      {
         mDependencyParserStrategy = dps;
         mSerializationStrategy = iss;

         _nodeIdentifierDict = new Dictionary<Node, int>();
         _nodeIdentifierReverseDict = new Dictionary<int, Node>();
         _nodeConnections = new List<NodeConnection>();

         iss.DeserializeFile(file, ref _nodeIdentifierDict, ref _nodeIdentifierReverseDict, ref _nodeConnections);

      }

      public void SaveToFile(string file)
      {
         StreamWriter SW = new StreamWriter(file);
         SW.Write(mSerializationStrategy.SerializeModel(this));
         SW.Close();
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

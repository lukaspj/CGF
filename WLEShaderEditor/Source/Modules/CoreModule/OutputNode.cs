using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graph;
using ShaderModuleAPI;

namespace CoreModule
{
    public class OutputNode : IModule
    {
       public Node CreateNode()
       {
          Node outputNode = new Node(GetNodeName());

          outputNode.ParentModule = this;

          return outputNode;
       }

       public string GetNodeName()
       {
          return "Output";
       }


       #region EventHandling
       public void HandleConnectionAdded(NodeConnection connection, bool input)
       {
          
       }

       public void HandleConnectionRemoved(NodeConnector fromNodeConnector, NodeConnector toNodeConnector, bool input)
       {
       }
       #endregion

       #region Serializing
       public bool IsAnOutputNode() { return true; }

       public string Serialize(Node node)
       {
          return "";
       }

       public Node Deserialize(string str)
       {
          return CreateNode();
       }
       #endregion

    }
}

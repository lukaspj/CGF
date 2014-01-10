using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graph;
using Graph.Items;
using ShaderModuleAPI;

namespace CoreModule
{
    public class PostFXPOutputNode : IModule
    {
       public Node CreateNode()
       {
          Node outputNode = new Node(GetNodeName());

          NodeItem item = new NodeLabelItem("Output", true, false, new[] { typeof(ShaderTypes.float4) }, null);
          outputNode.AddItem(item);
          outputNode.ParentModule = this;

          return outputNode;
       }

       public string GetNodeName()
       {
          return "PostFXPOutput";
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
       public string Serialize(Node node)
       {
          return "";
       }

       public Node Deserialize(string str)
       {
          return CreateNode();
       }
       #endregion

       #region Compiling
       public object GetCompiledData(Node node)
       {
          ShaderNodeDataTypes.ShaderNode shaderNode = new ShaderNodeDataTypes.ShaderNode();
          shaderNode.FunctionBodyString = "return {VARIABLE1_NAME};";
          return shaderNode;
       }

       public bool isMainInput() { return false; }
       #endregion
    }
}

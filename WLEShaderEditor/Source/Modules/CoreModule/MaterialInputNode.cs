using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graph;
using ShaderModuleAPI;

namespace CoreModule
{
    public class MaterialInputNode : IModule
    {
       public Node CreateNode()
       {
          Node outputNode = new Node(GetNodeName());

          outputNode.ParentModule = this;

          return outputNode;
       }

       public string GetNodeName()
       {
          return "Material";
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
       public object[] GetCompiledData(Node node)
       {
          ShaderNodeDataTypes.InputNodeType shaderNode = new ShaderNodeDataTypes.InputNodeType();
          shaderNode.CompiledHeaderString = "uniform sampler2D {OUTPUT1_NAME} : register({REGISTER_NUM});";
          return new[] {shaderNode};
       }

       public bool isMainInput() { return false; }
       #endregion
    }
}

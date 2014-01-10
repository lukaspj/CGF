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
    public class PostFXPInputNode : IModule
    {
       public Node CreateNode()
       {
          Node outputNode = new Node(GetNodeName());

          var hposNode = new NodeLabelItem("hpos", false, true, null, new[] { typeof(ShaderTypes.float4) });
          var uv0Node = new NodeLabelItem("uv0", false, true, null, new[] { typeof(ShaderTypes.float2) });
          var uv1Node = new NodeLabelItem("uv1", false, true, null, new[] { typeof(ShaderTypes.float2) });
          var uv2Node = new NodeLabelItem("uv2", false, true, null, new[] { typeof(ShaderTypes.float2) });
          var uv3Node = new NodeLabelItem("uv3", false, true, null, new[] { typeof(ShaderTypes.float2) });
          var wsEyeRayNode = new NodeLabelItem("wsEyeRay", false, true, null, new[] { typeof(ShaderTypes.float3) });

          outputNode.AddItem(hposNode);
          outputNode.AddItem(uv0Node);
          outputNode.AddItem(uv1Node);
          outputNode.AddItem(uv2Node);
          outputNode.AddItem(uv3Node);
          outputNode.AddItem(wsEyeRayNode);

          outputNode.ParentModule = this;
          outputNode.Tag = "IN.";

          return outputNode;
       }

       public string GetNodeName()
       {
          return "PostFXPixelShaderInput";
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
          ShaderNodeDataTypes.MainInputNodeType shaderNode = new ShaderNodeDataTypes.MainInputNodeType();
          shaderNode.returnType = "float4";
          shaderNode.postFix = "COLOR0";
          shaderNode.CompiledHeaderString =
@"struct ConnData
{
   float4 {OUTPUT1_NAME_IN_SCOPE_TAG}       : POSITION;
   float2 {OUTPUT2_NAME_IN_SCOPE_TAG}        : TEXCOORD0;
   float2 {OUTPUT3_NAME_IN_SCOPE_TAG}        : TEXCOORD1;
   float2 {OUTPUT4_NAME_IN_SCOPE_TAG}        : TEXCOORD2;
   float2 {OUTPUT5_NAME_IN_SCOPE_TAG}        : TEXCOORD3;
   float3 {OUTPUT6_NAME_IN_SCOPE_TAG}   : TEXCOORD4;
};";
          return shaderNode;
       }

       public bool isMainInput() { return true; }
       #endregion
    }
}

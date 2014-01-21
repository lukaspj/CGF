using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graph;
using Graph.Items;
using CGF;

namespace CoreModule
{
    public class PostFxPInputNode : IModule
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
          return "PostFxPInput";
       }

       public string GetCategoryPath()
       {
          return "PostFx";
       }

       public object GetDependencyObject()
       {
          return new List<string>() { "postFx.hlsl" };
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
          ShaderNodeDataTypes.MainInputNodeType shaderNode = new ShaderNodeDataTypes.MainInputNodeType();
          shaderNode.returnType = "float4";
          shaderNode.postFix = "COLOR0";
          shaderNode.CompiledHeaderString =
@"struct ConnData
{
   float4 hpos       : POSITION;
   float2 uv0        : TEXCOORD0;
   float2 uv1        : TEXCOORD1;
   float2 uv2        : TEXCOORD2;
   float2 uv3        : TEXCOORD3;
   float3 wsEyeRay   : TEXCOORD4;
};";
          KeyValuePair<object, string> posPair = new KeyValuePair<object, string>(node.Items.ElementAt(0), "IN.hpos");
          KeyValuePair<object, string> uv0Pair = new KeyValuePair<object, string>(node.Items.ElementAt(1), "IN.uv0");
          KeyValuePair<object, string> uv1Pair = new KeyValuePair<object, string>(node.Items.ElementAt(2), "IN.uv1");
          KeyValuePair<object, string> uv2Pair = new KeyValuePair<object, string>(node.Items.ElementAt(3), "IN.uv2");
          KeyValuePair<object, string> uv3Pair = new KeyValuePair<object, string>(node.Items.ElementAt(4), "IN.uv3");
          KeyValuePair<object, string> wsEyeRayPair = new KeyValuePair<object, string>(node.Items.ElementAt(5), "IN.wsEyeRay");
          return new object[] {shaderNode, posPair, uv0Pair, uv1Pair, uv2Pair, uv3Pair, wsEyeRayPair};
       }

       public bool isMainInput() { return true; }
       #endregion
    }
}

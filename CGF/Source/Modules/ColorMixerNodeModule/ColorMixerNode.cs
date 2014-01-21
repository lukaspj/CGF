using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using Graph;
using Graph.Items;
using CGF;
using T3DHLSLAPI;
using T3DHLSLAPI.Utility;

namespace ColorMixerNodeModule
{
    public class ColorMixerNode : IModule
    {
       private EventHandler<NodeItemEventArgs> color1Handler;
       private EventHandler<NodeItemEventArgs> color2Handler;

       public Node CreateNode()
       {
          var colorMixerNode = new Node(GetNodeName());
          colorMixerNode.Location = new Point(200, 50);
          var color1 = new NodeColorItem("Color 1", Color.Black, true, false, new[] { 
             typeof(ShaderTypes.float3), 
             typeof(ShaderTypes.float4) }, 
             null) { Tag = 1 };
          var color2 = new NodeColorItem("Color 2", Color.Black, true, false, new[] { 
             typeof(ShaderTypes.float3), 
             typeof(ShaderTypes.float4) }, 
             null) { Tag = 2 };
          var color3 = new NodeColorItem("Result", Color.Black, false, true, null, new [] {
             typeof(ShaderTypes.float3)}) 
             { Tag = 3 };

          colorMixerNode.AddItem(color1);
          colorMixerNode.AddItem(color2);
          colorMixerNode.AddItem(color3);

          colorMixerNode.ParentModule = this;

          return colorMixerNode;
       }

       public string GetNodeName()
       {
          return "Mix";
       }

       public string GetCategoryPath()
       {
          return "Color";
       }

       public object GetDependencyObject()
       {
          return null;
       }

       #region EventHandling
       public void HandleConnectionAdded(NodeConnection connection, bool input)
       {
          Node node;
          if (input)
             node = connection.To.Node;
          else
             node = connection.From.Node;
          var nodeItem1 = (NodeColorItem)node.Items.FirstOrDefault(item => item.Tag.Equals(1));
          var nodeItem2 = (NodeColorItem)node.Items.FirstOrDefault(item => item.Tag.Equals(2));

          if (connection.To.Item == nodeItem1)
          {
             var outItem = connection.From.Item;
             nodeItem1.Color = HelperMethods.ColorFromFloatStruct(outItem.OutputData);
             var outModule = (IModule)outItem.Node.ParentModule;
             //TODO: Should this be here?
             node.OutputChanged += color1Handler;
             if (color1Handler == null)
             {
                color1Handler = (sender, args) =>
                {
                   nodeItem1.Color = HelperMethods.ColorFromFloatStruct(outItem.OutputData);
                   SetNodeOutputColor(node, HelperMethods.MixColors(nodeItem1.Color, nodeItem2.Color));
                };
                if(input)
                  connection.From.Node.OutputChanged += color1Handler;
             }
          }
          if (connection.To.Item == nodeItem2)
          {
             var outItem = connection.From.Item;
             nodeItem2.Color = HelperMethods.ColorFromFloatStruct(outItem.OutputData);
             var outModule = (IModule)outItem.Node.ParentModule;
             node.OutputChanged += color2Handler;
             if (color2Handler == null)
             {
                color2Handler = (sender, args) =>
                {
                   nodeItem2.Color = HelperMethods.ColorFromFloatStruct(outItem.OutputData);
                   SetNodeOutputColor(node, HelperMethods.MixColors(nodeItem1.Color, nodeItem2.Color));
                };
                if (input)
                   connection.From.Node.OutputChanged += color2Handler;
             }
          }
          if(nodeItem1.Color != null && nodeItem2.Color != null)
            SetNodeOutputColor(node, HelperMethods.MixColors(nodeItem1.Color, nodeItem2.Color));
       }

       public void SetNodeOutputColor(Node node, Color color)
       {
          var nodeItem3 = (NodeColorItem)node.Items.FirstOrDefault(item => item.Tag.Equals(3));
          nodeItem3.Color = color;
          var args = new NodeItemEventArgs(nodeItem3);
          nodeItem3.OutputData = HelperMethods.Float3FromColor(color);
          node.UpdateOutput(args);
       }

       public void HandleConnectionRemoved(NodeConnector fromNodeConnector, NodeConnector toNodeConnector, bool input)
       {
          var node = input ? toNodeConnector.Node : fromNodeConnector.Node;
          var nodeItem1 = (NodeColorItem)node.Items.FirstOrDefault(item => item.Tag.Equals(1));
          var nodeItem2 = (NodeColorItem)node.Items.FirstOrDefault(item => item.Tag.Equals(2));
          if (input)
          {
             EventHandler<NodeItemEventArgs> handler;
             if (toNodeConnector.Item == nodeItem1)
             {
                handler = color1Handler;
                color1Handler = null;
                nodeItem1.Color = Color.Black;
             }
             else
             {
                handler = color2Handler;
                color2Handler = null;
                nodeItem2.Color = Color.Black;
             }
             fromNodeConnector.Node.OutputChanged -= handler;
          }
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
          ShaderNodeDataTypes.ShaderNode shaderNode = new ShaderNodeDataTypes.ShaderNode();
          //ShaderTypes.float3 f3 = (ShaderTypes.float3)node.Items.FirstOrDefault(item => item.Tag.Equals(3)).OutputData;
          NodeItem item1 = node.Items.FirstOrDefault(item => item.Tag.Equals(1));
          NodeItem item2 = node.Items.FirstOrDefault(item => item.Tag.Equals(2));
          object f1 = node.Connections.FirstOrDefault(conn => conn.To == item1.Input).From.Item.OutputData;
          object f2 = node.Connections.FirstOrDefault(conn => conn.To == item2.Input).From.Item.OutputData;
          string variable1 = "{VARIABLE1_NAME}", variable2 = "{VARIABLE2_NAME}";
          if (f1 is ShaderTypes.float3)
             variable1 = "float4({VARIABLE1_NAME}, 1)";
          if (f2 is ShaderTypes.float3)
             variable2 = "float4({VARIABLE2_NAME}, 1)";
          shaderNode.FunctionBodyString = "float4 {OUTPUT1_NAME} = lerp(" + variable1 + "," + variable2 + ",0.5);";
          return new[] {shaderNode};
       }

       public bool isMainInput() { return false; }
       #endregion
    }
}

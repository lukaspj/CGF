using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using Graph;
using Graph.Items;
using ShaderModuleAPI;
using ShaderModuleAPI.Utility;

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
             typeof(ShaderTypes.float3), 
             typeof(ShaderTypes.float4)}) 
             { Tag = 3 };

          colorMixerNode.AddItem(color1);
          colorMixerNode.AddItem(color2);
          colorMixerNode.AddItem(color3);

          colorMixerNode.ParentModule = this;

          return colorMixerNode;
       }

       public string GetNodeName()
       {
          return "ColorMixer";
       }


       #region EventHandling
       public void HandleConnectionAdded(NodeConnection connection, bool input)
       {
          NodeConnector connector;
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
          ShaderTypes.float3 outputDat = new ShaderTypes.float3()
          {
             x = color.R,
             y = color.G,
             z = color.B
          };
          nodeItem3.OutputData = color;
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
       public object GetCompiledData(Node node)
       {
          ShaderNodeDataTypes.ShaderNode shaderNode = new ShaderNodeDataTypes.ShaderNode();
          shaderNode.FunctionBodyString = "float4 {OUTPUT1_NAME} = lerp({VARIABLE1_NAME},{VARIABLE2_NAME},{VALUE}});";
          return shaderNode;
       }

       public bool isMainInput() { return false; }
       #endregion
    }
}

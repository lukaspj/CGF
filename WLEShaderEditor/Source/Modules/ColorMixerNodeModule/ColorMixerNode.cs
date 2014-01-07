using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using Graph;
using Graph.Items;
using ShaderModuleAPI;

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
          var color1 = new NodeColorItem("Color 1", Color.Black, true, false, new[] {typeof(Color)}, null) { Tag = 1 };
          var color2 = new NodeColorItem("Color 2", Color.Black, true, false, new[] {typeof(Color)}, null) { Tag = 2 };
          var color3 = new NodeColorItem("Result", Color.Black, false, true, null, new []{typeof(Color)}) { Tag = 3 };

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
          var nodeItem3 = (NodeColorItem)node.Items.FirstOrDefault(item => item.Tag.Equals(3));

          var outArgs = new NodeItemEventArgs(nodeItem3);

          if (connection.To.Item == nodeItem1)
          {
             var outItem = (NodeColorItem)connection.From.Item;
             nodeItem1.Color = outItem.Color;
             var outModule = (IModule)outItem.Node.ParentModule;
             //TODO: Should this be here?
             node.OutputChanged += color1Handler;
             if (color1Handler == null)
             {
                color1Handler = (sender, args) =>
                {
                   nodeItem1.Color = outItem.Color;
                   nodeItem3.Color = MixColors(nodeItem1.Color, nodeItem2.Color);
                   node.UpdateOutput(outArgs);
                };
                if(input)
                  connection.From.Node.OutputChanged += color1Handler;
             }
          }
          if (connection.To.Item == nodeItem2)
          {
             var outItem = (NodeColorItem)connection.From.Item;
             nodeItem2.Color = outItem.Color;
             var outModule = (IModule)outItem.Node.ParentModule;
             node.OutputChanged += color2Handler;
             if (color2Handler == null)
             {
                color2Handler = (sender, args) =>
                {
                   nodeItem2.Color = outItem.Color;
                   nodeItem3.Color = MixColors(nodeItem1.Color, nodeItem2.Color);
                   node.UpdateOutput(outArgs);
                };
                if (input)
                   connection.From.Node.OutputChanged += color2Handler;
             }
          }

          nodeItem3.Color = MixColors(nodeItem1.Color, nodeItem2.Color);
          node.UpdateOutput(outArgs);
       }

       private Color MixColors(Color color1, Color color2)
       {
          Color retColor;
          int r = Math.Min((color1.R + color2.R) / 2, 255);
          int g = Math.Min((color1.G + color2.G) / 2, 255);
          int b = Math.Min((color1.B + color2.B) / 2, 255);
          return Color.FromArgb(255, r, g, b);
       }

       public void SetNodeOutputColor(Node node, Color color)
       {
          var nodeItem3 = (NodeColorItem)node.Items.FirstOrDefault(item => item.Tag.Equals(3));
          nodeItem3.Color = color;
          var args = new NodeItemEventArgs(nodeItem3);
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

using System;
using System.Drawing;
using System.Windows.Forms;
using Graph;
using Graph.Items;
using ShaderModuleAPI;

namespace RGBNodeModule
{
   public class RGBNode : IModule
   {
      public event EventHandler<NodeItemEventArgs> OutputChanged;

      public Node CreateNode()
      {
         var colorNode = new Node(GetNodeName());
         colorNode.Location = new Point(200, 50);
         var redChannel = new NodeSliderItem("R", 64.0f, 16.0f, 0, 1.0f, 0.0f, false, false);
         var greenChannel = new NodeSliderItem("G", 64.0f, 16.0f, 0, 1.0f, 0.0f, false, false);
         var blueChannel = new NodeSliderItem("B", 64.0f, 16.0f, 0, 1.0f, 0.0f, false, false);
         var colorItem = new NodeColorItem("Color", Color.Black, false, true, null, new []{typeof(Color)}) { Tag = new Color() };

         EventHandler<NodeItemEventArgs> channelChangedDelegate = delegate(object sender, NodeItemEventArgs args)
         {
            var red = redChannel.Value;
            var blue = blueChannel.Value;
            var green = greenChannel.Value;
            colorItem.Color = Color.FromArgb((int)Math.Round(red * 255), (int)Math.Round(green * 255), (int)Math.Round(blue * 255));
            var outArgs = new NodeItemEventArgs(colorItem);
            if(OutputChanged != null)
               OutputChanged(this, outArgs);
         };
         redChannel.ValueChanged += channelChangedDelegate;
         greenChannel.ValueChanged += channelChangedDelegate;
         blueChannel.ValueChanged += channelChangedDelegate;


         colorNode.AddItem(redChannel);
         colorNode.AddItem(greenChannel);
         colorNode.AddItem(blueChannel);

         colorItem.Clicked += new EventHandler<NodeItemEventArgs>(OnColClicked);
         colorNode.AddItem(colorItem);

         colorNode.ParentModule = this;

         return colorNode;
      }

      public string GetNodeName()
      {
         return "RGB";
      }

      private void OnColClicked(object sender, NodeItemEventArgs e)
      {
         MessageBox.Show("Color");
      }

      public void HandleConnectionAdded(NodeConnection connection, bool input)
      {
      }

      public void HandleConnectionRemoved(NodeConnector fromNodeConnector, NodeConnector toNodeConnector, bool input)
      {
      }
   }
}

using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;
using Graph;
using Graph.Items;
using ShaderModuleAPI;

namespace RGBNodeModule
{
   public class RGBNode : IModule
   {
      public Node CreateNode()
      {
         var colorNode = new Node(GetNodeName());
         colorNode.Location = new Point(200, 50);
         var redChannel = new NodeSliderItem("R", 64.0f, 16.0f, 0, 1.0f, 0.0f, false, false) { Tag = "red" };
         var greenChannel = new NodeSliderItem("G", 64.0f, 16.0f, 0, 1.0f, 0.0f, false, false) { Tag = "green" };
         var blueChannel = new NodeSliderItem("B", 64.0f, 16.0f, 0, 1.0f, 0.0f, false, false) { Tag = "blue" };
         var colorItem = new NodeColorItem("Color", Color.Black, false, true, null, new[] { 
            typeof(ShaderTypes.float3) }) 
            { Tag = "out" };

         EventHandler<NodeItemEventArgs> channelChangedDelegate = delegate(object sender, NodeItemEventArgs args)
         {
            var red = redChannel.Value;
            var blue = blueChannel.Value;
            var green = greenChannel.Value;
            colorItem.Color = Color.FromArgb((int)Math.Round(red * 255), (int)Math.Round(green * 255), (int)Math.Round(blue * 255));
            colorItem.OutputData = new ShaderTypes.float3()
            {
               x = red,
               y = green,
               z = blue
            };
         };
         redChannel.ValueChanged += channelChangedDelegate;
         greenChannel.ValueChanged += channelChangedDelegate;
         blueChannel.ValueChanged += channelChangedDelegate;
         EventHandler<NodeItemEventArgs> endDragDelegate = delegate(object sender, NodeItemEventArgs args)
         {
            var outArgs = new NodeItemEventArgs(colorItem);
            args.Item.Node.UpdateOutput(outArgs);
         };
         redChannel.EndDrag += endDragDelegate;
         greenChannel.EndDrag += endDragDelegate;
         blueChannel.EndDrag += endDragDelegate;


         colorNode.AddItem(redChannel);
         colorNode.AddItem(greenChannel);
         colorNode.AddItem(blueChannel);

         colorItem.Clicked += new EventHandler<NodeItemEventArgs>(OnColClicked);
         colorNode.AddItem(colorItem);

         colorNode.ParentModule = this;

         colorItem.OutputData = new ShaderTypes.float3()
         {
            x = 0,
            y = 0,
            z = 0
         };

         return colorNode;
      }

      public string GetNodeName()
      {
         return "RGB";
      }

      #region Events
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
      #endregion

      #region Serializing
      public string Serialize(Node node)
      {
         NodeSliderItem redChannel, greenChannel, blueChannel;
         redChannel = greenChannel = blueChannel = null;
         foreach (NodeItem nItem in node.Items)
            if (nItem.Tag == "red")
               redChannel = (NodeSliderItem)nItem;
            else if (nItem.Tag == "green")
               greenChannel = (NodeSliderItem)nItem;
            else if (nItem.Tag == "blue")
               blueChannel = (NodeSliderItem)nItem;

         return String.Format("{0};{1};{2}", redChannel.Value, greenChannel.Value, blueChannel.Value);
      }

      public Node Deserialize(string str)
      {
         string[] rgb = str.Split(';');
         Node node = CreateNode();

         NodeSliderItem redChannel, greenChannel, blueChannel;
         redChannel = greenChannel = blueChannel = null;
         foreach (NodeItem nItem in node.Items)
            if (nItem.Tag == "red")
               redChannel = (NodeSliderItem)nItem;
            else if (nItem.Tag == "green")
               greenChannel = (NodeSliderItem)nItem;
            else if (nItem.Tag == "blue")
               blueChannel = (NodeSliderItem)nItem;

         redChannel.Value = float.Parse(rgb[0]);
         greenChannel.Value = float.Parse(rgb[1]);
         blueChannel.Value = float.Parse(rgb[2]);

         return node;
      }
      #endregion

      #region Compiling
      public object GetCompiledData(Node node)
      {
         ShaderNodeDataTypes.InputNodeType shaderNode = new ShaderNodeDataTypes.InputNodeType();
         ShaderTypes.float3 f3 = (ShaderTypes.float3)node.Items.FirstOrDefault(item => item.Tag == "out").OutputData;
         shaderNode.FunctionBodyString = "const float3 {OUTPUT1_NAME} = {" + f3.x.ToString(CultureInfo.InvariantCulture) + "," + 
            f3.y.ToString(CultureInfo.InvariantCulture) + "," + f3.z.ToString(CultureInfo.InvariantCulture) + "};";
         return shaderNode;
      }

      public bool isMainInput() { return false; }
      #endregion
   }
}

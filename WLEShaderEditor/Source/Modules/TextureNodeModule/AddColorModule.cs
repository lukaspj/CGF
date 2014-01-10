using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using Graph;
using Graph.Items;
using ShaderModuleAPI;

namespace TextureNodeModule
{
   public class AddColorNode : IModule
   {
      private Dictionary<Node, EventHandler<NodeItemEventArgs>> colorHandlerDict = new Dictionary<Node, EventHandler<NodeItemEventArgs>>();
      private Dictionary<Node, EventHandler<NodeItemEventArgs>> imageHandlerDict = new Dictionary<Node, EventHandler<NodeItemEventArgs>>();

      public Node CreateNode()
      {
         Node addColorNode = new Node(GetNodeName());
         addColorNode.Location = new Point(200, 50);
         var color = new NodeLabelItem("Color", true, false, new[] { 
            typeof(ShaderTypes.float3), 
            typeof(ShaderTypes.float4) }, 
            null) { Tag = 1 };
         var imageIn = new NodeLabelItem("Image", true, false, new[] { typeof(ShaderTypes.sampler2D) }, null) { Tag = 2 };
         var imageOut = new NodeImageItem(Properties.Resources.DefaultImage, 
                                          64, 
                                          64, 
                                          false, 
                                          true, 
                                          null, 
                                          new[] { typeof(Image) }) { Tag = 3 };

         addColorNode.AddItem(color);
         addColorNode.AddItem(imageIn);
         addColorNode.AddItem(imageOut);

         addColorNode.ParentModule = this;

         return addColorNode;
      }

      public string GetNodeName()
      {
         return "Add";
      }

      public void HandleConnectionAdded(NodeConnection connection, bool input)
      {
         NodeConnector connector;
         Node node;
         NodeColorItem colorFromItem = null;
         NodeImageItem imageFromItem = null;
         NodeImageItem imageOutputItem = null;

         if (input)
            node = connection.To.Node;
         else
            node = connection.From.Node;
         NodeItem colorToItem = (NodeItem)node.Items.FirstOrDefault(item => item.Tag.Equals(1));
         NodeItem imageToItem = (NodeItem)node.Items.FirstOrDefault(item => item.Tag.Equals(2));
         imageOutputItem = (NodeImageItem)node.Items.FirstOrDefault(item => item.Tag.Equals(3));

         var outArgs = new NodeItemEventArgs(imageOutputItem);

         foreach (NodeConnection conn in node.Connections)
         {
            if (conn.To.Item == null)
               continue;
            if (conn.To.Item == imageToItem)
               imageFromItem = conn.From.Item as NodeImageItem;
            if (conn.To.Item == colorToItem)
               colorFromItem = conn.From.Item as NodeColorItem;
         }

         if (!colorHandlerDict.ContainsKey(node) && colorFromItem != null && imageFromItem != null)
         {
            colorHandlerDict[node] = (sender, args) =>
            {
               if (imageFromItem != null && colorFromItem != null)
               {
                  imageOutputItem.Image = AddColor(colorFromItem.Color, imageFromItem.Image);
                  node.UpdateOutput(outArgs);
               }
            };
            colorFromItem.Node.OutputChanged += colorHandlerDict[node];
         }

         if (!imageHandlerDict.ContainsKey(node) && imageFromItem != null && colorFromItem != null)
         {
            imageHandlerDict[node] = (sender, args) =>
            {
               if (imageFromItem != null && colorFromItem != null)
               {
                  imageOutputItem.Image = AddColor(colorFromItem.Color, imageFromItem.Image);
                  node.UpdateOutput(outArgs);
               }
            };
            imageFromItem.Node.OutputChanged += imageHandlerDict[node];
         }

         if (imageFromItem != null && colorFromItem != null)
         {
            imageOutputItem.Image = AddColor(colorFromItem.Color, imageFromItem.Image);
            node.UpdateOutput(outArgs);
         }
      }

      private Image AddColor(Color color, Image image)
      {
         //You can change your new color here. Red,Green,LawnGreen any..
         Color newColor = color;
         Color actualColor;
         Bitmap originalImage = new Bitmap(image);
         //make an empty bitmap the same size as scrBitmap
         Bitmap retImage = new Bitmap(image.Width, image.Height);
         for (int i = 0; i < originalImage.Width; i++)
         {
            for (int j = 0; j < originalImage.Height; j++)
            {
               //get the pixel from the scrBitmap image
               actualColor = originalImage.GetPixel(i, j);
               int r = Math.Min((actualColor.R + color.R) / 2, 255);
               int g = Math.Min((actualColor.G + color.G) / 2, 255);
               int b = Math.Min((actualColor.B + color.B) / 2, 255);
               retImage.SetPixel(i, j, Color.FromArgb(255, r, g, b));

            }
         }

         return retImage;
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
         var nodeItem1 = (NodeLabelItem)node.Items.FirstOrDefault(item => item.Tag.Equals(1));
         var nodeItem2 = (NodeLabelItem)node.Items.FirstOrDefault(item => item.Tag.Equals(2));
         if (input)
         {
            EventHandler<NodeItemEventArgs> handler;
            if (toNodeConnector.Item == nodeItem1)
            {
               handler = colorHandlerDict[node];
               colorHandlerDict.Remove(node);
            }
            else
            {
               handler = imageHandlerDict[node];
               imageHandlerDict.Remove(node);
            }
            fromNodeConnector.Node.OutputChanged -= handler;
         }
      }

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using CGF;
using T3DHLSLAPI;
using Graph;

namespace TextureNodeModule
{
    public class TextureNode : IModule
    {
       public Node CreateNode()
       {
          Node textureNode = new Node(GetNodeName());

          NodeItem imageItem = new Graph.Items.NodeImageItem(Properties.Resources.DefaultImage, 64, 64, false, true, null, new[] { typeof(ShaderTypes.sampler2D) }) { Tag = "out" };
          textureNode.AddItem(imageItem);

          ShaderTypes.sampler2D sampler = new ShaderTypes.sampler2D();
          sampler.path = "./DefaultImage.png";
          imageItem.OutputData = sampler;
          SetImage(textureNode, sampler.path);

          textureNode.ParentModule = this;

          return textureNode;
       }

       public string GetNodeName()
       {
          return "Texture";
       }

       public string GetCategoryPath()
       {
          return "Input";
       }

       public object GetDependencyObject()
       {
          return null;
       }

       private void SetImage(Node node, string path)
       {
          Image img = new Bitmap(path);
          Graph.Items.NodeImageItem imageItem = (Graph.Items.NodeImageItem)node.Items.Where(item => item.Tag.Equals("out")).First();
          imageItem.Image = img;
       }

       #region EventHandling

       public void HandleConnectionAdded(Graph.NodeConnection connection, bool input)
       {

       }

       public void HandleConnectionRemoved(Graph.NodeConnector fromNodeConnector, Graph.NodeConnector toNodeConnector, bool input)
       {
          
       }

       public event EventHandler<Graph.NodeItemEventArgs> OutputChanged;

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

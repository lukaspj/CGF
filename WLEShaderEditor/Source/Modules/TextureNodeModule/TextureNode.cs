using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ShaderModuleAPI;
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

          textureNode.ParentModule = this;

          return textureNode;
       }

       public string GetNodeName()
       {
          return "Texture";
       }

       public void HandleConnectionAdded(Graph.NodeConnection connection, bool input)
       {

       }

       public void HandleConnectionRemoved(Graph.NodeConnector fromNodeConnector, Graph.NodeConnector toNodeConnector, bool input)
       {
          
       }

       public event EventHandler<Graph.NodeItemEventArgs> OutputChanged;

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
          ShaderNodeDataTypes.InputNodeType shaderNode = new ShaderNodeDataTypes.InputNodeType();
          shaderNode.CompiledHeaderString = "uniform sampler2D {OUTPUT1_NAME} : register({REGISTER_NUM});";
          return shaderNode;
       }

       public bool isMainInput() { return false; }
       #endregion
    }
}

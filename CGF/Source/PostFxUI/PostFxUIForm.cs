using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Graph;
using CGF;
using PostFxUI.Utility;
using PostFxUI.Variant;

using T3DHLSLAPI;
using T3DHLSLAPI.Variants;

using System.Globalization;
using System.Threading;

namespace PostFxUI
{
   public partial class PostFxUIForm : Form
   {
      Dictionary<string, string> Dependencies;

      public PostFxUIForm()
      {
         Dependencies = new Dictionary<string, string>();

         Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
         InitializeComponent();

         graphControl.CompatibilityStrategy = new TypeCompatibilityStrategy();

         foreach (var module in GlobalData.Modules)
            AddModuleToTree(module);
         graphControl.ConnectionAdded += (sender, args) =>
         {
            var fromModule = (IModule)args.Connection.From.Node.ParentModule;
            var toModule = (IModule)args.Connection.To.Node.ParentModule;
            fromModule.HandleConnectionAdded(args.Connection, false);
            toModule.HandleConnectionAdded(args.Connection, true);
         };
         graphControl.ConnectionRemoved += (sender, args) =>
         {
            var fromModule = (IModule)args.From.Node.ParentModule;
            var toModule = (IModule)args.To.Node.ParentModule;
            fromModule.HandleConnectionRemoved(args.From, args.To, false);
            toModule.HandleConnectionRemoved(args.From, args.To, true);
         };
      }

      public void AddModuleToTree(IModule module)
      {
         string[] catPath = module.GetCategoryPath().Split('/');
         if (catPath.Count() <= 0)
            return;
         NodeTreeView.BeginUpdate();
         TreeNode currentNode;
         if (NodeTreeView.Nodes.ContainsKey(catPath[0]))
            currentNode = NodeTreeView.Nodes[catPath[0]];
         else
         {
            currentNode = NodeTreeView.Nodes.Add(catPath[0]);
            currentNode.Name = catPath[0];
         }

         for (int i = 1; i < catPath.Count(); i++ )
         {
            if (currentNode.Nodes.ContainsKey(catPath[i]))
            {
               currentNode = currentNode.Nodes[catPath[i]];
               continue;
            }
            else
            {
               currentNode = NodeTreeView.Nodes.Add(catPath[i]);
               currentNode.Name = catPath[0];
            }
         }
         TreeNode newNode = currentNode.Nodes.Add(module.GetNodeName());
         newNode.Tag = module;
         newNode.Name = module.GetNodeName();
         NodeTreeView.EndUpdate();
      }

      private void NodeTreeView_MouseDown(object sender, MouseEventArgs e)
      {
         TreeNode nodeClicked = NodeTreeView.GetNodeAt(e.X, e.Y);

         if (nodeClicked != null)
         {
            IModule module = nodeClicked.Tag as IModule;
            if(module != null)
               this.DoDragDrop(module.CreateNode(), DragDropEffects.Copy);
         }
      }

      private void saveToolStripMenuItem_Click(object sender, System.EventArgs e)
      {
         GraphModel model = new GraphModel(new FileDependencyParserStrategy(), new DelimiterSerializationStrategy(), (List<Node>)graphControl.Nodes);
         model.SaveToFile("model.graph");
      }

      private void loadToolStripMenuItem_Click(object sender, System.EventArgs e)
      {
         graphControl.RemoveNodes(new List<Node>(graphControl.Nodes));
         GraphModel model = new GraphModel(new FileDependencyParserStrategy(), new DelimiterSerializationStrategy(), "model.graph");
         model.FillInToGraphControl(graphControl);
      }

      private void compileToolStripButton_Click(object sender, System.EventArgs e)
      {
         GraphModel model = new GraphModel(new FileDependencyParserStrategy(), new DelimiterSerializationStrategy(), (List<Node>)graphControl.Nodes);
         Compiler compiler = new T3DPostFxCompiler();
         ShaderOutputInfo outInfo = new ShaderOutputInfo();
         outInfo.outputFilename = "compiledFile";
         outInfo.outputPath = "../shaders/common/postFx/";
         outInfo.outputPath = "./";
         outInfo.scriptFilename = "compiledScriptFile";
         outInfo.scriptPath = "../core/scripts/client/postFx/";
         compiler.Compile(new ProgramGraph(model), outInfo);
      }

      private void dependencyToolStripButton_Click(object sender, System.EventArgs e)
      {
         GraphModel model = new GraphModel(new FileDependencyParserStrategy(), new DelimiterSerializationStrategy(), (List<Node>)graphControl.Nodes);
         List<object> dependencies = model.DependencyList;
         Dictionary<string, string> _dependencies = new Dictionary<string,string>();
         if (dependencies != null)
         {
            foreach (object obj in dependencies)
            {
               if ((obj as List<string>) != null)
               {
                  List<string> slist = obj as List<string>;
                  foreach(string s in slist)
                  {
                     if (Dependencies.Keys.Contains(s))
                        _dependencies[s] = Dependencies[s];
                     else
                        _dependencies[s] = null;
                  }
               }
            }
         }
         Dependencies = _dependencies;
         FileDependencyWindow FDW = new FileDependencyWindow(Dependencies, "./");
         FDW.Show();
      }
   }
}

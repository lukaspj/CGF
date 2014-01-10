using System;
using System.Windows.Forms;
using Graph;
using ShaderModuleAPI;
using WLEShaderEditor.Utility;
using WLEShaderEditor.Variant;

using WLEShaderEditor.Model;
using System.Collections.Generic;

namespace WLEShaderEditor
{
   public partial class EditorWindow : Form
   {
      public EditorWindow()
      {

         InitializeComponent();

         graphControl.CompatibilityStrategy = new TypeCompatibilityStrategy();

         foreach (var module in GlobalData.Modules)
         {
            var moduleLabel = new Label();
            moduleLabel.Text = module.GetNodeName();
            moduleLabel.Tag = module;
            var module1 = module;
            moduleLabel.MouseDown += (sender, e) => this.DoDragDrop(module1.CreateNode(), DragDropEffects.Copy);
            flowLayoutPanel1.Controls.Add(moduleLabel);
         }
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

      private void SaveButton_Click(object sender, EventArgs e)
      {
         GraphModel model = GraphModel.FromControls((List<Node>)graphControl.Nodes);
         model.SaveToFile("model.graph");
      }

      private void LoadButton_Click(object sender, EventArgs e)
      {
         graphControl.RemoveNodes(new List<Node>(graphControl.Nodes));
         GraphModel model = GraphModel.LoadFromFile("model.graph");
         model.FillInToGraphControl(graphControl);
      }

      private void CompileButton_Click(object sender, EventArgs e)
      {
         GraphModel model = GraphModel.FromControls((List<Node>)graphControl.Nodes);
         Compilers.Compiler compiler = new Compilers.HLSLCompiler();
         compiler.Compile(new ProgramGraph(model));
      }
   }
}

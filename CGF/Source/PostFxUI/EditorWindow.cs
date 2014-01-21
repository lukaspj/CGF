using System;
using System.Windows.Forms;
using Graph;
using CGF;
using PostFxUI.Utility;
using PostFxUI.Variant;
using T3DHLSLAPI.Variants;

using System.Collections.Generic;

using System.Globalization;
using System.Threading;

namespace PostFxUI
{
   public partial class EditorWindow : Form
   {
      public EditorWindow()
      {
         Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
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
         GraphModel model = new GraphModel(new FileDependencyParserStrategy(), 
            new DelimiterSerializationStrategy(), 
            (List<Node>)graphControl.Nodes);
         model.SaveToFile("model.graph");
      }

      private void LoadButton_Click(object sender, EventArgs e)
      {
         graphControl.RemoveNodes(new List<Node>(graphControl.Nodes));
         GraphModel model = new GraphModel(new FileDependencyParserStrategy(), new DelimiterSerializationStrategy(), "model.graph");
         model.FillInToGraphControl(graphControl);
      }

      private void CompileButton_Click(object sender, EventArgs e)
      {
         GraphModel model = new GraphModel(new FileDependencyParserStrategy(), new DelimiterSerializationStrategy(), (List<Node>)graphControl.Nodes);
         Compiler compiler = new T3DPostFxCompiler();
         ShaderOutputInfo outInfo = new ShaderOutputInfo();
         outInfo.outputFilename = "compiledFile";
         outInfo.outputPath = "output/";
         outInfo.scriptFilename = "compiledScriptFile";
         outInfo.scriptPath = "outputScript/";
         compiler.Compile(new ProgramGraph(model), outInfo);
      }
   }
}

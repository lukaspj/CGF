using System;
using System.Windows.Forms;
using Graph;
using ShaderModuleAPI;
using WLEShaderEditor.Utility;
using WLEShaderEditor.Variant;

namespace WLEShaderEditor
{
   public partial class EditorWindow : Form
   {
      private readonly GlobalData _globalData;

      public EditorWindow(GlobalData globalData)
      {
         _globalData = globalData;

         InitializeComponent();

         graphControl.CompatibilityStrategy = new TypeCompatibilityStrategy();

         foreach (var module in _globalData.Modules)
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

      private void showLabelsCheckBox_CheckedChanged(object sender, EventArgs e)
      {

      }
   }
}

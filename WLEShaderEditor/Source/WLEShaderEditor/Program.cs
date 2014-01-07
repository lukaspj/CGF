using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ModuleSystem.Utility;
using WLEShaderEditor.Utility;

namespace WLEShaderEditor
{
   static class Program
   {
      /// <summary>
      /// The main entry point for the application.
      /// </summary>
      [STAThread]
      static void Main()
      {
         var modules = ModuleLoader.LoadModules();
         GlobalData.Modules = modules;

         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         Application.Run(new EditorWindow());
      }
   }
}

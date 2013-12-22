using System.Collections.Generic;
using System.Linq;
using ShaderModuleAPI;

namespace WLEShaderEditor.Utility
{
   public class GlobalData
   {
      private readonly List<IModule> _modules;

      public GlobalData(List<IModule> modules)
      {
         _modules = modules;
      }

      public List<IModule> Modules
      {
         get { return _modules; }
      }
   }
}

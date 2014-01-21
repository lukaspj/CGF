using System.Collections.Generic;
using System.Linq;
using CGF;

namespace PostFxUI.Utility
{
   public static class GlobalData
   {
      static List<IModule> _modules;

      public static List<IModule> Modules
      {
         get 
         { 
            return _modules; 
         }
         set
         {
            _modules = value;
         }
      }
   }
}

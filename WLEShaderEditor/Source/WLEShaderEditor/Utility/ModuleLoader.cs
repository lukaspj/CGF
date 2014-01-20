using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

using ShaderModuleAPI;

namespace ModuleSystem.Utility
{
   public class ModuleLoader
   {
      public static List<IModule> LoadModules()
      {
         var ret = new List<IModule>();
         var modules = Directory.GetFiles("CGF/Modules", "*.dll");
         foreach (var module in modules)
         {
            var ass = Assembly.LoadFrom(module);
            var mod = new Module(ass);
            if (mod.HasValidModule())
               ret.AddRange(mod.GetModule());
         }
         return ret;
      }
   }

   public class Module
   {
      private Assembly _moduleAssembly;
      public Assembly ModuleAssembly
      {
         get { return _moduleAssembly; }
         set { _moduleAssembly = value; }
      }

      public Module(Assembly assembly)
      {
         ModuleAssembly = assembly;
      }

      public List<IModule> GetModule()
      {
         List<IModule> returnList = new List<IModule>();
         var types = _moduleAssembly.GetTypes();
         foreach (Type type in types)
         {
            if (typeof(IModule).IsAssignableFrom(type))
               returnList.Add((IModule)Activator.CreateInstance(type, new object[] { /* Parameters go here */ }));
         }
         return returnList;
      }

      public bool HasValidModule()
      {
         var types = new Type[] {};
         try
         {
            types = _moduleAssembly.GetTypes();
         }
         catch (ReflectionTypeLoadException e)
         {
            types = e.Types.Where(t => t != null).ToArray();
         }
         foreach (Type type in types)
         {
            if (typeof(IModule).IsAssignableFrom(type))
               return true;
         }
         return false;
      }
   }
}

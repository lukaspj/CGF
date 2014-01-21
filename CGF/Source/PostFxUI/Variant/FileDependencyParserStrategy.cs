using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WLEShaderEditor.Variant
{
   class FileDependencyParserStrategy : WLEShaderEditor.Framework.IDependencyParserStrategy
   {
      public object ParseDependencies(object dependency)
      {
         if (dependency == null || (dependency as List<string>) == null)
            return new List<string>();
         else
            return dependency;
      }
   }
}

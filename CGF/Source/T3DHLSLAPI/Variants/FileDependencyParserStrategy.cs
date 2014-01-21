using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CGF;

namespace T3DHLSLAPI.Variants
{
   public class FileDependencyParserStrategy : IDependencyParserStrategy
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

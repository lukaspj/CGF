using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CGF
{
   public interface IDependencyParserStrategy
   {
      object ParseDependencies(object dependency);
   }
}

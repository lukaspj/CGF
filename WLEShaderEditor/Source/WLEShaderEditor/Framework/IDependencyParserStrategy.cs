using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WLEShaderEditor.Framework
{
   public interface IDependencyParserStrategy
   {
      object ParseDependencies(object dependency);
   }
}

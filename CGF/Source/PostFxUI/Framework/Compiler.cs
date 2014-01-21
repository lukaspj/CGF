using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLEShaderEditor.Framework;

namespace WLEShaderEditor.Framework
{
   interface Compiler
   {
      void Compile(ProgramGraph graph, CompilerOutputInfo info);
   }
}

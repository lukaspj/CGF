using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGF
{
   public interface Compiler
   {
      void Compile(ProgramGraph graph, CompilerOutputInfo info);
   }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T3DHLSLAPI
{
   public class ShaderNodeDataTypes
   {
      public class ShaderNode
      {
         public string FunctionBodyString;
      }

      public class InputNodeType : ShaderNode
      {
         public string CompiledHeaderString;
      }

      public class MainInputNodeType : InputNodeType
      {
         public string returnType;
         public string postFix;
      }
   }
}

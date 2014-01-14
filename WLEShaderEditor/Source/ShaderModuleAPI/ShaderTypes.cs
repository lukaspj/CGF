using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShaderModuleAPI
{
   public static class ShaderTypes
   {
      public struct float4
      {
         public float x, y, z, w;
      }

      public struct float3
      {
         public float x, y, z;
      }

      public struct float2
      {
         public float x, y;
      }

      public struct sampler2D
      {
         public string path;
      }
   }
}

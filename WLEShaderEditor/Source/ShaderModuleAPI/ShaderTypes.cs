﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShaderModuleAPI
{
   public class ShaderTypes
   {
      public struct float4
      {
         float x, y, z, w;
      }

      public struct float3
      {
         float x, y, z;
      }

      public struct float2
      {
         float x, y;
      }

      public struct sampler2D
      {
         Image sampler;
      }
   }
}

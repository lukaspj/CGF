using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using CGF;

namespace CGF.Utility
{
   public static class HelperMethods
   {
      public static Color MixColors(Color color1, Color color2)
      {
         int r = color1.R + ((50 * (color2.R - color1.R)) / 100);
         int g = color1.G + ((50 * (color2.G - color1.G)) / 100);
         int b = color1.B + ((50 * (color2.B - color1.B)) / 100);
         return Color.FromArgb(255, r, g, b);
      }

      public static Color ColorFromFloatStruct(object theStruct)
      {
         if(theStruct is ShaderTypes.float3)
            return ColorFromFloat3((ShaderTypes.float3)theStruct);
         if (theStruct is ShaderTypes.float4)
            return ColorFromFloat4((ShaderTypes.float4)theStruct);
         else
            throw new NotSupportedException("Object type not supported");
      }

      public static Color ColorFromFloat3(ShaderTypes.float3 f3)
      {
         return Color.FromArgb((int)(f3.x * 255), (int)(f3.y * 255), (int)(f3.z * 255));
      }

      public static Color ColorFromFloat4(ShaderTypes.float4 f4)
      {
         return Color.FromArgb((int)(f4.x * 255), (int)(f4.y * 255), (int)(f4.z * 255));
      }

      public static ShaderTypes.float3 Float3FromColor(Color color)
      {
         return new ShaderTypes.float3()
         {
            x = color.R / 255.0f,
            y = color.G / 255.0f,
            z = color.B / 255.0f
         };
      }

      public static ShaderTypes.float4 Float4FromColor(Color color)
      {
         return new ShaderTypes.float4()
         {
            x = color.R / 255.0f,
            y = color.G / 255.0f,
            z = color.B / 255.0f,
            w = color.A / 255.0f
         };
      }
   }
}

using Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WLEShaderEditor.Framework;
using ShaderModuleAPI;
using System.IO;

namespace WLEShaderEditor.Variant
{
   class T3DPostFxCompiler : Compiler
   {
      private Compiler mCompiler;

      private Dictionary<string, int> RegisterDict;

      public void Compile(ProgramGraph graph, CompilerOutputInfo info)
      {
         if (!(info is ShaderOutputInfo))
            return;
         ShaderOutputInfo outputInfo = info as ShaderOutputInfo;
         RegisterDict = new Dictionary<string, int>();
         for(int i = 0; i < graph.getMaxDepth(); i++)
         {
            foreach (Vertex v in graph.getVerticesForLayer(i))
            {
               foreach (NodeItem outputItem in v.Data.Items.Where(item => item.Output.Enabled))
               {
                  if(outputItem.OutputData is ShaderTypes.sampler2D)
                  {
                     ShaderTypes.sampler2D Sampler = (ShaderTypes.sampler2D)outputItem.OutputData;
                     if (!RegisterDict.ContainsKey(Sampler.path))
                        RegisterDict.Add(Sampler.path, RegisterDict.Count);
                  }
               }
            }
         }
         mCompiler = new HLSLCompiler(new Dictionary<object, string>(), RegisterDict);
         WritePostFxScript(outputInfo);
         if (mCompiler == null)
            return;
         mCompiler.Compile(graph, outputInfo);
      }

      private void WritePostFxScript(ShaderOutputInfo info)
      {
         string textures = "";
         for(int i = 0; i < RegisterDict.Count; i++)
         {
            textures += "   texture[" + RegisterDict.Values.ElementAt(i) + "] = \"" + RegisterDict.Keys.ElementAt(i) + "\";\r\n";
         }

         StreamWriter SW = new StreamWriter(info.scriptPath + info.scriptFilename + ".cs");
         SW.Write(@"singleton ShaderData( PFX_" + info.outputFilename + @" )  
{     
   DXVertexShaderFile   = ""shaders/common/postFx/postFxV.hlsl"";  // bare-bones postFxV.hlsl
   DXPixelShaderFile    = """ + info.outputPath + info.outputFilename + @".hlsl"";  // new pixel shader   
   
   pixVersion = 3.0;  
};");
         SW.WriteLine();
         SW.Write(@"singleton PostEffect(" + info.outputFilename + @")  
{
   isEnabled = true;

   renderTime = ""PFXAfterDiffuse"";  

" + textures + @"
   shader = PFX_" + info.outputFilename + @";  
   stateBlock = PFX_DefaultStateBlock;    
};");
         SW.Close();
      }
   }
}

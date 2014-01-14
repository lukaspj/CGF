using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLEShaderEditor.Model;
using ShaderModuleAPI;
using System.IO;

namespace WLEShaderEditor.Compilers
{
   class HLSLCompiler : Compiler
   {
      private StreamWriter outStream;
      int currentDepth;
      Dictionary<object, string> InputDict;
      Dictionary<string, int> RegisterDict;

      public HLSLCompiler() { 
         InputDict = new Dictionary<object, string>();
      }

      public HLSLCompiler(Dictionary<object, string> inputDict, Dictionary<string, int> registerDict)
      {
         InputDict = inputDict;
         RegisterDict = registerDict;
      }

      public void Compile(ProgramGraph graph, CompilerOutputInfo info)
      {
         currentDepth = 0;
         List<Vertex> inputVertices = graph.getVerticesForLayer(0);
         Vertex main = GetMainInput(inputVertices);
         if (main == null)
            return;
         InputDict = new Dictionary<object, string>();
         outStream = new StreamWriter(info.outputPath + info.outputFilename + ".hlsl");
         WriteInputHeaders(inputVertices);
         WriteFunctionHeader(main);
         for (int i = 0; i <= graph.getMaxDepth(); i++)
         {
            WriteLayer(graph.getVerticesForLayer(i));
         }
         currentDepth--;
         WriteLine("}", IndentType.Decrease);
         outStream.Close();
      }

      private void WriteInputHeaders(List<Vertex> inputVertices)
      {
         foreach (Vertex v in inputVertices)
         {
            IModule module = v.Data.ParentModule as IModule;
            object[] compiledData = module.GetCompiledData(v.Data);
            ShaderNodeDataTypes.InputNodeType inputData
               = compiledData[0] as ShaderNodeDataTypes.InputNodeType;
            for (int i = 1; i < compiledData.Count(); i++)
            {
               if (compiledData[i] is KeyValuePair<object, string>)
               {
                  KeyValuePair<object, string> KVP = (KeyValuePair<object, string>)compiledData[i];
                  if (!InputDict.ContainsKey(KVP.Key))
                     InputDict.Add(KVP.Key, KVP.Value);
               }
            }

            if (inputData.CompiledHeaderString != null)
            {
               string HeaderString = inputData.CompiledHeaderString;
               if (HeaderString != null)
               {
                  ParseOutputs(v, ref HeaderString);
                  WriteLine(HeaderString);
               }
            }
         }
      }

      private void WriteFunctionHeader(Vertex main)
      {
         IModule module = main.Data.ParentModule as IModule;
         ShaderNodeDataTypes.MainInputNodeType inputData
            = module.GetCompiledData(main.Data)[0] as ShaderNodeDataTypes.MainInputNodeType;
         string postFix = "";
         if (inputData.postFix != null)
            postFix = " : " + inputData.postFix;
         WriteLine(inputData.returnType + " main(ConnData IN)" + postFix);
         WriteLine("{", IndentType.Increase);
      }

      private void WriteLayer(List<Vertex> list)
      {
         foreach (Vertex v in list)
         {
            IModule module = v.Data.ParentModule as IModule;
            object[] compiledData = module.GetCompiledData(v.Data);
            ShaderNodeDataTypes.ShaderNode inputData
               = compiledData[0] as ShaderNodeDataTypes.ShaderNode;
            for (int i = 1; i < compiledData.Count(); i++ )
            {
               if(compiledData[i] is KeyValuePair<object, string>)
               {
                  KeyValuePair<object, string> KVP = (KeyValuePair<object, string>)compiledData[i];
                  if (!InputDict.ContainsKey(KVP.Key))
                     InputDict.Add(KVP.Key, KVP.Value);
               }
            }

            if (inputData.FunctionBodyString != null)
            {
               string BodyString = inputData.FunctionBodyString;
               ParseVariables(v, ref BodyString);
               ParseOutputs(v, ref BodyString);
               WriteLine(BodyString);
            }
         }
      }

      private void ParseVariables(Vertex v, ref string BodyString)
      {
         int i = 0;
         foreach (object outputItem in v.Data.Items.Where(item => item.Input.Enabled))
         {
            Edge input = v.EdgesIn[i];
            BodyString = BodyString.Replace("{VARIABLE" + (i + 1) + "_NAME}",
               InputDict[input.FromItem]);
            i++;
         }
      }

      private void ParseOutputs(Vertex v, ref string BodyString)
      {
         int i = 0;
         foreach (object outputItem in v.Data.Items.Where(item => item.Output.Enabled))
         {
            EnsureVariableIsRegistered(v, BodyString, i, outputItem);
            BodyString = BodyString.Replace("{OUTPUT" + (i + 1) + "_NAME}",
               InputDict[outputItem]);
            BodyString = BodyString.Replace("{OUTPUT" + (i + 1) + "_NAME_IN_SCOPE_TAG}",
               InputDict[outputItem]);
            i++;
         }
      }

      private void EnsureVariableIsRegistered(Vertex v, string BodyString, int i, object var)
      {
         if (!InputDict.ContainsKey(var))
         {
            if (BodyString.Contains("{OUTPUT" + (i + 1) + "_NAME_IN_SCOPE_TAG}"))
               BodyString = BodyString.Replace("{OUTPUT" + (i + 1) + "_NAME_IN_SCOPE_TAG}",
                  InputDict[var] = v.Data.Tag + "Var" + InputDict.Count);
            else if (BodyString.Contains("{OUTPUT" + (i + 1) + "_NAME}"))
                  InputDict[var] = "Var" + InputDict.Count;
            else
               throw new NotSupportedException("Was unable to parse output macro from: " + BodyString);
         }
      }

      private string GetTabsForCurrentDepth()
      {
         string ret = "";
         for(int i = 0; i < currentDepth; i++)
            ret += "   ";
         return ret;
      }

      private void WriteLine(string line, IndentType indentType = IndentType.None)
      {
         outStream.WriteLine(GetTabsForCurrentDepth() + line);
         switch(indentType)
         {
            case IndentType.Increase:
               currentDepth++;
               break;
            case IndentType.Decrease:
               currentDepth--;
               break;
         }
      }

      enum IndentType
      {
         None,
         Increase,
         Decrease
      }

      private Vertex GetMainInput(List<Vertex> inputVertices)
      {
         foreach (Vertex v in inputVertices)
            if (((IModule)v.Data.ParentModule).isMainInput())
               return v;
         return null;
      }

   }
}

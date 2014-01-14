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
      Dictionary<object, int> RegisterDict;

      public void Compile(ProgramGraph graph)
      {
         currentDepth = 0;
         List<Vertex> inputVertices = graph.getVerticesForLayer(0);
         Vertex main = GetMainInput(inputVertices);
         if (main == null)
            return;
         InputDict = new Dictionary<object, string>();
         outStream = new StreamWriter("compiledFile.hlsl");
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
            ShaderNodeDataTypes.InputNodeType inputData 
               = module.GetCompiledData(v.Data)[0] as ShaderNodeDataTypes.InputNodeType;
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
            ShaderNodeDataTypes.ShaderNode inputData
               = module.GetCompiledData(v.Data)[0] as ShaderNodeDataTypes.ShaderNode;
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
         for (int i = 0; i < v.EdgesIn.Count; i++)
         {
            Edge input = v.EdgesIn[i];
            EnsureVariableIsRegistered(v, BodyString, i, input);
            BodyString = BodyString.Replace("{VARIABLE" + (i + 1) + "_NAME}",
               InputDict[input.FromItem]);
         }
      }

      private void ParseOutputs(Vertex v, ref string BodyString)
      {
         int i = 0;
         foreach (object outputItem in v.Data.Items.Where(item => item.Output.Enabled))
         {
            if (BodyString.Contains("{OUTPUT" + (i + 1) + "_NAME_IN_SCOPE_TAG}")) { 
               BodyString = BodyString.Replace("{OUTPUT" + (i + 1) + "_NAME_IN_SCOPE_TAG}",
                  "Var" + InputDict.Count);
               InputDict[outputItem] = v.Data.Tag + "Var" + InputDict.Count;
            }
            else
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
            if (BodyString.Contains("{OUTPUT" + (i + 1) + "_NAME}"))
               InputDict[var] = "Var" + InputDict.Count;
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

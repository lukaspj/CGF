﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graph;
using CGF;
using T3DHLSLAPI;

namespace CoreModule
{
    public class OutputNode : IModule
    {
       public Node CreateNode()
       {
          Node outputNode = new Node(GetNodeName());

          outputNode.ParentModule = this;

          return outputNode;
       }

       public string GetNodeName()
       {
          return "Output";
       }

       public string GetCategoryPath()
       {
          return "Output";
       }

       public object GetDependencyObject()
       {
          return null;
       }

       #region EventHandling
       public void HandleConnectionAdded(NodeConnection connection, bool input)
       {
          
       }

       public void HandleConnectionRemoved(NodeConnector fromNodeConnector, NodeConnector toNodeConnector, bool input)
       {
       }
       #endregion

       #region Serializing
       public string Serialize(Node node)
       {
          return "";
       }

       public Node Deserialize(string str)
       {
          return CreateNode();
       }
       #endregion

       #region Compiling
       public object[] GetCompiledData(Node node)
       {
          ShaderNodeDataTypes.ShaderNode shaderNode = new ShaderNodeDataTypes.ShaderNode();
          shaderNode.FunctionBodyString = "return {VARIABLE1_NAME};";
          return new[] {shaderNode};
       }

       public bool isMainInput() { return false; }
       #endregion
    }
}

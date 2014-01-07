using System;
using Graph;

namespace ShaderModuleAPI
{
    public interface IModule
    {
       Node CreateNode();
       string GetNodeName();
       string Serialize(Node node);
       Node Deserialize(string module);
       bool IsAnOutputNode();
       void HandleConnectionAdded(NodeConnection connection, bool input);
       void HandleConnectionRemoved(NodeConnector fromNodeConnector, NodeConnector toNodeConnector, bool input);
    }
}

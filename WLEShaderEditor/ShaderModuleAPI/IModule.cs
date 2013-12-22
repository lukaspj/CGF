using System;
using Graph;

namespace ShaderModuleAPI
{
    public interface IModule
    {
       Node CreateNode();
       string GetNodeName();
       void HandleConnectionAdded(NodeConnection connection, bool input);
       void HandleConnectionRemoved(NodeConnector fromNodeConnector, NodeConnector toNodeConnector, bool input);
       event EventHandler<NodeItemEventArgs> OutputChanged;
    }
}

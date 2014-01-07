using System.Collections.Generic;
using System.Linq;

namespace Graph
{
	public class NodeSelection : IElement
	{
		public NodeSelection(IEnumerable<Node> nodes) { Nodes = nodes.ToArray(); }
		public ElementType ElementType { get { return ElementType.NodeSelection; } }
		public readonly Node[] Nodes;
	}
}

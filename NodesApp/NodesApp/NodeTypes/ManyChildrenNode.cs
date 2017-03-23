using System.Collections.Generic;

namespace NodesApp.NodeTypes
{
    public class ManyChildrenNode : Node
    {
        public IEnumerable<Node> Children { get; }
        public ManyChildrenNode(string name, params Node[] children) : base(name)
        {
            Children = children;
        }
    }
}

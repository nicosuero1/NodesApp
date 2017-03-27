using System.Collections.Generic;

namespace NodesApp.NodeTypes
{
    public class ManyChildrenNode : Node
    {
        // Children
        public IEnumerable<Node> Children { get; }

        //Constructor
        public ManyChildrenNode(string name, params Node[] children) : base(name)
        {
            Children = children;
        }
    }
}

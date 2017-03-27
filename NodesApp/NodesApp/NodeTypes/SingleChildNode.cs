namespace NodesApp.NodeTypes
{
    public class SingleChildNode : Node
    {
        // Only child
        public Node Child { get; }

        // Constructor
        public SingleChildNode(string name, Node child) : base(name)
        {
            Child = child;
        }
    }
}

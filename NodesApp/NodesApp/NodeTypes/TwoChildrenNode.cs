
namespace NodesApp.NodeTypes
{
    public class TwoChildrenNode : Node
    {
        // First child
        public Node FirstChild { get; }

        // Second child
        public Node SecondChild { get; }

        // Constructor
        public TwoChildrenNode(string name, Node first, Node second) : base(name)
        {
            FirstChild = first;
            SecondChild = second;
        }
    }
}

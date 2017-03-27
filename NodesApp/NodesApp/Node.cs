
namespace NodesApp
{
    public abstract class Node
    {
        // Name of the node
        public string Name { get; }

        // Constructor
        protected Node(string name)
        {
            Name = name;
        }
    }
}

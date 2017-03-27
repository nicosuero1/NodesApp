
namespace NodesApp.NodeOperations
{
    public interface INodeDescriber
    {
        /// <summary>
        /// It outputs a C# description of how to create the tree of nodes
        /// </summary>
        /// <param name="node">An implementation of Node Class</param>
        /// <returns> The output have node per line, indented with four spaces per nesting level</returns>
        string Describe(Node node);
    }
}


namespace NodesApp.NodeOperations
{
    public interface INodeTransformer
    {
        /// <summary>
        /// It transforms a tree of nodes into a matching tree that uses the correct node types
        /// </summary>
        /// <param name="node">An implementation of Node Class</param>
        /// <returns>New node with correct node type, matching with the parameter node</returns>
        Node Transform(Node node);
    }
}

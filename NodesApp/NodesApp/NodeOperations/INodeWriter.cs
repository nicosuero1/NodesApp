using System.Threading.Tasks;

namespace NodesApp.NodeOperations
{
    public interface INodeWriter
    {
        /// <summary>
        /// Return a task that writes string representation into the specified file
        /// </summary>
        /// <param name="node">INodeDescriber where I will take the representation string</param>
        /// <param name="filePath">Path to file</param>
        /// <returns>Task to be executed</returns>
        Task WriteToFileAsync(Node node, string filePath);
    }
}

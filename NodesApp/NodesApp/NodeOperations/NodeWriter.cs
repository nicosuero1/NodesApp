using System.IO;
using System.Threading.Tasks;

namespace NodesApp.NodeOperations
{
    /// <summary>
    /// Implements the INodeWriter interface to write the content in a specific file
    /// </summary>
    public class NodeWriter : INodeWriter
    {
        // Describer
        private readonly INodeDescriber describer;

        // Constructor
        public NodeWriter(INodeDescriber des)
        {
            describer = des;
        }

        // Returns task that writes describer result in a file
        public Task WriteToFileAsync(Node node, string filePath)
        {
            return new Task(() => File.WriteAllText(filePath, describer.Describe(node)));
        }
    }
}

using NodesApp.Helpers;
using NodesApp.NodeTypes;
using System;
using System.Text;

namespace NodesApp.NodeOperations
{
    /// <summary>
    /// Implements Description of each type of node
    /// </summary>
    public class NodeDescriber : INodeDescriber
    {
        // Returns a description of how to create the tree of node in C#
        public string Describe(Node node)
        {
            string type = node.GetType().Name.ToString();
            switch (type)
            {
                case "NoChildrenNode": return DescribeNoChildrenNode(node as NoChildrenNode);
                case "SingleChildNode": return DescribeSingleChildNode(node as SingleChildNode);
                case "TwoChildrenNode": return DescribeTwoChildrenNode(node as TwoChildrenNode);
                case "ManyChildrenNode": return DescribeManyChildrenNode(node as ManyChildrenNode);
                default: throw new Exception("Node type not suported");
            }
        }

        private string DescribeNoChildrenNode(NoChildrenNode node)
        {
            return "new NoChildrenNode(\"" + node.Name + "\")";
        }
        private string DescribeSingleChildNode(SingleChildNode node)
        {
            return "new SingleChildNode(\"" + node.Name + "\"," 
                + Environment.NewLine
                + StringHelper.AddIdentation4(Describe(node.Child))
                +")";
        }
        private string DescribeTwoChildrenNode(TwoChildrenNode node)
        {
            return "new TwoChildrenNode(\"" + node.Name + "\"," 
                + Environment.NewLine
                + StringHelper.AddIdentation4(Describe(node.FirstChild)) + ","
                + Environment.NewLine
                + StringHelper.AddIdentation4(Describe(node.SecondChild))
                +")";
        }
        private string DescribeManyChildrenNode(ManyChildrenNode node)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("new ManyChildrenNode(\"" + node.Name + "\",");
            foreach (Node n in node.Children)
            {
                sb.Append(Environment.NewLine);
                sb.Append(StringHelper.AddIdentation4(Describe(n)) + ",");
            }
            string result = sb.ToString().TrimEnd(',') + ")";
            return result;
        }
    }
}

using NodesApp.NodeTypes;
using System.Collections.Generic;
using System.Linq;

namespace NodesApp.NodeOperations
{
    public class Transformer : INodeTransformer
    {
        public Node Transform(Node node)
        {
            string type = node.GetType().Name.ToString();
            switch (type)
            {
                case "NoChildrenNode": return TransformNoChildrenNode(node as NoChildrenNode);
                case "SingleChildNode": return TransformSingleChildNode(node as SingleChildNode);
                case "TwoChildrenNode": return TransformTwoChildrenNode(node as TwoChildrenNode);
                case "ManyChildrenNode": return TransformManyChildrenNode(node as ManyChildrenNode);
                default: return null;
            }
        }

        private Node TransformNoChildrenNode(NoChildrenNode node)
        {
            return node;
        }
        private Node TransformSingleChildNode(SingleChildNode node)
        {
            if (node.Child == null) return new NoChildrenNode(node.Name);
            return node;
        }
        private Node TransformTwoChildrenNode(TwoChildrenNode node)
        {
            if (node.FirstChild != null && node.SecondChild != null) return node;
            if (node.FirstChild == null && node.SecondChild == null) return new NoChildrenNode(node.Name);
            if (node.FirstChild != null) return new SingleChildNode(node.Name, Transform(node.FirstChild));
            return new SingleChildNode(node.Name, Transform(node.SecondChild));
        }
        private Node TransformManyChildrenNode(ManyChildrenNode node)
        {
            List<Node> noNullNodes = node.Children.Where(n => n != null).ToList();
            int NoNullNodesCount = noNullNodes.Count();
            switch (NoNullNodesCount)
            {
                case 0:
                    return new NoChildrenNode(node.Name);
                case 1:
                    return new SingleChildNode(node.Name, Transform(noNullNodes[0]));
                case 2:
                    return new TwoChildrenNode(node.Name, Transform(noNullNodes[0]), Transform(noNullNodes[1]));
                default:
                    //ManyChildrenNode newNode = n
                    List<Node> nodes = new List<Node>();
                    foreach(Node n in noNullNodes)
                    {
                        nodes.Add(Transform(n));
                    }
                    return new ManyChildrenNode(node.Name, nodes.ToArray());

            }
        }
    }
}

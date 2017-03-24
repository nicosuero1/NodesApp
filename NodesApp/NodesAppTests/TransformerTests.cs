using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NodesApp.NodeOperations;
using NodesApp.NodeTypes;

namespace NodesAppTests
{
    /// <summary>
    /// Implement test methods for Transformer Class
    /// </summary>
    [TestClass]
    public class TransformerTests
    {
        [TestMethod]
        public void TransformSingleChildNode_NoChild()
        {
            INodeTransformer implementation = new Tranformer();
            var testData = new SingleChildNode("root", null);
            var result = implementation.Transform(testData);
            string expected = "new NoChildrenNode(\"root\")";
            Assert.AreEqual(expected, result, "Single Child Node without child not transformed correctly");
        }
        [TestMethod]
        public void TransformTwoChildrenNode_NoChild()
        {
            INodeTransformer implementation = new Tranformer();
            var testData = new TwoChildrenNode("root", null
                                                     ,null);
            var result = implementation.Transform(testData);
            string expected = "new NoChildrenNode(\"root\")";
            Assert.AreEqual(expected, result, "Two Children Node without child not transformed correctly");
        }
        [TestMethod]
        public void TransformTwoChildrenNode_SingleChild()
        {
            INodeTransformer implementation = new Tranformer();
            var testData = new TwoChildrenNode("root", new NoChildrenNode("child")
                                                     , null);
            var result = implementation.Transform(testData);
            string expected = "new SingleChildNode(\"root\","
                + Environment.NewLine
                + "    new NoChildrenNode(\"child\"))";
            Assert.AreEqual(expected, result, "Two Children Node with 1 child not transformed correctly");
        }
        [TestMethod]
        public void TransformManyChildrenNode_NoChild()
        {
            INodeTransformer implementation = new Tranformer();
            var testData = new ManyChildrenNode("root", null
                                                      , null
                                                      , null);
            var result = implementation.Transform(testData);
            string expected = "new NoChildrenNode(\"root\")";
            Assert.AreEqual(expected, result, "Many Children Node without  child not transformed correctly");
        }
        [TestMethod]
        public void TransformManyChildrenNode_SingleChild()
        {
            INodeTransformer implementation = new Tranformer();
            var testData = new ManyChildrenNode("root", new NoChildrenNode("child")
                                                      , null
                                                      , null);
            var result = implementation.Transform(testData);
            string expected = "new SingleChildNode(\"root\","
               + Environment.NewLine
               + "    new NoChildrenNode(\"child\"))";
            Assert.AreEqual(expected, result, "Many Children Node with 1 child not transformed correctly");
        }
        [TestMethod]
        public void TransformManyChildrenNode_TwoChild()
        {
            INodeTransformer implementation = new Tranformer();
            var testData = new ManyChildrenNode("root", new NoChildrenNode("child1")
                                                      , new NoChildrenNode("child2")
                                                      , null);
            var result = implementation.Transform(testData);
            string expected = "new TwoChildrenNode(\"root\","
                 + Environment.NewLine
                 + "    new NoChildrenNode(\"child1\"),"
                 + Environment.NewLine
                 + "    new NoChildrenNode(\"child2\"))";
            Assert.AreEqual(expected, result, "Many Children Node with 2 child not transformed correctly");
        }
    }
}

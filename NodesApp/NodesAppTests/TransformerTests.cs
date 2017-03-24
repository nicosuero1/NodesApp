using System.Linq;
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
            INodeTransformer implementation = new Transformer();
            var testData = new SingleChildNode("root", null);
            var result = implementation.Transform(testData);
            NoChildrenNode expected = new NoChildrenNode("root");
            Assert.AreEqual(expected.GetType(), result.GetType(), "Single Child Node without child not transformed correctly (type)");
            Assert.AreEqual(expected.Name, result.Name, "Single Child Node without child not transformed correctly (Name property)");
        }
        [TestMethod]
        public void TransformTwoChildrenNode_NoChild()
        {
            INodeTransformer implementation = new Transformer();
            var testData = new TwoChildrenNode("root", null
                                                     ,null);
            var result = implementation.Transform(testData);
            NoChildrenNode expected = new NoChildrenNode("root");
            Assert.AreEqual(expected.GetType(), result.GetType(), "Two Children Node without child not transformed correctly (type)");
            Assert.AreEqual(expected.Name, result.Name, "Two Children Node without child not transformed correctly (Name property)");
        }
        [TestMethod]
        public void TransformTwoChildrenNode_SingleChild()
        {
            INodeTransformer implementation = new Transformer();
            NoChildrenNode child = new NoChildrenNode("child");
            var testData = new TwoChildrenNode("root", child
                                                     , null);
            var result = implementation.Transform(testData);
            SingleChildNode expected = new SingleChildNode("root", child);
            Assert.AreEqual(expected.GetType(), result.GetType(), "Two Children Node with 1 child not transformed correctly (type)");
            Assert.AreEqual(expected.Name, result.Name, "Two Children Node with 1 child not transformed correctly (Name property)");
            Assert.AreEqual(expected.Child, ((SingleChildNode)result).Child, "Two Children Node with 1 child not transformed correctly (Child property)");
        }
        [TestMethod]
        public void TransformManyChildrenNode_NoChild()
        {
            INodeTransformer implementation = new Transformer();
            var testData = new ManyChildrenNode("root", null
                                                      , null
                                                      , null);
            var result = implementation.Transform(testData);
            NoChildrenNode expected = new NoChildrenNode("root");
            Assert.AreEqual(expected.GetType(), result.GetType(), "Many Children Node without child not transformed correctly (type)");
            Assert.AreEqual(expected.Name, result.Name, "Many Children Node without child not transformed correctly (Name property)");
        }
        [TestMethod]
        public void TransformManyChildrenNode_SingleChild()
        {
            INodeTransformer implementation = new Transformer();
            NoChildrenNode child = new NoChildrenNode("child");
            var testData = new ManyChildrenNode("root", child
                                                      , null
                                                      , null);
            var result = implementation.Transform(testData);
            SingleChildNode expected = new SingleChildNode("root", child);
            Assert.AreEqual(expected.GetType(), result.GetType(), "Many Children Node with 1 child not transformed correctly (type)");
            Assert.AreEqual(expected.Name, result.Name, "Many Children Node with 1 child not transformed correctly (Name property)");
            Assert.AreEqual(expected.Child, ((SingleChildNode)result).Child, "Many Children Node with 1 child not transformed correctly (Child property)");
        }
        [TestMethod]
        public void TransformManyChildrenNode_TwoChild()
        {
            INodeTransformer implementation = new Transformer();
            NoChildrenNode child1 = new NoChildrenNode("child1");
            NoChildrenNode child2 = new NoChildrenNode("child2");
            var testData = new ManyChildrenNode("root", child1
                                                      , child2
                                                      , null);
            var result = implementation.Transform(testData);
            TwoChildrenNode expected = new TwoChildrenNode("root", child1, child2);
            Assert.AreEqual(expected.GetType(), result.GetType(), "Many Children Node with 2 child not transformed correctly (type)");
            Assert.AreEqual(expected.Name, result.Name, "Many Children Node with 2 child not transformed correctly (Name property)");
            Assert.AreEqual(expected.FirstChild, ((TwoChildrenNode)result).FirstChild, "Many Children Node with 2 child not transformed correctly (First child)");
            Assert.AreEqual(expected.SecondChild, ((TwoChildrenNode)result).SecondChild, "Many Children Node with 2 child not transformed correctly (Second child)");
        }

        public void TransformManyChildrenNode_ManyChildren()
        {
            INodeTransformer implementation = new Transformer();
            NoChildrenNode child1 = new NoChildrenNode("child1");
            NoChildrenNode child2 = new NoChildrenNode("child2");
            NoChildrenNode child3 = new NoChildrenNode("child3");
            var testData = new ManyChildrenNode("root", child1
                                                      , child2
                                                      , child3
                                                      , null
                                                      , null);
            var result = implementation.Transform(testData);
            ManyChildrenNode expected = new ManyChildrenNode("root", child1
                                                                   , child2
                                                                   , child3);
            Assert.AreEqual(expected.GetType(), result.GetType(), "Many Children Node with 2 child not transformed correctly (type)");
            Assert.AreEqual(expected.Name, result.Name, "Many Children Node with 2 child not transformed correctly (Name property)");
            Assert.AreEqual(expected.Children.Count(), ((ManyChildrenNode)result).Children.Count());
        }
        [TestMethod]
        public void NestedTranformer()
        {
            INodeTransformer implementation = new Transformer();
            var testData = new ManyChildrenNode("root",
            new ManyChildrenNode("child1",
            new ManyChildrenNode("leaf1"),
            new ManyChildrenNode("child2",
            new ManyChildrenNode("leaf2"))));
            var result = implementation.Transform(testData);

            //SingleChildNode expected = new SingleChildNode("root",
            // new TwoChildrenNode("child1",
            // new NoChildrenNode("leaf1"),
            // new SingleChildNode("child2",
            // new NoChildrenNode("leaf2"))));

            Assert.AreEqual(typeof(SingleChildNode), result.GetType(), "Nested transformer not pass: root type");
            Assert.AreEqual(typeof(TwoChildrenNode), ((SingleChildNode)result).Child.GetType(), "Nested transformer not pass: child1 type");
            Assert.AreEqual(typeof(NoChildrenNode), ((TwoChildrenNode)((SingleChildNode)result).Child).FirstChild.GetType(), "Nested transformer not pass: leaf1 type");
            Assert.AreEqual(typeof(SingleChildNode), ((TwoChildrenNode)((SingleChildNode)result).Child).SecondChild.GetType(), "Nested transformer not pass: child2 type");
            Assert.AreEqual(typeof(NoChildrenNode), ((SingleChildNode)((TwoChildrenNode)((SingleChildNode)result).Child).SecondChild).Child.GetType(), "Nested transformer not pass: leaf2 type");

            Assert.AreEqual("root", result.Name, "Nested transformer not pass: root name");
            Assert.AreEqual("child1", ((SingleChildNode)result).Child.Name, "Nested transformer not pass: child1 name");
            Assert.AreEqual("leaf1", ((TwoChildrenNode)((SingleChildNode)result).Child).FirstChild.Name, "Nested transformer not pass: leaf1 name");
            Assert.AreEqual("child2", ((TwoChildrenNode)((SingleChildNode)result).Child).SecondChild.Name, "Nested transformer not pass: child2 name");
            Assert.AreEqual("leaf2", ((SingleChildNode)((TwoChildrenNode)((SingleChildNode)result).Child).SecondChild).Child.Name, "Nested transformer not pass: leaf2 name");

        }
    }
}

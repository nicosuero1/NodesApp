using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NodesApp.NodeOperations;
using NodesApp.NodeTypes;
using Castle.Windsor;

namespace NodesAppTests
{
    /// <summary>
    /// Implement test methods for Transformer Class
    /// </summary>
    [TestClass]
    public class TransformerTests
    {
        private static INodeTransformer transformer;
        private static IWindsorContainer container;

        #region Class Initialize-Cleanup
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            // Create windsor container
            container = new WindsorContainer("windsorconfig.xml");

            // Create Resolve instance INodeTransformer
            transformer = container.Resolve<INodeTransformer>();
        }
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            container.Dispose();
        }
        #endregion

        #region TestMethods
        /// <summary>
        /// Tests if a SingleChildNode with no children is Transformed correctly to a NoChildNode
        /// </summary>
        [TestMethod]
        public void TransformSingleChildNode_NoChild()
        {
            var testData = new SingleChildNode("root", null);
            var result = transformer.Transform(testData);
            NoChildrenNode expected = new NoChildrenNode("root");
            Assert.AreEqual(expected.GetType(), result.GetType(), "Single Child Node without child not transformed correctly (type)");
            Assert.AreEqual(expected.Name, result.Name, "Single Child Node without child not transformed correctly (Name property)");
        }

        /// <summary>
        /// Tests if a TwoChildrenNode with no children is Transformed correctly to a NoChildNode
        /// </summary>
        [TestMethod]
        public void TransformTwoChildrenNode_NoChild()
        {
            var testData = new TwoChildrenNode("root", null
                                                     ,null);
            var result = transformer.Transform(testData);
            NoChildrenNode expected = new NoChildrenNode("root");
            Assert.AreEqual(expected.GetType(), result.GetType(), "Two Children Node without child not transformed correctly (type)");
            Assert.AreEqual(expected.Name, result.Name, "Two Children Node without child not transformed correctly (Name property)");
        }

        /// <summary>
        /// Tests if a TwoChildrenNode with one children is Transformed correctly to a SingleChildNode
        /// </summary>
        [TestMethod]
        public void TransformTwoChildrenNode_SingleChild()
        {
            NoChildrenNode child = new NoChildrenNode("child");
            var testData = new TwoChildrenNode("root", child
                                                     , null);
            var result = transformer.Transform(testData);
            SingleChildNode expected = new SingleChildNode("root", child);
            Assert.AreEqual(expected.GetType(), result.GetType(), "Two Children Node with 1 child not transformed correctly (type)");
            Assert.AreEqual(expected.Name, result.Name, "Two Children Node with 1 child not transformed correctly (Name property)");
            Assert.AreEqual(expected.Child, ((SingleChildNode)result).Child, "Two Children Node with 1 child not transformed correctly (Child property)");
        }

        /// <summary>
        /// Tests if a ManyChildrenNode with no children is Transformed correctly to a NoChildNode
        /// </summary>
        [TestMethod]
        public void TransformManyChildrenNode_NoChild()
        {
            var testData = new ManyChildrenNode("root", null
                                                      , null
                                                      , null);
            var result = transformer.Transform(testData);
            NoChildrenNode expected = new NoChildrenNode("root");
            Assert.AreEqual(expected.GetType(), result.GetType(), "Many Children Node without child not transformed correctly (type)");
            Assert.AreEqual(expected.Name, result.Name, "Many Children Node without child not transformed correctly (Name property)");
        }

        /// <summary>
        /// Tests if a ManyChildrenNode with one children is Transformed correctly to a SingleChildNode
        /// </summary>
        [TestMethod]
        public void TransformManyChildrenNode_SingleChild()
        {
            NoChildrenNode child = new NoChildrenNode("child");
            var testData = new ManyChildrenNode("root", child
                                                      , null
                                                      , null);
            var result = transformer.Transform(testData);
            SingleChildNode expected = new SingleChildNode("root", child);
            Assert.AreEqual(expected.GetType(), result.GetType(), "Many Children Node with 1 child not transformed correctly (type)");
            Assert.AreEqual(expected.Name, result.Name, "Many Children Node with 1 child not transformed correctly (Name property)");
            Assert.AreEqual(expected.Child, ((SingleChildNode)result).Child, "Many Children Node with 1 child not transformed correctly (Child property)");
        }

        /// <summary>
        /// Tests if a ManyChildrenNode with two children is Transformed correctly to a TwoChildNode
        /// </summary>
        [TestMethod]
        public void TransformManyChildrenNode_TwoChild()
        {
            NoChildrenNode child1 = new NoChildrenNode("child1");
            NoChildrenNode child2 = new NoChildrenNode("child2");
            var testData = new ManyChildrenNode("root", child1
                                                      , child2
                                                      , null);
            var result = transformer.Transform(testData);
            TwoChildrenNode expected = new TwoChildrenNode("root", child1, child2);
            Assert.AreEqual(expected.GetType(), result.GetType(), "Many Children Node with 2 child not transformed correctly (type)");
            Assert.AreEqual(expected.Name, result.Name, "Many Children Node with 2 child not transformed correctly (Name property)");
            Assert.AreEqual(expected.FirstChild, ((TwoChildrenNode)result).FirstChild, "Many Children Node with 2 child not transformed correctly (First child)");
            Assert.AreEqual(expected.SecondChild, ((TwoChildrenNode)result).SecondChild, "Many Children Node with 2 child not transformed correctly (Second child)");
        }

        /// <summary>
        /// Tests if a ManyChildrenNode with null child is Transformed correctly to a ManyChildrenNode with no null child
        /// </summary>
        [TestMethod]
        public void TransformManyChildrenNode_ManyChildren()
        {
            NoChildrenNode child1 = new NoChildrenNode("child1");
            NoChildrenNode child2 = new NoChildrenNode("child2");
            NoChildrenNode child3 = new NoChildrenNode("child3");
            var testData = new ManyChildrenNode("root", child1
                                                      , child2
                                                      , child3
                                                      , null
                                                      , null);
            var result = transformer.Transform(testData);
            ManyChildrenNode expected = new ManyChildrenNode("root", child1
                                                                   , child2
                                                                   , child3);
            Assert.AreEqual(expected.GetType(), result.GetType(), "Many Children Node with 2 child not transformed correctly (type)");
            Assert.AreEqual(expected.Name, result.Name, "Many Children Node with 2 child not transformed correctly (Name property)");
            Assert.AreEqual(expected.Children.Count(), ((ManyChildrenNode)result).Children.Count());
        }

        /// <summary>
        /// Tests if a complex and nested node tree is transformed correctly
        /// </summary>
        [TestMethod]
        public void TransformNested()
        {
            var testData = new ManyChildrenNode("root",
            new ManyChildrenNode("child1",
            new ManyChildrenNode("leaf1"),
            new ManyChildrenNode("child2",
            new ManyChildrenNode("leaf2"))));
            var result = transformer.Transform(testData);

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
        #endregion
    }
}

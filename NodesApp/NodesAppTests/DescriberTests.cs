using Castle.Windsor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NodesApp.NodeOperations;
using NodesApp.NodeTypes;
using System;

namespace NodesAppTests
{
    /// <summary>
    /// Implement test methods for Describer Class
    /// </summary>
    [TestClass]
    public class DescriberTests
    {
        private static IWindsorContainer container;
        private static INodeDescriber describer;

        #region Class Initialize-Cleanup
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            // Create windsor container
            container = new WindsorContainer("windsorconfig.xml");

            // Create Resolve instance INodeDescriber
            describer = container.Resolve<INodeDescriber>();
        }
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            container.Dispose();
        }
        #endregion

        #region TestMethods

        /// <summary>
        /// Tests if a NoChildrenNode is described correctly by the INodeDescriber
        /// </summary>
        [TestMethod]
        public void DescribeNoChildrenNode()
        {
            var testData = new NoChildrenNode("nochildren");
            var result = describer.Describe(testData);
            string expected = "new NoChildrenNode(\"nochildren\")";
            Assert.AreEqual(expected, result, "Describe with no children method doesn't work properly");
        }

        /// <summary>
        /// Tests if a SingleChildNode is described correctly by the INodeDescriber
        /// </summary>
        [TestMethod]
        public void DescribeSingleChildNode()
        {
            var testData = new SingleChildNode("singlechild", new NoChildrenNode("child"));
            var result = describer.Describe(testData);
            string expected = "new SingleChildNode(\"singlechild\","
                + Environment.NewLine
                + "    new NoChildrenNode(\"child\"))";
            Assert.AreEqual(expected, result, "Describe with single children method doesn't work properly");
        }
        
        /// <summary>
        /// Tests if a TwoChildrenNode is described correctly by the INodeDescriber
        /// </summary>
        [TestMethod]
        public void DescribeTwoChildrenNode()
        {
            var testData = new TwoChildrenNode("twochildren", new NoChildrenNode("child1")
                                                            , new NoChildrenNode("child2"));
            var result = describer.Describe(testData);
            string expected = "new TwoChildrenNode(\"twochildren\","
                + Environment.NewLine
                + "    new NoChildrenNode(\"child1\"),"
                + Environment.NewLine
                + "    new NoChildrenNode(\"child2\"))";
            Assert.AreEqual(expected, result, "Describe with two children method doesn't work properly");
        }

        /// <summary>
        /// Tests if a ManyChildrenNode is described correctly by the INodeDescriber
        /// </summary>
        [TestMethod]
        public void DescribeManyChildrenNode()
        {
            var testData = new ManyChildrenNode("manychildren", new NoChildrenNode("child1")
                                                                , new NoChildrenNode("child2")
                                                                , new NoChildrenNode("child3")
                                                                , new NoChildrenNode("child4"));
            var result = describer.Describe(testData);
            string expected = "new ManyChildrenNode(\"manychildren\","
                + Environment.NewLine
                + "    new NoChildrenNode(\"child1\"),"
                + Environment.NewLine
                + "    new NoChildrenNode(\"child2\"),"
                + Environment.NewLine
                + "    new NoChildrenNode(\"child3\"),"
                + Environment.NewLine
                + "    new NoChildrenNode(\"child4\"))";
            Assert.AreEqual(expected, result, "Describe with many children method doesn't work properly");
        }

        /// <summary>
        /// Tests if a Complex and Nested Node is described correctly by the INodeDescriber
        /// </summary>
        [TestMethod]
        public void DescribeNested()
        {
            var testData = new SingleChildNode("root",
                                new TwoChildrenNode("child1",
                                    new NoChildrenNode("leaf1"),
                                    new SingleChildNode("child2",
                                        new NoChildrenNode("leaf2"))));
            var result = describer.Describe(testData);

            string expected = "new SingleChildNode(\"root\","
                + Environment.NewLine
                + "    new TwoChildrenNode(\"child1\","
                + Environment.NewLine
                + "        new NoChildrenNode(\"leaf1\"),"
                + Environment.NewLine
                + "        new SingleChildNode(\"child2\","
                + Environment.NewLine
                + "            new NoChildrenNode(\"leaf2\"))))";

            Assert.AreEqual(expected, result, "Describe method not working properly");
        }
        #endregion
        
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NodesApp.NodeOperations;
using NodesApp.NodeTypes;
using System;

namespace NodesAppTests
{
    [TestClass]
    public class DescriberTests
    {
        [TestMethod]
        public void DescribeNoChildrenNode()
        {
            INodeDescriber implementation = new Describer();
            var testData = new NoChildrenNode("nochildren");
            var result = implementation.Describe(testData);
            string expected = "new NoChildrenNode(\"nochildren\")";
            Assert.AreEqual(expected, result, "Describe with no children method doesn't work properly");
        }
        [TestMethod]
        public void DescribeSingleChildNode()
        {
            INodeDescriber implementation = new Describer();
            var testData = new SingleChildNode("singlechild", new NoChildrenNode("child"));
            var result = implementation.Describe(testData);
            string expected = "new SingleChildNode(\"singlechild\","
                + Environment.NewLine
                + "    new NoChildrenNode(\"child\"))";
            Assert.AreEqual(expected, result, "Describe with single children method doesn't work properly");
        }
        [TestMethod]
        public void DescribeTwoChildrenNode()
        {
            INodeDescriber implementation = new Describer();
            var testData = new TwoChildrenNode("twochildren", new NoChildrenNode("child1")
                                                            , new NoChildrenNode("child2"));
            var result = implementation.Describe(testData);
            string expected = "new TwoChildrenNode(\"twochildren\","
                + Environment.NewLine
                + "    new NoChildrenNode(\"child1\")"
                + Environment.NewLine
                + "    new NoChildrenNode(\"child2\"))";
            Assert.AreEqual(expected, result, "Describe with two children method doesn't work properly");
        }
        [TestMethod]
        public void DescribeManyChildrenNode()
        {
            INodeDescriber implementation = new Describer();
            var testData = new ManyChildrenNode("manychildren", new NoChildrenNode("child1")
                                                                , new NoChildrenNode("child2")
                                                                , new NoChildrenNode("child3")
                                                                , new NoChildrenNode("child4"));
            var result = implementation.Describe(testData);
            string expected = "new ManyChildrenNode(\"manychildren\","
                + Environment.NewLine
                + "    new NoChildrenNode(\"child1\")"
                + Environment.NewLine
                + "    new NoChildrenNode(\"child2\")"
                + Environment.NewLine
                + "    new NoChildrenNode(\"child3\")"
                + Environment.NewLine
                + "    new NoChildrenNode(\"child4\"))";
            Assert.AreEqual(expected, result, "Describe with many children method doesn't work properly");
        }
        [TestMethod]
        public void DescribeNested()
        {
            INodeDescriber implementation = new Describer();
            var testData = new SingleChildNode("root",
                                new TwoChildrenNode("child1",
                                    new NoChildrenNode("leaf1"),
                                    new SingleChildNode("child2",
                                        new NoChildrenNode("leaf2"))));
            var result = implementation.Describe(testData);

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
    }
}

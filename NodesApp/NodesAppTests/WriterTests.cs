using Microsoft.VisualStudio.TestTools.UnitTesting;
using NodesApp.NodeOperations;
using System.Threading.Tasks;
using System.IO;
using Castle.Windsor;
using System;
using NodesApp.NodeTypes;

namespace NodesAppTests
{
    /// <summary>
    /// Test for writer
    /// </summary>
    [TestClass]
    public class WriterTests
    {
        private static INodeWriter writer;
        private static IWindsorContainer container;
        private static string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "name.txt");

        #region Class Initialize-Cleanup
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext) {
            // Create windsor container
            container = new WindsorContainer("windsorconfig.xml");
            
            // Create Resolve instance INodeWriter
            writer = container.Resolve<INodeWriter>();
        }
        [ClassCleanup()]
        public static void MyClassCleanup() {
            container.Dispose();
        }
        #endregion

        #region Test Initialize-Cleanup
        [TestInitialize()]
        public void MyTestInitialize() {
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
        }
        [TestCleanup()]
        public void MyTestCleanup() {
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
        }

        #endregion

        #region TestsMethods
        /// <summary>
        /// Tests if the writer return a Task no null
        /// </summary>
        [TestMethod]
        public void Writer_ReturnTaskNoNull()
        {
            NodesApp.Node n = new NodesApp.NodeTypes.NoChildrenNode("NoChildNode");
            Task t = writer.WriteToFileAsync(n, filepath);
            Assert.AreNotEqual(null, t);
        }

        /// <summary>
        /// Tests if the writer return a Task that create a file in the specified path
        /// </summary>
        [TestMethod]
        public void Writer_Task_FileExits()
        {
            NodesApp.Node n = new NodesApp.NodeTypes.NoChildrenNode("NoChildNode");
            writer.WriteToFileAsync(n, filepath).RunSynchronously();
            Assert.IsTrue(File.Exists(filepath));
        }

        /// <summary>
        /// Tests if the content of the file created by the "writer" is equal to the description provided by the "describer" for a NoChildrenNode
        /// </summary>
        [TestMethod]
        public void Writer_Task_FileContent_NoChild()
        {
            NodesApp.Node n = new NodesApp.NodeTypes.NoChildrenNode("NoChildNode");
            writer.WriteToFileAsync(n, filepath).RunSynchronously();
            string content = File.ReadAllText(filepath);
            INodeDescriber describer = container.Resolve<INodeDescriber>();
            string expected = describer.Describe(n);
            Assert.AreEqual(expected, content);
        }

        /// <summary>
        /// Tests if the content of the file created by the "writer" is equal to the description provided by the "describer" for a Complex tree Node
        /// </summary>
        [TestMethod]
        public void Writer_Task_FileContent_ComplexNode()
        {
            var n = new SingleChildNode("root",
                                new TwoChildrenNode("child1",
                                    new NoChildrenNode("leaf1"),
                                    new SingleChildNode("child2",
                                        new NoChildrenNode("leaf2"))));

            writer.WriteToFileAsync(n, filepath).RunSynchronously();
            string content = File.ReadAllText(filepath);
            INodeDescriber describer = container.Resolve<INodeDescriber>();
            string expected = describer.Describe(n);
            Assert.AreEqual(expected, content);
        }
        #endregion
    }
}

using ForRest.BTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TestProject
{
    
    
    /// <summary>
    ///This is a test class for BTreeTest and is intended
    ///to contain all BTreeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class BTreeTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for BTree`1 Constructor
        ///</summary>
        public void BTreeConstructorTestHelper<T>()
        {
            int degree = 0; // TODO: Initialize to an appropriate value
            BTree<T> target = new BTree<T>(degree);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [TestMethod()]
        public void BTreeConstructorTest()
        {
            BTreeConstructorTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for Add
        ///</summary>
        public void AddTestHelper<T>()
        {
            int degree = 0; // TODO: Initialize to an appropriate value
            BTree<T> target = new BTree<T>(degree); // TODO: Initialize to an appropriate value
            T data = default(T); // TODO: Initialize to an appropriate value
            target.Add(data);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [TestMethod()]
        public void AddTest()
        {
            AddTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for Clear
        ///</summary>
        public void ClearTestHelper<T>()
        {
            int degree = 0; // TODO: Initialize to an appropriate value
            BTree<T> target = new BTree<T>(degree); // TODO: Initialize to an appropriate value
            target.Clear();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [TestMethod()]
        public void ClearTest()
        {
            ClearTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for Contains
        ///</summary>
        public void ContainsTestHelper<T>()
        {
            int degree = 0; // TODO: Initialize to an appropriate value
            BTree<T> target = new BTree<T>(degree); // TODO: Initialize to an appropriate value
            T data = default(T); // TODO: Initialize to an appropriate value
            List<int> expected = null; // TODO: Initialize to an appropriate value
            List<int> actual;
            actual = target.Contains(data);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void ContainsTest()
        {
            ContainsTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        public void DeleteTestHelper<T>()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            BTree_Accessor<T> target = new BTree_Accessor<T>(param0); // TODO: Initialize to an appropriate value
            BTreeNode<T> node = null; // TODO: Initialize to an appropriate value
            T data = default(T); // TODO: Initialize to an appropriate value
            BTreeNode<T> expected = null; // TODO: Initialize to an appropriate value
            BTreeNode<T> actual;
            actual = target.Delete(node, data);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        [DeploymentItem("ForRest.BTree.dll")]
        public void DeleteTest()
        {
            DeleteTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for Insert
        ///</summary>
        public void InsertTestHelper<T>()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            BTree_Accessor<T> target = new BTree_Accessor<T>(param0); // TODO: Initialize to an appropriate value
            BTreeNode<T> node = null; // TODO: Initialize to an appropriate value
            T data = default(T); // TODO: Initialize to an appropriate value
            BTreeNode<T> expected = null; // TODO: Initialize to an appropriate value
            BTreeNode<T> actual;
            actual = target.Insert(node, data);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        [DeploymentItem("ForRest.BTree.dll")]
        public void InsertTest()
        {
            InsertTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for Remove
        ///</summary>
        public void RemoveTestHelper<T>()
        {
            int degree = 0; // TODO: Initialize to an appropriate value
            BTree<T> target = new BTree<T>(degree); // TODO: Initialize to an appropriate value
            T data = default(T); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Remove(data);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void RemoveTest()
        {
            RemoveTestHelper<GenericParameterHelper>();
        }
    }
}

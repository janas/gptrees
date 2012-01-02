using ForRest._23Tree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using ForRest._234Tree;

namespace TestProject
{
    
    
    /// <summary>
    ///This is a test class for __TreeTest and is intended
    ///to contain all __TreeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class @__TreeTest
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
        ///A test for _234Tree`1 Constructor
        ///</summary>
        public void @__TreeConstructorTest1Helper<T>()
        {
            _234Tree<T> target = new _234Tree<T>();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [TestMethod()]
        public void @__TreeConstructorTest1()
        {
            @__TreeConstructorTest1Helper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for Add
        ///</summary>
        public void AddTest1Helper<T>()
        {
            _234Tree<T> target = new _234Tree<T>(); // TODO: Initialize to an appropriate value
            T data = default(T); // TODO: Initialize to an appropriate value
            target.Add(data);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [TestMethod()]
        public void AddTest1()
        {
            AddTest1Helper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for Clear
        ///</summary>
        public void ClearTest1Helper<T>()
        {
            _234Tree<T> target = new _234Tree<T>(); // TODO: Initialize to an appropriate value
            target.Clear();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [TestMethod()]
        public void ClearTest1()
        {
            ClearTest1Helper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for Contains
        ///</summary>
        public void ContainsTest1Helper<T>()
        {
            _234Tree<T> target = new _234Tree<T>(); // TODO: Initialize to an appropriate value
            T data = default(T); // TODO: Initialize to an appropriate value
            List<int> expected = null; // TODO: Initialize to an appropriate value
            List<int> actual;
            actual = target.Contains(data);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void ContainsTest1()
        {
            ContainsTest1Helper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        public void DeleteTest1Helper<T>()
        {
            _234Tree_Accessor<T> target = new _234Tree_Accessor<T>(); // TODO: Initialize to an appropriate value
            _234TreeNode<T> node = null; // TODO: Initialize to an appropriate value
            T data = default(T); // TODO: Initialize to an appropriate value
            _234TreeNode<T> expected = null; // TODO: Initialize to an appropriate value
            _234TreeNode<T> actual;
            actual = target.Delete(node, data);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        [DeploymentItem("ForRest.234Tree.dll")]
        public void DeleteTest1()
        {
            DeleteTest1Helper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for Insert
        ///</summary>
        public void InsertTest1Helper<T>()
        {
            _234Tree_Accessor<T> target = new _234Tree_Accessor<T>(); // TODO: Initialize to an appropriate value
            _234TreeNode<T> node = null; // TODO: Initialize to an appropriate value
            T data = default(T); // TODO: Initialize to an appropriate value
            _234TreeNode<T> expected = null; // TODO: Initialize to an appropriate value
            _234TreeNode<T> actual;
            actual = target.Insert(node, data);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        [DeploymentItem("ForRest.234Tree.dll")]
        public void InsertTest1()
        {
            InsertTest1Helper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for Remove
        ///</summary>
        public void RemoveTest1Helper<T>()
        {
            _234Tree<T> target = new _234Tree<T>(); // TODO: Initialize to an appropriate value
            T data = default(T); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Remove(data);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void RemoveTest1()
        {
            RemoveTest1Helper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for _23Tree`1 Constructor
        ///</summary>
        public void @__TreeConstructorTestHelper<T>()
        {
            _23Tree<T> target = new _23Tree<T>();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [TestMethod()]
        public void @__TreeConstructorTest()
        {
            @__TreeConstructorTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for Add
        ///</summary>
        public void AddTestHelper<T>()
        {
            _23Tree<T> target = new _23Tree<T>(); // TODO: Initialize to an appropriate value
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
            _23Tree<T> target = new _23Tree<T>(); // TODO: Initialize to an appropriate value
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
            _23Tree<T> target = new _23Tree<T>(); // TODO: Initialize to an appropriate value
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
            _23Tree_Accessor<T> target = new _23Tree_Accessor<T>(); // TODO: Initialize to an appropriate value
            _23TreeNode<T> node = null; // TODO: Initialize to an appropriate value
            T data = default(T); // TODO: Initialize to an appropriate value
            _23TreeNode<T> expected = null; // TODO: Initialize to an appropriate value
            _23TreeNode<T> actual;
            actual = target.Delete(node, data);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        [DeploymentItem("ForRest.23Tree.dll")]
        public void DeleteTest()
        {
            DeleteTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for Insert
        ///</summary>
        public void InsertTestHelper<T>()
        {
            _23Tree_Accessor<T> target = new _23Tree_Accessor<T>(); // TODO: Initialize to an appropriate value
            _23TreeNode<T> node = null; // TODO: Initialize to an appropriate value
            T data = default(T); // TODO: Initialize to an appropriate value
            _23TreeNode<T> expected = null; // TODO: Initialize to an appropriate value
            _23TreeNode<T> actual;
            actual = target.Insert(node, data);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        [DeploymentItem("ForRest.23Tree.dll")]
        public void InsertTest()
        {
            InsertTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for Remove
        ///</summary>
        public void RemoveTestHelper<T>()
        {
            _23Tree<T> target = new _23Tree<T>(); // TODO: Initialize to an appropriate value
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

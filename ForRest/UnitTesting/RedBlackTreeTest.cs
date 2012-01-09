using ForRest.RedBlackTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UnitTesting
{
    
    
    /// <summary>
    ///This is a test class for RedBlackTreeTest and is intended
    ///to contain all RedBlackTreeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RedBlackTreeTest
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
        ///A test for Add
        ///</summary>
        public void AddTestHelper<T>()
        {
            RedBlackTree<double> target = new RedBlackTree<double>();
            double data = 3;
            target.Add(1);
            target.Add(2);
            target.Add(data);
            List<int> actual;
            actual = target.Contains(data);
            Assert.IsNotNull(actual);
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
            RedBlackTree<double> target = new RedBlackTree<double>();
            target.Add(1);
            target.Add(2);
            target.Add(3);
            target.Clear();
            Assert.IsNull(target.Root);
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
            RedBlackTree<double> target = new RedBlackTree<double>();
            double data = 3;
            target.Add(1);
            target.Add(2);
            target.Add(data);
            List<int> actual;
            actual = target.Contains(data);
            Assert.IsNotNull(actual);
        }

        [TestMethod()]
        public void ContainsTest()
        {
            ContainsTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for Remove
        ///</summary>
        public void RemoveTestHelper<T>()
        {
            RedBlackTree<double> target = new RedBlackTree<double>();
            double data = 3;
            target.Add(1);
            target.Add(2);
            target.Add(data);
            target.Remove(data);
            List<int> actual = target.Contains(data);
            Assert.IsNull(actual);
        }

        [TestMethod()]
        public void RemoveTest()
        {
            RemoveTestHelper<GenericParameterHelper>();
        }
    }
}

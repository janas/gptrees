using ForRest.AVLTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

using ForRest.Provider.BLL;

namespace UnitTesting
{
    
    
    /// <summary>
    ///This is a test class for AVLTreeTest and is intended
    ///to contain all AVLTreeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AVLTreeTest
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
            AVLTree<double> target = new AVLTree<double>();
            double data = 3;
            target.Add(1);
            target.Add(2);
            target.Add(data);
            SearchResult actual = target.Contains(data);
            Assert.IsNotNull(actual.SearchPath);
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
            AVLTree<double> target = new AVLTree<double>();
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
            AVLTree<double> target = new AVLTree<double>();
            double data = 3;
            target.Add(1);
            target.Add(2);
            target.Add(data);
            SearchResult actual = target.Contains(data);
            Assert.IsNotNull(actual.SearchPath);
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
            AVLTree<double> target = new AVLTree<double>();
            double data = 3;
            target.Add(1);
            target.Add(2);
            target.Add(data);
            target.Remove(data);
            SearchResult actual = target.Contains(data);
            Assert.IsNotNull(actual.SearchPath);
        }

        [TestMethod()]
        public void RemoveTest()
        {
            RemoveTestHelper<GenericParameterHelper>();
        }
    }
}

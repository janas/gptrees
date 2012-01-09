﻿using ForRest.BTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UnitTesting
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
        ///A test for Add
        ///</summary>
        public void AddTestHelper<T>()
        {
            int degree = 2;
            BTree<double> target = new BTree<double>(degree);
            double data = 6;
            target.Add(1);
            target.Add(2);
            target.Add(3);
            target.Add(4);
            target.Add(5);
            target.Add(6);
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
            int degree = 2;
            BTree<double> target = new BTree<double>(degree);
            double data = 6;
            target.Add(1);
            target.Add(2);
            target.Add(3);
            target.Add(4);
            target.Add(5);
            target.Add(data);
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
            int degree = 2;
            BTree<double> target = new BTree<double>(degree);
            double data = 6;
            target.Add(1);
            target.Add(2);
            target.Add(3);
            target.Add(4);
            target.Add(5);
            target.Add(6);
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
            int degree = 2;
            BTree<double> target = new BTree<double>(degree);
            double data = 6;
            target.Add(1);
            target.Add(2);
            target.Add(3);
            target.Add(4);
            target.Add(5);
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
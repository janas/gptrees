﻿using ForRest._234Tree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using ForRest._23Tree;
using ForRest.Provider.BLL;

namespace UnitTesting
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
        ///A test for Add
        ///</summary>
        public void AddTestHelper<T>()
        {
            _234Tree<double> target = new _234Tree<double>();
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
            _234Tree<double> target = new _234Tree<double>();
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
            _234Tree<double> target = new _234Tree<double>();
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
            _234Tree<double> target = new _234Tree<double>();
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

        /// <summary>
        ///A test for Add
        ///</summary>
        public void AddTest1Helper<T>()
        {
            _23Tree<double> target = new _23Tree<double>();
            double data = 3;
            target.Add(1);
            target.Add(2);
            target.Add(data);
            SearchResult actual = target.Contains(data);
            Assert.IsNotNull(actual.SearchPath);
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
            _23Tree<double> target = new _23Tree<double>();
            target.Add(1);
            target.Add(2);
            target.Add(3);
            target.Clear();
            Assert.IsNull(target.Root);
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
            _23Tree<double> target = new _23Tree<double>();
            double data = 3;
            target.Add(1);
            target.Add(2);
            target.Add(data);
            SearchResult actual = target.Contains(data);
            Assert.IsNotNull(actual.SearchPath);
        }

        [TestMethod()]
        public void ContainsTest1()
        {
            ContainsTest1Helper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for Remove
        ///</summary>
        public void RemoveTest1Helper<T>()
        {
            _23Tree<double> target = new _23Tree<double>();
            double data = 3;
            target.Add(1);
            target.Add(2);
            target.Add(data);
            target.Remove(data);
            SearchResult actual = target.Contains(data);
            Assert.IsNotNull(actual.SearchPath);
        }

        [TestMethod()]
        public void RemoveTest1()
        {
            RemoveTest1Helper<GenericParameterHelper>();
        }
    }
}

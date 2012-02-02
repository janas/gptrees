// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchPerformer.cs" company="Warsaw University of Technology">
//   
// </copyright>
// <summary>
//   Class responsible for performing searching on batch tree objects.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForRest.BLL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;

    using ForRest.Provider;
    using ForRest.Provider.BLL;

    /// <summary>
    /// Class responsible for performing searching on batch tree objects.
    /// </summary>
    public class SearchPerformer
    {
        #region Constants and Fields
        /// <summary>
        /// The provider.
        /// </summary>
        private readonly Provider provider;

        /// <summary>
        /// The background worker.
        /// </summary>
        private readonly BackgroundWorker backgroundWorker;

        /// <summary>
        /// The counter.
        /// </summary>
        private int counter;

        /// <summary>
        /// The denominator.
        /// </summary>
        private int denominator;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchPerformer"/> class.
        /// </summary>
        /// <param name="provider">
        /// The provider.
        /// </param>
        /// <param name="bgwrk">
        /// The bgwrk.
        /// </param>
        public SearchPerformer(Provider provider, BackgroundWorker bgwrk)
        {
            this.provider = provider;
            this.backgroundWorker = bgwrk;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The generic batch search.
        /// </summary>
        /// <param name="searchItems">
        /// The search items.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <typeparam name="T">
        /// Parameter can be string or double.
        /// </typeparam>
        public void GenericBatchSearch<T>(List<T> searchItems, string type)
        {
            Stopwatch watch = null;
            SearchResult searchResult;

            this.counter = 1;
            this.denominator = this.provider.BatchTreeObject.Count * searchItems.Count;

            foreach (var treeObject in this.provider.BatchTreeObject)
            {
                if (!treeObject.Type.Equals(type))
                {
                    continue;
                }

                foreach (var item in searchItems)
                {
                    if (type.Equals("text"))
                    {
                        watch = new Stopwatch();
                        watch.Start();
                        searchResult = treeObject.TextTree.Contains(item as string);
                        watch.Stop();
                    }
                    else //if (type.Equals("numeric"))
                    {
                        watch = new Stopwatch();
                        watch.Start();
                        searchResult = treeObject.NumericTree.Contains((double)(object)item);
                        watch.Stop();
                    }

                    PerformanceSet performanceSet;
                    if (searchResult.searchPath != null)
                    {
                        performanceSet = new PerformanceSet
                            {
                                TreeName = treeObject.Name, 
                                SearchTime = watch.ElapsedMilliseconds.ToString(), 
                                TypeOfNodes = this.CheckNodeType(type), 
                                TypeOfTree = this.GetTreeType(treeObject), 
                                NoOfNodes = searchResult.nodesVisited.ToString()
                            };
                        this.provider.BatchPerformanceSet.Add(performanceSet);
                        var currentDateTime = DateTime.Now.TimeOfDay.ToString();
                        var progress = "[" + currentDateTime.Substring(0, 13) + "]" + "\tGroup tree name: "
                                       + treeObject.Name + " Value: " + item + " Found in " + watch.ElapsedMilliseconds
                                       + " ms for tree type: " + this.GetTreeType(treeObject) + Environment.NewLine;
                        var percent = (100 * this.counter) / this.denominator;
                        this.backgroundWorker.ReportProgress(percent, progress);
                    }
                    else
                    {
                        performanceSet = new PerformanceSet
                            {
                                TreeName = treeObject.Name, 
                                SearchTime = watch.ElapsedMilliseconds + "/Not Found", 
                                TypeOfNodes = this.CheckNodeType(type), 
                                TypeOfTree = this.GetTreeType(treeObject), 
                                NoOfNodes = searchResult.nodesVisited.ToString()
                            };
                        this.provider.BatchPerformanceSet.Add(performanceSet);
                        var currentDateTime = DateTime.Now.TimeOfDay.ToString();
                        var progress = "[" + currentDateTime.Substring(0, 13) + "]" + "\tGroup tree name: "
                                       + treeObject.Name + " Value: " + item + " Not found in "
                                       + watch.ElapsedMilliseconds + " ms for tree type: "
                                       + this.GetTreeType(treeObject) + Environment.NewLine;
                        var percent = (100 * this.counter) / this.denominator;
                        this.backgroundWorker.ReportProgress(percent, progress);
                    }

                    this.counter++;
                }
            }
        }

        /// <summary>
        /// Generic search method.
        /// </summary>
        /// <param name="treeObject">
        /// The tree object.
        /// </param>
        /// <param name="searchItem">
        /// Item we want to search for. Can be string or double.
        /// </param>
        /// <typeparam name="T">
        /// Parameter can be string or double.
        /// </typeparam>
        /// <returns>
        /// Returns list of type int containing path to node, null if not found.
        /// </returns>
        public SearchResult GenericSearch<T>(TreeObject treeObject, T searchItem)
        {
            Stopwatch watch;
            PerformanceSet performanceSet;
            SearchResult searchResult;
            string type;
            string[] progress = new string[2];

            if (typeof(T) == typeof(string))
            {
                watch = new Stopwatch();
                watch.Start();
                searchResult = treeObject.TextTree.Contains(searchItem as string);
                watch.Stop();
                type = "text";
            }
            else
            {
                watch = new Stopwatch();
                watch.Start();
                searchResult = treeObject.NumericTree.Contains((double)(object)searchItem);
                watch.Stop();
                type = "numeric";
            }
            
            if (searchResult.searchPath != null)
            {
                performanceSet = new PerformanceSet
                {
                    TreeName = treeObject.Name,
                    SearchTime = watch.ElapsedMilliseconds.ToString(),
                    TypeOfNodes = this.CheckNodeType(type),
                    TypeOfTree = this.GetTreeType(treeObject),
                    NoOfNodes = searchResult.nodesVisited.ToString()
                };
                this.provider.PerformanceSets.Add(performanceSet);
                progress[0] = watch.ElapsedMilliseconds + " ms";
                progress[1] = searchResult.nodesVisited.ToString();
                this.backgroundWorker.ReportProgress(0, progress);
            }
            else
            {
                performanceSet = new PerformanceSet
                {
                    TreeName = treeObject.Name,
                    SearchTime = watch.ElapsedMilliseconds + "/Not Found",
                    TypeOfNodes = this.CheckNodeType(type),
                    TypeOfTree = this.GetTreeType(treeObject),
                    NoOfNodes = searchResult.nodesVisited.ToString()
                };
                this.provider.PerformanceSets.Add(performanceSet);
                progress[0] = watch.ElapsedMilliseconds + " ms/NF";
                progress[1] = searchResult.nodesVisited.ToString();
                this.backgroundWorker.ReportProgress(0, progress);
            }

            return searchResult;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The check node type.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// Returns "String" if tree is of type text, "Double" otherwise.
        /// </returns>
        private string CheckNodeType(string type)
        {
            return type.Equals("text") ? "String" : "Double";
        }

        /// <summary>
        /// The get tree type.
        /// </summary>
        /// <param name="treeObject">
        /// The tree object.
        /// </param>
        /// <returns>
        /// Returns "text" string if tree object is of type text, numeric otherwise.
        /// </returns>
        private string GetTreeType(TreeObject treeObject)
        {
            return treeObject.Type.Equals("text") ? treeObject.TextTree.TreeType : treeObject.NumericTree.TreeType;
        }

        #endregion
    }
}
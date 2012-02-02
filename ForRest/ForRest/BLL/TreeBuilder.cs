// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TreeBuilder.cs" company="Warsaw University of Technology">
//   
// </copyright>
// <summary>
//   Class responsible for building trees.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForRest.BLL
{
    using System.Collections.Generic;
    using System.ComponentModel;

    using ForRest.Provider;
    using ForRest.Provider.BLL;

    /// <summary>
    /// Class responsible for building trees.
    /// </summary>
    public class TreeBuilder
    {
        #region Constants and Fields

        /// <summary>
        /// The provider.
        /// </summary>
        private readonly Provider provider;

        /// <summary>
        /// The tree name.
        /// </summary>
        private readonly string treeName;

        /// <summary>
        /// The background worker.
        /// </summary>
        private readonly BackgroundWorker backgroundWorker;

        /// <summary>
        /// The no of trees.
        /// </summary>
        private int noOfTrees;

        /// <summary>
        /// The type of trees.
        /// </summary>
        private string typeOfTrees;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeBuilder"/> class.
        /// </summary>
        /// <param name="provider">
        /// The provider.
        /// </param>
        /// <param name="treeName">
        /// The tree name.
        /// </param>
        /// <param name="bgwrk">
        /// The bgwrk.
        /// </param>
        public TreeBuilder(Provider provider, string treeName, BackgroundWorker bgwrk)
        {
            this.provider = provider;
            this.treeName = treeName;
            this.backgroundWorker = bgwrk;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets NumberOfTrees.
        /// </summary>
        public int NumberOfTrees
        {
            get
            {
                return this.noOfTrees;
            }
        }

        /// <summary>
        /// Gets TypeOfTrees.
        /// </summary>
        public string TypeOfTrees
        {
            get
            {
                return this.typeOfTrees;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The build batch tree.
        /// </summary>
        /// <param name="factory">
        /// The factory.
        /// </param>
        /// <typeparam name="T">
        /// Parameter type string/double.
        /// </typeparam>
        public void BuildBatchTree<T>(ITreeFactory factory)
        {
            List<List<T>> batchData = this.GetBatchData<T>();
            foreach (var data in batchData)
            {
                ITree<T> genericTree = factory.GetTree<T>();
                int count = data.Count;
                for (int i = 0; i < count; i++)
                {
                    genericTree.Add(data[i]);
                    this.CalculateProgress(i, count);
                }

                this.AddToBatchList(genericTree);
            }
        }

        /// <summary>
        /// The build batch tree.
        /// </summary>
        /// <param name="factory">
        /// The factory.
        /// </param>
        /// <param name="degree">
        /// The degree.
        /// </param>
        /// <typeparam name="T">
        /// Parameter type string/double.
        /// </typeparam>
        public void BuildBatchTree<T>(ITreeFactory factory, int degree)
        {
            List<List<T>> batchData = this.GetBatchData<T>();
            foreach (var data in batchData)
            {
                ITree<T> genericTree = factory.GetTree<T>(degree);
                int count = data.Count;
                for (int i = 0; i < count; i++)
                {
                    genericTree.Add(data[i]);
                    this.CalculateProgress(i, count);
                }

                this.AddToBatchList(genericTree);
            }
        }

        /// <summary>
        /// The build tree.
        /// </summary>
        /// <param name="factory">
        /// The factory.
        /// </param>
        /// <typeparam name="T">
        /// Parameter type string/double.
        /// </typeparam>
        public void BuildTree<T>(ITreeFactory factory)
        {
            ITree<T> genericTree = factory.GetTree<T>();
            this.AddToList(genericTree);
        }

        /// <summary>
        /// The build tree.
        /// </summary>
        /// <param name="factory">
        /// The factory.
        /// </param>
        /// <param name="degree">
        /// The degree.
        /// </param>
        /// <typeparam name="T">
        /// Parameter type string/double.
        /// </typeparam>
        public void BuildTree<T>(ITreeFactory factory, int degree)
        {
            ITree<T> genericTree = factory.GetTree<T>(degree);
            this.AddToList(genericTree);
        }

        /// <summary>
        /// The build tree from file.
        /// </summary>
        /// <param name="factory">
        /// The factory.
        /// </param>
        /// <typeparam name="T">
        /// Parameter type string/double.
        /// </typeparam>
        public void BuildTreeFromFile<T>(ITreeFactory factory)
        {
            ITree<T> genericTree = factory.GetTree<T>();
            List<T> temp = this.GetData<T>();
            int count = 0;
            if (this.CheckType<T>().Equals("text"))
            {
                count = this.provider.TextData.Count;
            }

            if (this.CheckType<T>().Equals("numeric"))
            {
                count = this.provider.NumericData.Count;
            }

            for (int i = 0; i < count; i++)
            {
                genericTree.Add(temp[i]);
                this.CalculateProgress(i, count);
            }

            this.AddToList(genericTree);
        }

        /// <summary>
        /// The build tree from file.
        /// </summary>
        /// <param name="factory">
        /// The factory.
        /// </param>
        /// <param name="degree">
        /// The degree.
        /// </param>
        /// <typeparam name="T">
        /// Parameter type string/double.
        /// </typeparam>
        public void BuildTreeFromFile<T>(ITreeFactory factory, int degree)
        {
            ITree<T> genericTree = factory.GetTree<T>();
            List<T> temp = this.GetData<T>();
            int count = 0;
            if (this.CheckType<T>().Equals("text"))
            {
                count = this.provider.TextData.Count;
            }

            if (this.CheckType<T>().Equals("numeric"))
            {
                count = this.provider.NumericData.Count;
            }

            for (int i = 0; i < count; i++)
            {
                genericTree.Add(temp[i]);
                this.CalculateProgress(i, count);
            }

            this.AddToList(genericTree);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The add to batch list.
        /// </summary>
        /// <param name="genericTree">
        /// The generic tree.
        /// </param>
        /// <typeparam name="T">
        /// Parameter type string/double.
        /// </typeparam>
        private void AddToBatchList<T>(T genericTree)
        {
            TreeObject genericTreeObject = null;

            if (this.CheckType<T>().Equals("text"))
            {
                genericTreeObject = new TreeObject(this.treeName, this.CheckType<T>(), (ITree<string>)genericTree);
            }

            if (this.CheckType<T>().Equals("numeric"))
            {
                genericTreeObject = new TreeObject(this.treeName, this.CheckType<T>(), (ITree<double>)genericTree);
            }

            this.provider.BatchTreeObject.Add(genericTreeObject);
            this.typeOfTrees = genericTreeObject.Type;
            this.noOfTrees++;
        }

        /// <summary>
        /// Adds tree object created to the list holding all trees.
        /// </summary>
        /// <param name="genericTree">
        /// The generic tree. 
        /// </param>
        /// <typeparam name="T">
        /// Parameter type string/double. 
        /// </typeparam>
        private void AddToList<T>(T genericTree)
        {
            TreeObject genericTreeObject = null;

            if (this.CheckType<T>().Equals("text"))
            {
                genericTreeObject = new TreeObject(this.treeName, this.CheckType<T>(), (ITree<string>)genericTree);
            }

            if (this.CheckType<T>().Equals("numeric"))
            {
                genericTreeObject = new TreeObject(this.treeName, this.CheckType<T>(), (ITree<double>)genericTree);
            }

            this.provider.TreeObjects.Add(genericTreeObject);
        }

        /// <summary>
        /// The calculate progress.
        /// </summary>
        /// <param name="current">
        /// The current.
        /// </param>
        /// <param name="count">
        /// The count.
        /// </param>
        private void CalculateProgress(int current, int count)
        {
            if (count >= 100)
            {
                int percentageStep = count / 100;
                if (current % percentageStep != 0)
                {
                    return;
                }

                this.backgroundWorker.ReportProgress(current / percentageStep);
            }
            else
            {
                int percentageStep = (100 * current) / count;
                this.backgroundWorker.ReportProgress(percentageStep);
            }
        }

        /// <summary>
        /// Method checking type of a tree which is to be build.
        /// </summary>
        /// <typeparam name="T">
        /// Parameter to be checked (string/double). 
        /// </typeparam>
        /// <returns>
        /// Returns string value "text" if paramter T is type of string, "numeric" if paramter T is type of double. 
        /// </returns>
        private string CheckType<T>()
        {
            if (typeof(T) == typeof(double) || typeof(T) == typeof(ITree<double>))
            {
                return "numeric";
            }

            return "text";
        }

        /// <summary>
        /// The get batch data.
        /// </summary>
        /// <typeparam name="T">
        /// Parameter type string/double.
        /// </typeparam>
        /// <returns>
        /// Returns list of lists containg readed data to build batch trees.
        /// </returns>
        private List<List<T>> GetBatchData<T>()
        {
            if (this.CheckType<T>().Equals("numeric"))
            {
                return (List<List<T>>)(object)this.provider.BatchNumericData;
            }

            return (List<List<T>>)(object)this.provider.BatchTextData;
        }

        /// <summary>
        /// Method that gets ith element of the list of specified type (string or double).
        /// </summary>
        /// <typeparam name="T">
        /// Parameter string/double. 
        /// </typeparam>
        /// <returns>
        /// Returns element of the list of specified type (string or double). 
        /// </returns>
        private List<T> GetData<T>()
        {
            if (this.CheckType<T>().Equals("numeric"))
            {
                return (List<T>)(object)this.provider.NumericData;
            }

            return (List<T>)(object)this.provider.TextData;
        }

        #endregion
    }
}
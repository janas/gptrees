// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BPlusTreeFactory.cs" company="Warsaw University of Technology">
//   
// </copyright>
// <summary>
//   Class responsible for managing B+ tree.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForRest.BPlusTree
{
    using System;

    using ForRest.Provider.BLL;

    /// <summary>
    /// Class responsible for managing B+ tree implementing ITreeFactory.
    /// </summary>
    public class BPlusTreeFactory : ITreeFactory
    {
        #region Public Properties

        /// <summary>
        ///   Gets tree name.
        /// </summary>
        public string Name
        {
            get
            {
                return "B+ Tree";
            }
        }

        /// <summary>
        ///   Indicates whether tree needs degree.
        /// </summary>
        public bool NeedDegree
        {
            get
            {
                return true;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets plugin description.
        /// </summary>
        /// <returns>
        /// The get plugin description.
        /// </returns>
        public string GetPluginDescription()
        {
            return "Plugin that implements B+ Tree Algorithms";
        }

        /// <summary>
        /// Gets plugin name.
        /// </summary>
        /// <returns>
        /// The get plugin name.
        /// </returns>
        public string GetPluginName()
        {
            return "B+ Tree Plugin";
        }

        /// <summary>
        /// Create instance of tree.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// </returns>
        public ITree<T> GetTree<T>()
        {
            // default degree if not specified explicitly
            return this.GetTree<T>(2);
        }

        /// <summary>
        /// The get tree.
        /// </summary>
        /// <param name="degree">
        /// The degree.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// </returns>
        public ITree<T> GetTree<T>(int degree)
        {
            object obj = Activator.CreateInstance(typeof(BPlusTree<>).MakeGenericType(typeof(T)), degree);
            var t = (ITree<T>)obj;
            return t;
        }

        #endregion
    }
}
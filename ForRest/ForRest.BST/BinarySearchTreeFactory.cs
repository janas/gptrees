// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BinarySearchTreeFactory.cs" company="Warsaw University of Technology">
//   
// </copyright>
// <summary>
//   Class responsible for managing BST tree.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForRest.BST
{
    using System;

    using ForRest.Provider.BLL;

    /// <summary>
    /// Class responsible for managing BST tree implementing ITreeFactory.
    /// </summary>
    public class BinarySearchTreeFactory : ITreeFactory
    {
        #region Public Properties

        /// <summary>
        /// Gets Name.
        /// </summary>
        public string Name
        {
            get
            {
                return "Binary Search Tree";
            }
        }

        /// <summary>
        /// Gets a value indicating whether NeedDegree.
        /// </summary>
        public bool NeedDegree
        {
            get
            {
                return false;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// The get plugin description.
        /// </summary>
        /// <returns>
        /// The get plugin description.
        /// </returns>
        public string GetPluginDescription()
        {
            return "Plugin that implements Binary Search Tree Algorithms";
        }

        /// <summary>
        /// The get plugin name.
        /// </summary>
        /// <returns>
        /// The get plugin name.
        /// </returns>
        public string GetPluginName()
        {
            return "Binary Search Tree Plugin";
        }

        /// <summary>
        /// The get tree.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// </returns>
        public ITree<T> GetTree<T>()
        {
            object obj = Activator.CreateInstance(typeof(BinarySearchTree<>).MakeGenericType(typeof(T)));
            var t = (ITree<T>)obj;
            return t;
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
            return this.GetTree<T>();
        }

        #endregion
    }
}
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AVLTreeFactory.cs" company="Warsaw University of Technology">
//   
// </copyright>
// <summary>
//   Class responsible for managing AVL tree.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForRest.AVLTree
{
    using System;

    using ForRest.Provider.BLL;

    /// <summary>
    /// Class responsible for managing AVL tree implementing ITreeFactory.
    /// </summary>
    public class AVLTreeFactory : ITreeFactory
    {
        #region Public Properties

        /// <summary>
        ///   Gets tree name.
        /// </summary>
        public string Name
        {
            get
            {
                return "AVL Tree";
            }
        }

        /// <summary>
        ///   Indicates whether tree needs degree.
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
        /// Gets plugin description.
        /// </summary>
        /// <returns>
        /// The get plugin description.
        /// </returns>
        public string GetPluginDescription()
        {
            return "Plugin that implements AVL Tree Algorithms";
        }

        /// <summary>
        /// Gets plugin name.
        /// </summary>
        /// <returns>
        /// The get plugin name.
        /// </returns>
        public string GetPluginName()
        {
            return "AVL Tree Plugin";
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
            object obj = Activator.CreateInstance(typeof(AVLTree<>).MakeGenericType(typeof(T)));
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
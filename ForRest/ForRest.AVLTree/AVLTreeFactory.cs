// -------------------------------------------------------------------------------
// <copyright file="_234TreeFactory.cs" company="Warsaw Univeristy of Technology">
// All rights reserved.
// </copyright>
// <summary>
// Class responsible for managing AVL tree.
// </summary>
// -------------------------------------------------------------------------------

using System;
using ForRest.Provider.BLL;

namespace ForRest.AVLTree
{
    /// <summary>
    /// Class responsible for managing AVL tree implementing ITreeFactory.
    /// </summary>
    public class AVLTreeFactory : ITreeFactory
    {
        /// <summary>
        /// Gets tree name.
        /// </summary>
        public string Name
        {
            get { return "AVL Tree"; }
        }

        /// <summary>
        /// Indicates whether tree needs degree.
        /// </summary>
        public bool NeedDegree
        {
            get { return false; }
        }

        /// <summary>
        /// Gets plugin name.
        /// </summary>
        /// <returns></returns>
        public string GetPluginName()
        {
            return "AVL Tree Plugin";
        }

        /// <summary>
        /// Gets plugin description.
        /// </summary>
        /// <returns></returns>
        public string GetPluginDescription()
        {
            return "Plugin that implements AVL Tree Algorithms";
        }

        /// <summary>
        /// Create instance of tree.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ITree<T> GetTree<T>()
        {
            object obj = Activator.CreateInstance(typeof (AVLTree<>).MakeGenericType(typeof (T)));
            var t = (ITree<T>) obj;
            return t;
        }

        /// <summary>
        /// Create instance of tree with specific degree.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="degree">Degree in Bayer&McCreight1972 notation.</param>
        /// <returns></returns>
        public ITree<T> GetTree<T>(int degree)
        {
            return GetTree<T>();
        }
    }
}

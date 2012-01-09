// -------------------------------------------------------------------------------
// <copyright file="_234TreeFactory.cs" company="Warsaw Univeristy of Technology">
// All rights reserved.
// </copyright>
// <summary>
// Class responsible for managing B+ tree.
// </summary>
// -------------------------------------------------------------------------------

using System;
using ForRest.Provider.BLL;

namespace ForRest.BPlusTree
{
    /// <summary>
    /// Class responsible for managing B+ tree implementing ITreeFactory.
    /// </summary>
    public class BPlusTreeFactory : ITreeFactory
    {
        /// <summary>
        /// Gets tree name.
        /// </summary>
        public string Name
        {
            get { return "B+ Tree"; }
        }

        /// <summary>
        /// Indicates whether tree needs degree.
        /// </summary>
        public bool NeedDegree
        {
            get { return true; }
        }

        /// <summary>
        /// Gets plugin name.
        /// </summary>
        /// <returns></returns>
        public string GetPluginName()
        {
            return "B+ Tree Plugin";
        }

        /// <summary>
        /// Gets plugin description.
        /// </summary>
        /// <returns></returns>
        public string GetPluginDescription()
        {
            return "Plugin that implements B+ Tree Algorithms";
        }

        /// <summary>
        /// Create instance of tree.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ITree<T> GetTree<T>()
        {
            //default degree if not specified explicitly
            return GetTree<T>(2);
        }

        /// <summary>
        /// Create instance of tree with specific degree.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="degree">Degree in Bayer&McCreight1972 notation.</param>
        /// <returns></returns>
        public ITree<T> GetTree<T>(int degree)
        {
            object obj = Activator.CreateInstance(typeof (BPlusTree<>).MakeGenericType(typeof (T)), degree);
            var t = (ITree<T>) obj;
            return t;
        }
    }
}

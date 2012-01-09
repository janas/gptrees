// -------------------------------------------------------------------------------
// <copyright file="_234TreeFactory.cs" company="Warsaw Univeristy of Technology">
// All rights reserved.
// </copyright>
// <summary>
// Class responsible for managing BST tree.
// </summary>
// -------------------------------------------------------------------------------

using System;
using ForRest.Provider.BLL;

namespace ForRest.BST
{
    /// <summary>
    /// Class responsible for managing BST tree implementing ITreeFactory.
    /// </summary>
    public class BinarySearchTreeFactory : ITreeFactory
    {
        public string Name
        {
            get { return "Binary Search Tree"; }
        }

        public bool NeedDegree
        {
            get { return false; }
        }

        public string GetPluginName()
        {
            return "Binary Search Tree Plugin";
        }

        public string GetPluginDescription()
        {
            return "Plugin that implements Binary Search Tree Algorithms";
        }

        public ITree<T> GetTree<T>()
        {
            object obj = Activator.CreateInstance(typeof (BinarySearchTree<>).MakeGenericType(typeof (T)));
            var t = (ITree<T>) obj;
            return t;
        }

        public ITree<T> GetTree<T>(int degree)
        {
            return GetTree<T>();
        }
    }
}

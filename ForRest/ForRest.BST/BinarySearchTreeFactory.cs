// -----------------------------------------------------------------------
// <copyright file="BinarySearchTreeFactory.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using ForRest.Provider.BLL;

namespace ForRest.BST
{

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class BinarySearchTreeFactory : ITreeFactory
    {
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
            object obj = Activator.CreateInstance(typeof(BinarySearchTree<>).MakeGenericType(typeof(T)));
            var t = (ITree<T>) obj;
            return t;
        }
    }
}

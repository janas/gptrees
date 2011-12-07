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

        public ITree GetTree(Type type)
        {
            object obj = (ITree)Activator.CreateInstance(typeof(BinarySearchTree<>).MakeGenericType(type));
            var t = (ITree) obj;
            return t;
        }
    }
}

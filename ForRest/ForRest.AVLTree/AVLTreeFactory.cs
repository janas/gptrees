// -----------------------------------------------------------------------
// <copyright file="AVLTreeFactory.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using ForRest.Provider.BLL;

namespace ForRest.AVLTree
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class AVLTreeFactory : ITreeFactory
    {
        public string Name
        {
            get { return "AVL Tree"; }
        }

        public bool NeedDegree
        {
            get { return false; }
        }

        public string GetPluginName()
        {
            return "AVL Tree Plugin";
        }

        public string GetPluginDescription()
        {
            return "Plugin that implements AVL Tree Algorithms";
        }

        public ITree<T> GetTree<T>()
        {
            object obj = Activator.CreateInstance(typeof (AVLTree<>).MakeGenericType(typeof (T)));
            var t = (ITree<T>) obj;
            return t;
        }

        public ITree<T> GetTree<T>(int degree)
        {
            return GetTree<T>();
        }
    }
}

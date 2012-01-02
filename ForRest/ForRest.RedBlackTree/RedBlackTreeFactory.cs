// -----------------------------------------------------------------------
// <copyright file="RedBlackTreeFactory.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using ForRest.Provider.BLL;

namespace ForRest.RedBlackTree
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class RedBlackTreeFactory : ITreeFactory
    {
        public string Name
        {
            get { return "Red-Black Tree"; }
        }

        public bool NeedDegree
        {
            get { return false; }
        }

        public string GetPluginName()
        {
            return "Red-Black Tree Plugin";
        }

        public string GetPluginDescription()
        {
            return "Plugin that implements Red-Black Tree Algorithms";
        }

        public ITree<T> GetTree<T>()
        {
            object obj = Activator.CreateInstance(typeof (RedBlackTree<>).MakeGenericType(typeof (T)));
            var t = (ITree<T>) obj;
            return t;
        }

        public ITree<T> GetTree<T>(int degree)
        {
            return GetTree<T>();
        }
    }
}

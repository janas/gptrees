// -----------------------------------------------------------------------
// <copyright file="BPlusTreeFactory.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using ForRest.Provider.BLL;

namespace ForRest.BPlusTree
{

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class BPlusTreeFactory : ITreeFactory
    {
        public string Name
        {
            get { return "B+ Tree"; }
        }

        public bool NeedDegree
        {
            get { return true; }
        }

        public string GetPluginName()
        {
            return "B+ Tree Plugin";
        }

        public string GetPluginDescription()
        {
            return "Plugin that implements B+ Tree Algorithms";
        }

        public ITree<T> GetTree<T>()
        {
            object obj = Activator.CreateInstance(typeof(BPlusTree<>).MakeGenericType(typeof(T)));
            var t = (ITree<T>)obj;
            return t;
        }

        public ITree<T> GetTree<T>(int degree)
        {
            object obj = Activator.CreateInstance(typeof(BPlusTree<>).MakeGenericType(typeof(T)), degree);
            var t = (ITree<T>)obj;
            return t;
        }
    }
}

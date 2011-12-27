// -----------------------------------------------------------------------
// <copyright file="SplayTreeFactory.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using ForRest.Provider.BLL;

namespace ForRest.SplayTree
{

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SplayTreeFactory : ITreeFactory
    {
        public string Name
        {
            get { return "Splay Tree"; }
        }

        public bool NeedDegree
        {
            get { return false; }
        }

        public string GetPluginName()
        {
            return "Splay Tree Plugin";
        }

        public string GetPluginDescription()
        {
            return "Plugin that implements Splay Tree Algorithms";
        }

        public ITree<T> GetTree<T>()
        {
            object obj = Activator.CreateInstance(typeof(SplayTree<>).MakeGenericType(typeof(T)));
            var t = (ITree<T>)obj;
            return t;
        }

        public ITree<T> GetTree<T>(int degree)
        {
            throw new NotImplementedException();
        }
    }
}

// -----------------------------------------------------------------------
// <copyright file="BTreeFactory.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using ForRest.Provider.BLL;

namespace ForRest.BTree
{

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class BTreeFactory : ITreeFactory
    {
        public string Name
        {
            get { return "B Tree"; }
        }

        public bool NeedDegree
        {
            get { return true; }
        }

        public string GetPluginName()
        {
            return "B-Tree Plugin";
        }

        public string GetPluginDescription()
        {
            return "Plugin that implements B-Tree Algorithms";
        }
        
        public ITree<T> GetTree<T>()
        {
            throw new NotImplementedException();
        }

        public ITree<T> GetTree<T>(int degree)
        {
            object obj = Activator.CreateInstance(typeof(BTree<>).MakeGenericType(typeof(T)), degree);
            var t = (ITree<T>)obj;
            return t;
        }
    }
}

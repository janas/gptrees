// -----------------------------------------------------------------------
// <copyright file="_23TreeFactory.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using ForRest.Provider.BLL;

namespace ForRest._23Tree
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class _23TreeFactory : ITreeFactory
    {
        public string Name
        {
            get { return "2-3 Tree"; }
        }

        public string GetPluginName()
        {
            return "2-3 Tree Plugin";
        }

        public string GetPluginDescription()
        {
            return "Plugin that implements 2-3 Tree Algorithms";
        }

        public ITree<T> GetTree<T>()
        {
            object obj = Activator.CreateInstance(typeof(_23Tree<>).MakeGenericType(typeof(T)));
            var t = (ITree<T>)obj;
            return t;
        }

        public ITree<T> GetTree<T>(int degree)
        {
            throw new NotImplementedException();
        }
    }
}

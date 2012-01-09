using System;
using ForRest.Provider.BLL;

namespace ForRest.SplayTree
{
    /// <summary>
    /// Class responsible for managing Splay tree implementing ITreeFactory.
    /// </summary>
    public class SplayTreeFactory : ITreeFactory
    {
        /// <summary>
        /// Gets tree name.
        /// </summary>
        public string Name
        {
            get { return "Splay Tree"; }
        }

        /// <summary>
        /// Indicates whether tree needs degree.
        /// </summary>
        public bool NeedDegree
        {
            get { return false; }
        }

        /// <summary>
        /// Gets plugin name.
        /// </summary>
        /// <returns></returns>
        public string GetPluginName()
        {
            return "Splay Tree Plugin";
        }

        /// <summary>
        /// Gets plugin description.
        /// </summary>
        /// <returns></returns>
        public string GetPluginDescription()
        {
            return "Plugin that implements Splay Tree Algorithms";
        }

        /// <summary>
        /// Create instance of tree.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ITree<T> GetTree<T>()
        {
            object obj = Activator.CreateInstance(typeof(SplayTree<>).MakeGenericType(typeof(T)));
            var t = (ITree<T>)obj;
            return t;
        }

        /// <summary>
        /// Create instance of tree with specific degree.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="degree">Degree in Bayer&McCreight1972 notation.</param>
        /// <returns></returns>
        public ITree<T> GetTree<T>(int degree)
        {
            throw new NotImplementedException();
        }
    }
}

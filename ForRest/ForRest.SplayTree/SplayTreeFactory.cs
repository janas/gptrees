// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SplayTreeFactory.cs" company="Warsaw University of Technology">
//   
// </copyright>
// <summary>
//   Class responsible for managing Splay tree implementing ITreeFactory.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForRest.SplayTree
{
    using System;

    using ForRest.Provider.BLL;

    /// <summary>
    /// Class responsible for managing Splay tree implementing ITreeFactory.
    /// </summary>
    public class SplayTreeFactory : ITreeFactory
    {
        #region Public Properties

        /// <summary>
        ///   Gets tree name.
        /// </summary>
        public string Name
        {
            get
            {
                return "Splay Tree";
            }
        }

        /// <summary>
        ///   Indicates whether tree needs degree.
        /// </summary>
        public bool NeedDegree
        {
            get
            {
                return false;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets plugin description.
        /// </summary>
        /// <returns>
        /// The get plugin description.
        /// </returns>
        public string GetPluginDescription()
        {
            return "Plugin that implements Splay Tree Algorithms";
        }

        /// <summary>
        /// Gets plugin name.
        /// </summary>
        /// <returns>
        /// The get plugin name.
        /// </returns>
        public string GetPluginName()
        {
            return "Splay Tree Plugin";
        }

        /// <summary>
        /// Create instance of tree.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// </returns>
        public ITree<T> GetTree<T>()
        {
            object obj = Activator.CreateInstance(typeof(SplayTree<>).MakeGenericType(typeof(T)));
            var t = (ITree<T>)obj;
            return t;
        }

        /// <summary>
        /// The get tree.
        /// </summary>
        /// <param name="degree">
        /// The degree.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public ITree<T> GetTree<T>(int degree)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
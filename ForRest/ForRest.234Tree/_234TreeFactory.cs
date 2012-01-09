// --------------------------------------------------------------------------------------------------------------------
// <copyright file="_234TreeFactory.cs" company="Warsaw University of Technology">
//   
// </copyright>
// <summary>
//   Class responsible for managing 2-3-4 tree.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForRest._234Tree
{
    using System;

    using ForRest.Provider.BLL;

    /// <summary>
    /// Class responsible for managing 2-3-4 tree implementing ITreeFactory.
    /// </summary>
    public class _234TreeFactory : ITreeFactory
    {
        #region Public Properties

        /// <summary>
        ///   Gets tree name.
        /// </summary>
        public string Name
        {
            get
            {
                return "2-3-4 Tree";
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
            return "Plugin that implements 2-3-4 Tree Algorithms";
        }

        /// <summary>
        /// Gets plugin name.
        /// </summary>
        /// <returns>
        /// The get plugin name.
        /// </returns>
        public string GetPluginName()
        {
            return "2-3-4 Tree Plugin";
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
            object obj = Activator.CreateInstance(typeof(_234Tree<>).MakeGenericType(typeof(T)));
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
        public ITree<T> GetTree<T>(int degree)
        {
            return this.GetTree<T>();
        }

        #endregion
    }
}
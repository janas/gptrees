// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Tree.cs" company="Warsaw University of Technology">
//   
// </copyright>
// <summary>
//   Generic abstract class implementing generic ITree interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForRest.Provider.BLL
{
    using System.Collections.Generic;

    /// <summary>
    /// Generic abstract class implementing generic ITree interface.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public abstract class Tree<T> : ITree<T>
    {
        #region ITree<T> Members

        /// <summary>
        /// Gets tree type.
        /// </summary>
        public abstract string TreeType { get; }
        
        /// <summary>
        /// Gets or sets Root.
        /// </summary>
        public abstract Node<T> Root { get; set; }

        /// <summary>
        /// The clear.
        /// </summary>
        public abstract void Clear();

        /// <summary>
        /// The contains.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// Returns list of initigers as a path to searched node, null otherwise.
        /// </returns>
        public abstract List<int> Contains(T data);

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        public abstract void Add(T data);

        /// <summary>
        /// The remove.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// True if item was removed or false otherwise.
        /// </returns>
        public abstract bool Remove(T data);

        #endregion
    }
}
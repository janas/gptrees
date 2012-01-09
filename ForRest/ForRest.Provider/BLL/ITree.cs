// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITree.cs" company="Warsaw University of Technology">
//   
// </copyright>
// <summary>
//   Interface providing methods allowing manipulation on the tree such as Add, Contains and Remove.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForRest.Provider.BLL
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface providing methods allowing manipulation on the tree such as Add, Contains and Remove.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public interface ITree<T>
    {
        /// <summary>
        /// Gets tree type.
        /// </summary>
        string TreeType { get; }

        /// <summary>
        /// Gets or sets Root.
        /// </summary>
        Node<T> Root { get; set; }

        /// <summary>
        /// The clear.
        /// </summary>
        void Clear();

        /// <summary>
        /// The contains.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// List of intigers as a sequence in which one has to traverse the tree to find the value.
        /// </returns>
        List<int> Contains(T data);

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        void Add(T data);

        /// <summary>
        /// The remove.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// True if item was removed or false otherwise.
        /// </returns>
        bool Remove(T data);
    }
}
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Node.cs" company="">
//   
// </copyright>
// <summary>
//   TODO: Update summary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForRest.Provider.BLL
{
    using System.Collections.Generic;
    using System.Drawing;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class Node<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Node{T}"/> class.
        /// </summary>
        public Node()
        {
            this.Values = null;
            this.Neighbors = null;

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Node{T}"/> class.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        public Node(List<T> data)
        {
            this.Values = data;
            this.Neighbors = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Node{T}"/> class.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <param name="neighbors">
        /// The neighbors.
        /// </param>
        public Node(List<T> data, NodeList<T> neighbors)
        {
            this.Values = data;
            this.Neighbors = neighbors;
        }

        /// <summary>
        /// Gets or sets Values.
        /// </summary>
        public List<T> Values { get; set; }

        /// <summary>
        /// Gets or sets Neighbors.
        /// </summary>
        public NodeList<T> Neighbors { get; set; }

        /// <summary>
        /// Gets NodeInfo.
        /// </summary>
        public virtual string NodeInfo
        {
            get { return string.Empty; }
        }

        public virtual Color NodeColor
        {
            get { return Color.Black; }
        }

        /// <summary>
        /// Gets or sets Parent.
        /// </summary>
        public virtual Node<T> Parent { get; set; }

        /// <summary>
        /// The get neighbors array.
        /// </summary>
        /// <returns>
        /// Returns the array of type Node containing neighbors.
        /// </returns>
        public Node<T>[] GetNeighborsArray()
        {
            var result = new Node<T>[this.Neighbors.Count];
            this.Neighbors.CopyTo(result, 0);
            return result;
        }

        /// <summary>
        /// The get neighbors list.
        /// </summary>
        /// <returns>
        /// Returns the list of type NodeList containing neighbors.
        /// </returns>
        public NodeList<T> GetNeighborsList()
        {
            return this.Neighbors;
        }
    }
}
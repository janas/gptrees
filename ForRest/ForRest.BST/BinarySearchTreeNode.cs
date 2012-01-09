// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BinarySearchTreeNode.cs" company="Warsaw University of Technology">
//   
// </copyright>
// <summary>
//   The binary search tree node.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForRest.BST
{
    using System.Collections.Generic;

    using ForRest.Provider.BLL;

    /// <summary>
    /// The binary search tree node.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class BinarySearchTreeNode<T> : Node<T>
    {
        #region Constants and Fields

        /// <summary>
        /// The _parent.
        /// </summary>
        private BinarySearchTreeNode<T> _parent;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BinarySearchTreeNode{T}"/> class. 
        ///   Constructor.
        /// </summary>
        public BinarySearchTreeNode()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinarySearchTreeNode{T}"/> class. 
        /// Constructor.
        /// </summary>
        /// <param name="data">
        /// Values for the node. 
        /// </param>
        public BinarySearchTreeNode(List<T> data)
            : base(data, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinarySearchTreeNode{T}"/> class. 
        /// Constructor.
        /// </summary>
        /// <param name="data">
        /// Values for the node. 
        /// </param>
        /// <param name="left">
        /// Left child node. 
        /// </param>
        /// <param name="right">
        /// Right child node. 
        /// </param>
        public BinarySearchTreeNode(List<T> data, BinarySearchTreeNode<T> left, BinarySearchTreeNode<T> right)
        {
            this.Values = data;
            var children = new NodeList<T>(2);
            children[0] = left;
            children[1] = right;
            this.Neighbors = children;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets left child node.
        /// </summary>
        public BinarySearchTreeNode<T> Left
        {
            get
            {
                if (this.Neighbors == null)
                {
                    return null;
                }

                return (BinarySearchTreeNode<T>)this.Neighbors[0];
            }

            set
            {
                if (this.Neighbors == null)
                {
                    this.Neighbors = new NodeList<T>(2);
                }

                this.Neighbors[0] = value;
            }
        }

        /// <summary>
        ///   Gets node info.
        /// </summary>
        public override string NodeInfo
        {
            get
            {
                if (this._parent == null)
                {
                    return string.Empty;
                }

                string result = "<";
                for (int i = 0; i < this._parent.Values.Count; i++)
                {
                    result += this._parent.Values[i].ToString();
                }

                result += "> ";
                return result;
            }
        }

        /// <summary>
        ///   Gets parent of the node.
        /// </summary>
        public override Node<T> Parent
        {
            get
            {
                return this._parent;
            }

            set
            {
                this._parent = (BinarySearchTreeNode<T>)value;
            }
        }

        /// <summary>
        ///   Gets right child node.
        /// </summary>
        public BinarySearchTreeNode<T> Right
        {
            get
            {
                if (this.Neighbors == null)
                {
                    return null;
                }

                return (BinarySearchTreeNode<T>)this.Neighbors[1];
            }

            set
            {
                if (this.Neighbors == null)
                {
                    this.Neighbors = new NodeList<T>(2);
                }

                this.Neighbors[1] = value;
            }
        }

        #endregion
    }
}
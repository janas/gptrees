// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AVLTreeNode.cs" company="Warsaw University of Technology">
//   
// </copyright>
// <summary>
//   The avl tree node.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForRest.AVLTree
{
    using System.Collections.Generic;

    using ForRest.Provider.BLL;

    /// <summary>
    /// The avl tree node.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class AVLTreeNode<T> : Node<T>
    {
        #region Constants and Fields

        /// <summary>
        /// The _height.
        /// </summary>
        private int _height;

        /// <summary>
        /// The _parent.
        /// </summary>
        private AVLTreeNode<T> _parent;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AVLTreeNode{T}"/> class. 
        ///   Constructor.
        /// </summary>
        public AVLTreeNode()
        {
            this._height = 1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AVLTreeNode{T}"/> class. 
        /// Constructor.
        /// </summary>
        /// <param name="data">
        /// Values for the node. 
        /// </param>
        public AVLTreeNode(List<T> data)
            : base(data, null)
        {
            this._height = 1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AVLTreeNode{T}"/> class. 
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
        public AVLTreeNode(List<T> data, AVLTreeNode<T> left, AVLTreeNode<T> right)
        {
            this.Values = data;
            var children = new NodeList<T>(2);
            children[0] = left;
            children[1] = right;
            this.Neighbors = children;
            if (left.Height > right.Height)
            {
                this._height = left.Height + 1;
            }
            else
            {
                this._height = right.Height + 1;
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets height of the node.
        /// </summary>
        public int Height
        {
            get
            {
                return this._height;
            }
        }

        /// <summary>
        ///   Gets left child node.
        /// </summary>
        public AVLTreeNode<T> Left
        {
            get
            {
                if (this.Neighbors == null)
                {
                    return null;
                }

                return (AVLTreeNode<T>)this.Neighbors[0];
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
                string result = "h=" + this._height + " ";
                if (this._parent == null)
                {
                    return result;
                }

                result += "<";
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
                this._parent = (AVLTreeNode<T>)value;
            }
        }

        /// <summary>
        ///   Gets right child node.
        /// </summary>
        public AVLTreeNode<T> Right
        {
            get
            {
                if (this.Neighbors == null)
                {
                    return null;
                }

                return (AVLTreeNode<T>)this.Neighbors[1];
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

        #region Public Methods

        /// <summary>
        /// Balances node.
        /// </summary>
        /// <returns>
        /// The balance.
        /// </returns>
        public bool Balance()
        {
            if (this.Left != null)
            {
                this.Left.Balance();
            }

            if (this.Right != null)
            {
                this.Right.Balance();
            }

            this.UpdateHeight();
            int leftHeight = 0;
            int rightHeight = 0;
            if (this.Left != null)
            {
                leftHeight = this.Left.Height;
            }

            if (this.Right != null)
            {
                rightHeight = this.Right.Height;
            }

            if (leftHeight - rightHeight > 1)
            {
                // right rotation
                AVLTreeNode<T> z = this;
                AVLTreeNode<T> y = this.Left;
                AVLTreeNode<T> b = this.Left.Right;
                bool zIsleftNode = z.Parent != null && ((AVLTreeNode<T>)z.Parent).Left == this;
                z.Left = b;
                if (b != null)
                {
                    b.Parent = z;
                }

                y.Right = z;
                if (y != null)
                {
                    y.Parent = z.Parent;
                    if (y.Parent != null)
                    {
                        if (zIsleftNode)
                        {
                            ((AVLTreeNode<T>)y.Parent).Left = y;
                        }
                        else
                        {
                            ((AVLTreeNode<T>)y.Parent).Right = y;
                        }
                    }
                }

                z.Parent = y;
                return true;
            }

            if (rightHeight - leftHeight > 1)
            {
                // left rotation
                AVLTreeNode<T> z = this;
                AVLTreeNode<T> y = this.Right;
                AVLTreeNode<T> b = this.Right.Left;
                bool zIsleftNode = z.Parent != null && ((AVLTreeNode<T>)z.Parent).Left == this;
                z.Right = b;
                if (b != null)
                {
                    b.Parent = z;
                }

                y.Left = z;
                if (y != null)
                {
                    y.Parent = z.Parent;
                    if (y.Parent != null)
                    {
                        if (zIsleftNode)
                        {
                            ((AVLTreeNode<T>)y.Parent).Left = y;
                        }
                        else
                        {
                            ((AVLTreeNode<T>)y.Parent).Right = y;
                        }
                    }
                }

                z.Parent = y;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Update height of the node.
        /// </summary>
        /// <returns>
        /// The update height.
        /// </returns>
        public int UpdateHeight()
        {
            int leftHeight = 0;
            int rightHeight = 0;
            if (this.Left != null)
            {
                leftHeight = this.Left.UpdateHeight();
            }

            if (this.Right != null)
            {
                rightHeight = this.Right.UpdateHeight();
            }

            if (rightHeight > leftHeight)
            {
                this._height = rightHeight + 1;
            }
            else
            {
                this._height = leftHeight + 1;
            }

            return this._height;
        }

        #endregion
    }
}
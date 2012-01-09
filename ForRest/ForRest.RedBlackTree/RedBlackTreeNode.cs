// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RedBlackTreeNode.cs" company="Warsaw University of Technology">
//   
// </copyright>
// <summary>
//   The red black tree node.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForRest.RedBlackTree
{
    using System.Collections.Generic;
    using System.Drawing;

    using ForRest.Provider.BLL;

    /// <summary>
    /// The red black tree node.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class RedBlackTreeNode<T> : Node<T>
    {
        #region Constants and Fields

        /// <summary>
        /// The _max height.
        /// </summary>
        private int _maxHeight;

        /// <summary>
        /// The _min height.
        /// </summary>
        private int _minHeight;

        /// <summary>
        /// The _parent.
        /// </summary>
        private RedBlackTreeNode<T> _parent;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RedBlackTreeNode{T}"/> class. 
        ///   Constructor.
        /// </summary>
        public RedBlackTreeNode()
        {
            this._maxHeight = 1;
            this._maxHeight = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedBlackTreeNode{T}"/> class. 
        /// Constructor.
        /// </summary>
        /// <param name="data">
        /// Values for the node. 
        /// </param>
        public RedBlackTreeNode(List<T> data)
            : base(data, null)
        {
            this._maxHeight = 1;
            this._maxHeight = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedBlackTreeNode{T}"/> class. 
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
        public RedBlackTreeNode(List<T> data, RedBlackTreeNode<T> left, RedBlackTreeNode<T> right)
        {
            this.Values = data;
            var children = new NodeList<T>(2);
            children[0] = left;
            children[1] = right;
            this.Neighbors = children;
            this.UpdateMaxHeight();
            this.UpdateMinHeight();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Indicates whether node is red.
        /// </summary>
        public bool IsRed
        {
            get
            {
                if (this._parent == null)
                {
                    return false;
                }

                if (this._parent.IsRed)
                {
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        ///   Gets left child node.
        /// </summary>
        public RedBlackTreeNode<T> Left
        {
            get
            {
                if (this.Neighbors == null)
                {
                    return null;
                }

                return (RedBlackTreeNode<T>)this.Neighbors[0];
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
        ///   Gets maxHeight of the node.
        /// </summary>
        public int MaxHeight
        {
            get
            {
                return this._maxHeight;
            }
        }

        /// <summary>
        ///   Gets minHeight of the node.
        /// </summary>
        public int MinHeight
        {
            get
            {
                return this._minHeight;
            }
        }

        /// <summary>
        ///   Gets node color.
        /// </summary>
        public override Color NodeColor
        {
            get
            {
                if (this.IsRed)
                {
                    return Color.Red;
                }

                return Color.Black;
            }
        }

        /// <summary>
        ///   Gets node info.
        /// </summary>
        public override string NodeInfo
        {
            get
            {
                if (this.IsRed)
                {
                    return "<Red>";
                }

                return "<Black>";
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
                this._parent = (RedBlackTreeNode<T>)value;
            }
        }

        /// <summary>
        ///   Gets right child node.
        /// </summary>
        public RedBlackTreeNode<T> Right
        {
            get
            {
                if (this.Neighbors == null)
                {
                    return null;
                }

                return (RedBlackTreeNode<T>)this.Neighbors[1];
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

            this.UpdateMaxHeight();
            this.UpdateMinHeight();
            int maxLeftHeight = 0;
            int maxRightHeight = 0;
            int minLeftHeight = 0;
            int minRightHeight = 0;
            if (this.Left != null)
            {
                maxLeftHeight = this.Left.MaxHeight;
                minLeftHeight = this.Left.MinHeight;
            }

            if (this.Right != null)
            {
                maxRightHeight = this.Right.MaxHeight;
                minRightHeight = this.Right.MinHeight;
            }

            int leftMinusRight = maxLeftHeight - minRightHeight - 1;
            if (leftMinusRight > 0)
            {
                {
                    // for (int i = 0; i < leftMinusRight; i++)
                    // right rotation
                    RedBlackTreeNode<T> z = this;
                    RedBlackTreeNode<T> y = this.Left;
                    RedBlackTreeNode<T> b = this.Left.Right;
                    bool zIsleftNode = z.Parent != null && ((RedBlackTreeNode<T>)z.Parent).Left == this;
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
                                ((RedBlackTreeNode<T>)y.Parent).Left = y;
                            }
                            else
                            {
                                ((RedBlackTreeNode<T>)y.Parent).Right = y;
                            }
                        }
                    }

                    z.Parent = y;
                }

                return true;
            }

            int rightMinusLeft = maxRightHeight - minLeftHeight - 1;
            if (rightMinusLeft > 0)
            {
                {
                    // for (int j = 0; j < rightMinusLeft; j++)
                    // left rotation
                    RedBlackTreeNode<T> z = this;
                    RedBlackTreeNode<T> y = this.Right;
                    RedBlackTreeNode<T> b = this.Right.Left;
                    bool zIsleftNode = z.Parent != null && ((RedBlackTreeNode<T>)z.Parent).Left == this;
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
                                ((RedBlackTreeNode<T>)y.Parent).Left = y;
                            }
                            else
                            {
                                ((RedBlackTreeNode<T>)y.Parent).Right = y;
                            }
                        }
                    }

                    z.Parent = y;
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Update maxHeight of the node.
        /// </summary>
        /// <returns>
        /// The update max height.
        /// </returns>
        public int UpdateMaxHeight()
        {
            int leftHeight = 0;
            int rightHeight = 0;
            if (this.Left != null)
            {
                leftHeight = this.Left.UpdateMaxHeight();
            }

            if (this.Right != null)
            {
                rightHeight = this.Right.UpdateMaxHeight();
            }

            if (rightHeight > leftHeight)
            {
                this._maxHeight = rightHeight + 1;
            }
            else
            {
                this._maxHeight = leftHeight + 1;
            }

            return this._maxHeight;
        }

        /// <summary>
        /// Update minHeight of the node.
        /// </summary>
        /// <returns>
        /// The update min height.
        /// </returns>
        public int UpdateMinHeight()
        {
            int leftHeight = 0;
            int rightHeight = 0;
            if (this.Left != null)
            {
                leftHeight = this.Left.UpdateMinHeight();
            }

            if (this.Right != null)
            {
                rightHeight = this.Right.UpdateMinHeight();
            }

            if (rightHeight > leftHeight)
            {
                this._minHeight = leftHeight + 1;
            }
            else
            {
                this._minHeight = rightHeight + 1;
            }

            return this._minHeight;
        }

        #endregion
    }
}
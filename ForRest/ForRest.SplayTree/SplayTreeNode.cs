// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SplayTreeNode.cs" company="Warsaw University of Technology">
//   
// </copyright>
// <summary>
//   The splay tree node.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForRest.SplayTree
{
    using System.Collections.Generic;

    using ForRest.Provider.BLL;

    /// <summary>
    /// The splay tree node.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class SplayTreeNode<T> : Node<T>
    {
        #region Constants and Fields

        /// <summary>
        /// The _height.
        /// </summary>
        private int _height;

        /// <summary>
        /// The _parent.
        /// </summary>
        private SplayTreeNode<T> _parent;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SplayTreeNode{T}"/> class. 
        ///   Constructor.
        /// </summary>
        public SplayTreeNode()
        {
            this._height = 1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SplayTreeNode{T}"/> class. 
        /// Constructor.
        /// </summary>
        /// <param name="data">
        /// Values for the node. 
        /// </param>
        public SplayTreeNode(List<T> data)
            : base(data, null)
        {
            this._height = 1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SplayTreeNode{T}"/> class. 
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
        public SplayTreeNode(List<T> data, SplayTreeNode<T> left, SplayTreeNode<T> right)
        {
            this.Values = data;
            NodeList<T> children = new NodeList<T>(2);
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
        public SplayTreeNode<T> Left
        {
            get
            {
                if (this.Neighbors == null)
                {
                    return null;
                }

                return (SplayTreeNode<T>)this.Neighbors[0];
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
                string result = string.Empty;

                // result += "h=" + _height.ToString() + " ";
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
                this._parent = (SplayTreeNode<T>)value;
            }
        }

        /// <summary>
        ///   Gets right child node.
        /// </summary>
        public SplayTreeNode<T> Right
        {
            get
            {
                if (this.Neighbors == null)
                {
                    return null;
                }

                return (SplayTreeNode<T>)this.Neighbors[1];
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
                SplayTreeNode<T> z = this;
                SplayTreeNode<T> y = this.Left;
                SplayTreeNode<T> a = this.Left.Left;
                SplayTreeNode<T> b = this.Left.Right;
                SplayTreeNode<T> c = this.Right;
                bool zIsleftNode = false;
                if (z.Parent != null && ((SplayTreeNode<T>)z.Parent).Left == this)
                {
                    zIsleftNode = true;
                }

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
                            ((SplayTreeNode<T>)y.Parent).Left = y;
                        }
                        else
                        {
                            ((SplayTreeNode<T>)y.Parent).Right = y;
                        }
                    }
                }

                z.Parent = y;
                return true;
            }
            else if (rightHeight - leftHeight > 1)
            {
                // left rotation
                SplayTreeNode<T> z = this;
                SplayTreeNode<T> y = this.Right;
                SplayTreeNode<T> a = this.Left;
                SplayTreeNode<T> b = this.Right.Left;
                SplayTreeNode<T> c = this.Right.Right;
                bool zIsleftNode = false;
                if (z.Parent != null && ((SplayTreeNode<T>)z.Parent).Left == this)
                {
                    zIsleftNode = true;
                }

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
                            ((SplayTreeNode<T>)y.Parent).Left = y;
                        }
                        else
                        {
                            ((SplayTreeNode<T>)y.Parent).Right = y;
                        }
                    }
                }

                z.Parent = y;
                return true;
            }

            bool balanced = false;
            if (this.Left != null && this.Left.Balance())
            {
                balanced = true;
            }

            if (this.Right != null && this.Right.Balance())
            {
                balanced = true;
            }

            return balanced;
        }

        /// <summary>
        /// Performs left or right rotation.
        /// </summary>
        /// <param name="leftBalance">
        /// Indicates whether perform left rotation. 
        /// </param>
        public void Balance(bool leftBalance)
        {
            if (leftBalance)
            {
                // right rotation
                SplayTreeNode<T> z = this;
                SplayTreeNode<T> y = this.Left;
                SplayTreeNode<T> a = this.Left.Left;
                SplayTreeNode<T> b = this.Left.Right;
                SplayTreeNode<T> c = this.Right;
                bool zIsleftNode = false;
                if (z.Parent != null && ((SplayTreeNode<T>)z.Parent).Left == this)
                {
                    zIsleftNode = true;
                }

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
                            ((SplayTreeNode<T>)y.Parent).Left = y;
                        }
                        else
                        {
                            ((SplayTreeNode<T>)y.Parent).Right = y;
                        }
                    }
                }

                z.Parent = y;
            }
            else
            {
                // left rotation
                SplayTreeNode<T> z = this;
                SplayTreeNode<T> y = this.Right;
                SplayTreeNode<T> a = this.Left;
                SplayTreeNode<T> b = this.Right.Left;
                SplayTreeNode<T> c = this.Right.Right;
                bool zIsleftNode = false;
                if (z.Parent != null && ((SplayTreeNode<T>)z.Parent).Left == this)
                {
                    zIsleftNode = true;
                }

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
                            ((SplayTreeNode<T>)y.Parent).Left = y;
                        }
                        else
                        {
                            ((SplayTreeNode<T>)y.Parent).Right = y;
                        }
                    }
                }

                z.Parent = y;
            }
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
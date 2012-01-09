// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SplayTree.cs" company="Warsaw University of Technology">
//   
// </copyright>
// <summary>
//   The splay tree.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForRest.SplayTree
{
    using System.Collections.Generic;

    using ForRest.Provider.BLL;

    /// <summary>
    /// The splay tree.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class SplayTree<T> : Tree<T>
    {
        #region Constants and Fields

        /// <summary>
        /// The _comparer.
        /// </summary>
        private readonly IComparer<T> _comparer = Comparer<T>.Default;

        /// <summary>
        /// The _root.
        /// </summary>
        private SplayTreeNode<T> _root;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SplayTree{T}"/> class. 
        ///   Basic constructor.
        /// </summary>
        public SplayTree()
        {
            this._root = null;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets root of the tree.
        /// </summary>
        public override Node<T> Root
        {
            get
            {
                return this._root;
            }

            set
            {
                this._root = (SplayTreeNode<T>)value;
            }
        }

        /// <summary>
        ///   Gets type of the tree.
        /// </summary>
        public override string TreeType
        {
            get
            {
                return "Splay Tree";
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds element to the tree.
        /// </summary>
        /// <param name="data">
        /// Element to be added. 
        /// </param>
        public override void Add(T data)
        {
            var dataList = new List<T>(1) { data };
            var node = new SplayTreeNode<T>(dataList);
            SplayTreeNode<T> current = this._root, parent = null;
            int result;
            while (current != null)
            {
                result = this._comparer.Compare(current.Values[0], data);
                if (result > 0)
                {
                    parent = current;
                    current = current.Left;
                }
                else
                {
                    parent = current;
                    current = current.Right;
                }
            }

            if (parent == null)
            {
                this._root = node;
            }
            else
            {
                result = this._comparer.Compare(parent.Values[0], data);
                if (result > 0)
                {
                    parent.Left = node;
                }
                else
                {
                    parent.Right = node;
                }
            }

            node.Parent = parent;
            this.Splay(node);
        }

        /// <summary>
        /// Clears nodes of the tree.
        /// </summary>
        public override void Clear()
        {
            this._root = null;
        }

        /// <summary>
        /// Indicates whether tree contains element.
        /// </summary>
        /// <param name="data">
        /// Element to be searched. 
        /// </param>
        /// <returns>
        /// </returns>
        public override List<int> Contains(T data)
        {
            List<int> path = new List<int>();
            SplayTreeNode<T> lastVisited = null;
            SplayTreeNode<T> current = this._root;
            while (current != null)
            {
                int result = this._comparer.Compare(current.Values[0], data);
                if (result == 0)
                {
                    this.Splay(current);
                    if (path != null)
                    {
                        path.Clear();
                        path = new List<int>();
                    }

                    return path;
                }

                if (result > 0)
                {
                    lastVisited = current;
                    current = current.Left;
                    path.Add(0);
                }
                else
                {
                    lastVisited = current;
                    current = current.Right;
                    path.Add(1);
                }
            }

            this.Splay(lastVisited);
            return null;
        }

        /// <summary>
        /// Removes element from the tree.
        /// </summary>
        /// <param name="data">
        /// Element to be removed. 
        /// </param>
        /// <returns>
        /// The remove.
        /// </returns>
        public override bool Remove(T data)
        {
            if (this._root == null)
            {
                return false;
            }

            SplayTreeNode<T> current = this._root, parent = null;
            int result = this._comparer.Compare(current.Values[0], data);
            while (result != 0)
            {
                if (result > 0)
                {
                    parent = current;
                    current = current.Left;
                }
                else
                {
                    parent = current;
                    current = current.Right;
                }

                if (current == null)
                {
                    this.Splay(parent);
                    return false;
                }

                result = this._comparer.Compare(current.Values[0], data);
            }

            if (current.Right == null)
            {
                if (parent == null)
                {
                    if (current.Left != null)
                    {
                        current.Left.Parent = parent;
                    }

                    this._root = current.Left;
                }
                else
                {
                    result = this._comparer.Compare(parent.Values[0], current.Values[0]);
                    if (result > 0)
                    {
                        if (current.Left != null)
                        {
                            current.Left.Parent = parent;
                        }

                        parent.Left = current.Left;
                    }
                    else
                    {
                        if (current.Right != null)
                        {
                            current.Right.Parent = parent;
                        }

                        parent.Right = current.Right;
                    }
                }
            }
            else if (current.Right.Left == null)
            {
                current.Right.Left = current.Left;
                if (current.Left != null)
                {
                    current.Left.Parent = current.Right;
                }

                if (parent == null)
                {
                    if (current.Right != null)
                    {
                        current.Right.Parent = parent;
                    }

                    this._root = current.Right;
                }
                else
                {
                    result = this._comparer.Compare(parent.Values[0], current.Values[0]);
                    if (result > 0)
                    {
                        if (current.Right != null)
                        {
                            current.Right.Parent = parent;
                        }

                        parent.Left = current.Right;
                    }
                    else
                    {
                        if (current.Right != null)
                        {
                            current.Right.Parent = parent;
                        }

                        parent.Right = current.Right;
                    }
                }
            }
            else
            {
                SplayTreeNode<T> leftMost = current.Right, lmParent = current;
                while (leftMost.Left != null)
                {
                    lmParent = leftMost;
                    leftMost = lmParent.Left;
                }

                lmParent.Left = leftMost.Right;
                leftMost.Left = current.Left;
                leftMost.Right = current.Right;
                if (lmParent.Left != null)
                {
                    lmParent.Left.Parent = leftMost;
                }

                if (leftMost.Right != null)
                {
                    leftMost.Right.Parent = lmParent;
                }

                if (current.Left != null)
                {
                    current.Left.Parent = leftMost;
                }

                if (current.Right != null)
                {
                    current.Right.Parent = leftMost;
                }

                if (parent == null)
                {
                    leftMost.Parent = parent;
                    this._root = leftMost;
                }
                else
                {
                    result = this._comparer.Compare(parent.Values[0], current.Values[0]);
                    if (result > 0)
                    {
                        leftMost.Parent = parent;
                        parent.Left = leftMost;
                    }
                    else
                    {
                        leftMost.Parent = parent;
                        parent.Right = leftMost;
                    }
                }
            }

            if (current != null)
            {
                this.Splay((SplayTreeNode<T>)current.Parent);
            }

            current.Parent = current.Left = current.Right = null;
            return true;
        }

        /// <summary>
        /// Performs Splay operation on input node.
        /// </summary>
        /// <param name="node">
        /// Node to be splayed. 
        /// </param>
        public void Splay(SplayTreeNode<T> node)
        {
            if (this._root == null || node == null)
            {
                return;
            }

            SplayTreeNode<T> current = this._root;
            SplayTreeNode<T> parent = null;
            while (current != node)
            {
                if (current == null || current.Neighbors == null)
                {
                    return;
                }

                int result = this._comparer.Compare(current.Values[0], node.Values[0]);
                if (result > 0)
                {
                    if (current.Left != null)
                    {
                        parent = current;
                        current = (SplayTreeNode<T>)current.Left;
                    }
                }
                else
                {
                    if (current.Right != null)
                    {
                        parent = current;
                        current = (SplayTreeNode<T>)current.Right;
                    }
                }
            }

            if (parent == null)
            {
                return;
            }

            // ZIG
            if (parent.Left == current)
            {
                parent.Balance(true);
            }
                
                // ZAG
            else
            {
                parent.Balance(false);
            }

            while (current.Parent != null)
            {
                current = (SplayTreeNode<T>)current.Parent;
            }

            this._root = current;
            if (this._root != node)
            {
                this.Splay(node);
            }
            else if (this._root != null)
            {
                this._root.UpdateHeight();
            }
        }

        #endregion
    }
}
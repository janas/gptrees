// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BPlusTree.cs" company="Warsaw University of Technology">
//   
// </copyright>
// <summary>
//   The b plus tree.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForRest.BPlusTree
{
    using System.Collections.Generic;

    using ForRest.Provider.BLL;

    /// <summary>
    /// The b plus tree.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class BPlusTree<T> : Tree<T>
    {
        #region Constants and Fields

        /// <summary>
        /// The _comparer.
        /// </summary>
        private readonly IComparer<T> _comparer = Comparer<T>.Default;

        /// <summary>
        /// The _m.
        /// </summary>
        private readonly int _m;

        /// <summary>
        /// The _root.
        /// </summary>
        private BPlusTreeNode<T> _root;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BPlusTree{T}"/> class.
        /// </summary>
        /// <param name="degree">
        /// The degree.
        /// </param>
        public BPlusTree(int degree)
        {
            this._root = null;
            this._m = degree;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets degree of the tree.
        /// </summary>
        public int M
        {
            get
            {
                return this._m;
            }
        }

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
                this._root = (BPlusTreeNode<T>)value;
            }
        }

        /// <summary>
        ///   Gets type of the tree.
        /// </summary>
        public override string TreeType
        {
            get
            {
                return "B Plus Tree";
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
            if (this._root == null)
            {
                var dataList = new List<T> { data };
                var node = new BPlusTreeNode<T>(this._m, null, dataList);
                this._root = node;
            }
            else
            {
                BPlusTreeNode<T> node = this.Insert(this._root, data);
                while (node.Parent != null)
                {
                    node = (BPlusTreeNode<T>)node.Parent;
                }

                this._root = node;
            }
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
            var path = new List<int>();
            BPlusTreeNode<T> current = this._root;
            while (current != null)
            {
                for (int i = 0; i < current.Values.Count; i++)
                {
                    int result = this._comparer.Compare(current.Values[i], data);
                    if (result == 0)
                    {
                        return path;
                    }

                    if (result > 0)
                    {
                        if (current.Neighbors == null)
                        {
                            return null;
                        }

                        current = (BPlusTreeNode<T>)current.Neighbors[i];
                        path.Add(i);
                        break;
                    }

                    if (i + 1 == current.Values.Count)
                    {
                        if (current.Neighbors == null)
                        {
                            return null;
                        }

                        current = (BPlusTreeNode<T>)current.Neighbors[i + 1];
                        path.Add(i + 1);
                        break;
                    }
                }
            }

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
            BPlusTreeNode<T> node = this.Delete(this._root, data);
            if (node == null)
            {
                return false;
            }

            while (node.Parent != null)
            {
                node = (BPlusTreeNode<T>)node.Parent;
            }

            if (node.Values.Count > 0)
            {
                this._root = node;
            }
            else
            {
                this._root = null;
            }

            return true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Deletes data from given node.
        /// </summary>
        /// <param name="node">
        /// Node from which element will be removed. 
        /// </param>
        /// <param name="data">
        /// Element to be removed. 
        /// </param>
        /// <returns>
        /// </returns>
        private BPlusTreeNode<T> Delete(BPlusTreeNode<T> node, T data)
        {
            while (node != null)
            {
                for (int i = 0; i < node.Values.Count; i++)
                {
                    int result = this._comparer.Compare(node.Values[i], data);
                    if (result == 0)
                    {
                        return node.Delete(data, i);
                    }

                    if (result > 0)
                    {
                        if (node.Neighbors == null)
                        {
                            return null;
                        }

                        return this.Delete((BPlusTreeNode<T>)node.Neighbors[i], data);
                    }

                    if (i + 1 == node.Values.Count)
                    {
                        if (node.Neighbors == null)
                        {
                            return null;
                        }

                        return this.Delete((BPlusTreeNode<T>)node.Neighbors[i + 1], data);
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Inserts data into given node.
        /// </summary>
        /// <param name="node">
        /// Node in which element will be added. 
        /// </param>
        /// <param name="data">
        /// Element to be added. 
        /// </param>
        /// <returns>
        /// </returns>
        private BPlusTreeNode<T> Insert(BPlusTreeNode<T> node, T data)
        {
            if (!node.IsLeaf)
            {
                // Look for child to go to
                for (int i = 0; i < node.Values.Count; i++)
                {
                    int result = this._comparer.Compare(node.Values[i], data);
                    if (result > 0)
                    {
                        return this.Insert(node.ChildAt(i), data);
                    }

                    if (i + 1 == node.Values.Count)
                    {
                        return this.Insert(node.ChildAt(i + 1), data);
                    }
                }

                return null;
            }

            if (!node.IsFull)
            {
                return node.Add(data);
            }

            // return node.Split(data);
            if (node == this._root)
            {
                this._root = node.Split(data);
                return this._root;
            }

            node.Parent = node.Split(data);
            return (BPlusTreeNode<T>)node.Parent;
        }

        #endregion
    }
}
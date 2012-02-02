// --------------------------------------------------------------------------------------------------------------------
// <copyright file="_234Tree.cs" company="Warsaw University of Technology">
//   
// </copyright>
// <summary>
//   The _234 tree.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForRest._234Tree
{
    using System.Collections.Generic;

    using ForRest.Provider.BLL;

    /// <summary>
    /// The _234 tree.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class _234Tree<T> : Tree<T>
    {
        #region Constants and Fields

        /// <summary>
        /// The _comparer.
        /// </summary>
        private readonly IComparer<T> _comparer = Comparer<T>.Default;

        /// <summary>
        /// The _root.
        /// </summary>
        private _234TreeNode<T> _root;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="_234Tree{T}"/> class.
        /// </summary>
        public _234Tree()
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
                this._root = (_234TreeNode<T>)value;
            }
        }

        /// <summary>
        ///   Gets type of the tree.
        /// </summary>
        public override string TreeType
        {
            get
            {
                return "2-3-4 Tree";
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
                var node = new _234TreeNode<T>(null, dataList);
                this._root = node;
            }
            else
            {
                _234TreeNode<T> node = this.Insert(this._root, data);
                while (node.Parent != null)
                {
                    node = (_234TreeNode<T>)node.Parent;
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
        public override SearchResult Contains(T data)
        {
            SearchResult searchResult;
            searchResult.searchPath = new List<int>();
            searchResult.nodesVisited = 0;
            _234TreeNode<T> current = this._root;
            while (current != null)
            {
                searchResult.nodesVisited++;
                for (int i = 0; i < current.Values.Count; i++)
                {
                    int result = this._comparer.Compare(current.Values[i], data);
                    if (result == 0)
                    {
                        return searchResult;
                    }

                    if (result > 0)
                    {
                        if (current.Neighbors == null)
                        {
                            searchResult.searchPath = null;
                            return searchResult;
                        }
                        current = (_234TreeNode<T>)current.Neighbors[i];
                        searchResult.searchPath.Add(i);
                        break;
                    }

                    if (i + 1 == current.Values.Count)
                    {
                        if (current.Neighbors == null)
                        {
                            searchResult.searchPath = null;
                            return searchResult;
                        }

                        current = (_234TreeNode<T>)current.Neighbors[i + 1];
                        searchResult.searchPath.Add(i + 1);
                        break;
                    }
                }
            }
            searchResult.searchPath = null;
            return searchResult;
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
            _234TreeNode<T> node = this.Delete(this._root, data);
            if (node == null)
            {
                return false;
            }

            while (node.Parent != null)
            {
                node = (_234TreeNode<T>)node.Parent;
            }

            this._root = node.Values.Count > 0 ? node : null;
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
        private _234TreeNode<T> Delete(_234TreeNode<T> node, T data)
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

                        return this.Delete((_234TreeNode<T>)node.Neighbors[i], data);
                    }

                    if (i + 1 == node.Values.Count)
                    {
                        if (node.Neighbors == null)
                        {
                            return null;
                        }

                        return this.Delete((_234TreeNode<T>)node.Neighbors[i + 1], data);
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
        private _234TreeNode<T> Insert(_234TreeNode<T> node, T data)
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
            return (_234TreeNode<T>)node.Parent;
        }

        #endregion
    }
}
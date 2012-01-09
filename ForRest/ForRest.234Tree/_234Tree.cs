﻿using System.Collections.Generic;
using ForRest.Provider.BLL;

namespace ForRest._234Tree
{
    /// <summary>
    /// 2-3-4 tree class implementing Tree<T>.
    /// </summary>
    public class _234Tree<T> : Tree<T>
    {
        private _234TreeNode<T> _root;
        private readonly IComparer<T> _comparer = Comparer<T>.Default;

        /// <summary>
        /// Basic constructor.
        /// </summary>
        /// <param name="degree">Degree in Bayer&McCreight1972 notation.</param>
        public _234Tree()
        {
            _root = null;
        }

        /// <summary>
        /// Gets type of the tree.
        /// </summary>
        public override string TreeType
        {
            get { return "2-3-4 Tree"; }
        }

        /// <summary>
        /// Gets root of the tree.
        /// </summary>
        public override Node<T> Root
        {
            get { return _root; }
            set { _root = (_234TreeNode<T>) value; }
        }

        /// <summary>
        /// Clears nodes of the tree.
        /// </summary>
        public override void Clear()
        {
            _root = null;
        }

        /// <summary>
        /// Indicates whether tree contains element.
        /// </summary>
        /// <param name="data">Element to be searched.</param>
        /// <returns></returns>
        public override List<int> Contains(T data)
        {
            var path = new List<int>();
            _234TreeNode<T> current = _root;
            while (current != null)
            {
                for (int i = 0; i < current.Values.Count; i++)
                {
                    int result = _comparer.Compare(current.Values[i], data);
                    if (result == 0)
                        return path;
                    if (result > 0)
                    {
                        if (current.Neighbors == null)
                            return null;
                        current = (_234TreeNode<T>) current.Neighbors[i];
                        path.Add(i);
                        break;
                    }
                    if (i + 1 == current.Values.Count)
                    {
                        if (current.Neighbors == null)
                            return null;
                        current = (_234TreeNode<T>) current.Neighbors[i + 1];
                        path.Add(i + 1);
                        break;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Inserts data into given node.
        /// </summary>
        /// <param name="node">Node in which element will be added.</param>
        /// <param name="data">Element to be added.</param>
        /// <returns></returns>
        private _234TreeNode<T> Insert(_234TreeNode<T> node, T data)
        {
            if (!node.IsLeaf)
            {
                // Look for child to go to
                for (int i = 0; i < node.Values.Count; i++)
                {
                    int result = _comparer.Compare(node.Values[i], data);
                    if (result > 0)
                        return Insert(node.ChildAt(i), data);
                    if (i + 1 == node.Values.Count)
                        return Insert(node.ChildAt(i + 1), data);
                }
                return null;
            }
            if (!node.IsFull)
            {
                return node.Add(data);
            }
            //return node.Split(data);
            if (node == _root)
            {
                _root = node.Split(data);
                return _root;
            }
            node.Parent = node.Split(data);
            return (_234TreeNode<T>) node.Parent;
        }

        /// <summary>
        /// Adds element to the tree.
        /// </summary>
        /// <param name="data">Element to be added.</param>
        public override void Add(T data)
        {
            if (_root == null)
            {
                var dataList = new List<T> {data};
                var node = new _234TreeNode<T>(null, dataList);
                _root = node;
            }
            else
            {
                _234TreeNode<T> node = Insert(_root, data);
                while (node.Parent != null)
                    node = (_234TreeNode<T>) node.Parent;
                _root = node;
            }
        }

        /// <summary>
        /// Deletes data from given node.
        /// </summary>
        /// <param name="node">Node from which element will be removed.</param>
        /// <param name="data">Element to be removed.</param>
        /// <returns></returns>
        private _234TreeNode<T> Delete(_234TreeNode<T> node, T data)
        {
            while (node != null)
            {
                for (int i = 0; i < node.Values.Count; i++)
                {
                    int result = _comparer.Compare(node.Values[i], data);
                    if (result == 0)
                        return node.Delete(data, i);
                    if (result > 0)
                    {
                        if (node.Neighbors == null)
                            return null;
                        return Delete((_234TreeNode<T>) node.Neighbors[i], data);
                    }
                    if (i + 1 == node.Values.Count)
                    {
                        if (node.Neighbors == null)
                            return null;
                        return Delete((_234TreeNode<T>) node.Neighbors[i + 1], data);
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Removes element from the tree.
        /// </summary>
        /// <param name="data">Element to be removed.</param>
        /// <returns></returns>
        public override bool Remove(T data)
        {
            _234TreeNode<T> node = Delete(_root, data);
            if (node == null)
                return false;
            while (node.Parent != null)
                node = (_234TreeNode<T>) node.Parent;
            _root = node.Values.Count > 0 ? node : null;
            return true;
        }
    }
}

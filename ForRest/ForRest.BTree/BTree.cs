﻿using ForRest.Provider.BLL;
using System.Collections.Generic;

namespace ForRest.BTree
{
    public class BTree<T> : Tree<T>
    {
        private BTreeNode<T> _root;
        private IComparer<T> _comparer = Comparer<T>.Default;
        private int _M;

        public BTree(int degree)
        {
            _root = null;
            _M = degree;
        }

        public int M
        {
            get { return _M; }
        }

        public override Node<T> Root
        {
            get { return _root; }
            set { _root = (BTreeNode<T>) value; }
        }

        public override void Clear()
        {
            _root = null;
        }

        public override List<int> Contains(T data)
        {
            List<int> path = new List<int>();
            BTreeNode<T> current = _root;
            while (current != null)
            {
                for (int i = 0; i < current.Values.Count; i++)
                {
                    int result = _comparer.Compare(current.Values[i], data);
                    if (result == 0)
                        return path;
                    else if (result > 0)
                    {
                        if (current.Neighbors == null)
                            return null;
                        current = (BTreeNode<T>)current.Neighbors[i];
                        path.Add(i);
                    }
                    else
                    {
                        if (i + 1 == current.Values.Count)
                        {
                            if (current.Neighbors == null)
                                return null;
                            current = (BTreeNode<T>)current.Neighbors[i + 1];
                            path.Add(i + 1);
                        }
                    }
                }
            }
            return null;
        }

        private BTreeNode<T> Insert(BTreeNode<T> node, T data)
        {
            if (!node.isLeaf)
            {
                // Look for child to go to
                for (int i = 0; i < node.Values.Count; i++)
                {
                    int result = _comparer.Compare(node.Values[i], data);
                    if (result > 0)
                        return Insert(node.ChildAt(i), data);
                    else
                        if (i + 1 == node.Values.Count)
                            return Insert(node.ChildAt(i + 1), data);
                }
                return null;
            }
            else
            {
                if (!node.IsFull)
                    return node.Add(data);
                else
                {
                    //return node.Split(data);
                    if (node == _root)
                    {
                        _root = node.Split(data);
                        return _root;
                    }
                    else
                    {
                        node.Parent = node.Split(data);
                        return node.Parent;
                    }
                }
            }
        }

        public override void Add(T data)
        {
            if (_root == null)
            {
                List<T> dataList = new List<T>();
                dataList.Add(data);
                BTreeNode<T> node = new BTreeNode<T>(_M, null, dataList);
                _root = node;
            }
            else
            {
                BTreeNode<T> node = Insert(_root, data);
                while (node.Parent != null)
                    node = node.Parent;
                _root = node;
            }
        }

        private bool Delete(BTreeNode<T> node, T data)
        {
            while (node != null)
            {
                for (int i = 0; i < node.Values.Count; i++)
                {
                    int result = _comparer.Compare(node.Values[i], data);
                    if (result == 0)
                        return node.Delete(data, i);
                    else if (result > 0)
                    {
                        if (node.Neighbors == null)
                            return false;
                        node = (BTreeNode<T>)node.Neighbors[i];
                        return Delete(node, data);
                    }
                    else
                    {
                        if (i + 1 == node.Values.Count)
                        {
                            if (node.Neighbors == null)
                                return false;
                            node = (BTreeNode<T>)node.Neighbors[i + 1];
                            return Delete(node, data);
                        }
                    }
                }
            }
            return false;
        }

        public override bool Remove(T data)
        {
            List<int> path = new List<int>();
            if (_root == null)
                return false;
            else
                return Delete(_root, data);
        }
    }
}
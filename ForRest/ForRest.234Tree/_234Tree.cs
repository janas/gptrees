using System.Collections.Generic;
using ForRest.Provider.BLL;

namespace ForRest._234Tree
{
    public class _234Tree<T> : Tree<T>
    {
        private _234TreeNode<T> _root;
        private readonly IComparer<T> _comparer = Comparer<T>.Default;

        private void UpdateNodesInfo()
        {
            if (_root != null)
                _root.UpdateNodesInfo();
        }

        public _234Tree()
        {
            _root = null;
            UpdateNodesInfo();
        }

        public override Node<T> Root
        {
            get { return _root; }
            set { _root = (_234TreeNode<T>) value; }
        }

        public override void Clear()
        {
            _root = null;
            UpdateNodesInfo();
        }

        public override List<int> Contains(T data)
        {
            List<int> path = new List<int>();
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
                    else
                    {
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
            }
            return null;
        }

        private _234TreeNode<T> Insert(_234TreeNode<T> node, T data)
        {
            if (!node.isLeaf)
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
            return node.Parent;
        }

        public override void Add(T data)
        {
            if (_root == null)
            {
                List<T> dataList = new List<T>();
                dataList.Add(data);
                _234TreeNode<T> node = new _234TreeNode<T>(null, dataList);
                _root = node;
            }
            else
            {
                _234TreeNode<T> node = Insert(_root, data);
                while (node.Parent != null)
                    node = node.Parent;
                _root = node;
            }
            UpdateNodesInfo();
        }

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
                        return Delete((_234TreeNode<T>)node.Neighbors[i], data);
                    }
                    else
                    {
                        if (i + 1 == node.Values.Count)
                        {
                            if (node.Neighbors == null)
                                return null;
                            return Delete((_234TreeNode<T>)node.Neighbors[i + 1], data);
                        }
                    }
                }
            }
            return null;
        }

        public override bool Remove(T data)
        {
            _234TreeNode<T> node = Delete(_root, data);
            if (node == null)
                return false;
            else
            {
                while (node.Parent != null)
                    node = node.Parent;
                if (node.Values.Count > 0)
                    _root = node;
                else
                    _root = null;
                UpdateNodesInfo();
                return true;
            }
        }
    }
}

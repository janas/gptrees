using System.Collections.Generic;
using ForRest.Provider.BLL;

namespace ForRest._23Tree
{
    public class _23Tree<T> : Tree<T>
    {
        private _23TreeNode<T> _root;
        private readonly IComparer<T> _comparer = Comparer<T>.Default;

        public _23Tree()
        {
            _root = null;
        }

        public override string TreeType
        {
            get { return "2-3 Tree"; }
        }

        public override Node<T> Root
        {
            get { return _root; }
            set { _root = (_23TreeNode<T>) value; }
        }

        public override void Clear()
        {
            _root = null;
        }

        public override List<int> Contains(T data)
        {
            var path = new List<int>();
            _23TreeNode<T> current = _root;
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
                        current = (_23TreeNode<T>) current.Neighbors[i];
                        path.Add(i);
                        break;
                    }
                    if (i + 1 == current.Values.Count)
                    {
                        if (current.Neighbors == null)
                            return null;
                        current = (_23TreeNode<T>) current.Neighbors[i + 1];
                        path.Add(i + 1);
                        break;
                    }
                }
            }
            return null;
        }

        private _23TreeNode<T> Insert(_23TreeNode<T> node, T data)
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
            return (_23TreeNode<T>) node.Parent;
        }

        public override void Add(T data)
        {
            if (_root == null)
            {
                var dataList = new List<T> {data};
                var node = new _23TreeNode<T>(null, dataList);
                _root = node;
            }
            else
            {
                _23TreeNode<T> node = Insert(_root, data);
                while (node.Parent != null)
                    node = (_23TreeNode<T>) node.Parent;
                _root = node;
            }
        }

        private _23TreeNode<T> Delete(_23TreeNode<T> node, T data)
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
                        return Delete((_23TreeNode<T>) node.Neighbors[i], data);
                    }
                    if (i + 1 == node.Values.Count)
                    {
                        if (node.Neighbors == null)
                            return null;
                        return Delete((_23TreeNode<T>) node.Neighbors[i + 1], data);
                    }
                }
            }
            return null;
        }

        public override bool Remove(T data)
        {
            _23TreeNode<T> node = Delete(_root, data);
            if (node == null)
                return false;
            while (node.Parent != null)
                node = (_23TreeNode<T>) node.Parent;
            if (node.Values.Count > 0)
                _root = node;
            else
                _root = null;
            return true;
        }
    }
}

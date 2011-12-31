using ForRest.Provider.BLL;
using System.Collections.Generic;

namespace ForRest.BPlusTree
{
    public class BPlusTree<T> : Tree<T>
    {
        private BPlusTreeNode<T> _root;
        private readonly IComparer<T> _comparer = Comparer<T>.Default;
        private readonly int _m;

        private void UpdateNodesInfo()
        {
            if (_root != null)
                _root.UpdateNodesInfo();
        }

        public BPlusTree(int degree)
        {
            _root = null;
            _m = degree;
            UpdateNodesInfo();
        }

        public int M
        {
            get { return _m; }
        }

        public override Node<T> Root
        {
            get { return _root; }
            set { _root = (BPlusTreeNode<T>) value; }
        }

        public override void Clear()
        {
            _root = null;
            UpdateNodesInfo();
        }

        public override List<int> Contains(T data)
        {
            List<int> path = new List<int>();
            BPlusTreeNode<T> current = _root;
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
                        current = (BPlusTreeNode<T>) current.Neighbors[i];
                        path.Add(i);
                        break;
                    }
                    else
                    {
                        if (i + 1 == current.Values.Count)
                        {
                            if (current.Neighbors == null)
                                return null;
                            current = (BPlusTreeNode<T>) current.Neighbors[i + 1];
                            path.Add(i + 1);
                            break;
                        }
                    }
                }
            }
            return null;
        }

        private BPlusTreeNode<T> Insert(BPlusTreeNode<T> node, T data)
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
                BPlusTreeNode<T> node = new BPlusTreeNode<T>(_m, null, dataList);
                _root = node;
            }
            else
            {
                BPlusTreeNode<T> node = Insert(_root, data);
                while (node.Parent != null)
                    node = node.Parent;
                _root = node;
            }
            UpdateNodesInfo();
        }

        private BPlusTreeNode<T> Delete(BPlusTreeNode<T> node, T data)
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
                        return Delete((BPlusTreeNode<T>)node.Neighbors[i], data);
                    }
                    else
                    {
                        if (i + 1 == node.Values.Count)
                        {
                            if (node.Neighbors == null)
                                return null;
                            return Delete((BPlusTreeNode<T>)node.Neighbors[i + 1], data);
                        }
                    }
                }
            }
            return null;
        }

        public override bool Remove(T data)
        {
            BPlusTreeNode<T> node = Delete(_root, data);
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

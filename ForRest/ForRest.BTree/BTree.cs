using ForRest.Provider.BLL;
using System.Collections.Generic;

namespace ForRest.BTree
{
    public class BTree<T> : Tree<T>
    {
        private BTreeNode<T> _root;
        private readonly IComparer<T> _comparer = Comparer<T>.Default;
        private readonly int _m;

        /*
        private void UpdateNodesInfo()
        {
            if (_root != null)
                _root.UpdateNodesInfo();
        }*/

        public BTree(int degree)
        {
            _root = null;
            _m = degree;
            //UpdateNodesInfo();
        }

        public int M
        {
            get { return _m; }
        }

        public override Node<T> Root
        {
            get { return _root; }
            set { _root = (BTreeNode<T>) value; }
        }

        public override void Clear()
        {
            _root = null;
            //UpdateNodesInfo();
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
                    if (result > 0)
                    {
                        if (current.Neighbors == null)
                            return null;
                        current = (BTreeNode<T>) current.Neighbors[i];
                        path.Add(i);
                        break;
                    }
                    else
                    {
                        if (i + 1 == current.Values.Count)
                        {
                            if (current.Neighbors == null)
                                return null;
                            current = (BTreeNode<T>) current.Neighbors[i + 1];
                            path.Add(i + 1);
                            break;
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
                    if (i + 1 == node.Values.Count)
                        return Insert(node.ChildAt(i + 1), data);
                }
                return null;
            }
            if (!node.IsFull)
            {
                return node.Add(data);
            }
            if (node == _root)
            {
                _root = node.Split(data);
                return _root;
            }
            node.Parent = node.Split(data);
            return (BTreeNode<T>)node.Parent;
        }

        public override void Add(T data)
        {
            if (_root == null)
            {
                List<T> dataList = new List<T>();
                dataList.Add(data);
                BTreeNode<T> node = new BTreeNode<T>(_m, null, dataList);
                _root = node;
            }
            else
            {
                BTreeNode<T> node = Insert(_root, data);
                while (node.Parent != null)
                    node = (BTreeNode<T>)node.Parent;
                _root = node;
            }
            //UpdateNodesInfo();
        }

        private BTreeNode<T> Delete(BTreeNode<T> node, T data)
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
                        return Delete((BTreeNode<T>)node.Neighbors[i], data);
                    }
                    else
                    {
                        if (i + 1 == node.Values.Count)
                        {
                            if (node.Neighbors == null)
                                return null;
                            return Delete((BTreeNode<T>)node.Neighbors[i + 1], data);
                        }
                    }
                }
            }
            return null;
        }

        public override bool Remove(T data)
        {
            BTreeNode<T> node = Delete(_root, data);
            if (node == null)
                return false;
            else
            {
                while (node.Parent != null)
                    node = (BTreeNode<T>)node.Parent;
                if (node.Values.Count > 0)
                    _root = node;
                else
                    _root = null;
                //UpdateNodesInfo();
                return true;
            }
        }
    }
}
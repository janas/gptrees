using ForRest.Provider.BLL;
using System.Collections.Generic;

namespace ForRest.BST
{
    public class BinarySearchTree<T> : Tree<T>
    {
        private BinarySearchTreeNode<T> _root;
        private int _count;
        private IComparer<T> _comparer = Comparer<T>.Default; 

        public BinarySearchTree()
        {
            _root = null;
            _count = 0;
        }

        public BinarySearchTreeNode<T> Root
        {
            get { return _root; }
        }

        public int Count
        {
            get { return _count; }
        }

        public override void Clear()
        {
            _root = null;
            _count = 0;
        }

        public override List<int> Contains(T data)
        {
            List<int> path = new List<int>();
            BinarySearchTreeNode<T> current = _root;
            while (current != null)
            {
                int result = _comparer.Compare(current.Values[0], data);
                if (result == 0)
                    return path;
                else if (result > 0)
                {
                    current = current.Left;
                    path.Add(0);
                }
                else
                {
                    current = current.Right;
                    path.Add(1);
                }
            }
            return null;
        }

        public override void Add(T data)
        {
            List<T> dataList = new List<T>(1);
            dataList.Add(data);
            BinarySearchTreeNode<T> node = new BinarySearchTreeNode<T>(dataList);
            BinarySearchTreeNode<T> current = _root, parent = null;
            int result;
            while (current != null)
            {
                result = _comparer.Compare(current.Values[0], data);
                if (result == 0)
                    return;
                else if (result > 0)
                {
                    parent = current;
                    current = current.Left;
                }
                else if (result < 0)
                {
                    parent = current;
                    current = current.Right;
                }
            }
            _count++;
            if (parent == null)
                _root = node;
            else
            {
                result = _comparer.Compare(parent.Values[0], data);
                if (result > 0)
                    parent.Left = node;
                else
                    parent.Right = node;
            }
        }

        public override bool Remove(T data)
        {
            if (_root == null)
                return false;
            BinarySearchTreeNode<T> current = _root, parent = null;
            int result = _comparer.Compare(current.Values[0], data);
            while (result !=0 )
            {
                if (result > 0)
                {
                    parent = current;
                    current = current.Left;
                }
                else if (result < 0)
                {
                    parent = current;
                    current = current.Right;
                }
                if (current == null)
                    return false;
                result = _comparer.Compare(current.Values[0], data);
            }
            _count--;

            if (current.Right == null)
            {
                if (parent == null)
                    _root = parent.Left;
                else
                {
                    result = _comparer.Compare(parent.Values[0], current.Values[0]);
                    if (result > 0)
                        parent.Left = current.Left;
                    else if (result < 0)
                        parent.Right = current.Right;
                }
            }
            else if (current.Right.Left == null)
            {
                current.Right.Left = current.Left;
                if (parent == null)
                    _root = current.Right;
                else
                {
                    result = _comparer.Compare(parent.Values[0], current.Values[0]);
                    if (result > 0)
                        parent.Left = current.Right;
                    else if (result < 0)
                        parent.Right = current.Right;
                }
            }
            else
            {
                BinarySearchTreeNode<T> leftmost = current.Right.Left, lmParent = current.Right;
                while (leftmost.Left != null)
                {
                    lmParent = leftmost;
                    leftmost = lmParent.Left;
                }
                lmParent.Left = leftmost.Right;
                leftmost.Left = current.Left;
                leftmost.Right = current.Right;
                if (parent == null)
                    _root = leftmost;
                else
                {
                    result = _comparer.Compare(parent.Values[0], current.Values[0]);
                    if (result > 0)
                        parent.Left = leftmost;
                    else if(result < 0)
                        parent.Right = leftmost;
                }
            }
            current.Left = current.Right = null;
            return true;
        }
    }
}

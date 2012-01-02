using ForRest.Provider.BLL;
using System.Collections.Generic;

namespace ForRest.RedBlackTree
{
    public class RedBlackTree<T> : Tree<T>
    {
        private RedBlackTreeNode<T> _root;
        private int _count;
        private readonly IComparer<T> _comparer = Comparer<T>.Default;

        public RedBlackTree()
        {
            _root = null;
            _count = 0;
        }

        public override Node<T> Root
        {
            get { return _root; }
            set { _root = (RedBlackTreeNode<T>) value; }
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
            RedBlackTreeNode<T> current = _root;
            while (current != null)
            {
                int result = _comparer.Compare(current.Values[0], data);
                if (result == 0)
                    return path;
                if (result > 0)
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
            var dataList = new List<T>(1) {data};
            var node = new RedBlackTreeNode<T>(dataList);
            RedBlackTreeNode<T> current = _root, parent = null;
            int result;
            while (current != null)
            {
                result = _comparer.Compare(current.Values[0], data);
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
            node.Parent = parent;
            while (_root.Balance())
                ;
            while (_root.Parent != null)
                _root = (RedBlackTreeNode<T>)_root.Parent;
        }

        public override bool Remove(T data)
        {
            if (_root == null)
                return false;
            RedBlackTreeNode<T> current = _root, parent = null;
            int result = _comparer.Compare(current.Values[0], data);
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
                    return false;
                result = _comparer.Compare(current.Values[0], data);
            }
            _count--;

            if (current.Right == null)
            {
                if (parent == null)
                {
                    if (current.Left != null)
                        current.Left.Parent = parent;
                    _root = current.Left;
                }
                else
                {
                    result = _comparer.Compare(parent.Values[0], current.Values[0]);
                    if (result > 0)
                    {
                        if (current.Left != null)
                            current.Left.Parent = parent;
                        parent.Left = current.Left;
                    }
                    else
                    {
                        if (current.Right != null)
                            current.Right.Parent = parent;
                        parent.Right = current.Right;
                    }
                }
            }
            else if (current.Right.Left == null)
            {
                current.Right.Left = current.Left;
                if (current.Left != null)
                    current.Left.Parent = current.Right;
                if (parent == null)
                {
                    if (current.Right != null)
                        current.Right.Parent = parent;
                    _root = current.Right;
                }
                else
                {
                    result = _comparer.Compare(parent.Values[0], current.Values[0]);
                    if (result > 0)
                    {
                        if (current.Right != null)
                            current.Right.Parent = parent;
                        parent.Left = current.Right;
                    }
                    else
                    {
                        if (current.Right != null)
                            current.Right.Parent = parent;
                        parent.Right = current.Right;
                    }
                }
            }
            else
            {
                RedBlackTreeNode<T> leftMost = current.Right, lmParent = current;
                //RedBlackTreeNode<T> leftMost = current.Right.Left, lmParent = current.Right;
                while (leftMost.Left != null)
                {
                    lmParent = leftMost;
                    leftMost = lmParent.Left;
                }
                lmParent.Left = leftMost.Right;
                leftMost.Left = current.Left;
                leftMost.Right = current.Right;
                if (lmParent.Left != null)
                    lmParent.Left.Parent = leftMost;
                if (leftMost.Right != null)
                    leftMost.Right.Parent = lmParent;
                if (current.Left != null)
                    current.Left.Parent = leftMost;
                if (current.Right != null)
                    current.Right.Parent = leftMost;
                if (parent == null)
                {
                    leftMost.Parent = parent;
                    _root = leftMost;
                }
                else
                {
                    result = _comparer.Compare(parent.Values[0], current.Values[0]);
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
            current.Parent = current.Left = current.Right = null;
            while (_root.Balance())
                ;
            return true;
        }
    }
}

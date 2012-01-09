using ForRest.Provider.BLL;
using System.Collections.Generic;

namespace ForRest.BST
{
    /// <summary>
    /// BST tree class implementing Tree<T>.
    /// </summary>
    public class BinarySearchTree<T> : Tree<T>
    {
        private BinarySearchTreeNode<T> _root;
        private readonly IComparer<T> _comparer = Comparer<T>.Default;

        /// <summary>
        /// Basic constructor.
        /// </summary>
        public BinarySearchTree()
        {
            _root = null;
        }

        /// <summary>
        /// Gets type of the tree.
        /// </summary>
        public override string TreeType
        {
            get { return "Binary Search Tree"; }
        }

        /// <summary>
        /// Gets root of the tree.
        /// </summary>
        public override Node<T> Root
        {
            get { return _root; }
            set { _root = (BinarySearchTreeNode<T>) value; }
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
            BinarySearchTreeNode<T> current = _root;
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

        /// <summary>
        /// Adds element to the tree.
        /// </summary>
        /// <param name="data">Element to be added.</param>
        public override void Add(T data)
        {
            var dataList = new List<T>(1) {data};
            var node = new BinarySearchTreeNode<T>(dataList);
            BinarySearchTreeNode<T> current = _root, parent = null;
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
        }

        /// <summary>
        /// Removes element from the tree.
        /// </summary>
        /// <param name="data">Element to be removed.</param>
        /// <returns></returns>
        public override bool Remove(T data)
        {
            if (_root == null)
                return false;
            BinarySearchTreeNode<T> current = _root, parent = null;
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
                BinarySearchTreeNode<T> leftMost = current.Right, lmParent = current;
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
            return true;
        }
    }
}

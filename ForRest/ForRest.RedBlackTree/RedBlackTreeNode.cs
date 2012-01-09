// -----------------------------------------------------------------------
// <copyright file="RedBlackTreeNode.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using ForRest.Provider.BLL;
using System.Collections.Generic;
using System.Drawing;

namespace ForRest.RedBlackTree
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class RedBlackTreeNode<T> : Node<T>
    {
        private RedBlackTreeNode<T> _parent;
        private int _maxHeight;
        private int _minHeight;

        public override Node<T> Parent
        {
            get { return _parent; }
            set { _parent = (RedBlackTreeNode<T>)value; }
        }

        public int MaxHeight
        {
            get { return _maxHeight; }
        }

        public int MinHeight
        {
            get { return _minHeight; }
        }

        public int UpdateMaxHeight()
        {
            int leftHeight = 0;
            int rightHeight = 0;
            if (Left != null)
                leftHeight = Left.UpdateMaxHeight();
            if (Right != null)
                rightHeight = Right.UpdateMaxHeight();
            if (rightHeight > leftHeight)
                _maxHeight = rightHeight + 1;
            else
                _maxHeight = leftHeight + 1;
            return _maxHeight;
        }

        public int UpdateMinHeight()
        {
            int leftHeight = 0;
            int rightHeight = 0;
            if (Left != null)
                leftHeight = Left.UpdateMinHeight();
            if (Right != null)
                rightHeight = Right.UpdateMinHeight();
            if (rightHeight > leftHeight)
                _minHeight = leftHeight + 1;
            else
                _minHeight = rightHeight + 1;
            return _minHeight;
        }

        public bool Balance()
        {
            if (Left != null)
                Left.Balance();
            if (Right != null)
                Right.Balance();
            UpdateMaxHeight();
            UpdateMinHeight();
            int maxLeftHeight = 0;
            int maxRightHeight = 0;
            int minLeftHeight = 0;
            int minRightHeight = 0;
            if (Left != null)
            {
                maxLeftHeight = Left.MaxHeight;
                minLeftHeight = Left.MinHeight;
            }
            if (Right != null)
            {
                maxRightHeight = Right.MaxHeight;
                minRightHeight = Right.MinHeight;
            }
            int leftMinusRight = maxLeftHeight - minRightHeight - 1;
            if (leftMinusRight > 0)
            {
                //for (int i = 0; i < leftMinusRight; i++)
                {
                    // right rotation
                    RedBlackTreeNode<T> z = this;
                    RedBlackTreeNode<T> y = Left;
                    RedBlackTreeNode<T> b = Left.Right;
                    bool zIsleftNode = z.Parent != null && ((RedBlackTreeNode<T>)z.Parent).Left == this;
                    z.Left = b;
                    if (b != null)
                        b.Parent = z;
                    y.Right = z;
                    if (y != null)
                    {
                        y.Parent = z.Parent;
                        if (y.Parent != null)
                        {
                            if (zIsleftNode)
                                ((RedBlackTreeNode<T>)y.Parent).Left = y;
                            else
                                ((RedBlackTreeNode<T>)y.Parent).Right = y;
                        }
                    }
                    z.Parent = y;
                }
                return true;
            }
            int rightMinusLeft = maxRightHeight - minLeftHeight - 1;
            if (rightMinusLeft > 0)
            {
                //for (int j = 0; j < rightMinusLeft; j++)
                {
                    // left rotation
                    RedBlackTreeNode<T> z = this;
                    RedBlackTreeNode<T> y = Right;
                    RedBlackTreeNode<T> b = Right.Left;
                    bool zIsleftNode = z.Parent != null && ((RedBlackTreeNode<T>)z.Parent).Left == this;
                    z.Right = b;
                    if (b != null)
                        b.Parent = z;
                    y.Left = z;
                    if (y != null)
                    {
                        y.Parent = z.Parent;
                        if (y.Parent != null)
                        {
                            if (zIsleftNode)
                                ((RedBlackTreeNode<T>)y.Parent).Left = y;
                            else
                                ((RedBlackTreeNode<T>)y.Parent).Right = y;
                        }
                    }
                    z.Parent = y;
                }
                return true;
            }
            return false;
        }

        public bool IsRed
        {
            get
            {
                if (_parent == null)
                    return false;
                if (_parent.IsRed)
                    return false;
                return true;
            }
        }

        public override string NodeInfo
        {
            get
            {
                if (IsRed)
                    return "<Red>";
                return "<Black>";
            }
        }

        public override Color NodeColor
        {
            get
            {
                if (IsRed)
                    return Color.Red;
                return Color.Black;
            }
        }

        public RedBlackTreeNode()
        {
            _maxHeight = 1;
            _maxHeight = 0;
        }

        public RedBlackTreeNode(List<T> data)
            : base(data, null)
        {
            _maxHeight = 1;
            _maxHeight = 0;
        }

        public RedBlackTreeNode(List<T> data, RedBlackTreeNode<T> left, RedBlackTreeNode<T> right)
        {
            Values = data;
            var children = new NodeList<T>(2);
            children[0] = left;
            children[1] = right;
            Neighbors = children;
            UpdateMaxHeight();
            UpdateMinHeight();
        }

        public RedBlackTreeNode<T> Left
        {
            get
            {
                if (Neighbors == null)
                    return null;
                return (RedBlackTreeNode<T>)Neighbors[0];
            }
            set
            {
                if (Neighbors == null)
                    Neighbors = new NodeList<T>(2);
                Neighbors[0] = value;
            }
        }

        public RedBlackTreeNode<T> Right
        {
            get
            {
                if (Neighbors == null)
                    return null;
                return (RedBlackTreeNode<T>)Neighbors[1];
            }
            set
            {
                if (Neighbors == null)
                    Neighbors = new NodeList<T>(2);
                Neighbors[1] = value;
            }
        }
    }
}

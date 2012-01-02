// -----------------------------------------------------------------------
// <copyright file="AVLTreeNode.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using ForRest.Provider.BLL;
using System.Collections.Generic;

namespace ForRest.AVLTree
{

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class AVLTreeNode<T> : Node<T>
    {
        private AVLTreeNode<T> _parent;
        private int _height;

        public override Node<T> Parent
        {
            get { return _parent; }
            set { _parent = (AVLTreeNode<T>) value; }
        }

        public int Height
        {
            get { return _height; }
        }

        public int UpdateHeight()
        {
            int leftHeight = 0;
            int rightHeight = 0;
            if (Left != null)
                leftHeight = Left.UpdateHeight();
            if (Right != null)
                rightHeight = Right.UpdateHeight();
            if (rightHeight > leftHeight)
                _height = rightHeight + 1;
            else
                _height = leftHeight + 1;
            return _height;
        }

        public bool Balance()
        {
            UpdateHeight();
            int leftHeight = 0;
            int rightHeight = 0;
            if (Left != null)
                leftHeight = Left.Height;
            if (Right != null)
                rightHeight = Right.Height;
            if (leftHeight - rightHeight > 1)
            {
                // right rotation
                AVLTreeNode<T> z = this;
                AVLTreeNode<T> y = Left;
                AVLTreeNode<T> b = Left.Right;
                bool zIsleftNode = z.Parent != null && ((AVLTreeNode<T>) z.Parent).Left == this;
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
                            ((AVLTreeNode<T>) y.Parent).Left = y;
                        else
                            ((AVLTreeNode<T>) y.Parent).Right = y;
                    }
                }
                z.Parent = y;
                return true;
            }
            if (rightHeight - leftHeight > 1)
            {
                // left rotation
                AVLTreeNode<T> z = this;
                AVLTreeNode<T> y = Right;
                AVLTreeNode<T> b = Right.Left;
                bool zIsleftNode = z.Parent != null && ((AVLTreeNode<T>) z.Parent).Left == this;
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
                            ((AVLTreeNode<T>) y.Parent).Left = y;
                        else
                            ((AVLTreeNode<T>) y.Parent).Right = y;
                    }
                }
                z.Parent = y;
                return true;
            }
            bool balanced = Left != null && Left.Balance();
            if (Right != null && Right.Balance())
                balanced = true;
            return balanced;
        }

        public override string NodeInfo
        {
            get
            {
                string result = "h=" + _height + " ";
                if (_parent == null)
                    return result;
                result += "<";
                for (int i = 0; i < _parent.Values.Count; i++)
                    result += _parent.Values[i].ToString();
                result += "> ";
                return result;
            }
        }

        public AVLTreeNode()
        {
            _height = 1;
        }

        public AVLTreeNode(List<T> data)
            : base(data, null)
        {
            _height = 1;
        }

        public AVLTreeNode(List<T> data, AVLTreeNode<T> left, AVLTreeNode<T> right)
        {
            Values = data;
            var children = new NodeList<T>(2);
            children[0] = left;
            children[1] = right;
            Neighbors = children;
            if (left.Height > right.Height)
                _height = left.Height + 1;
            else
                _height = right.Height + 1;
        }

        public AVLTreeNode<T> Left
        {
            get
            {
                if (Neighbors == null)
                    return null;
                return (AVLTreeNode<T>) Neighbors[0];
            }
            set
            {
                if (Neighbors == null)
                    Neighbors = new NodeList<T>(2);
                Neighbors[0] = value;
            }
        }

        public AVLTreeNode<T> Right
        {
            get
            {
                if (Neighbors == null)
                    return null;
                return (AVLTreeNode<T>) Neighbors[1];
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

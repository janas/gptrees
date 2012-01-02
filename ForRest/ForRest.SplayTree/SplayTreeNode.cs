// -----------------------------------------------------------------------
// <copyright file="SplayTreeNode.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using ForRest.Provider.BLL;
using System.Collections.Generic;

namespace ForRest.SplayTree
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SplayTreeNode<T> : Node<T>
    {
        private SplayTreeNode<T> _parent;
        private int _height;

        public override Node<T> Parent
        {
            get { return _parent; }
            set { _parent = (SplayTreeNode<T>)value; }
        }

        public int Height {
            get
            { return _height; }
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
                SplayTreeNode<T> z = this;
                SplayTreeNode<T> y = Left;
                SplayTreeNode<T> a = Left.Left;
                SplayTreeNode<T> b = Left.Right;
                SplayTreeNode<T> c = Right;
                bool zIsleftNode = false;
                if (z.Parent!=null && ((SplayTreeNode<T>)z.Parent).Left == this)
                    zIsleftNode = true;
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
                            ((SplayTreeNode<T>)y.Parent).Left = y;
                        else
                            ((SplayTreeNode<T>)y.Parent).Right = y;
                    }
                }
                z.Parent = y;
                return true;
            }
            else if (rightHeight - leftHeight > 1)
            {
                // left rotation
                SplayTreeNode<T> z = this;
                SplayTreeNode<T> y = Right;
                SplayTreeNode<T> a = Left;
                SplayTreeNode<T> b = Right.Left;
                SplayTreeNode<T> c = Right.Right;
                bool zIsleftNode = false;
                if (z.Parent != null && ((SplayTreeNode<T>)z.Parent).Left == this)
                    zIsleftNode = true;
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
                            ((SplayTreeNode<T>)y.Parent).Left = y;
                        else
                            ((SplayTreeNode<T>)y.Parent).Right = y;
                    }
                }
                z.Parent = y;
                return true;
            }
            bool balanced = false;
            if (Left != null && Left.Balance())
                balanced = true;
            if (Right != null && Right.Balance())
                balanced = true;
            return balanced;
        }

        public override string NodeInfo
        {
            get
            {
                string result = "h=" + _height.ToString() + " ";
                if (_parent == null)
                    return result;
                result += "<";
                for (int i = 0; i < _parent.Values.Count; i++)
                    result += _parent.Values[i].ToString();
                result += "> ";
                return result;
            }
        }

        public SplayTreeNode()
        {
            _height = 1;
        }

        public SplayTreeNode(List<T> data) : base(data, null)
        {
            _height = 1;
        }

        public SplayTreeNode(List<T> data, SplayTreeNode<T> left, SplayTreeNode<T> right)
        {
            Values = data;
            NodeList<T> children = new NodeList<T>(2);
            children[0] = left;
            children[1] = right;
            Neighbors = children;
            if (left.Height > right.Height)
                _height = left.Height + 1;
            else
                _height = right.Height + 1;
        }

        public SplayTreeNode<T> Left
        {
            get
            {
                if (Neighbors == null)
                    return null;
                return (SplayTreeNode<T>) Neighbors[0];
            }
            set
            {
                if (Neighbors == null)
                    Neighbors = new NodeList<T>(2);
                Neighbors[0] = value;
            }
        }
        public SplayTreeNode<T> Right
        {
            get
            {
                if (Neighbors == null)
                    return null;
                return (SplayTreeNode<T>) Neighbors[1];
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

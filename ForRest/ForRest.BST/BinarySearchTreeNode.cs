// -----------------------------------------------------------------------
// <copyright file="BinarySearchTreeNode.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using ForRest.Provider.BLL;
using System.Collections.Generic;

namespace ForRest.BST
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class BinarySearchTreeNode<T> : Node<T>
    {
        private BinarySearchTreeNode<T> _parent;

        public override Node<T> Parent
        {
            get { return _parent; }
            set { _parent = (BinarySearchTreeNode<T>)value; }
        }

        public override string NodeInfo
        {
            get
            {
                if (_parent == null )
                    return "";
                string result = "<";
                for (int i = 0; i < _parent.Values.Count; i++)
                    result += _parent.Values[i].ToString();
                result += "> ";
                return result;
            }
        }

        public BinarySearchTreeNode()
        {
        }

        public BinarySearchTreeNode(List<T> data) : base(data, null)
        {
        }

        public BinarySearchTreeNode(List<T> data, BinarySearchTreeNode<T> left, BinarySearchTreeNode<T> right)
        {
            Values = data;
            NodeList<T> children = new NodeList<T>(2);
            children[0] = left;
            children[1] = right;
            Neighbors = children;
        }

        public BinarySearchTreeNode<T> Left
        {
            get
            {
                if (Neighbors == null)
                    return null;
                return (BinarySearchTreeNode<T>) Neighbors[0];
            }
            set
            {
                if (Neighbors == null)
                    Neighbors = new NodeList<T>(2);
                Neighbors[0] = value;
            }
        }
        public BinarySearchTreeNode<T> Right
        {
            get
            {
                if (Neighbors == null)
                    return null;
                return (BinarySearchTreeNode<T>) Neighbors[1];
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

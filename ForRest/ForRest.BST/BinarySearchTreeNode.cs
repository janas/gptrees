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

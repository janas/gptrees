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
        public AVLTreeNode()
        {
        }

        public AVLTreeNode(List<T> data) : base(data, null)
        {
        }

        public AVLTreeNode(List<T> data, AVLTreeNode<T> left, AVLTreeNode<T> right)
        {
            Values = data;
            NodeList<T> children = new NodeList<T>(2);
            children[0] = left;
            children[1] = right;
            Neighbors = children;
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

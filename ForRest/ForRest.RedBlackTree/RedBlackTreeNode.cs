// -----------------------------------------------------------------------
// <copyright file="RedBlackTreeNode.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using ForRest.Provider.BLL;
using System.Collections.Generic;

namespace ForRest.RedBlackTree
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class RedBlackTreeNode<T> : Node<T>
    {
        public RedBlackTreeNode()
        {
        }

        public RedBlackTreeNode(List<T> data) : base(data, null)
        {
        }

        public RedBlackTreeNode(List<T> data, RedBlackTreeNode<T> left, RedBlackTreeNode<T> right)
        {
            Values = data;
            NodeList<T> children = new NodeList<T>(2);
            children[0] = left;
            children[1] = right;
            Neighbors = children;
        }

        public RedBlackTreeNode<T> Left
        {
            get
            {
                if (Neighbors == null)
                    return null;
                return (RedBlackTreeNode<T>) Neighbors[0];
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
                return (RedBlackTreeNode<T>) Neighbors[1];
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

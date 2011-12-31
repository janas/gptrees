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
        public SplayTreeNode()
        {
        }

        public SplayTreeNode(List<T> data) : base(data, null)
        {
        }

        public SplayTreeNode(List<T> data, SplayTreeNode<T> left, SplayTreeNode<T> right)
        {
            Values = data;
            NodeList<T> children = new NodeList<T>(2);
            children[0] = left;
            children[1] = right;
            Neighbors = children;
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

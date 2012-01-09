using ForRest.Provider.BLL;
using System.Collections.Generic;

namespace ForRest.BST
{
    /// <summary>
    /// BST tree node class implementing Node<T>.
    /// </summary>
    public class BinarySearchTreeNode<T> : Node<T>
    {
        private BinarySearchTreeNode<T> _parent;

        /// <summary>
        /// Gets parent of the node.
        /// </summary>
        public override Node<T> Parent
        {
            get { return _parent; }
            set { _parent = (BinarySearchTreeNode<T>) value; }
        }

        /// <summary>
        /// Gets node info.
        /// </summary>
        public override string NodeInfo
        {
            get
            {
                if (_parent == null)
                    return "";
                string result = "<";
                for (int i = 0; i < _parent.Values.Count; i++)
                    result += _parent.Values[i].ToString();
                result += "> ";
                return result;
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public BinarySearchTreeNode()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="data">Values for the node.</param>
        public BinarySearchTreeNode(List<T> data)
            : base(data, null)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="data">Values for the node.</param>
        /// <param name="left">Left child node.</param>
        /// <param name="right">Right child node.</param>
        public BinarySearchTreeNode(List<T> data, BinarySearchTreeNode<T> left, BinarySearchTreeNode<T> right)
        {
            Values = data;
            var children = new NodeList<T>(2);
            children[0] = left;
            children[1] = right;
            Neighbors = children;
        }

        /// <summary>
        /// Gets left child node.
        /// </summary>
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

        /// <summary>
        /// Gets right child node.
        /// </summary>
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

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BPlusTreeNode.cs" company="Warsaw University of Technology">
//   
// </copyright>
// <summary>
//   The b plus tree node.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForRest.BPlusTree
{
    using System;
    using System.Collections.Generic;

    using ForRest.Provider.BLL;

    /// <summary>
    /// The b plus tree node.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class BPlusTreeNode<T> : Node<T>
    {
        #region Constants and Fields

        /// <summary>
        /// The _comparer.
        /// </summary>
        private readonly IComparer<T> _comparer = Comparer<T>.Default;

        /// <summary>
        /// The _m.
        /// </summary>
        private readonly int _m;

        /// <summary>
        /// The _is leaf.
        /// </summary>
        private bool _isLeaf;

        /// <summary>
        /// The _parent.
        /// </summary>
        private BPlusTreeNode<T> _parent;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BPlusTreeNode{T}"/> class. 
        /// Constructor.
        /// </summary>
        /// <param name="degree">
        /// The degree.
        /// </param>
        /// <param name="parent">
        /// Parent node. 
        /// </param>
        /// <param name="data">
        /// Values for the node. 
        /// </param>
        public BPlusTreeNode(int degree, BPlusTreeNode<T> parent, List<T> data)
            : base(data, null)
        {
            this._parent = parent;
            this.Values = data;
            this.Neighbors = null;
            this._isLeaf = true;
            this._m = degree;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BPlusTreeNode{T}"/> class. 
        /// Constructor.
        /// </summary>
        /// <param name="degree">
        /// The degree.
        /// </param>
        /// <param name="parent">
        /// Parent node. 
        /// </param>
        /// <param name="data">
        /// Values for the node. 
        /// </param>
        /// <param name="children">
        /// Child nodes. 
        /// </param>
        public BPlusTreeNode(int degree, BPlusTreeNode<T> parent, List<T> data, NodeList<T> children)
        {
            this._parent = parent;
            this.Values = data;
            this.Neighbors = children;
            this._isLeaf = true;
            this._m = degree;
            foreach (Node<T> t in this.Neighbors)
            {
                t.Parent = this;
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Indicates whether node has less than minimal number of values.
        /// </summary>
        public bool IsDeficient
        {
            get
            {
                if (this.Values.Count < this._m)
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        ///   Indicates whether node has at least maximal number of values.
        /// </summary>
        public bool IsFull
        {
            get
            {
                if (this.Values.Count < this._m * 2)
                {
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        ///   Indicates whether node has at most minimal number of values.
        /// </summary>
        public bool IsHalf
        {
            get
            {
                if (this.Values.Count > this._m)
                {
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        ///   Indicates whether node is a leaf.
        /// </summary>
        public bool IsLeaf
        {
            get
            {
                return this._isLeaf;
            }

            set
            {
                this._isLeaf = value;
            }
        }

        /// <summary>
        ///   Gets node info.
        /// </summary>
        public override string NodeInfo
        {
            get
            {
                string result = "<";
                if (this._parent == null)
                {
                    result += "R";
                }

                if (this.IsLeaf)
                {
                    result += "L";
                }

                if (this.IsDeficient)
                {
                    result += "D";
                }

                if (this.IsHalf)
                {
                    result += "H";
                }

                if (this.IsFull)
                {
                    result += "F";
                }

                result += "> ";
                return result;
            }
        }

        /// <summary>
        ///   Gets parent of the node.
        /// </summary>
        public override Node<T> Parent
        {
            get
            {
                return this._parent;
            }

            set
            {
                this._parent = (BPlusTreeNode<T>)value;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds input node values and child nodes .
        /// </summary>
        /// <param name="node">
        /// Input node. 
        /// </param>
        public BPlusTreeNode<T> Add(BPlusTreeNode<T> node)
        {
            if (node.Values.Count != 1 || node.Neighbors.Count != 2)
            {
                throw new NotImplementedException();
            }

            T nodeData = node.Values[0];
            var rightNode = (BPlusTreeNode<T>)node.Neighbors[1];
            var leftNode = (BPlusTreeNode<T>)node.Neighbors[0];
            rightNode.Parent = this;
            leftNode.Parent = this;
            for (int m = 0; m < this.Values.Count; m++)
            {
                int result = this._comparer.Compare(this.Values[m], nodeData);
                if (result > 0)
                {
                    if (this.IsFull)
                    {
                        this.Values.Insert(m, nodeData);
                        this.Neighbors.RemoveAt(m);
                        this.Neighbors.Insert(m, rightNode);
                        this.Neighbors.Insert(m, leftNode);

                        return this.Split();
                    }

                    this.Values.Insert(m, nodeData);
                    this.Neighbors.RemoveAt(m);
                    this.Neighbors.Insert(m, rightNode);
                    this.Neighbors.Insert(m, leftNode);
                    break;
                }

                if (m + 1 == this.Values.Count)
                {
                    if (this.IsFull)
                    {
                        this.Values.Add(nodeData);
                        this.Neighbors.RemoveAt(m + 1);
                        this.Neighbors.Add(leftNode);
                        this.Neighbors.Add(rightNode);

                        return this.Split();
                    }

                    this.Values.Add(nodeData);
                    this.Neighbors.RemoveAt(m + 1);
                    this.Neighbors.Add(leftNode);
                    this.Neighbors.Add(rightNode);
                    break;
                }
            }

            return this;
        }

        /// <summary>
        /// Adds input item to list of values.
        /// </summary>
        /// <param name="data">
        /// Input item. 
        /// </param>
        public BPlusTreeNode<T> Add(T data)
        {
            for (int i = 0; i < this.Values.Count; i++)
            {
                int result = this._comparer.Compare(this.Values[i], data);
                if (result > 0)
                {
                    this.Values.Insert(i, data);
                    return this;
                }

                if (i + 1 == this.Values.Count)
                {
                    this.Values.Add(data);
                    return this;
                }
            }

            return this;
        }

        /// <summary>
        /// Returns i-th child node.
        /// </summary>
        /// <param name="i">
        /// Index of the child node. 
        /// </param>
        /// <returns>
        /// Child node of index i. 
        /// </returns>
        public BPlusTreeNode<T> ChildAt(int i)
        {
            return (BPlusTreeNode<T>)this.Neighbors[i];
        }

        /// <summary>
        /// Deletes element in i-th position.
        /// </summary>
        /// <param name="data">
        /// Element to be deleted. 
        /// </param>
        /// <param name="index">
        /// Position index. 
        /// </param>
        public BPlusTreeNode<T> Delete(T data, int index)
        {
            if (this.IsLeaf)
            {
                if (!this.IsHalf)
                {
                    this.Values.RemoveAt(index);
                    return this;
                }

                if (this._parent == null)
                {
                    this.Values.RemoveAt(index);
                    return this;
                }

                this.Values.Remove(data);
                return this.Merge();
            }

            // Find successor.
            BPlusTreeNode<T> successorNode = null;
            for (int i = 0; i < this.Values.Count; i++)
            {
                int result = this._comparer.Compare(this.Values[i], data);
                if (result <= 0)
                {
                    successorNode = (BPlusTreeNode<T>)this.Neighbors[i + 1];
                }
            }

            while (successorNode.Neighbors != null)
            {
                successorNode = (BPlusTreeNode<T>)successorNode.Neighbors[0];
            }

            this.Values[index] = successorNode.Values[0];
            return successorNode.Delete(this.Values[index], 0);
        }

        /// <summary>
        /// Merge node.
        /// </summary>
        public BPlusTreeNode<T> Merge()
        {
            BPlusTreeNode<T> left = null;
            BPlusTreeNode<T> right = null;
            int myIndex = -1;

            // Get right and left sibling.
            for (int i = 0; i < this._parent.Neighbors.Count; i++)
            {
                if (this._parent.Neighbors[i] == this)
                {
                    myIndex = i;
                    if (i > 0)
                    {
                        left = (BPlusTreeNode<T>)this._parent.Neighbors[i - 1];
                    }

                    if (i + 1 < this._parent.Neighbors.Count)
                    {
                        right = (BPlusTreeNode<T>)this._parent.Neighbors[i + 1];
                    }
                }
            }

            // If it is the only child.
            if (myIndex == -1 || (left == null && right == null))
            {
                return this;
            }

            // If it has right sibling.
            if (right != null)
            {
                if (!right.IsHalf)
                {
                    // Borrow from right.
                    this.Values.Add(this._parent.Values[myIndex]);
                    T borrowed = right.Values[0];
                    this._parent.Values[myIndex] = borrowed;
                    right.Values.Remove(borrowed);
                    if (right.Neighbors != null)
                    {
                        this.Neighbors.Add(right.Neighbors[0]);
                        right.Neighbors[0].Parent = this;
                        right.Neighbors.RemoveAt(0);
                    }
                }
                else
                {
                    // Merge with right
                    T fromParent = this._parent.Values[myIndex];
                    this.Values.Add(fromParent);
                    for (int i = 0; i < right.Values.Count; i++)
                    {
                        this.Values.Add(right.Values[i]);
                    }

                    if (right.Neighbors != null)
                    {
                        for (int j = 0; j < right.Neighbors.Count; j++)
                        {
                            this.Neighbors.Add(right.Neighbors[j]);
                            right.Neighbors[j].Parent = this;
                        }
                    }

                    this._parent.Neighbors.Remove(right);
                    this._parent.Values.Remove(fromParent);
                }
            }
            else
            {
                if (!left.IsHalf)
                {
                    // Borrow from left.
                    this.Values.Insert(0, this._parent.Values[myIndex - 1]);
                    T borrowed = left.Values[left.Values.Count - 1];
                    this._parent.Values[myIndex - 1] = borrowed;
                    left.Values.Remove(borrowed);
                    if (left.Neighbors != null)
                    {
                        this.Neighbors.Add(left.Neighbors[left.Neighbors.Count - 1]);
                        left.Neighbors[left.Neighbors.Count - 1].Parent = this;
                        left.Neighbors.RemoveAt(left.Neighbors.Count - 1);
                    }
                }
                else
                {
                    // Merge with left
                    T fromParent = this._parent.Values[myIndex - 1];
                    left.Values.Add(fromParent);
                    for (int i = 0; i < this.Values.Count; i++)
                    {
                        left.Values.Add(this.Values[i]);
                    }

                    if (this.Neighbors != null)
                    {
                        for (int j = 0; j < this.Neighbors.Count; j++)
                        {
                            left.Neighbors.Add(this.Neighbors[j]);
                            this.Neighbors[j].Parent = this;
                        }
                    }

                    this._parent.Neighbors.Remove(this);
                    this._parent.Values.Remove(fromParent);
                }
            }

            if (this._parent.IsDeficient && this._parent.Parent != null)
            {
                return this._parent.Merge();
            }

            if (this._parent.Parent == null && (this._parent.Values == null || this._parent.Values.Count == 0))
            {
                this.Parent = null;
            }

            return this;
        }

        /// <summary>
        /// Splits node.
        /// </summary>
        public BPlusTreeNode<T> Split()
        {
            var leftData = new List<T>();
            var rightData = new List<T>();
            var leftNeighbors = new NodeList<T>(0);
            var rightNeighbors = new NodeList<T>(0);
            int i, j;
            for (i = 0; i < this.Values.Count / 2; i++)
            {
                leftData.Add(this.Values[i]);
                leftNeighbors.Add(this.Neighbors[i]);
            }

            if (leftNeighbors[leftNeighbors.Count - 1].Neighbors != null)
            {
                leftNeighbors[leftNeighbors.Count - 1].Neighbors.RemoveAt(
                    leftNeighbors[leftNeighbors.Count - 1].Neighbors.Count - 1);
            }

            T centerData = this.Values[i];

            // leftNeighbors.Add(Neighbors[i]);
            for (j = i; j < this.Values.Count; j++)
            {
                rightData.Add(this.Values[j]);
                rightNeighbors.Add(this.Neighbors[j]);
            }

            rightNeighbors.Add(this.Neighbors[j]);

            this.Values.Clear();
            var rightNode = new BPlusTreeNode<T>(this._m, (BPlusTreeNode<T>)this.Parent, rightData, rightNeighbors)
                {
                   IsLeaf = false 
                };
            var leftNode = new BPlusTreeNode<T>(this._m, (BPlusTreeNode<T>)this.Parent, leftData, leftNeighbors)
                {
                   IsLeaf = false 
                };

            var thisParent = (BPlusTreeNode<T>)this.Parent;

            this.Values = leftNode.Values;
            this.Neighbors = leftNode.Neighbors;
            this.Parent = leftNode.Parent;
            this.IsLeaf = leftNode.IsLeaf;
            this.Neighbors.Add(rightNode);

            var centerDataList = new List<T> { centerData };
            var children = new NodeList<T>(0) { this, rightNode };
            var centerNode = new BPlusTreeNode<T>(this._m, null, centerDataList, children) { IsLeaf = false };

            if (thisParent != null)
            {
                thisParent.IsLeaf = false;
                thisParent = thisParent.Add(centerNode);
                return thisParent;
            }

            return centerNode;
        }

        /// <summary>
        /// Splits node regarding input element.
        /// </summary>
        /// <param name="data">
        /// Element to be inserted. 
        /// </param>
        public BPlusTreeNode<T> Split(T data)
        {
            var leftData = new List<T>();
            var rightData = new List<T>();
            int i, j;
            for (i = 0; i < this.Values.Count / 2; i++)
            {
                leftData.Add(this.Values[i]);
            }

            T centerData = this.Values[i];
            int centerResult = this._comparer.Compare(this.Values[i], data);
            for (j = i; j < this.Values.Count; j++)
            {
                rightData.Add(this.Values[j]);
            }

            if (centerResult > 0)
            {
                for (int k = 0; k < leftData.Count; k++)
                {
                    int leftResult = this._comparer.Compare(leftData[k], data);
                    if (leftResult > 0)
                    {
                        leftData.Insert(k, data);
                        break;
                    }

                    if (k + 1 == leftData.Count)
                    {
                        leftData.Insert(k + 1, data);
                        break;
                    }
                }
            }
            else
            {
                for (int l = 0; l < rightData.Count; l++)
                {
                    int rightResult = this._comparer.Compare(rightData[l], data);
                    if (rightResult > 0)
                    {
                        rightData.Insert(l, data);
                        break;
                    }

                    if (l + 1 == rightData.Count)
                    {
                        rightData.Insert(l + 1, data);
                        break;
                    }
                }
            }

            this.Values.Clear();
            var leftNode = new BPlusTreeNode<T>(this._m, (BPlusTreeNode<T>)this.Parent, leftData);
            var rightNode = new BPlusTreeNode<T>(this._m, (BPlusTreeNode<T>)this.Parent, rightData);

            var centerDataList = new List<T> { centerData };
            var children = new NodeList<T>(0) { leftNode, rightNode };
            var centerNode = new BPlusTreeNode<T>(this._m, null, centerDataList, children) { IsLeaf = false };

            if (this._parent != null)
            {
                leftNode.Parent = this._parent;
                rightNode.Parent = this._parent;
                this._parent.IsLeaf = false;
                this._parent = this._parent.Add(centerNode);

                return this._parent;
            }

            leftNode.Parent = centerNode;
            rightNode.Parent = centerNode;

            return centerNode;
        }

        #endregion
    }
}
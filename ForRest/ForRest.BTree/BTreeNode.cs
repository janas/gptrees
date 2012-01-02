// -----------------------------------------------------------------------
// <copyright file="BTreeNode.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using ForRest.Provider.BLL;

namespace ForRest.BTree
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class BTreeNode<T> : Node<T>
    {
        private BTreeNode<T> _parent;
        private bool _isLeaf;
        private readonly int _m;
        private readonly IComparer<T> _comparer = Comparer<T>.Default;

        public override Node<T> Parent
        {
            get { return _parent; }
            set { _parent = (BTreeNode<T>) value; }
        }

        public override string NodeInfo
        {
            get
            {
                string result = "<";
                if (_parent == null)
                    result += "R";
                if (IsLeaf)
                    result += "L";
                if (IsDeficient)
                    result += "D";
                if (IsHalf)
                    result += "H";
                if (IsFull)
                    result += "F";
                result += "> ";
                return result;
            }
        }

        /*public void UpdateNodesInfo()
        {
            UpdateNodeInfo();
            if (Neighbors != null)
                foreach (Node<T> n in Neighbors)
                    ((BTreeNode<T>)n).UpdateNodesInfo();
        }*/

        /// <summary>
        /// Indicates whether BTreeNode is a leaf
        /// </summary>
        public bool IsLeaf
        {
            set { _isLeaf = value; }
            get { return _isLeaf; }
        }

        /// <summary>
        /// Indicates whether BTreeNode has maximal nuber of values
        /// </summary>
        public bool IsFull
        {
            get
            {
                if (Values.Count < _m*2)
                    return false;
                return true;
            }
        }

        /// <summary>
        /// Indicates whether BTreeNode has at most minimal nuber of values
        /// </summary>
        public bool IsHalf
        {
            get
            {
                if (Values.Count > _m)
                    return false;
                return true;
            }
        }

        /// <summary>
        /// Indicates whether BTreeNode has less than minimal nuber of values
        /// </summary>
        public bool IsDeficient
        {
            get
            {
                if (Values.Count < _m)
                    return true;
                return false;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="degree">Degree of BTree in Knuth's notation</param>
        /// <param name="parent">Parent node</param>
        /// <param name="data">Values for the node</param>
        public BTreeNode(int degree, BTreeNode<T> parent, List<T> data)
            : base(data, null)
        {
            _parent = parent;
            Values = data;
            Neighbors = null;
            _isLeaf = true;
            _m = degree;

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="degree">Degree of BTree in Knuth's notation</param>
        /// <param name="parent">Parent node</param>
        /// <param name="data">Values for the node</param>
        /// <param name="children">Child nodes</param>
        public BTreeNode(int degree, BTreeNode<T> parent, List<T> data, NodeList<T> children)
        {
            _parent = parent;
            Values = data;
            Neighbors = children;
            _isLeaf = true;
            _m = degree;
            foreach (Node<T> t in Neighbors)
            {
                t.Parent = this;
            }
        }

        /// <summary>
        /// Returns i-th child node
        /// </summary>
        /// <param name="i">Index of the child node</param>
        /// <returns>Child node of index i</returns>
        public BTreeNode<T> ChildAt(int i)
        {
            return (BTreeNode<T>) Neighbors[i];
        }

        /// <summary>
        /// Adds input node values and child nodes 
        /// </summary>
        /// <param name="node">Input node</param>
        public BTreeNode<T> Add(BTreeNode<T> node)
        {
            if (node.Values.Count != 1 || node.Neighbors.Count != 2)
                throw new System.NotImplementedException();
            T nodeData = node.Values[0];
            var rightNode = (BTreeNode<T>) node.Neighbors[1];
            var leftNode = (BTreeNode<T>) node.Neighbors[0];
            rightNode.Parent = this;
            leftNode.Parent = this;
            for (int m = 0; m < Values.Count; m++)
            {
                int result = _comparer.Compare(Values[m], nodeData);
                if (result > 0)
                {
                    if (IsFull)
                    {
                        Values.Insert(m, nodeData);
                        Neighbors.RemoveAt(m);
                        Neighbors.Insert(m, rightNode);
                        Neighbors.Insert(m, leftNode);
                        return Split();
                    }
                    Values.Insert(m, nodeData);
                    Neighbors.RemoveAt(m);
                    Neighbors.Insert(m, rightNode);
                    Neighbors.Insert(m, leftNode);
                    break;
                }
                if (m + 1 == Values.Count)
                {
                    if (IsFull)
                    {
                        Values.Add(nodeData);
                        Neighbors.RemoveAt(m + 1);
                        Neighbors.Add(leftNode);
                        Neighbors.Add(rightNode);

                        return Split();
                    }
                    Values.Add(nodeData);
                    Neighbors.RemoveAt(m + 1);
                    Neighbors.Add(leftNode);
                    Neighbors.Add(rightNode);
                    break;
                }
            }
            return this;
        }

        /// <summary>
        /// Adds input item to list of values
        /// </summary>
        /// <param name="data">Input item</param>
        public BTreeNode<T> Add(T data)
        {
            for (int i = 0; i < Values.Count; i++)
            {
                int result = _comparer.Compare(Values[i], data);
                if (result > 0)
                {
                    Values.Insert(i, data);
                    return this;
                }
                if (i + 1 == Values.Count)
                {
                    Values.Add(data);
                    return this;
                }
            }
            return this;
        }

        /// <summary>
        /// Splits node 
        /// </summary>
        public BTreeNode<T> Split()
        {
            var leftData = new List<T>();
            var rightData = new List<T>();
            var leftNeighbors = new NodeList<T>(0);
            var rightNeighbors = new NodeList<T>(0);
            int i, j;
            for (i = 0; i < Values.Count/2; i++)
            {
                leftData.Add(Values[i]);
                leftNeighbors.Add(Neighbors[i]);
            }
            T centerData = Values[i];
            leftNeighbors.Add(Neighbors[i]);
            for (j = ++i; j < Values.Count; j++)
            {
                rightData.Add(Values[j]);
                rightNeighbors.Add(Neighbors[j]);
            }
            rightNeighbors.Add(Neighbors[j]);

            Values.Clear();
            var leftNode = new BTreeNode<T>(_m, (BTreeNode<T>) Parent, leftData, leftNeighbors) {IsLeaf = false};
            var rightNode = new BTreeNode<T>(_m, (BTreeNode<T>) Parent, rightData, rightNeighbors) {IsLeaf = false};

            var centerDataList = new List<T> {centerData};
            var children = new NodeList<T>(0) {leftNode, rightNode};
            var centerNode = new BTreeNode<T>(_m, null, centerDataList, children) {IsLeaf = false};

            if (_parent != null)
            {
                _parent.IsLeaf = false;
                _parent = _parent.Add(centerNode);
                return _parent;
            }
            return centerNode;
        }

        /// <summary>
        /// Splits node 
        /// </summary>
        /// <param name="data">Input item forcing the split</param>
        public BTreeNode<T> Split(T data)
        {
            var leftData = new List<T>();
            var rightData = new List<T>();
            int i, j;
            for (i = 0; i < Values.Count/2; i++)
                leftData.Add(Values[i]);
            T centerData = Values[i];
            int centerResult = _comparer.Compare(Values[i], data);
            for (j = ++i; j < Values.Count; j++)
                rightData.Add(Values[j]);
            if (centerResult > 0)
            {
                for (int k = 0; k < leftData.Count; k++)
                {
                    int leftResult = _comparer.Compare(leftData[k], data);
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
                if (leftData.Count == 0)
                    leftData.Add(data);
            }
            else
            {
                for (int l = 0; l < rightData.Count; l++)
                {
                    int rightResult = _comparer.Compare(rightData[l], data);
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
                if (rightData.Count == 0)
                    rightData.Add(data);
            }
            Values.Clear();
            var leftNode = new BTreeNode<T>(_m, (BTreeNode<T>) Parent, leftData);
            var rightNode = new BTreeNode<T>(_m, (BTreeNode<T>) Parent, rightData);

            var centerDataList = new List<T> {centerData};
            var children = new NodeList<T>(0) {leftNode, rightNode};
            var centerNode = new BTreeNode<T>(_m, null, centerDataList, children) {IsLeaf = false};

            if (_parent != null)
            {
                leftNode.Parent = _parent;
                rightNode.Parent = _parent;
                _parent.IsLeaf = false;
                _parent = _parent.Add(centerNode);

                return _parent;
            }
            leftNode.Parent = centerNode;
            rightNode.Parent = centerNode;

            return centerNode;
        }

        public BTreeNode<T> Merge()
        {
            BTreeNode<T> left = null;
            BTreeNode<T> right = null;
            int myIndex = -1;
            // Get right and left sibling.
            for (int i = 0; i < _parent.Neighbors.Count; i++)
            {
                if (_parent.Neighbors[i] == this)
                {
                    myIndex = i;
                    if (i > 0)
                    {
                        left = (BTreeNode<T>) _parent.Neighbors[i - 1];
                    }
                    if (i + 1 < _parent.Neighbors.Count)
                    {
                        right = (BTreeNode<T>) _parent.Neighbors[i + 1];
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
                    Values.Add(_parent.Values[myIndex]);
                    T borrowed = right.Values[0];
                    _parent.Values[myIndex] = borrowed;
                    right.Values.Remove(borrowed);
                    if (right.Neighbors != null)
                    {
                        Neighbors.Add(right.Neighbors[0]);
                        right.Neighbors[0].Parent = this;
                        right.Neighbors.RemoveAt(0);
                    }
                }
                else
                {
                    // Merge with right
                    T fromParent = _parent.Values[myIndex];
                    Values.Add(fromParent);
                    for (int i = 0; i < right.Values.Count; i++)
                        Values.Add(right.Values[i]);
                    if (right.Neighbors != null)
                        for (int j = 0; j < right.Neighbors.Count; j++)
                        {
                            Neighbors.Add(right.Neighbors[j]);
                            right.Neighbors[j].Parent = this;
                        }
                    _parent.Neighbors.Remove(right);
                    _parent.Values.Remove(fromParent);
                }
            }
            else
            {
                if (!left.IsHalf)
                {
                    // Borrow from left.
                    Values.Insert(0, _parent.Values[myIndex - 1]);
                    T borrowed = left.Values[left.Values.Count - 1];
                    _parent.Values[myIndex - 1] = borrowed;
                    left.Values.Remove(borrowed);
                    if (left.Neighbors != null)
                    {
                        Neighbors.Add(left.Neighbors[left.Neighbors.Count - 1]);
                        left.Neighbors[left.Neighbors.Count - 1].Parent = this;
                        left.Neighbors.RemoveAt(left.Neighbors.Count - 1);
                    }
                }
                else
                {
                    // Merge with left
                    T fromParent = _parent.Values[myIndex - 1];
                    left.Values.Add(fromParent);
                    for (int i = 0; i < Values.Count; i++)
                        left.Values.Add(Values[i]);
                    if (Neighbors != null)
                        for (int j = 0; j < Neighbors.Count; j++)
                        {
                            left.Neighbors.Add(Neighbors[j]);
                            Neighbors[j].Parent = this;
                        }
                    _parent.Neighbors.Remove(this);
                    _parent.Values.Remove(fromParent);
                }
            }
            if (_parent.IsDeficient && _parent.Parent != null)
            {
                return _parent.Merge();
            }
            if (_parent.Parent == null && (_parent.Values == null || _parent.Values.Count == 0))
            {
                Parent = null;
            }
            return this;
        }

        public BTreeNode<T> Delete(T data, int index)
        {
            if (IsLeaf)
            {
                if (!IsHalf)
                {
                    Values.RemoveAt(index);
                    return this;
                }
                if (_parent == null)
                {
                    Values.RemoveAt(index);
                    return this;
                }
                Values.Remove(data);
                return Merge();
            }
            // Find successor.
            BTreeNode<T> successorNode = null;
            for (int i = 0; i < Values.Count; i++)
            {
                int result = _comparer.Compare(Values[i], data);
                if (result <= 0)
                    successorNode = (BTreeNode<T>) Neighbors[i + 1];
            }
            while (successorNode.Neighbors != null)
                successorNode = (BTreeNode<T>) successorNode.Neighbors[0];
            Values[index] = successorNode.Values[0];
            return successorNode.Delete(Values[index], 0);
        }
    }
}

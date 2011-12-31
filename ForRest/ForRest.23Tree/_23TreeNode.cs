// -----------------------------------------------------------------------
// <copyright file="_23TreeNode.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using ForRest.Provider.BLL;

namespace ForRest._23Tree
{

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class _23TreeNode<T> : Node<T>
    {
        private _23TreeNode<T> _parent;
        private bool _isLeaf;
        private readonly IComparer<T> _comparer = Comparer<T>.Default;

        public _23TreeNode<T> Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        private void UpdateNodeInfo()
        {
            string result = "<";
            if (_parent == null)
                result += "R";
            if (isLeaf)
                result += "L";
            if (IsDeficient)
                result += "D";
            if (IsHalf)
                result += "H";
            if (IsFull)
                result += "F";
            result += "> ";
            NodeInfo = result;
        }

        public void UpdateNodesInfo()
        {
            UpdateNodeInfo();
            if (Neighbors != null)
                foreach (Node<T> n in Neighbors)
                    ((_23TreeNode<T>)n).UpdateNodesInfo();
        }

        /// <summary>
        /// Indicates whether _23TreeNode is a leaf
        /// </summary>
        public bool isLeaf
        {
            set { _isLeaf = value; }
            get { return _isLeaf; }
        }

        /// <summary>
        /// Indicates whether _23TreeNode has at least maximal nuber of values
        /// </summary>
        public bool IsFull
        {
            get
            {
                if (Values.Count < 2)
                    return false;
                else
                    return true;
            }
        }

        /// <summary>
        /// Indicates whether _23TreeNode has at most minimal nuber of values
        /// </summary>
        public bool IsHalf
        {
            get
            {
                if (Values.Count > 1)
                    return false;
                else
                    return true;
            }
        }

        /// <summary>
        /// Indicates whether _23TreeNode has less than minimal nuber of values
        /// </summary>
        public bool IsDeficient
        {
            get
            {
                if (Values.Count < 1)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="degree">Degree of BTree in Knuth's notation</param>
        /// <param name="parent">Parent node</param>
        /// <param name="data">Values for the node</param>
        public _23TreeNode(_23TreeNode<T> parent, List<T> data)
            : base(data, null)
        {
            _parent = parent;
            Values = data;
            Neighbors = null;
            _isLeaf = true;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="degree">Degree of BTree in Knuth's notation</param>
        /// <param name="parent">Parent node</param>
        /// <param name="data">Values for the node</param>
        /// <param name="children">Child nodes</param>
        public _23TreeNode(_23TreeNode<T> parent, List<T> data, NodeList<T> children)
        {
            _parent = parent;
            Values = data;
            Neighbors = children;
            _isLeaf = true;
            for (int i = 0; i < Neighbors.Count; i++)
            {
                ((_23TreeNode<T>)Neighbors[i]).Parent = this;
            }
             
        }

        /// <summary>
        /// Returns i-th child node
        /// </summary>
        /// <param name="i">Index of the child node</param>
        /// <returns>Child node of index i</returns>
        public _23TreeNode<T> ChildAt(int i)
        {
            return (_23TreeNode<T>)Neighbors[i];
        }

        /// <summary>
        /// Adds input node values and child nodes 
        /// </summary>
        /// <param name="node">Input node</param>
        public _23TreeNode<T> Add(_23TreeNode<T> node)
        {
            if (node.Values.Count != 1 || node.Neighbors.Count != 2)
                throw new System.NotImplementedException();
            T nodeData = node.Values[0];
            _23TreeNode<T> rightNode = (_23TreeNode<T>)node.Neighbors[1];
            _23TreeNode<T> leftNode = (_23TreeNode<T>)node.Neighbors[0];
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
                    else
                    {
                        Values.Insert(m, nodeData);
                        Neighbors.RemoveAt(m);
                        Neighbors.Insert(m, rightNode);
                        Neighbors.Insert(m, leftNode);
                        break;
                    }
                }
                else
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
                        else
                        {
                            Values.Add(nodeData);
                            Neighbors.RemoveAt(m + 1);
                            Neighbors.Add(leftNode);
                            Neighbors.Add(rightNode);
                            break;
                        }
                    }
            }
             
            return this;
        }

        /// <summary>
        /// Adds input item to list of values
        /// </summary>
        /// <param name="data">Input item</param>
        public _23TreeNode<T> Add(T data)
        {
            for (int i = 0; i < Values.Count; i++)
            {
                int result = _comparer.Compare(Values[i], data);
                if (result > 0)
                {
                    Values.Insert(i, data);
                    return this;
                }
                else
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
        public _23TreeNode<T> Split()
        {
            T centerData;
            List<T> leftData = new List<T>();
            List<T> rightData = new List<T>();
            NodeList<T> leftNeighbors = new NodeList<T>(0);
            NodeList<T> rightNeighbors = new NodeList<T>(0);
            int i, j;
            for (i = 0; i < Values.Count / 2; i++)
            {
                leftData.Add(Values[i]);
                leftNeighbors.Add(Neighbors[i]);
            }
            centerData = Values[i];
            leftNeighbors.Add(Neighbors[i]);
            for (j = ++i; j < Values.Count; j++)
            {
                rightData.Add(Values[j]);
                rightNeighbors.Add(Neighbors[j]);
            }
            rightNeighbors.Add(Neighbors[j]);

            Values.Clear();
            _23TreeNode<T> leftNode = new _23TreeNode<T>(this.Parent, leftData, leftNeighbors);
            leftNode.isLeaf = false;
            _23TreeNode<T> rightNode = new _23TreeNode<T>(this.Parent, rightData, rightNeighbors);
            rightNode.isLeaf = false;

            List<T> centerDataList = new List<T>();
            centerDataList.Add(centerData);
            NodeList<T> children = new NodeList<T>(0);
            children.Add(leftNode);
            children.Add(rightNode);
            _23TreeNode<T> centerNode = new _23TreeNode<T>(null, centerDataList, children);
            centerNode.isLeaf = false;

            if (_parent != null)
            {
                _parent.isLeaf = false;
                _parent = _parent.Add(centerNode);
                return _parent;
            }
            else
            {
                return centerNode;
            }
        }

        public _23TreeNode<T> Split(T data)
        {
            T centerData;
            List<T> leftData = new List<T>();
            List<T> rightData = new List<T>();
            int zeroResult, oneResult;
            zeroResult = _comparer.Compare(Values[0], data);
            oneResult = _comparer.Compare(Values[1], data);
            if (zeroResult > 0)
            {
                leftData.Add(data);
                centerData = Values[0];
                rightData.Add(Values[1]);
            }
            else if (oneResult > 0)
            {
                leftData.Add(Values[0]);
                centerData = data;
                rightData.Add(Values[1]);
            }
            else
            {
                leftData.Add(Values[0]);
                centerData = Values[1];
                rightData.Add(data);
            }
            Values.Clear();
            _23TreeNode<T> leftNode = new _23TreeNode<T>(this.Parent, leftData);
            _23TreeNode<T> rightNode = new _23TreeNode<T>(this.Parent, rightData);

            List<T> centerDataList = new List<T>();
            centerDataList.Add(centerData);
            NodeList<T> children = new NodeList<T>(0);
            children.Add(leftNode);
            children.Add(rightNode);
            _23TreeNode<T> centerNode = new _23TreeNode<T>(null, centerDataList, children);
            centerNode.isLeaf = false;

            if (_parent != null)
            {
                leftNode.Parent = _parent;
                rightNode.Parent = _parent;
                _parent.isLeaf = false;
                _parent = _parent.Add(centerNode);
                return _parent;
            }
            else
            {
                leftNode.Parent = centerNode;
                rightNode.Parent = centerNode;
                return centerNode;
            }
        }

        public _23TreeNode<T> Merge()
        {
            _23TreeNode<T> left = null;
            _23TreeNode<T> right = null;
            int myIndex = -1;
            // Get right and left sibling.
            for (int i = 0; i < _parent.Neighbors.Count; i++)
            {
                if (_parent.Neighbors[i] == this)
                {
                    myIndex = i;
                    try
                    {
                        left = (_23TreeNode<T>)_parent.Neighbors[i - 1];
                    }
                    catch { }
                    try
                    {
                        right = (_23TreeNode<T>)_parent.Neighbors[i + 1];
                    }
                    catch { }
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
                        ((_23TreeNode<T>)right.Neighbors[0]).Parent = this;
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
                            ((_23TreeNode<T>)right.Neighbors[j]).Parent = this;
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
                        ((_23TreeNode<T>)left.Neighbors[left.Neighbors.Count - 1]).Parent = this;
                        left.Neighbors.RemoveAt(left.Neighbors.Count - 1);
                    }
                }
                else
                {
                    // Merge with left
                    T fromParent = _parent.Values[myIndex - 1];
                    left.Values.Add(fromParent);
                    for (int i = 0; i < this.Values.Count; i++)
                        left.Values.Add(this.Values[i]);
                    if (this.Neighbors != null)
                        for (int j = 0; j < this.Neighbors.Count; j++)
                        {
                            left.Neighbors.Add(this.Neighbors[j]);
                            ((_23TreeNode<T>)this.Neighbors[j]).Parent = this;
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
                this.Parent = null;
            }
            return this;
        }

        public _23TreeNode<T> Delete(T data, int index)
        {
            if (isLeaf)
            {
                if (!IsHalf)
                {
                    Values.RemoveAt(index);
                    return this;
                }
                else
                {
                    if (_parent == null)
                    {
                        Values.RemoveAt(index);
                        return this;
                    }
                    else
                    {
                        Values.Remove(data);
                        return Merge();
                    }
                }
            }
            else
            {
                // Find successor.
                _23TreeNode<T> successorNode = null;
                for (int i = 0; i < Values.Count; i++)
                {
                    int result = _comparer.Compare(Values[i], data);
                    if (result <= 0)
                        successorNode = (_23TreeNode<T>)Neighbors[i + 1];
                }
                while (successorNode.Neighbors != null)
                    successorNode = (_23TreeNode<T>)successorNode.Neighbors[0];
                Values[index] = successorNode.Values[0];
                return successorNode.Delete(Values[index], 0);
            }
        }
    }
}

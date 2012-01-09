using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using ForRest.Provider.BLL;
using Microsoft.Glee.Drawing;
using Microsoft.Glee.GraphViewerGdi;
using Color = System.Drawing.Color;
using Size = System.Drawing.Size;

namespace ForRest
{
    public partial class Search : Form
    {
        private readonly Provider.Provider _provider;
        private TreeView _treeViewCreate;
        private Panel _graphPanel;
        private GViewer _gleeGraphViewer;
        private Graph _gleeGraph;
        private readonly Color _graphPanelMarkColor = Color.Green;
        private const int GraphPanelMarkLineWidth = 2;

        public int GraphMode { get; set; }

        public Search(Provider.Provider provider, int graphMode)
        {
            InitializeComponent();
            _provider = provider;
            GraphMode = graphMode;
            FillSelectedTreeComboBox();
            switch (GraphMode)
            {
                case 0:
                    InitializeTreeView();
                    break;
                case 1:
                    InitializeGraph();
                    break;
                case 2:
                    InitializeGleeGraph();
                    break;
            }
        }

        public void FillSelectedTreeComboBox()
        {
            comboBoxSelectTree.Items.Clear();
            foreach (var treeObject in _provider.TreeObjects)
            {
                comboBoxSelectTree.Items.Add(treeObject);
                comboBoxSelectTree.DisplayMember = "Name";
            }
        }

        public void ChangeGraphMode()
        {
            switch (GraphMode)
            {
                case 0:
                    if (_graphPanel != null)
                    {
                        Controls.Remove(_graphPanel);
                    }
                    if (_gleeGraphViewer != null)
                    {
                        Controls.Remove(_gleeGraphViewer);
                    }
                    InitializeTreeView();
                    break;
                case 1:
                    if (_treeViewCreate != null)
                    {
                        Controls.Remove(_treeViewCreate);
                    }
                    if (_gleeGraphViewer != null)
                    {
                        Controls.Remove(_gleeGraphViewer);
                    }
                    InitializeGraph();
                    break;
                case 2:
                    if (_treeViewCreate != null)
                    {
                        Controls.Remove(_treeViewCreate);
                    }
                    if (_graphPanel != null)
                    {
                        Controls.Remove(_graphPanel);
                    }
                    InitializeGleeGraph();
                    break;
            }
            List<int> result = null;
            ShowTree(ref result);
        }

        private void ShowTree(ref List<int> result)
        {
            switch (GraphMode)
            {
                case 0:
                    ShowTreeView();
                    break;
                case 1:
                    DrawGraph(ref result);
                    break;
                case 2:
                    DrawGleeGraph(ref result);
                    break;
            }
        }

        #region TREE VIEW
        private void InitializeTreeView()
        {
            _treeViewCreate = new TreeView
            {
                Anchor = ((AnchorStyles.Top | AnchorStyles.Bottom)
                          | AnchorStyles.Left)
                         | AnchorStyles.Right,
                Location = new Point(195, 16),
                Name = "treeViewCreate",
                Size = new Size(Width - 220, Height - 65),
            };
            Controls.Add(_treeViewCreate);
        }

        private void ShowTreeView()
        {
            _treeViewCreate.Nodes.Clear();
            if (comboBoxSelectTree.SelectedItem == null) return;
            var treeObject = (TreeObject) comboBoxSelectTree.SelectedItem;
            if (treeObject.Type.Equals("text"))
            {
                ITree<string> iTree = treeObject.TextTree;
                TreeNode tn;
                if (iTree.Root == null)
                    return;
                string print = iTree.Root.NodeInfo;
                for (int i = 0; i < iTree.Root.Values.Count; i++)
                    print += iTree.Root.Values[i] + " ";
                if (NextLevel(iTree.Root) == null)
                    tn = new TreeNode(print);
                else
                    tn = new TreeNode(print, NextLevel(iTree.Root));
                _treeViewCreate.Nodes.Add(tn);
                _treeViewCreate.ExpandAll();
            }
            if (treeObject.Type.Equals("numeric"))
            {
                ITree<double> iTree = treeObject.NumericTree;
                TreeNode tn;
                if (iTree.Root == null)
                    return;
                string print = iTree.Root.NodeInfo;
                for (int i = 0; i < iTree.Root.Values.Count; i++)
                    print += iTree.Root.Values[i] + " ";
                if (NextLevel(iTree.Root) == null)
                    tn = new TreeNode(print);
                else
                    tn = new TreeNode(print, NextLevel(iTree.Root));
                _treeViewCreate.Nodes.Add(tn);
                _treeViewCreate.ExpandAll();
            }
        }

        private static TreeNode[] NextLevel(Node<string> node)
        {
            if (node == null)
                return null;
            NodeList<string> nodeList = node.GetNeighborsList();
            if (nodeList == null)
                return null;
            var resultList = new List<TreeNode>();
            foreach (Node<string> n in nodeList)
            {
                if (n == null)
                    continue;
                var print = n.NodeInfo;
                for (int i = 0; i < n.Values.Count; i++)
                    print += n.Values[i] + " ";
                TreeNode[] whatever = NextLevel(n);
                if (whatever == null)
                    resultList.Add(new TreeNode(print));
                else
                    resultList.Add(new TreeNode(print, whatever));
            }
            resultList.RemoveAll(item => item == null);
            var result = new TreeNode[resultList.Count];
            resultList.CopyTo(result);
            return result;
        }

        private static TreeNode[] NextLevel(Node<double> node)
        {
            if (node == null)
                return null;
            NodeList<double> nodeList = node.GetNeighborsList();
            if (nodeList == null)
                return null;
            var resultList = new List<TreeNode>();
            foreach (Node<double> n in nodeList)
            {
                if (n == null)
                    continue;
                var print = n.NodeInfo;
                for (int i = 0; i < n.Values.Count; i++)
                    print += n.Values[i].ToString() + " ";
                TreeNode[] whatever = NextLevel(n);
                if (whatever == null)
                    resultList.Add(new TreeNode(print));
                else
                    resultList.Add(new TreeNode(print, whatever));
            }
            resultList.RemoveAll(item => item == null);
            var result = new TreeNode[resultList.Count];
            resultList.CopyTo(result);
            return result;
        }
        #endregion

        #region GRAPH
        private void InitializeGraph()
        {
            _graphPanel = new Panel
            {
                Anchor = ((AnchorStyles.Top | AnchorStyles.Bottom)
                          | AnchorStyles.Left)
                         | AnchorStyles.Right,
                Location = new Point(195, 16),
                Name = "graphPanel",
                Size = new Size(Width - 220, Height - 65),
                AutoScroll = true
            };
            Controls.Add(_graphPanel);
        }

        private void DrawGraph(ref List<int> result)
        {
            _graphPanel.Invoke((MethodInvoker) (() => _graphPanel.Controls.Clear()));
            object comboBoxSelectTreeSelectedItem = null;
            if (comboBoxSelectTree.InvokeRequired)
            {
                comboBoxSelectTree.Invoke(new MethodInvoker
                                              (delegate
                                                   { comboBoxSelectTreeSelectedItem = comboBoxSelectTree.SelectedItem; }));
            }
            else
            {
                comboBoxSelectTreeSelectedItem = comboBoxSelectTree.SelectedItem;
            }
            if (comboBoxSelectTreeSelectedItem == null) return;
            var treeObject = (TreeObject) comboBoxSelectTreeSelectedItem;
            if (treeObject.Type.Equals("text"))
            {
                ITree<string> iTree = treeObject.TextTree;
                var rootRectangle = new Rectangle(5, 5, _graphPanel.Width - 27, 24);
                NextControls(rootRectangle, 0, iTree.Root, ref result, -1);
            }
            else if (treeObject.Type.Equals("numeric"))
            {
                ITree<double> iTree = treeObject.NumericTree;
                var rootRectangle = new Rectangle(5, 5, _graphPanel.Width - 27, 24);
                NextControls(rootRectangle, 0, iTree.Root, ref result, -1);
            }
        }

        private void NextControls(Rectangle rectangle, int levelBlank, Node<double> node,
                                  ref List<int> result, int nodeIndex)
        {
            if (node == null)
                return;
            var text = new List<string>();
            for (int i = 0; i < node.Values.Count; i++)
                text.Add(node.Values[i].ToString());

            // Draw parent
            UserControlNode ucn;
            if (result != null &&
                (result.Count == 0 || result[0] == nodeIndex || nodeIndex == -1))
            {
                ucn = new UserControlNode(text, node.NodeInfo, rectangle, _graphPanelMarkColor, GraphPanelMarkLineWidth);
                if (nodeIndex > -1)
                {
                    if (result.Count < 2)
                        result = null;
                    else
                        result.RemoveAt(0);
                }
            }
            else
                ucn = new UserControlNode(text, node.NodeInfo, rectangle, node.NodeColor);
            ucn.Location = rectangle.Location;
            ucn.Size = rectangle.Size;
            ucn.VerifySize();

            // Draw children 
            if (node.Neighbors != null && node.Neighbors.Count > 0)
            {
                int notNullChildren = 0;
                int notNullChildrenIndex = 0;
                for (int k = 0; k < node.Neighbors.Count; k++)
                    if (node.Neighbors[k] != null)
                        notNullChildren++;
                int blank = 0;
                if (notNullChildren > 1)
                    blank = ucn.GetMyArea().Width / (10 * (notNullChildren - 1));
                int rWidth = ucn.GetMyArea().Width;
                if (notNullChildren > 0)
                    rWidth = (ucn.GetMyArea().Width - (notNullChildren - 1) * blank)
                             / notNullChildren;
                if (rWidth < 3)
                    rWidth = 3;
                bool allEdgesMarked = false;
                for (int j = 0; j < node.Neighbors.Count; j++)
                {
                    if (node.Neighbors[j] == null)
                    {
                        continue;
                    }
                    var r = new Rectangle(
                        ucn.GetMyArea().X + notNullChildrenIndex * (rWidth + blank),
                        ucn.GetMyArea().Y + ucn.GetMyArea().Height * 2,
                        rWidth,
                        ucn.GetMyArea().Height);
                    // Drawing edge
                    int nodeValuesCount = 1;
                    if (node.Values.Count > nodeValuesCount)
                        nodeValuesCount = node.Values.Count;
                    var from = new Point(
                        ucn.Location.X + j *
                        (ucn.Width / nodeValuesCount),
                        ucn.Location.Y + ucn.Height);
                    var to = new Point(
                        r.Location.X + r.Width / 2,
                        r.Location.Y);
                    int drawChild = -1;
                    if (node.Parent != null && node.Parent.Neighbors != null)
                    {
                        for (int l = 0; l < node.Parent.Neighbors.Count; l++)
                            if (node.Parent.Neighbors[l] == node && l + 1 < node.Parent.Neighbors.Count
                                && node.Neighbors[j] == node.Parent.Neighbors[l + 1])
                            {
                                to = new Point(
                                    ucn.GetMyArea().Location.X + ucn.GetMyArea().Width * 3 / 2 + levelBlank,
                                    ucn.GetMyArea().Location.Y);
                                drawChild = l + 1;
                            }
                    }
                    var e = new Rectangle(
                        Math.Min(from.X, to.X),
                        Math.Min(from.Y, to.Y),
                        Math.Abs(from.X - to.X),
                        Math.Abs(from.Y - to.Y));
                    if (e.Width == 0)
                        e.Width = 1;
                    if (e.Height == 0)
                        e.Height = 1;
                    bool ltr;
                    if (from.X > to.X)
                        if (from.Y > to.Y)
                            ltr = true;
                        else
                            ltr = false;
                    else if (from.Y > to.Y)
                        ltr = false;
                    else
                        ltr = true;
                    UserControlEdge uce;
                    if (result != null && result.Count > 0 && result[0] == j && !allEdgesMarked)
                    {
                        uce = new UserControlEdge(ltr, _graphPanelMarkColor, GraphPanelMarkLineWidth);
                        allEdgesMarked = true;
                    }
                    else
                        uce = new UserControlEdge(ltr);
                    uce.Location = e.Location;
                    uce.Size = e.Size;
                    _graphPanel.Controls.Add(uce);
                    notNullChildrenIndex++;

                    // Draw child
                    if (drawChild == -1)
                    {
                        if (result != null && result.Count > 0 && result[0] == j)
                            NextControls(r, blank, node.Neighbors[j], ref result, j);
                        else
                        {
                            // Dumb C#... I have to pass it this way
                            List<int> nullResult = null;
                            NextControls(r, blank, node.Neighbors[j], ref nullResult, j);
                        }
                    }
                    else
                    {
                        if (result != null && result.Count > 0)
                            result[0] = drawChild;
                    }
                }
            }
            _graphPanel.Controls.Add(ucn);
        }

        private void NextControls(Rectangle rectangle, int levelBlank, Node<string> node,
                                  ref List<int> result, int nodeIndex)
        {
            if (node == null)
                return;
            var text = new List<string>();
            for (int i = 0; i < node.Values.Count; i++)
                text.Add(node.Values[i].ToString());

            // Draw parent
            UserControlNode ucn;
            if (result != null &&
                (result.Count == 0 || result[0] == nodeIndex || nodeIndex == -1))
            {
                ucn = new UserControlNode(text, node.NodeInfo, rectangle, _graphPanelMarkColor, GraphPanelMarkLineWidth);
                if (nodeIndex > -1)
                {
                    if (result.Count < 2)
                        result = null;
                    else
                        result.RemoveAt(0);
                }
            }
            else
                ucn = new UserControlNode(text, node.NodeInfo, rectangle, node.NodeColor);
            ucn.Location = rectangle.Location;
            ucn.Size = rectangle.Size;
            ucn.VerifySize();

            // Draw children 
            if (node.Neighbors != null && node.Neighbors.Count > 0)
            {
                int notNullChildren = 0;
                int notNullChildrenIndex = 0;
                for (int k = 0; k < node.Neighbors.Count; k++)
                    if (node.Neighbors[k] != null)
                        notNullChildren++;
                int blank = 0;
                if (notNullChildren > 1)
                    blank = ucn.GetMyArea().Width / (10 * (notNullChildren - 1));
                int rWidth = ucn.GetMyArea().Width;
                if (notNullChildren > 0)
                    rWidth = (ucn.GetMyArea().Width - (notNullChildren - 1) * blank)
                             / notNullChildren;
                if (rWidth < 3)
                    rWidth = 3;
                bool allEdgesMarked = false;
                for (int j = 0; j < node.Neighbors.Count; j++)
                {
                    if (node.Neighbors[j] == null)
                    {
                        continue;
                    }
                    var r = new Rectangle(
                        ucn.GetMyArea().X + notNullChildrenIndex * (rWidth + blank),
                        ucn.GetMyArea().Y + ucn.GetMyArea().Height * 2,
                        rWidth,
                        ucn.GetMyArea().Height);
                    // Drawing edge
                    int nodeValuesCount = 1;
                    if (node.Values.Count > nodeValuesCount)
                        nodeValuesCount = node.Values.Count;
                    var from = new Point(
                        ucn.Location.X + j *
                        (ucn.Width / nodeValuesCount),
                        ucn.Location.Y + ucn.Height);
                    var to = new Point(
                        r.Location.X + r.Width / 2,
                        r.Location.Y);
                    int drawChild = -1;
                    if (node.Parent != null && node.Parent.Neighbors != null)
                    {
                        for (int l = 0; l < node.Parent.Neighbors.Count; l++)
                            if (node.Parent.Neighbors[l] == node && l + 1 < node.Parent.Neighbors.Count
                                && node.Neighbors[j] == node.Parent.Neighbors[l + 1])
                            {
                                to = new Point(
                                    ucn.GetMyArea().Location.X + ucn.GetMyArea().Width * 3 / 2 + levelBlank,
                                    ucn.GetMyArea().Location.Y);
                                drawChild = l + 1;
                            }
                    }
                    var e = new Rectangle(
                        Math.Min(from.X, to.X),
                        Math.Min(from.Y, to.Y),
                        Math.Abs(from.X - to.X),
                        Math.Abs(from.Y - to.Y));
                    if (e.Width == 0)
                        e.Width = 1;
                    if (e.Height == 0)
                        e.Height = 1;
                    bool ltr;
                    if (from.X > to.X)
                        if (from.Y > to.Y)
                            ltr = true;
                        else
                            ltr = false;
                    else if (from.Y > to.Y)
                        ltr = false;
                    else
                        ltr = true;
                    UserControlEdge uce;
                    if (result != null && result.Count > 0 && result[0] == j && !allEdgesMarked)
                    {
                        uce = new UserControlEdge(ltr, _graphPanelMarkColor, GraphPanelMarkLineWidth);
                        allEdgesMarked = true;
                    }
                    else
                        uce = new UserControlEdge(ltr);
                    uce.Location = e.Location;
                    uce.Size = e.Size;
                    _graphPanel.Controls.Add(uce);
                    notNullChildrenIndex++;

                    // Draw child
                    if (drawChild == -1)
                    {
                        if (result != null && result.Count > 0 && result[0] == j)
                            NextControls(r, blank, node.Neighbors[j], ref result, j);
                        else
                        {
                            // Dumb C#... I have to pass it this way
                            List<int> nullResult = null;
                            NextControls(r, blank, node.Neighbors[j], ref nullResult, j);
                        }
                    }
                    else
                    {
                        if (result != null && result.Count > 0)
                            result[0] = drawChild;
                    }
                }
            }
            _graphPanel.Controls.Add(ucn);
        }
        #endregion

        #region GLEE GRAPH
        private void InitializeGleeGraph()
        {
            _gleeGraphViewer = new GViewer
            {
                Anchor = ((AnchorStyles.Top | AnchorStyles.Bottom)
                          | AnchorStyles.Left)
                         | AnchorStyles.Right,
                AsyncLayout = false,
                AutoScroll = true,
                BackwardEnabled = false,
                ForwardEnabled = false,
                Graph = null,
                Location = new Point(195, 16),
                MouseHitDistance = 0.05D,
                Name = "graphViewer",
                NavigationVisible = true,
                PanButtonPressed = false,
                SaveButtonVisible = true,
                Size = new Size(Width - 220, Height - 65),
                ZoomF = 1D,
                ZoomFraction = 0.5D,
                ZoomWindowThreshold = 0.05D
            };
            Controls.Add(_gleeGraphViewer);
        }

        private void DrawGleeGraph(ref List<int> result)
        {
            if (comboBoxSelectTree.SelectedItem == null) return;
            var treeObject = (TreeObject)comboBoxSelectTree.SelectedItem;
            string graphName = treeObject.Name;
            _gleeGraph = new Graph(graphName);
            if (treeObject.Type.Equals("text"))
            {
                ITree<string> iTree = treeObject.TextTree;
                Node<string> iTreeRoot = iTree.Root;
                if (iTreeRoot == null || iTreeRoot.Values == null)
                    return;
                Node root = _gleeGraph.AddNode(iTreeRoot.GetHashCode().ToString());
                string rootText = "";
                for (int i = 0; i < iTreeRoot.Values.Count; i++)
                {
                    if (i > 0)
                        rootText += " | ";
                    rootText += iTreeRoot.Values[i];
                }
                root.Attr.Label = rootText;
                if (result != null)
                {
                    root.Attr.Color = new Microsoft.Glee.Drawing.Color(
                        _graphPanelMarkColor.A, _graphPanelMarkColor.R,
                        _graphPanelMarkColor.G, _graphPanelMarkColor.B);
                    root.Attr.LineWidth = GraphPanelMarkLineWidth;
                }
                else
                    root.Attr.Color = new Microsoft.Glee.Drawing.Color(
                        iTreeRoot.NodeColor.A, iTreeRoot.NodeColor.R,
                        iTreeRoot.NodeColor.G, iTreeRoot.NodeColor.B);
                NextGleeNodes(iTreeRoot, ref result);
            }
            else if (treeObject.Type.Equals("numeric"))
            {
                ITree<double> iTree = treeObject.NumericTree;
                Node<double> iTreeRoot = iTree.Root;
                if (iTreeRoot == null || iTreeRoot.Values == null)
                    return;
                Node root = _gleeGraph.AddNode(iTreeRoot.GetHashCode().ToString());
                string rootText = "";
                for (int i = 0; i < iTreeRoot.Values.Count; i++)
                {
                    if (i > 0)
                        rootText += " | ";
                    rootText += iTreeRoot.Values[i].ToString();
                }
                root.Attr.Label = rootText;
                if (result != null)
                {
                    root.Attr.Color = new Microsoft.Glee.Drawing.Color(
                        _graphPanelMarkColor.A, _graphPanelMarkColor.R,
                        _graphPanelMarkColor.G, _graphPanelMarkColor.B);
                    root.Attr.LineWidth = GraphPanelMarkLineWidth;
                }
                else
                    root.Attr.Color = new Microsoft.Glee.Drawing.Color(
                        iTreeRoot.NodeColor.A, iTreeRoot.NodeColor.R,
                        iTreeRoot.NodeColor.G, iTreeRoot.NodeColor.B);
                NextGleeNodes(iTreeRoot, ref result);
            }
            _gleeGraphViewer.Graph = _gleeGraph;
        }

        private void NextGleeNodes(Node<double> node, ref List<int> result)
        {
            if (node == null || node.Values == null)
                return;
            string parentHashCode = node.GetHashCode().ToString();
            if (node.Neighbors != null && node.Neighbors.Count > 0)
            {
                var adoptedChildren = new List<int>();
                for (int j = 0; j < node.Neighbors.Count; j++)
                {
                    // Draw children
                    if (node.Neighbors[j] == null)
                        continue;
                    string childHashCode = node.Neighbors[j].GetHashCode().ToString();
                    Node child = _gleeGraph.FindNode(childHashCode);
                    if (child == null)
                    {
                        string childText = "";
                        for (int i = 0; i < node.Neighbors[j].Values.Count; i++)
                        {
                            if (i > 0)
                                childText += " | ";
                            childText += node.Neighbors[j].Values[i].ToString();
                        }
                        child = _gleeGraph.AddNode(childHashCode);
                        child.Attr.Label = childText;
                        if (result != null && result.Count > 0 && result[0] == j)
                        {
                            child.Attr.Color = new Microsoft.Glee.Drawing.Color(
                                _graphPanelMarkColor.A, _graphPanelMarkColor.R,
                                _graphPanelMarkColor.G, _graphPanelMarkColor.B);
                            child.Attr.LineWidth = GraphPanelMarkLineWidth;
                        }
                        else
                            child.Attr.Color = new Microsoft.Glee.Drawing.Color(
                                node.Neighbors[j].NodeColor.A, node.Neighbors[j].NodeColor.R,
                                node.Neighbors[j].NodeColor.G, node.Neighbors[j].NodeColor.B);
                    }
                    else
                        adoptedChildren.Add(j);
                    // Draw edge
                    Edge edge = _gleeGraph.AddEdge(parentHashCode, childHashCode);
                    if (result != null && result.Count > 0 && result[0] == j)
                    {
                        edge.Attr.Color = new Microsoft.Glee.Drawing.Color(
                                _graphPanelMarkColor.A, _graphPanelMarkColor.R,
                                _graphPanelMarkColor.G, _graphPanelMarkColor.B);
                        edge.Attr.LineWidth = GraphPanelMarkLineWidth;
                    }
                }
                bool pathMarked = false;
                for (int k = 0; k < node.Neighbors.Count; k++)
                {
                    if (!adoptedChildren.Contains(k))
                    {
                        if (result != null && result.Count > 0 && result[0] == k && !pathMarked)
                        {
                            pathMarked = true;
                            result.RemoveAt(0);
                            NextGleeNodes(node.Neighbors[k], ref result);
                        }
                        else
                        {
                            List<int> nullResult = null;
                            NextGleeNodes(node.Neighbors[k], ref nullResult);
                        }
                    }
                }
            }
        }

        private void NextGleeNodes(Node<string> node, ref List<int> result)
        {
            if (node == null || node.Values == null)
                return;
            string parentHashCode = node.GetHashCode().ToString();
            if (node.Neighbors != null && node.Neighbors.Count > 0)
            {
                var adoptedChildren = new List<int>();
                for (int j = 0; j < node.Neighbors.Count; j++)
                {
                    // Draw children
                    if (node.Neighbors[j] == null)
                        continue;
                    string childHashCode = node.Neighbors[j].GetHashCode().ToString();
                    Node child = _gleeGraph.FindNode(childHashCode);
                    if (child == null)
                    {
                        string childText = "";
                        for (int i = 0; i < node.Neighbors[j].Values.Count; i++)
                        {
                            if (i > 0)
                                childText += " | ";
                            childText += node.Neighbors[j].Values[i];
                        }
                        child = _gleeGraph.AddNode(childHashCode);
                        child.Attr.Label = childText;
                        if (result != null && result.Count > 0 && result[0] == j)
                        {
                            child.Attr.Color = new Microsoft.Glee.Drawing.Color(
                                _graphPanelMarkColor.A, _graphPanelMarkColor.R,
                                _graphPanelMarkColor.G, _graphPanelMarkColor.B);
                            child.Attr.LineWidth = GraphPanelMarkLineWidth;
                        }
                        else
                            child.Attr.Color = new Microsoft.Glee.Drawing.Color(
                                node.Neighbors[j].NodeColor.A, node.Neighbors[j].NodeColor.R,
                                node.Neighbors[j].NodeColor.G, node.Neighbors[j].NodeColor.B);
                    }
                    else
                        adoptedChildren.Add(j);
                    // Draw edge
                    Edge edge = _gleeGraph.AddEdge(parentHashCode, childHashCode);
                    if (result != null && result.Count > 0 && result[0] == j)
                    {
                        edge.Attr.Color = new Microsoft.Glee.Drawing.Color(
                                _graphPanelMarkColor.A, _graphPanelMarkColor.R,
                                _graphPanelMarkColor.G, _graphPanelMarkColor.B);
                        edge.Attr.LineWidth = GraphPanelMarkLineWidth;
                    }
                }
                bool pathMarked = false;
                for (int k = 0; k < node.Neighbors.Count; k++)
                {
                    if (!adoptedChildren.Contains(k))
                    {
                        if (result != null && result.Count > 0 && result[0] == k && !pathMarked)
                        {
                            pathMarked = true;
                            result.RemoveAt(0);
                            NextGleeNodes(node.Neighbors[k], ref result);
                        }
                        else
                        {
                            List<int> nullResult = null;
                            NextGleeNodes(node.Neighbors[k], ref nullResult);
                        }
                    }
                }
            }
        }
        #endregion

        private void BtnSearchClick(object sender, EventArgs e)
        {
            if (comboBoxSelectTree.SelectedItem != null)
            {
                double numericValue;
                var treeObject = (TreeObject) comboBoxSelectTree.SelectedItem;
                if (treeObject.Type.Equals("numeric") && textBoxSearchFor.Text != null)
                    if (!double.TryParse(textBoxSearchFor.Text, NumberStyles.Any, CultureInfo.InvariantCulture,
                                         out numericValue))
                    {
                        toolTipHelperSearch.ToolTipTitle = "Incorrect input format";
                        toolTipHelperSearch.Show("Please provide apropriate input for selected tree.", textBoxSearchFor,
                                                 3000);
                    }
                btnSearch.Enabled = false;
                backgroundWorkerSearch.RunWorkerAsync(treeObject);
            }
            else
            {
                toolTipHelperSearch.ToolTipTitle = "No tree is selected";
                toolTipHelperSearch.Show("No tree is selected. Please select tree from list first.", comboBoxSelectTree,
                                         3000);
            }
        }

        private void BtnResetResultsSetClick(object sender, EventArgs e)
        {
            _provider.PerformanceSets.Clear();
        }

        private void BtnShowResultsSetClick(object sender, EventArgs e)
        {
            var resultsSet = new ResultsSet(_provider);
            resultsSet.ShowDialog();
        }

        private void BackgroundWorkerSearchDoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var treeObject = e.Argument as TreeObject;
            if (treeObject != null && (treeObject.Type.Equals("text") && textBoxSearchFor.Text != null))
            {
                var watch = new Stopwatch();
                string textValue = textBoxSearchFor.Text;
                watch.Start();
                List<int> result = treeObject.TextTree.Contains(textValue);
                watch.Stop();
                if (result != null)
                {
                    var peroformanceSet = new PerformanceSet();
                    labelTime.Invoke((MethodInvoker) (() => labelTime.ResetText()));
                    labelTime.Invoke((MethodInvoker) (() => labelTime.Text = watch.ElapsedMilliseconds + " ms"));
                    peroformanceSet.TreeName = treeObject.Name;
                    peroformanceSet.SearchTime = watch.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture);
                    peroformanceSet.TypeOfNodes = "String";
                    peroformanceSet.TypeOfTree = treeObject.TextTree.TreeType;
                    peroformanceSet.NoOfNodes = "notImplemented";
                    _provider.PerformanceSets.Add(peroformanceSet);
                }
                else
                {
                    var peroformanceSet = new PerformanceSet();
                    labelTime.Invoke((MethodInvoker) (() => labelTime.ResetText()));
                    labelTime.Invoke((MethodInvoker) (() => labelTime.Text = watch.ElapsedMilliseconds + 
                        " ms - Item not found "));
                    peroformanceSet.TreeName = treeObject.Name;
                    peroformanceSet.SearchTime = watch.ElapsedMilliseconds + "/Not Found";
                    peroformanceSet.TypeOfNodes = "String";
                    peroformanceSet.TypeOfTree = treeObject.TextTree.TreeType;
                    peroformanceSet.NoOfNodes = "notImplemented";
                    _provider.PerformanceSets.Add(peroformanceSet);
                }
                e.Result = result;
            }
            else if (treeObject != null && (treeObject.Type.Equals("numeric") && textBoxSearchFor.Text != null))
            {
                var watch = new Stopwatch();
                double numericValue;
                if (double.TryParse(textBoxSearchFor.Text, NumberStyles.Any, CultureInfo.InvariantCulture,
                                    out numericValue))
                {
                    watch.Start();
                    List<int> result = treeObject.NumericTree.Contains(numericValue);
                    watch.Stop();
                    if (result != null)
                    {
                        var peroformanceSet = new PerformanceSet();
                        labelTime.Invoke((MethodInvoker) (() => labelTime.ResetText()));
                        labelTime.Invoke((MethodInvoker) (() => labelTime.Text = watch.ElapsedMilliseconds + " ms"));
                        peroformanceSet.TreeName = treeObject.Name;
                        peroformanceSet.SearchTime = watch.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture);
                        peroformanceSet.TypeOfNodes = "Double";
                        peroformanceSet.TypeOfTree = treeObject.NumericTree.TreeType;
                        peroformanceSet.NoOfNodes = "notImplemented";
                        _provider.PerformanceSets.Add(peroformanceSet);
                    }
                    else
                    {
                        var peroformanceSet = new PerformanceSet();
                        labelTime.Invoke((MethodInvoker) (() => labelTime.ResetText()));
                        labelTime.Invoke(
                            (MethodInvoker)(() => labelTime.Text = watch.ElapsedMilliseconds +
                        " ms - Item not found "));
                        peroformanceSet.TreeName = treeObject.Name;
                        peroformanceSet.SearchTime = watch.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture) +
                                                     "/Not Found";
                        peroformanceSet.TypeOfNodes = "Double";
                        peroformanceSet.TypeOfTree = treeObject.NumericTree.TreeType;
                        peroformanceSet.NoOfNodes = "notImplemented";
                        _provider.PerformanceSets.Add(peroformanceSet);
                    }
                    e.Result = result;
                }
            }
        }

        private void BackgroundWorkerSearchRunWorkerCompleted(object sender,
                                                              System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            List<int> result = e.Result as List<int>;
            ShowTree(ref result);
            btnSearch.Enabled = true;
        }

        private void ComboBoxSelectTreeSelectedIndexChanged(object sender, EventArgs e)
        {
            List<int> result = null;
            ShowTree(ref result);
            if (comboBoxSelectTree.SelectedItem == null)
            {
                btnSearch.Enabled = false;
            }
            else
            {
                btnSearch.Enabled = true;
            }
        }

        private void SearchResize(object sender, EventArgs e)
        {
            List<int> result = null;
            ShowTree(ref result);
        }

        private void SearchFormClosing(object sender, FormClosingEventArgs e)
        {
            var mainForm = (MainForm) MdiParent;
            mainForm.SearchClosing();
        }
    }
}

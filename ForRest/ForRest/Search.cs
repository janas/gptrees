// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Search.cs" company="Warsaw University of Technology">
//   
// </copyright>
// <summary>
//   The search.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForRest
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    using ForRest.BLL;
    using ForRest.Provider.BLL;

    using Microsoft.Glee.Drawing;
    using Microsoft.Glee.GraphViewerGdi;

    using Color = System.Drawing.Color;
    using Size = System.Drawing.Size;

    /// <summary>
    /// The search.
    /// </summary>
    public partial class Search : Form
    {
        #region Constants and Fields

        /// <summary>
        /// The graph panel mark line width.
        /// </summary>
        private const int GraphPanelMarkLineWidth = 2;

        /// <summary>
        /// The graph panel mark color.
        /// </summary>
        private readonly Color graphPanelMarkColor = Color.Green;

        /// <summary>
        /// The provider.
        /// </summary>
        private readonly Provider.Provider provider;

        /// <summary>
        /// The glee graph.
        /// </summary>
        private Graph gleeGraph;

        /// <summary>
        /// The glee graph viewer.
        /// </summary>
        private GViewer gleeGraphViewer;

        /// <summary>
        /// The graph panel.
        /// </summary>
        private Panel graphPanel;

        /// <summary>
        /// The tree view create.
        /// </summary>
        private TreeView treeViewCreate;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Search"/> class.
        /// </summary>
        /// <param name="provider">
        /// The provider.
        /// </param>
        /// <param name="graphMode">
        /// The graph mode.
        /// </param>
        public Search(Provider.Provider provider, int graphMode)
        {
            this.InitializeComponent();
            this.provider = provider;
            this.GraphMode = graphMode;
            this.FillSelectedTreeComboBox();
            switch (this.GraphMode)
            {
                case 0:
                    // do nothing
                    break;
                case 1:
                    this.InitializeTreeView();
                    break;
                case 2:
                    this.InitializeGraph();
                    break;
                case 3:
                    this.InitializeGleeGraph();
                    break;
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets GraphMode.
        /// </summary>
        public int GraphMode { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// The change graph mode.
        /// </summary>
        public void ChangeGraphMode()
        {
            switch (this.GraphMode)
            {
                case 0:
                    if (this.treeViewCreate != null)
                    {
                        this.Controls.Remove(this.treeViewCreate);
                    }

                    if (this.graphPanel != null)
                    {
                        this.Controls.Remove(this.graphPanel);
                    }

                    if (this.gleeGraphViewer != null)
                    {
                        this.Controls.Remove(this.gleeGraphViewer);
                    }

                    break;
                case 1:
                    if (this.graphPanel != null)
                    {
                        this.Controls.Remove(this.graphPanel);
                    }

                    if (this.gleeGraphViewer != null)
                    {
                        this.Controls.Remove(this.gleeGraphViewer);
                    }

                    this.InitializeTreeView();
                    break;
                case 2:
                    if (this.treeViewCreate != null)
                    {
                        this.Controls.Remove(this.treeViewCreate);
                    }

                    if (this.gleeGraphViewer != null)
                    {
                        this.Controls.Remove(this.gleeGraphViewer);
                    }

                    this.InitializeGraph();
                    break;
                case 3:
                    if (this.treeViewCreate != null)
                    {
                        this.Controls.Remove(this.treeViewCreate);
                    }

                    if (this.graphPanel != null)
                    {
                        this.Controls.Remove(this.graphPanel);
                    }

                    this.InitializeGleeGraph();
                    break;
            }

            List<int> result = null;
            this.ShowTree(ref result);
        }

        /// <summary>
        /// The fill selected tree combo box.
        /// </summary>
        public void FillSelectedTreeComboBox()
        {
            this.comboBoxSelectTree.Items.Clear();
            foreach (var treeObject in this.provider.TreeObjects)
            {
                this.comboBoxSelectTree.Items.Add(treeObject);
                this.comboBoxSelectTree.DisplayMember = "Name";
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The next level.
        /// </summary>
        /// <param name="node">
        /// The node.
        /// </param>
        /// <returns>
        /// Returns TreeNode type.
        /// </returns>
        private static TreeNode[] NextLevel(Node<string> node)
        {
            if (node == null)
            {
                return null;
            }

            NodeList<string> nodeList = node.GetNeighborsList();
            if (nodeList == null)
            {
                return null;
            }

            var resultList = new List<TreeNode>();
            foreach (Node<string> n in nodeList)
            {
                if (n == null)
                {
                    continue;
                }

                var print = n.NodeInfo;
                for (int i = 0; i < n.Values.Count; i++)
                {
                    print += n.Values[i] + " ";
                }

                TreeNode[] whatever = NextLevel(n);
                if (whatever == null)
                {
                    resultList.Add(new TreeNode(print));
                }
                else
                {
                    resultList.Add(new TreeNode(print, whatever));
                }
            }

            resultList.RemoveAll(item => item == null);
            var result = new TreeNode[resultList.Count];
            resultList.CopyTo(result);
            return result;
        }

        /// <summary>
        /// The next level.
        /// </summary>
        /// <param name="node">
        /// The node.
        /// </param>
        /// <returns>
        /// Returns TreeNode type.
        /// </returns>
        private static TreeNode[] NextLevel(Node<double> node)
        {
            if (node == null)
            {
                return null;
            }

            NodeList<double> nodeList = node.GetNeighborsList();
            if (nodeList == null)
            {
                return null;
            }

            var resultList = new List<TreeNode>();
            foreach (Node<double> n in nodeList)
            {
                if (n == null)
                {
                    continue;
                }

                var print = n.NodeInfo;
                for (int i = 0; i < n.Values.Count; i++)
                {
                    print += n.Values[i].ToString() + " ";
                }

                TreeNode[] whatever = NextLevel(n);
                if (whatever == null)
                {
                    resultList.Add(new TreeNode(print));
                }
                else
                {
                    resultList.Add(new TreeNode(print, whatever));
                }
            }

            resultList.RemoveAll(item => item == null);
            var result = new TreeNode[resultList.Count];
            resultList.CopyTo(result);
            return result;
        }

        /// <summary>
        /// The btn reset results set click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BtnResetResultsSetClick(object sender, EventArgs e)
        {
            this.provider.PerformanceSets.Clear();
        }

        /// <summary>
        /// The btn search click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BtnSearchClick(object sender, EventArgs e)
        {
            if (this.comboBoxSelectTree.SelectedItem != null)
            {
                var treeObject = (TreeObject)this.comboBoxSelectTree.SelectedItem;
                if (treeObject.Type.Equals("numeric") && this.textBoxSearchFor.Text != null)
                {
                    double numericValue;
                    if (
                        !double.TryParse(
                            this.textBoxSearchFor.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out numericValue))
                    {
                        this.toolTipHelperSearch.ToolTipTitle = "Incorrect input format";
                        this.toolTipHelperSearch.Show(
                            "Please provide apropriate input for selected tree.", this.textBoxSearchFor, 3000);
                    }
                }

                this.btnSearch.Enabled = false;
                this.backgroundWorkerSearch.RunWorkerAsync(treeObject);
                Application.DoEvents();
            }
            else
            {
                this.toolTipHelperSearch.ToolTipTitle = "No tree is selected";
                this.toolTipHelperSearch.Show(
                    "No tree is selected. Please select tree from list first.", this.comboBoxSelectTree, 3000);
            }
        }

        /// <summary>
        /// The btn show results set click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BtnShowResultsSetClick(object sender, EventArgs e)
        {
            var resultsSet = new ResultsSet(this.provider);
            resultsSet.ShowDialog();
        }

        /// <summary>
        /// The combo box select tree selected index changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void ComboBoxSelectTreeSelectedIndexChanged(object sender, EventArgs e)
        {
            List<int> result = null;
            this.ShowTree(ref result);
            this.btnSearch.Enabled = this.comboBoxSelectTree.SelectedItem != null;
        }
        
        /// <summary>
        /// The background worker search do work.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BackgroundWorkerSearchDoWork(object sender, DoWorkEventArgs e)
        {
            List<int> result = null;
            var treeObject = e.Argument as TreeObject;
            var searchPerformer = new SearchPerformer(this.provider, this.backgroundWorkerSearch);
            if (treeObject != null && (treeObject.Type.Equals("text") && this.textBoxSearchFor.Text != null))
            {
                result = searchPerformer.GenericSearch(treeObject, textBoxSearchFor.Text);
            }
            else if (treeObject != null && (treeObject.Type.Equals("numeric") && this.textBoxSearchFor.Text != null))
            {
                double numericValue;
                if (double.TryParse(
                    this.textBoxSearchFor.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out numericValue))
                {
                    result = searchPerformer.GenericSearch(treeObject, numericValue);
                }
            }

            e.Result = result;
        }

        /// <summary>
        /// The background worker batch process progress changed.
        /// </summary>
        /// <param name="sender">
        /// The sender. 
        /// </param>
        /// <param name="e">
        /// The e. 
        /// </param>
        private void BackgroundWorkerSearchProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var progress = (string)e.UserState;
            labelTime.ResetText();
            labelTime.Text = progress;
        }

        /// <summary>
        /// The background worker search run worker completed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BackgroundWorkerSearchRunWorkerCompleted(
            object sender, RunWorkerCompletedEventArgs e)
        {
            var result = e.Result as List<int>;
            this.ShowTree(ref result);
            this.btnSearch.Enabled = true;
        }

        /// <summary>
        /// The search form closing.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void SearchFormClosing(object sender, FormClosingEventArgs e)
        {
            var mainForm = (MainForm)this.MdiParent;
            mainForm.SearchClosing();
        }

        /// <summary>
        /// The search resize.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void SearchResize(object sender, EventArgs e)
        {
            List<int> result = null;
            this.ShowTree(ref result);
        }
        
        /// <summary>
        /// The initialize tree view.
        /// </summary>
        private void InitializeTreeView()
        {
            this.treeViewCreate = new TreeView
                {
                    Anchor = ((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right,
                    Location = new Point(195, 16),
                    Name = "treeViewCreate",
                    Size = new Size(this.Width - 220, this.Height - 65),
                };
            this.Controls.Add(this.treeViewCreate);
        }

        /// <summary>
        /// The initialize graph.
        /// </summary>
        private void InitializeGraph()
        {
            this.graphPanel = new Panel
                {
                    Anchor = ((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right, 
                    Location = new Point(195, 16), 
                    Name = "graphPanel", 
                    Size = new Size(this.Width - 220, this.Height - 65), 
                    AutoScroll = true
                };
            this.Controls.Add(this.graphPanel);
        }

        /// <summary>
        /// The initialize glee graph.
        /// </summary>
        private void InitializeGleeGraph()
        {
            this.gleeGraphViewer = new GViewer
            {
                Anchor = ((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right,
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
                Size = new Size(this.Width - 220, this.Height - 65),
                ZoomF = 1D,
                ZoomFraction = 0.5D,
                ZoomWindowThreshold = 0.05D
            };
            this.Controls.Add(this.gleeGraphViewer);
        }

        /// <summary>
        /// The show tree.
        /// </summary>
        /// <param name="result">
        /// The result.
        /// </param>
        private void ShowTree(ref List<int> result)
        {
            switch (this.GraphMode)
            {
                case 0:
                    // do nothing
                    break;
                case 1:
                    this.ShowTreeView();
                    break;
                case 2:
                    this.DrawGraph(ref result);
                    break;
                case 3:
                    this.DrawGleeGraph(ref result);
                    break;
            }
        }
        
        /// <summary>
        /// The show tree view.
        /// </summary>
        private void ShowTreeView()
        {
            this.treeViewCreate.Nodes.Clear();
            if (this.comboBoxSelectTree.SelectedItem == null)
            {
                return;
            }

            var treeObject = (TreeObject)this.comboBoxSelectTree.SelectedItem;
            if (treeObject.Type.Equals("text"))
            {
                ITree<string> iTree = treeObject.TextTree;
                TreeNode tn;
                if (iTree.Root == null)
                {
                    return;
                }

                string print = iTree.Root.NodeInfo;
                for (int i = 0; i < iTree.Root.Values.Count; i++)
                {
                    print += iTree.Root.Values[i] + " ";
                }

                if (NextLevel(iTree.Root) == null)
                {
                    tn = new TreeNode(print);
                }
                else
                {
                    tn = new TreeNode(print, NextLevel(iTree.Root));
                }

                this.treeViewCreate.Nodes.Add(tn);
                this.treeViewCreate.ExpandAll();
            }

            if (treeObject.Type.Equals("numeric"))
            {
                ITree<double> iTree = treeObject.NumericTree;
                TreeNode tn;
                if (iTree.Root == null)
                {
                    return;
                }

                string print = iTree.Root.NodeInfo;
                for (int i = 0; i < iTree.Root.Values.Count; i++)
                {
                    print += iTree.Root.Values[i] + " ";
                }

                if (NextLevel(iTree.Root) == null)
                {
                    tn = new TreeNode(print);
                }
                else
                {
                    tn = new TreeNode(print, NextLevel(iTree.Root));
                }
                this.treeViewCreate.Nodes.Add(tn);
                this.treeViewCreate.ExpandAll();
            }
        }
        
        /// <summary>
        /// The draw graph.
        /// </summary>
        /// <param name="result">
        /// The result.
        /// </param>
        private void DrawGraph(ref List<int> result)
        {
            this.graphPanel.Controls.Clear();
            var treeObject = comboBoxSelectTree.SelectedItem as TreeObject;
            if (treeObject != null && treeObject.Type.Equals("text"))
            {
                ITree<string> iTree = treeObject.TextTree;
                var rootRectangle = new Rectangle(5, 5, this.graphPanel.Width - 27, 24);
                this.NextControls(rootRectangle, 0, iTree.Root, ref result, -1);
            }
            else if (treeObject != null && treeObject.Type.Equals("numeric"))
            {
                ITree<double> iTree = treeObject.NumericTree;
                var rootRectangle = new Rectangle(5, 5, this.graphPanel.Width - 27, 24);
                this.NextControls(rootRectangle, 0, iTree.Root, ref result, -1);
            }
        }
        
        /// <summary>
        /// The draw glee graph.
        /// </summary>
        /// <param name="result">
        /// The result.
        /// </param>
        private void DrawGleeGraph(ref List<int> result)
        {
            if (this.comboBoxSelectTree.SelectedItem == null)
            {
                return;
            }

            var treeObject = (TreeObject)this.comboBoxSelectTree.SelectedItem;
            string graphName = treeObject.Name;
            this.gleeGraph = new Graph(graphName);
            if (treeObject.Type.Equals("text"))
            {
                ITree<string> iTree = treeObject.TextTree;
                Node<string> iTreeRoot = iTree.Root;
                if (iTreeRoot == null || iTreeRoot.Values == null)
                {
                    return;
                }

                Node root = this.gleeGraph.AddNode(iTreeRoot.GetHashCode().ToString());
                string rootText = string.Empty;
                for (int i = 0; i < iTreeRoot.Values.Count; i++)
                {
                    if (i > 0)
                    {
                        rootText += " | ";
                    }

                    rootText += iTreeRoot.Values[i];
                }

                root.Attr.Label = rootText;
                if (result != null)
                {
                    root.Attr.Color = new Microsoft.Glee.Drawing.Color(
                        this.graphPanelMarkColor.A,
                        this.graphPanelMarkColor.R,
                        this.graphPanelMarkColor.G,
                        this.graphPanelMarkColor.B);
                    root.Attr.LineWidth = GraphPanelMarkLineWidth;
                }
                else
                {
                    root.Attr.Color = new Microsoft.Glee.Drawing.Color(
                        iTreeRoot.NodeColor.A, iTreeRoot.NodeColor.R, iTreeRoot.NodeColor.G, iTreeRoot.NodeColor.B);
                }

                this.NextGleeNodes(iTreeRoot, ref result);
            }
            else if (treeObject.Type.Equals("numeric"))
            {
                ITree<double> iTree = treeObject.NumericTree;
                Node<double> iTreeRoot = iTree.Root;
                if (iTreeRoot == null || iTreeRoot.Values == null)
                {
                    return;
                }

                Node root = this.gleeGraph.AddNode(iTreeRoot.GetHashCode().ToString());
                string rootText = string.Empty;
                for (int i = 0; i < iTreeRoot.Values.Count; i++)
                {
                    if (i > 0)
                    {
                        rootText += " | ";
                    }

                    rootText += iTreeRoot.Values[i].ToString();
                }

                root.Attr.Label = rootText;
                if (result != null)
                {
                    root.Attr.Color = new Microsoft.Glee.Drawing.Color(
                        this.graphPanelMarkColor.A,
                        this.graphPanelMarkColor.R,
                        this.graphPanelMarkColor.G,
                        this.graphPanelMarkColor.B);
                    root.Attr.LineWidth = GraphPanelMarkLineWidth;
                }
                else
                {
                    root.Attr.Color = new Microsoft.Glee.Drawing.Color(
                        iTreeRoot.NodeColor.A, iTreeRoot.NodeColor.R, iTreeRoot.NodeColor.G, iTreeRoot.NodeColor.B);
                }

                this.NextGleeNodes(iTreeRoot, ref result);
            }

            this.gleeGraphViewer.Graph = this.gleeGraph;
        }

        /// <summary>
        /// The next controls.
        /// </summary>
        /// <param name="rectangle">
        /// The rectangle.
        /// </param>
        /// <param name="levelBlank">
        /// The level blank.
        /// </param>
        /// <param name="node">
        /// The node.
        /// </param>
        /// <param name="result">
        /// The result.
        /// </param>
        /// <param name="nodeIndex">
        /// The node index.
        /// </param>
        private void NextControls(
            Rectangle rectangle, int levelBlank, Node<double> node, ref List<int> result, int nodeIndex)
        {
            if (node == null)
            {
                return;
            }

            var text = new List<string>();
            for (int i = 0; i < node.Values.Count; i++)
            {
                text.Add(node.Values[i].ToString());
            }

            // Draw parent
            UserControlNode ucn;
            if (result != null && (result.Count == 0 || result[0] == nodeIndex || nodeIndex == -1))
            {
                ucn = new UserControlNode(
                    text, node.NodeInfo, rectangle, this.graphPanelMarkColor, GraphPanelMarkLineWidth);
                if (nodeIndex > -1)
                {
                    if (result.Count < 2)
                    {
                        result = null;
                    }
                    else
                    {
                        result.RemoveAt(0);
                    }
                }
            }
            else
            {
                ucn = new UserControlNode(text, node.NodeInfo, rectangle, node.NodeColor);
            }

            ucn.Location = rectangle.Location;
            ucn.Size = rectangle.Size;
            ucn.VerifySize();

            // Draw children 
            if (node.Neighbors != null && node.Neighbors.Count > 0)
            {
                int notNullChildren = 0;
                int notNullChildrenIndex = 0;
                for (int k = 0; k < node.Neighbors.Count; k++)
                {
                    if (node.Neighbors[k] != null)
                    {
                        notNullChildren++;
                    }
                }

                int blank = 0;
                if (notNullChildren > 1)
                {
                    blank = ucn.GetMyArea().Width / (10 * (notNullChildren - 1));
                }

                int rWidth = ucn.GetMyArea().Width;
                if (notNullChildren > 0)
                {
                    rWidth = (ucn.GetMyArea().Width - (notNullChildren - 1) * blank) / notNullChildren;
                }

                if (rWidth < 3)
                {
                    rWidth = 3;
                }

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
                    {
                        nodeValuesCount = node.Values.Count;
                    }

                    var from = new Point(
                        ucn.Location.X + j * (ucn.Width / nodeValuesCount), ucn.Location.Y + ucn.Height);
                    var to = new Point(r.Location.X + r.Width / 2, r.Location.Y);
                    int drawChild = -1;
                    if (node.Parent != null && node.Parent.Neighbors != null)
                    {
                        for (int l = 0; l < node.Parent.Neighbors.Count; l++)
                        {
                            if (node.Parent.Neighbors[l] == node && l + 1 < node.Parent.Neighbors.Count
                                && node.Neighbors[j] == node.Parent.Neighbors[l + 1])
                            {
                                to = new Point(
                                    ucn.GetMyArea().Location.X + ucn.GetMyArea().Width * 3 / 2 + levelBlank, 
                                    ucn.GetMyArea().Location.Y);
                                drawChild = l + 1;
                            }
                        }
                    }

                    var e = new Rectangle(
                        Math.Min(from.X, to.X), Math.Min(from.Y, to.Y), Math.Abs(from.X - to.X), Math.Abs(from.Y - to.Y));
                    if (e.Width == 0)
                    {
                        e.Width = 1;
                    }

                    if (e.Height == 0)
                    {
                        e.Height = 1;
                    }

                    bool ltr;
                    if (from.X > to.X)
                    {
                        if (from.Y > to.Y)
                        {
                            ltr = true;
                        }
                        else
                        {
                            ltr = false;
                        }
                    }
                    else if (from.Y > to.Y)
                    {
                        ltr = false;
                    }
                    else
                    {
                        ltr = true;
                    }

                    UserControlEdge uce;
                    if (result != null && result.Count > 0 && result[0] == j && !allEdgesMarked)
                    {
                        uce = new UserControlEdge(ltr, this.graphPanelMarkColor, GraphPanelMarkLineWidth);
                        allEdgesMarked = true;
                    }
                    else
                    {
                        uce = new UserControlEdge(ltr);
                    }

                    uce.Location = e.Location;
                    uce.Size = e.Size;
                    this.graphPanel.Controls.Add(uce);
                    notNullChildrenIndex++;

                    // Draw child
                    if (drawChild == -1)
                    {
                        if (result != null && result.Count > 0 && result[0] == j)
                        {
                            this.NextControls(r, blank, node.Neighbors[j], ref result, j);
                        }
                        else
                        {
                            // Dumb C#... I have to pass it this way
                            List<int> nullResult = null;
                            this.NextControls(r, blank, node.Neighbors[j], ref nullResult, j);
                        }
                    }
                    else
                    {
                        if (result != null && result.Count > 0)
                        {
                            result[0] = drawChild;
                        }
                    }
                }
            }

            this.graphPanel.Controls.Add(ucn);
        }

        /// <summary>
        /// The next controls.
        /// </summary>
        /// <param name="rectangle">
        /// The rectangle.
        /// </param>
        /// <param name="levelBlank">
        /// The level blank.
        /// </param>
        /// <param name="node">
        /// The node.
        /// </param>
        /// <param name="result">
        /// The result.
        /// </param>
        /// <param name="nodeIndex">
        /// The node index.
        /// </param>
        private void NextControls(
            Rectangle rectangle, int levelBlank, Node<string> node, ref List<int> result, int nodeIndex)
        {
            if (node == null)
            {
                return;
            }

            var text = new List<string>();
            for (int i = 0; i < node.Values.Count; i++)
            {
                text.Add(node.Values[i]);
            }

            // Draw parent
            UserControlNode ucn;
            if (result != null && (result.Count == 0 || result[0] == nodeIndex || nodeIndex == -1))
            {
                ucn = new UserControlNode(
                    text, node.NodeInfo, rectangle, this.graphPanelMarkColor, GraphPanelMarkLineWidth);
                if (nodeIndex > -1)
                {
                    if (result.Count < 2)
                    {
                        result = null;
                    }
                    else
                    {
                        result.RemoveAt(0);
                    }
                }
            }
            else
            {
                ucn = new UserControlNode(text, node.NodeInfo, rectangle, node.NodeColor);
            }

            ucn.Location = rectangle.Location;
            ucn.Size = rectangle.Size;
            ucn.VerifySize();

            // Draw children 
            if (node.Neighbors != null && node.Neighbors.Count > 0)
            {
                int notNullChildren = 0;
                int notNullChildrenIndex = 0;
                for (int k = 0; k < node.Neighbors.Count; k++)
                {
                    if (node.Neighbors[k] != null)
                    {
                        notNullChildren++;
                    }
                }

                int blank = 0;
                if (notNullChildren > 1)
                {
                    blank = ucn.GetMyArea().Width / (10 * (notNullChildren - 1));
                }

                int rWidth = ucn.GetMyArea().Width;
                if (notNullChildren > 0)
                {
                    rWidth = (ucn.GetMyArea().Width - (notNullChildren - 1) * blank) / notNullChildren;
                }

                if (rWidth < 3)
                {
                    rWidth = 3;
                }

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
                    {
                        nodeValuesCount = node.Values.Count;
                    }

                    var from = new Point(
                        ucn.Location.X + j * (ucn.Width / nodeValuesCount), ucn.Location.Y + ucn.Height);
                    var to = new Point(r.Location.X + r.Width / 2, r.Location.Y);
                    int drawChild = -1;
                    if (node.Parent != null && node.Parent.Neighbors != null)
                    {
                        for (int l = 0; l < node.Parent.Neighbors.Count; l++)
                        {
                            if (node.Parent.Neighbors[l] == node && l + 1 < node.Parent.Neighbors.Count
                                && node.Neighbors[j] == node.Parent.Neighbors[l + 1])
                            {
                                to = new Point(
                                    ucn.GetMyArea().Location.X + ucn.GetMyArea().Width * 3 / 2 + levelBlank, 
                                    ucn.GetMyArea().Location.Y);
                                drawChild = l + 1;
                            }
                        }
                    }

                    var e = new Rectangle(
                        Math.Min(from.X, to.X), Math.Min(from.Y, to.Y), Math.Abs(from.X - to.X), Math.Abs(from.Y - to.Y));
                    if (e.Width == 0)
                    {
                        e.Width = 1;
                    }

                    if (e.Height == 0)
                    {
                        e.Height = 1;
                    }

                    bool ltr;
                    if (from.X > to.X)
                    {
                        if (from.Y > to.Y)
                        {
                            ltr = true;
                        }
                        else
                        {
                            ltr = false;
                        }
                    }
                    else if (from.Y > to.Y)
                    {
                        ltr = false;
                    }
                    else
                    {
                        ltr = true;
                    }

                    UserControlEdge uce;
                    if (result != null && result.Count > 0 && result[0] == j && !allEdgesMarked)
                    {
                        uce = new UserControlEdge(ltr, this.graphPanelMarkColor, GraphPanelMarkLineWidth);
                        allEdgesMarked = true;
                    }
                    else
                    {
                        uce = new UserControlEdge(ltr);
                    }

                    uce.Location = e.Location;
                    uce.Size = e.Size;
                    this.graphPanel.Controls.Add(uce);
                    notNullChildrenIndex++;

                    // Draw child
                    if (drawChild == -1)
                    {
                        if (result != null && result.Count > 0 && result[0] == j)
                        {
                            NextControls(r, blank, node.Neighbors[j], ref result, j);
                        }
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
                        {
                            result[0] = drawChild;
                        }
                    }
                }
            }

            this.graphPanel.Controls.Add(ucn);
        }

        /// <summary>
        /// The next glee nodes.
        /// </summary>
        /// <param name="node">
        /// The node.
        /// </param>
        /// <param name="result">
        /// The result.
        /// </param>
        private void NextGleeNodes(Node<double> node, ref List<int> result)
        {
            if (node == null || node.Values == null)
            {
                return;
            }

            string parentHashCode = node.GetHashCode().ToString();
            if (node.Neighbors != null && node.Neighbors.Count > 0)
            {
                var adoptedChildren = new List<int>();
                for (int j = 0; j < node.Neighbors.Count; j++)
                {
                    // Draw children
                    if (node.Neighbors[j] == null)
                    {
                        continue;
                    }

                    string childHashCode = node.Neighbors[j].GetHashCode().ToString();
                    Node child = this.gleeGraph.FindNode(childHashCode);
                    if (child == null)
                    {
                        string childText = string.Empty;
                        for (int i = 0; i < node.Neighbors[j].Values.Count; i++)
                        {
                            if (i > 0)
                            {
                                childText += " | ";
                            }

                            childText += node.Neighbors[j].Values[i].ToString();
                        }

                        child = this.gleeGraph.AddNode(childHashCode);
                        child.Attr.Label = childText;
                        if (result != null && result.Count > 0 && result[0] == j)
                        {
                            child.Attr.Color = new Microsoft.Glee.Drawing.Color(
                                this.graphPanelMarkColor.A, 
                                this.graphPanelMarkColor.R, 
                                this.graphPanelMarkColor.G, 
                                this.graphPanelMarkColor.B);
                            child.Attr.LineWidth = GraphPanelMarkLineWidth;
                        }
                        else
                        {
                            child.Attr.Color = new Microsoft.Glee.Drawing.Color(
                                node.Neighbors[j].NodeColor.A, 
                                node.Neighbors[j].NodeColor.R, 
                                node.Neighbors[j].NodeColor.G, 
                                node.Neighbors[j].NodeColor.B);
                        }
                    }
                    else
                    {
                        adoptedChildren.Add(j);
                    }

                    // Draw edge
                    Edge edge = this.gleeGraph.AddEdge(parentHashCode, childHashCode);
                    if (result != null && result.Count > 0 && result[0] == j)
                    {
                        edge.Attr.Color = new Microsoft.Glee.Drawing.Color(
                            this.graphPanelMarkColor.A, 
                            this.graphPanelMarkColor.R, 
                            this.graphPanelMarkColor.G, 
                            this.graphPanelMarkColor.B);
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
                            this.NextGleeNodes(node.Neighbors[k], ref result);
                        }
                        else
                        {
                            List<int> nullResult = null;
                            this.NextGleeNodes(node.Neighbors[k], ref nullResult);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// The next glee nodes.
        /// </summary>
        /// <param name="node">
        /// The node.
        /// </param>
        /// <param name="result">
        /// The result.
        /// </param>
        private void NextGleeNodes(Node<string> node, ref List<int> result)
        {
            if (node == null || node.Values == null)
            {
                return;
            }

            string parentHashCode = node.GetHashCode().ToString();
            if (node.Neighbors != null && node.Neighbors.Count > 0)
            {
                var adoptedChildren = new List<int>();
                for (int j = 0; j < node.Neighbors.Count; j++)
                {
                    // Draw children
                    if (node.Neighbors[j] == null)
                    {
                        continue;
                    }

                    string childHashCode = node.Neighbors[j].GetHashCode().ToString();
                    Node child = this.gleeGraph.FindNode(childHashCode);
                    if (child == null)
                    {
                        string childText = string.Empty;
                        for (int i = 0; i < node.Neighbors[j].Values.Count; i++)
                        {
                            if (i > 0)
                            {
                                childText += " | ";
                            }

                            childText += node.Neighbors[j].Values[i];
                        }

                        child = this.gleeGraph.AddNode(childHashCode);
                        child.Attr.Label = childText;
                        if (result != null && result.Count > 0 && result[0] == j)
                        {
                            child.Attr.Color = new Microsoft.Glee.Drawing.Color(
                                this.graphPanelMarkColor.A, 
                                this.graphPanelMarkColor.R, 
                                this.graphPanelMarkColor.G, 
                                this.graphPanelMarkColor.B);
                            child.Attr.LineWidth = GraphPanelMarkLineWidth;
                        }
                        else
                        {
                            child.Attr.Color = new Microsoft.Glee.Drawing.Color(
                                node.Neighbors[j].NodeColor.A, 
                                node.Neighbors[j].NodeColor.R, 
                                node.Neighbors[j].NodeColor.G, 
                                node.Neighbors[j].NodeColor.B);
                        }
                    }
                    else
                    {
                        adoptedChildren.Add(j);
                    }

                    // Draw edge
                    Edge edge = this.gleeGraph.AddEdge(parentHashCode, childHashCode);
                    if (result != null && result.Count > 0 && result[0] == j)
                    {
                        edge.Attr.Color = new Microsoft.Glee.Drawing.Color(
                            this.graphPanelMarkColor.A, 
                            this.graphPanelMarkColor.R, 
                            this.graphPanelMarkColor.G, 
                            this.graphPanelMarkColor.B);
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
                            this.NextGleeNodes(node.Neighbors[k], ref result);
                        }
                        else
                        {
                            List<int> nullResult = null;
                            this.NextGleeNodes(node.Neighbors[k], ref nullResult);
                        }
                    }
                }
            }
        }

        #endregion
    }
}
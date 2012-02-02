// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Create.cs" company="Warsaw University of Technology">
//   
// </copyright>
// <summary>
//   The create.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForRest
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    using ForRest.Provider.BLL;

    using Microsoft.Glee.Drawing;
    using Microsoft.Glee.GraphViewerGdi;

    using Color = System.Drawing.Color;
    using Size = System.Drawing.Size;

    /// <summary>
    /// The create.
    /// </summary>
    public partial class Create : Form
    {
        #region Constants and Fields

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
        /// Initializes a new instance of the <see cref="Create"/> class.
        /// </summary>
        /// <param name="provider">
        /// The provider.
        /// </param>
        /// <param name="mode">
        /// The mode.
        /// </param>
        /// <param name="graphMode">
        /// The graph mode.
        /// </param>
        public Create(Provider.Provider provider, int mode, int graphMode)
        {
            this.InitializeComponent();
            this.provider = provider;
            this.Mode = mode;
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

        /// <summary>
        /// Gets or sets Mode.
        /// </summary>
        public int Mode { get; set; }

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

            this.ShowTree();
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
        /// The btn add node click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BtnAddNodeClick(object sender, EventArgs e)
        {
            if (this.comboBoxSelectTree.SelectedItem != null)
            {
                var treeObject = (TreeObject)this.comboBoxSelectTree.SelectedItem;
                if (treeObject.Type.Equals("text") && this.textBoxValue.Text != null)
                {
                    string textValue = this.textBoxValue.Text;
                    treeObject.TextTree.Add(textValue);
                    this.labelResult.ResetText();
                    this.labelResult.ForeColor = Color.Green;
                    this.labelResult.Text = "Action performed successfully.";
                    this.ShowTree();
                }
                else if (treeObject.Type.Equals("numeric") && this.textBoxValue.Text != null)
                {
                    double numericValue;
                    if (double.TryParse(
                        this.textBoxValue.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out numericValue))
                    {
                        treeObject.NumericTree.Add(numericValue);
                        this.labelResult.ResetText();
                        this.labelResult.ForeColor = Color.Green;
                        this.labelResult.Text = "Action performed successfully.";
                        this.ShowTree();
                    }
                    else
                    {
                        this.labelResult.ForeColor = Color.Red;
                        this.labelResult.Text = "Incorrect input format!";
                        this.toolTipHelperCreate.ToolTipTitle = "Incorrect input format";
                        this.toolTipHelperCreate.Show(
                            "Please provide apropriate input for selected tree.", this.textBoxValue, 3000);
                    }
                }
            }
            else
            {
                this.toolTipHelperCreate.ToolTipTitle = "No tree is selected";
                this.toolTipHelperCreate.Show(
                    "No tree is selected. Please select tree from list first.", this.comboBoxSelectTree, 3000);
            }
        }

        /// <summary>
        /// The btn add tree click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BtnAddTreeClick(object sender, EventArgs e)
        {
            var addTree = new AddTree(this.provider, false) { Owner = this };
            addTree.ShowDialog();
            this.VerifyLayout();
        }

        /// <summary>
        /// The btn add tree from file click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BtnAddTreeFromFileClick(object sender, EventArgs e)
        {
            if (this.provider.NumericData.Count != 0 || this.provider.TextData.Count != 0)
            {
                var addTree = new AddTree(this.provider, true, this.Mode) { Owner = this };
                addTree.ShowDialog();
            }
            else
            {
                var mainForm = (MainForm)this.MdiParent;
                var toolStrip = (ToolStrip)mainForm.Controls["toolStrip"];
                ToolStripItem toolStripBtnOpen = toolStrip.Items["toolStripBtnOpen"];
                this.toolTipHelperCreate.ToolTipTitle = "No file loaded";
                this.toolTipHelperCreate.Show(
                    "No file loaded. Please load file first.", 
                    toolStrip, 
                    toolStripBtnOpen.Image.Width, 
                    toolStripBtnOpen.Image.Height / 2, 
                    3000);
            }

            this.VerifyLayout();
        }

        /// <summary>
        /// The btn remove node click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BtnRemoveNodeClick(object sender, EventArgs e)
        {
            if (this.comboBoxSelectTree.SelectedItem != null)
            {
                var treeObject = (TreeObject)this.comboBoxSelectTree.SelectedItem;
                if (treeObject.Type.Equals("text") && this.textBoxValue.Text != null)
                {
                    string textValue = this.textBoxValue.Text;
                    bool result = treeObject.TextTree.Remove(textValue);
                    this.labelResult.ResetText();
                    if (result)
                    {
                        this.labelResult.ForeColor = Color.Green;
                        this.labelResult.Text = "Action performed successfully.";
                    }
                    else
                    {
                        this.labelResult.ForeColor = Color.Red;
                        this.labelResult.Text = "Item not found!";
                    }

                    this.ShowTree();
                }
                else if (treeObject.Type.Equals("numeric") && this.textBoxValue.Text != null)
                {
                    double numericValue;
                    if (double.TryParse(
                        this.textBoxValue.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out numericValue))
                    {
                        bool result = treeObject.NumericTree.Remove(numericValue);
                        this.labelResult.ResetText();
                        if (result)
                        {
                            this.labelResult.ForeColor = Color.Green;
                            this.labelResult.Text = "Action performed successfully.";
                        }
                        else
                        {
                            this.labelResult.ForeColor = System.Drawing.Color.Red;
                            this.labelResult.Text = "Item not found!";
                        }

                        this.ShowTree();
                    }
                    else
                    {
                        this.labelResult.ForeColor = Color.Red;
                        this.labelResult.Text = "Incorrect input format!";
                        this.toolTipHelperCreate.ToolTipTitle = "Incorrect input format";
                        this.toolTipHelperCreate.Show(
                            "Please provide apropriate input for selected tree.", this.textBoxValue, 3000);
                    }
                }
            }
            else
            {
                this.toolTipHelperCreate.ToolTipTitle = "No tree is selected";
                this.toolTipHelperCreate.Show(
                    "No tree is selected. Please select tree from list first.", this.comboBoxSelectTree, 3000);
            }
        }

        /// <summary>
        /// The btn remove tree click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BtnRemoveTreeClick(object sender, EventArgs e)
        {
            if (this.comboBoxSelectTree.SelectedItem != null)
            {
                var treeObject = (TreeObject)this.comboBoxSelectTree.SelectedItem;
                this.provider.TreeObjects.Remove(treeObject);
                this.FillSelectedTreeComboBox();
                this.labelResult.ResetText();
                this.labelResult.ForeColor = Color.Green;
                this.labelResult.Text = "Action performed successfully.";
            }
            else
            {
                this.toolTipHelperCreate.ToolTipTitle = "No tree is selected";
                this.toolTipHelperCreate.Show(
                    "No tree is selected. Please select tree from list first.", this.comboBoxSelectTree, 3000);
            }

            this.VerifyLayout();
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
            this.VerifyLayout();
        }

        /// <summary>
        /// The create form closing.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void CreateFormClosing(object sender, FormClosingEventArgs e)
        {
            var mainForm = (MainForm)this.MdiParent;
            mainForm.CreateClosing();
        }

        /// <summary>
        /// The create resize.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void CreateResize(object sender, EventArgs e)
        {
            this.ShowTree();
        }

        /// <summary>
        /// The verify layout.
        /// </summary>
        private void VerifyLayout()
        {
            this.ShowTree();
            if (this.comboBoxSelectTree.SelectedItem == null)
            {
                this.btnRemoveTree.Enabled = false;
                this.btnAddNode.Enabled = false;
                this.btnRemoveNode.Enabled = false;
            }
            else
            {
                this.btnRemoveTree.Enabled = true;
                this.btnAddNode.Enabled = true;
                this.btnRemoveNode.Enabled = true;
            }
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
        private void ShowTree()
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
                    this.DrawGraph();
                    break;
                case 3:
                    this.DrawGleeGraph();
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
        private void DrawGraph()
        {
            this.graphPanel.Controls.Clear();
            if (this.comboBoxSelectTree.SelectedItem == null)
            {
                return;
            }
            var treeObject = (TreeObject)this.comboBoxSelectTree.SelectedItem;
            if (treeObject.Type.Equals("text"))
            {
                ITree<string> iTree = treeObject.TextTree;
                var rootRectangle = new Rectangle(5, 5, this.graphPanel.Width - 27, 24);
                NextControls(rootRectangle, 0, iTree.Root);
            }
            else if (treeObject.Type.Equals("numeric"))
            {
                ITree<double> iTree = treeObject.NumericTree;
                var rootRectangle = new Rectangle(5, 5, this.graphPanel.Width - 27, 24);
                NextControls(rootRectangle, 0, iTree.Root);
            }
        }
        
        /// <summary>
        /// The draw glee graph.
        /// </summary>
        private void DrawGleeGraph()
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
                root.Attr.Color = new Microsoft.Glee.Drawing.Color(
                    iTreeRoot.NodeColor.A, iTreeRoot.NodeColor.R, iTreeRoot.NodeColor.G, iTreeRoot.NodeColor.B);
                this.NextGleeNodes(iTreeRoot);
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
                root.Attr.Color = new Microsoft.Glee.Drawing.Color(
                    iTreeRoot.NodeColor.A, iTreeRoot.NodeColor.R, iTreeRoot.NodeColor.G, iTreeRoot.NodeColor.B);
                NextGleeNodes(iTreeRoot);
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
        private void NextControls(Rectangle rectangle, int levelBlank, Node<double> node)
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
            var ucn = new UserControlNode(text, node.NodeInfo, rectangle, node.NodeColor);
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

                    var uce = new UserControlEdge(ltr) { Location = e.Location, Size = e.Size };
                    this.graphPanel.Controls.Add(uce);
                    notNullChildrenIndex++;

                    // Draw child
                    if (drawChild == -1)
                    {
                        this.NextControls(r, blank, node.Neighbors[j]);
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
        private void NextControls(Rectangle rectangle, int levelBlank, Node<string> node)
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
            var ucn = new UserControlNode(text, node.NodeInfo, rectangle, node.NodeColor)
                { Location = rectangle.Location, Size = rectangle.Size };
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

                    var uce = new UserControlEdge(ltr) { Location = e.Location, Size = e.Size };
                    this.graphPanel.Controls.Add(uce);
                    notNullChildrenIndex++;

                    // Draw child
                    if (drawChild == -1)
                    {
                        this.NextControls(r, blank, node.Neighbors[j]);
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
        private void NextGleeNodes(Node<double> node)
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
                        child.Attr.Color = new Microsoft.Glee.Drawing.Color(
                            node.Neighbors[j].NodeColor.A, 
                            node.Neighbors[j].NodeColor.R, 
                            node.Neighbors[j].NodeColor.G, 
                            node.Neighbors[j].NodeColor.B);
                    }
                    else
                    {
                        adoptedChildren.Add(j);
                    }

                    // Draw edge
                    this.gleeGraph.AddEdge(parentHashCode, childHashCode);
                }

                for (int k = 0; k < node.Neighbors.Count; k++)
                {
                    if (!adoptedChildren.Contains(k))
                    {
                        this.NextGleeNodes(node.Neighbors[k]);
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
        private void NextGleeNodes(Node<string> node)
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
                        child.Attr.Color = new Microsoft.Glee.Drawing.Color(
                            node.Neighbors[j].NodeColor.A, 
                            node.Neighbors[j].NodeColor.R, 
                            node.Neighbors[j].NodeColor.G, 
                            node.Neighbors[j].NodeColor.B);
                    }
                    else
                    {
                        adoptedChildren.Add(j);
                    }

                    // Draw edge
                    this.gleeGraph.AddEdge(parentHashCode, childHashCode);
                }

                for (int k = 0; k < node.Neighbors.Count; k++)
                {
                    if (!adoptedChildren.Contains(k))
                    {
                        this.NextGleeNodes(node.Neighbors[k]);
                    }
                }
            }
        }

        #endregion
    }
}
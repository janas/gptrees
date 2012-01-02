using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using System.Drawing;
using ForRest.Provider.BLL;

namespace ForRest
{
    public partial class Create : Form
    {
        private readonly Provider.Provider _provider;
        private TreeView _treeViewCreate;
        private Panel _graphPanel;

        public int Mode { get; set; }
        public int GraphMode { get; set; }

        public Create(Provider.Provider provider, int mode, int graphMode)
        {
            InitializeComponent();
            _provider = provider;
            Mode = mode;
            GraphMode = graphMode;
            FillSelectedTreeComboBox();
            if (GraphMode == 0)
            {
                InitializeTreeView();
            }
            else
            {
                InitializeGraph();
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
                    InitializeTreeView();
                    break;
                case 1:
                    if (_treeViewCreate != null)
                    {
                        Controls.Remove(_treeViewCreate);
                    }
                    InitializeGraph();
                    break;
            }
            ShowTree();
        }

        private void BtnAddNodeClick(object sender, EventArgs e)
        {
            if (comboBoxSelectTree.SelectedItem != null)
            {
                var treeObject = (TreeObject) comboBoxSelectTree.SelectedItem;
                if (treeObject.Type.Equals("text") && textBoxValue.Text != null)
                {
                    string textValue = textBoxValue.Text;
                    treeObject.TextTree.Add(textValue);
                    labelResult.ResetText();
                    labelResult.ForeColor = Color.Green;
                    labelResult.Text = "Action performed successfully.";
                    ShowTree();
                }
                else if (treeObject.Type.Equals("numeric") && textBoxValue.Text != null)
                {
                    double numericValue;
                    if (double.TryParse(textBoxValue.Text, NumberStyles.Any, CultureInfo.InvariantCulture,
                                        out numericValue))
                    {
                        treeObject.NumericTree.Add(numericValue);
                        labelResult.ResetText();
                        labelResult.ForeColor = Color.Green;
                        labelResult.Text = "Action performed successfully.";
                        ShowTree();
                    }
                    else
                    {
                        labelResult.ForeColor = Color.Red;
                        labelResult.Text = "Incorrect input format!";
                        toolTipHelperCreate.ToolTipTitle = "Incorrect input format";
                        toolTipHelperCreate.Show("Please provide apropriate input for selected tree.", textBoxValue,
                                                 3000);
                    }
                }
            }
            else
            {
                toolTipHelperCreate.ToolTipTitle = "No tree is selected";
                toolTipHelperCreate.Show("No tree is selected. Please select tree from list first.", comboBoxSelectTree,
                                         3000);
            }
        }

        private void BtnRemoveNodeClick(object sender, EventArgs e)
        {
            if (comboBoxSelectTree.SelectedItem != null)
            {
                var treeObject = (TreeObject) comboBoxSelectTree.SelectedItem;
                if (treeObject.Type.Equals("text") && textBoxValue.Text != null)
                {
                    string textValue = textBoxValue.Text;
                    bool result = treeObject.TextTree.Remove(textValue);
                    labelResult.ResetText();
                    if (result)
                    {
                        labelResult.ForeColor = Color.Green;
                        labelResult.Text = "Action performed successfully.";
                    }
                    else
                    {
                        labelResult.ForeColor = Color.Red;
                        labelResult.Text = "Item not found!";
                    }
                    ShowTree();
                }
                else if (treeObject.Type.Equals("numeric") && textBoxValue.Text != null)
                {
                    double numericValue;
                    if (double.TryParse(textBoxValue.Text, NumberStyles.Any, CultureInfo.InvariantCulture,
                                        out numericValue))
                    {
                        bool result = treeObject.NumericTree.Remove(numericValue);
                        labelResult.ResetText();
                        if (result)
                        {
                            labelResult.ForeColor = Color.Green;
                            labelResult.Text = "Action performed successfully.";
                        }
                        else
                        {
                            labelResult.ForeColor = System.Drawing.Color.Red;
                            labelResult.Text = "Item not found!";
                        }
                        ShowTree();
                    }
                    else
                    {
                        labelResult.ForeColor = Color.Red;
                        labelResult.Text = "Incorrect input format!";
                        toolTipHelperCreate.ToolTipTitle = "Incorrect input format";
                        toolTipHelperCreate.Show("Please provide apropriate input for selected tree.", textBoxValue,
                                                 3000);
                    }
                }
            }
            else
            {
                toolTipHelperCreate.ToolTipTitle = "No tree is selected";
                toolTipHelperCreate.Show("No tree is selected. Please select tree from list first.", comboBoxSelectTree,
                                         3000);
            }
        }

        private void BtnAddTreeFromFileClick(object sender, EventArgs e)
        {
            if (_provider.NumericData.Count != 0 || _provider.TextData.Count != 0)
            {
                var addTree = new AddTree(_provider, true, Mode) {Owner = this};
                addTree.ShowDialog();
            }
            else
                MessageBox.Show("No file is loaded. Please load file with data first.", "Error!", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
        }

        private void BtnAddTreeClick(object sender, EventArgs e)
        {
            var addTree = new AddTree(_provider, false) {Owner = this};
            addTree.ShowDialog();
        }

        private void BtnRemoveTreeClick(object sender, EventArgs e)
        {
            if (comboBoxSelectTree.SelectedItem != null)
            {
                var treeObject = (TreeObject) comboBoxSelectTree.SelectedItem;
                _provider.TreeObjects.Remove(treeObject);
                FillSelectedTreeComboBox();
                labelResult.ResetText();
                labelResult.ForeColor = Color.Green;
                labelResult.Text = "Action performed successfully.";
            }
            else
            {
                toolTipHelperCreate.ToolTipTitle = "No tree is selected";
                toolTipHelperCreate.Show("No tree is selected. Please select tree from list first.", comboBoxSelectTree,
                                         3000);
            }
        }

        private void ComboBoxSelectTreeSelectedIndexChanged(object sender, EventArgs e)
        {
            ShowTree();
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

        private void InitializeTreeView()
        {
            _treeViewCreate = new TreeView
                                  {
                                      Anchor = ((AnchorStyles.Top | AnchorStyles.Bottom)
                                                | AnchorStyles.Left)
                                               | AnchorStyles.Right,
                                      Location = new Point(195, 16),
                                      Name = "treeViewCreate",
                                      Size = new Size(Width-220, Height-65),
                                  };
            Controls.Add(_treeViewCreate);
        }

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

        private void ShowTree()
        {
            if (GraphMode == 0)
                ShowTreeView();
            else
                DrawGraph();
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

        private void NextControls(Rectangle rectangle, int levelBlank, Node<double> node)
        {
            if (node == null)
                return;
            var text = new List<string>();
            for (int i = 0; i < node.Values.Count; i++)
                text.Add(node.Values[i].ToString());

            // Draw parent
            var ucn = new UserControlNode(text, node.NodeInfo, rectangle, false);
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
                int rWidth = 0;
                if (notNullChildren > 1)
                    blank = ucn.GetMyArea().Width / (10 * (notNullChildren - 1));
                if (notNullChildren > 0)
                    rWidth = (ucn.GetMyArea().Width - (notNullChildren - 1) * blank)
                             / notNullChildren;
                if (rWidth < 3)
                    rWidth = 3;
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
                    var from = new Point(
                        ucn.Location.X + j *
                        (ucn.Width / node.Values.Count),
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
                    var uce = new UserControlEdge(ltr, false);
                    uce.Location = e.Location;
                    uce.Size = e.Size;
                    _graphPanel.Controls.Add(uce);
                    if (uce._mark)
                        _graphPanel.Controls.SetChildIndex(uce, 0);
                    notNullChildrenIndex++;

                    // Draw child
                    if (drawChild == -1)
                    {
                        NextControls(r, blank, node.Neighbors[j]);
                    }
                }
            }
            _graphPanel.Controls.Add(ucn);
        }

        private void NextControls(Rectangle rectangle, int levelBlank, Node<string> node)
        {
            if (node == null)
                return;
            var text = new List<string>();
            for (int i = 0; i < node.Values.Count; i++)
                text.Add(node.Values[i].ToString());

            // Draw parent
            var ucn = new UserControlNode(text, node.NodeInfo, rectangle, false);
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
                int rWidth = (ucn.GetMyArea().Width - (notNullChildren - 1) * blank)
                             / notNullChildren;
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
                    var from = new Point(
                        ucn.Location.X + j *
                        (ucn.Width / node.Values.Count),
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
                    var uce = new UserControlEdge(ltr, false);
                    uce.Location = e.Location;
                    uce.Size = e.Size;
                    _graphPanel.Controls.Add(uce);
                    if (uce._mark)
                        _graphPanel.Controls.SetChildIndex(uce, 0);
                    notNullChildrenIndex++;

                    // Draw child
                    if (drawChild == -1)
                    {
                        NextControls(r, blank, node.Neighbors[j]);
                    }
                }
            }
            _graphPanel.Controls.Add(ucn);
        }

        private void DrawGraph()
        {
            _graphPanel.Controls.Clear();
            if (comboBoxSelectTree.SelectedItem == null) return;
            var treeObject = (TreeObject) comboBoxSelectTree.SelectedItem;
            if (treeObject.Type.Equals("text"))
            {
                ITree<string> iTree = treeObject.TextTree;
                var rootRectangle = new Rectangle(5, 5, _graphPanel.Width - 10, 24);
                NextControls(rootRectangle, 0, iTree.Root);
            }
            else if (treeObject.Type.Equals("numeric"))
            {
                ITree<double> iTree = treeObject.NumericTree;
                var rootRectangle = new Rectangle(5, 5, _graphPanel.Width - 10, 24);
                NextControls(rootRectangle, 0, iTree.Root);
            }
        }

        private void CreateResize(object sender, EventArgs e)
        {
            ShowTree();
        }

        private void Create_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm mainForm = (MainForm)MdiParent;
            mainForm.CreateClosing();
        }
    }
}

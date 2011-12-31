using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using ForRest.Provider.BLL;

namespace ForRest
{
    public partial class Search : Form
    {
        private readonly Provider.Provider _provider;
        private TreeView _treeViewCreate;
        private Panel _graphPanel;

        public int GraphMode { get; set; }

        public Search(Provider.Provider provider, int graphMode)
        {
            InitializeComponent();
            _provider = provider;
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
            List<int> result = null;
            ShowTree(ref result);
        }

        private void ShowTree(ref List<int> result)
        {
            if (GraphMode == 0)
                ShowTreeView();
            else
                DrawGraph(ref result);
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
                var rootRectangle = new Rectangle(5, 5, _graphPanel.Width - 10, 24);
                NextControls(rootRectangle, iTree.Root, ref result, -1);
            }
            else if (treeObject.Type.Equals("numeric"))
            {
                ITree<double> iTree = treeObject.NumericTree;
                var rootRectangle = new Rectangle(5, 5, _graphPanel.Width - 10, 24);
                NextControls(rootRectangle, iTree.Root, ref result, -1);
            }
        }

        private void NextControls(Rectangle rectangle, Node<double> node,
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
                ucn = new UserControlNode(text, rectangle, true);
                if (nodeIndex > -1)
                {
                    if (result.Count < 2)
                        result = null;
                    else
                        result.RemoveAt(0);
                }
            }
            else
                ucn = new UserControlNode(text, rectangle, false);
            ucn.Location = rectangle.Location;
            ucn.Size = rectangle.Size;
            ucn.verifySize();

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
                    blank = ucn.GetMyArea().Width/(10*(notNullChildren - 1));
                int rWidth = (ucn.GetMyArea().Width - (notNullChildren - 1)*blank)
                             /notNullChildren;
                for (int j = 0; j < node.Neighbors.Count; j++)
                {
                    if (node.Neighbors[j] == null)
                    {
                        continue;
                    }
                    var r = new Rectangle(
                        ucn.GetMyArea().X + notNullChildrenIndex*(rWidth + blank),
                        ucn.GetMyArea().Y + ucn.GetMyArea().Height*2,
                        rWidth,
                        ucn.GetMyArea().Height);

                    // Drawing edge
                    var from = new Point(
                        ucn.Location.X + j*
                        (ucn.Width/node.Values.Count),
                        ucn.Location.Y + ucn.Height);
                    var to = new Point(
                        r.Location.X + r.Width/2,
                        r.Location.Y);
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
                    if (result != null && result.Count > 0 && result[0] == j)
                        uce = new UserControlEdge(ltr, true);
                    else
                        uce = new UserControlEdge(ltr, false);
                    uce.Location = e.Location;
                    uce.Size = e.Size;
                    _graphPanel.Controls.Add(uce);
                    notNullChildrenIndex++;

                    // Draw child
                    if (result != null && result.Count > 0 && result[0] == j)
                        NextControls(r, node.Neighbors[j], ref result, j);
                    else
                    {
                        // Dumb C#... I have to pass it this way
                        List<int> nullResult = null;
                        NextControls(r, node.Neighbors[j], ref nullResult, j);
                    }
                }
            }
            _graphPanel.Controls.Add(ucn);
        }

        private void NextControls(Rectangle rectangle, Node<string> node,
                                  ref List<int> result, int nodeIndex)
        {
            if (node == null)
                return;
            var text = new List<string>();
            for (int i = 0; i < node.Values.Count; i++)
                text.Add(node.Values[i]);

            // Draw parent
            UserControlNode ucn;
            if (result != null &&
                (result.Count == 0 || result[0] == nodeIndex || nodeIndex == -1))
            {
                ucn = new UserControlNode(text, rectangle, true);
                if (nodeIndex > -1)
                {
                    if (result.Count < 2)
                        result = null;
                    else
                        result.RemoveAt(0);
                }
            }
            else
                ucn = new UserControlNode(text, rectangle, false);
            ucn.Location = rectangle.Location;
            ucn.Size = rectangle.Size;
            ucn.verifySize();

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
                    blank = ucn.GetMyArea().Width/(10*(notNullChildren - 1));
                int rWidth = (ucn.GetMyArea().Width - (notNullChildren - 1)*blank)
                             /notNullChildren;
                for (int j = 0; j < node.Neighbors.Count; j++)
                {
                    if (node.Neighbors[j] == null)
                    {
                        continue;
                    }
                    var r = new Rectangle(
                        ucn.GetMyArea().X + notNullChildrenIndex*(rWidth + blank),
                        ucn.GetMyArea().Y + ucn.GetMyArea().Height*2,
                        rWidth,
                        ucn.GetMyArea().Height);

                    // Drawing edge
                    var from = new Point(
                        ucn.Location.X + j*
                        (ucn.Width/node.Values.Count),
                        ucn.Location.Y + ucn.Height);
                    var to = new Point(
                        r.Location.X + r.Width/2,
                        r.Location.Y);
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
                    if (result != null && result.Count > 0 && result[0] == j)
                        uce = new UserControlEdge(ltr, true);
                    else
                        uce = new UserControlEdge(ltr, false);
                    uce.Location = e.Location;
                    uce.Size = e.Size;
                    _graphPanel.Invoke((MethodInvoker) (() => _graphPanel.Controls.Add(uce)));
                    notNullChildrenIndex++;

                    // Draw child
                    if (result != null && result.Count > 0 && result[0] == j)
                        NextControls(r, node.Neighbors[j], ref result, j);
                    else
                    {
                        // Dumb C#... I have to pass it this way
                        List<int> nullResult = null;
                        NextControls(r, node.Neighbors[j], ref nullResult, j);
                    }
                }
            }
            _graphPanel.Invoke((MethodInvoker) (() => _graphPanel.Controls.Add(ucn)));
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
                                      Size = new Size(377, 434),
                                      TabIndex = 5
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
                                  Size = new Size(377, 434),
                                  TabIndex = 5,
                                  AutoScroll = true
                              };
            Controls.Add(_graphPanel);
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
                    peroformanceSet.TypeOfTree = treeObject.TextTree.GetType().ToString();
                    peroformanceSet.NoOfNodes = "notImplemented";
                    _provider.PerformanceSets.Add(peroformanceSet);
                }
                else
                {
                    var peroformanceSet = new PerformanceSet();
                    labelTime.Invoke((MethodInvoker) (() => labelTime.ResetText()));
                    labelTime.Invoke((MethodInvoker) (() => labelTime.Text = watch.ElapsedMilliseconds + " ms/Error"));
                    peroformanceSet.TreeName = treeObject.Name;
                    peroformanceSet.SearchTime = watch.ElapsedMilliseconds + "/Not Found";
                    peroformanceSet.TypeOfNodes = "String";
                    peroformanceSet.TypeOfTree = treeObject.TextTree.GetType().ToString();
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
                        peroformanceSet.TypeOfTree = treeObject.NumericTree.GetType().ToString();
                        peroformanceSet.NoOfNodes = "notImplemented";
                        _provider.PerformanceSets.Add(peroformanceSet);
                    }
                    else
                    {
                        var peroformanceSet = new PerformanceSet();
                        labelTime.Invoke((MethodInvoker) (() => labelTime.ResetText()));
                        labelTime.Invoke(
                            (MethodInvoker) (() => labelTime.Text = watch.ElapsedMilliseconds + " ms/Error"));
                        peroformanceSet.TreeName = treeObject.Name;
                        peroformanceSet.SearchTime = watch.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture) +
                                                     "/Not Found";
                        peroformanceSet.TypeOfNodes = "Double";
                        peroformanceSet.TypeOfTree = treeObject.NumericTree.GetType().ToString();
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
        }
    }
}

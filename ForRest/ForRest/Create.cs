using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using ForRest.Provider.BLL;

namespace ForRest
{
    public partial class Create : Form
    {
        private readonly Provider.Provider _provider;

        public int Mode { get; set; }
        public int GraphMode { get; set; }

        public Create(Provider.Provider provider, int mode, int graphMode)
        {
            InitializeComponent();
            _provider = provider;
            Mode = mode;
            GraphMode = graphMode;
            FillSelectedTreeComboBox();
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
                    labelResult.ForeColor = System.Drawing.Color.Green;
                    labelResult.Text = "Action performed successfully.";
                    ShowTree();
                }
                else if (treeObject.Type.Equals("numeric") && textBoxValue.Text != null)
                {
                    double numericValue = double.Parse(textBoxValue.Text, NumberStyles.Any, CultureInfo.InvariantCulture);
                    treeObject.NumericTree.Add(numericValue);
                    labelResult.ResetText();
                    labelResult.ForeColor = System.Drawing.Color.Green;
                    labelResult.Text = "Action performed successfully.";
                    ShowTree();
                }
            }
            else
                MessageBox.Show("No tree is selected. Please select tree from list first.", "Error!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        labelResult.ForeColor = System.Drawing.Color.Green;
                        labelResult.Text = "Action performed successfully.";
                    }
                    else
                    {
                        labelResult.ForeColor = System.Drawing.Color.Red;
                        labelResult.Text = "Item not found!";
                    }
                    ShowTree();
                }
                else if (treeObject.Type.Equals("numeric") && textBoxValue.Text != null)
                {
                    double numericValue = double.Parse(textBoxValue.Text, NumberStyles.Any, CultureInfo.InvariantCulture);
                    bool result = treeObject.NumericTree.Remove(numericValue);
                    labelResult.ResetText();
                    if (result)
                    {
                        labelResult.ForeColor = System.Drawing.Color.Green;
                        labelResult.Text = "Action performed successfully.";
                    }
                    else
                    {
                        labelResult.ForeColor = System.Drawing.Color.Red;
                        labelResult.Text = "Item not found!";
                    }
                    ShowTree();
                }
            }
            else
                MessageBox.Show("No tree is selected. Please select tree from list first.", "Error!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                labelResult.ForeColor = System.Drawing.Color.Green;
                labelResult.Text = "Action performed successfully.";
            }
            else
                MessageBox.Show("No tree is selected. Please select tree from list first.", "Error!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            List<TreeNode> resultList = new List<TreeNode>();
            foreach (Node<string> n in nodeList)
            {
                if (n == null)
                    continue;
                var print = "";
                for (int i = 0; i < n.Values.Count; i++)
                    print += n.Values[i] + " ";
                TreeNode[] whatever = NextLevel(n);
                if (whatever == null)
                    resultList.Add(new TreeNode(print));
                else
                    resultList.Add(new TreeNode(print, whatever));
            }
            resultList.RemoveAll(item => item == null);
            TreeNode[] result = new TreeNode[resultList.Count];
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
            List<TreeNode> resultList = new List<TreeNode>();
            foreach (Node<double> n in nodeList)
            {
                if (n == null)
                    continue;
                var print = "";
                for (int i = 0; i < n.Values.Count; i++)
                    print += n.Values[i].ToString() + " ";
                TreeNode[] whatever = NextLevel(n);
                if (whatever == null)
                    resultList.Add(new TreeNode(print));
                else
                    resultList.Add(new TreeNode(print, whatever));
            }
            resultList.RemoveAll(item => item == null);
            TreeNode[] result = new TreeNode[resultList.Count];
            resultList.CopyTo(result);
            return result;
        }

        private void ShowTree()
        {
            treeViewCreate.Nodes.Clear();
            if (comboBoxSelectTree.SelectedItem == null) return;
            var treeObject = (TreeObject) comboBoxSelectTree.SelectedItem;
            if (treeObject.Type.Equals("text"))
            {
                ITree<string> iTree = treeObject.TextTree;
                TreeNode tn = NextLevel(iTree.Root) == null
                                  ? new TreeNode(iTree.Root.Values[0])
                                  : new TreeNode(iTree.Root.Values[0], NextLevel(iTree.Root));
                treeViewCreate.Nodes.Add(tn);
                treeViewCreate.ExpandAll();
            }
            else if (treeObject.Type.Equals("numeric"))
            {
                ITree<double> iTree = treeObject.NumericTree;
                TreeNode tn = NextLevel(iTree.Root) == null
                                  ? new TreeNode(iTree.Root.Values[0].ToString())
                                  : new TreeNode(iTree.Root.Values[0].ToString(), NextLevel(iTree.Root));
                treeViewCreate.Nodes.Add(tn);
                treeViewCreate.ExpandAll();
            }
        }

        private void DrawGraph()
        {
            //todo
        }
    }
}

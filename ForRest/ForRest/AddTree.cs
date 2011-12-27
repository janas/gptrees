using System;
using System.Drawing;
using System.Windows.Forms;
using ForRest.Provider.BLL;

namespace ForRest
{
    public partial class AddTree : Form
    {
        private readonly Provider.Provider _provider;
        private readonly bool _fromFile;
        private readonly bool _group;
        private readonly int _mode;
        private int _noOfTrees;
        private string _typeOfTrees;
        private string _groupTreeName;

        public AddTree(Provider.Provider provider, bool fromFile)
        {
            InitializeComponent();
            InitializeAdd();
            _provider = provider;
            _fromFile = fromFile;
            FillTrees();
        }

        public AddTree(Provider.Provider provider, bool fromFile, int mode)
        {
            InitializeComponent();
            _provider = provider;
            _fromFile = fromFile;
            _mode = mode;
            FillTrees();
        }

        public AddTree(Provider.Provider provider, bool fromFile, int mode, bool group)
        {
            InitializeComponent();
            _provider = provider;
            _fromFile = fromFile;
            _mode = mode;
            _group = @group;
            FillTrees();
        }

        private void InitializeAdd()
        {
            Width = 315;
            labelDataType.Visible = true;
            comboBoxDataType.Visible = true;
            btnAdd.Location = new Point(86, 124);
        }

        private void FillTrees()
        {
            string applicationPath = Application.ExecutablePath;
            _provider.CreatePluginList(applicationPath);
            comboBoxAvailableTrees.DataSource = _provider.PluginList;
            comboBoxAvailableTrees.DisplayMember = "Name";
        }

        private void ShowToolTip()
        {
            toolTipHelper.ToolTipTitle = "Please enter tree degree";
            toolTipHelper.Show("Please key in tree degree first.", maskedTextBoxDegree, 3000);
        }

        private bool PerformAdd()
        {
            var factory = (ITreeFactory) comboBoxAvailableTrees.SelectedItem;
            switch (factory.NeedDegree)
            {
                case true:
                    //from file, group
                    if (_fromFile && _group)
                    {
                        if (_mode == 0 && _provider.BatchTextData != null &&
                            !string.IsNullOrEmpty(maskedTextBoxDegree.Text))
                        {
                            _noOfTrees = 0;
                            foreach (var batchTree in _provider.BatchTextData)
                            {
                                ITree<string> tree = factory.GetTree<string>(int.Parse(maskedTextBoxDegree.Text));
                                foreach (var node in batchTree)
                                {
                                    tree.Add(node);
                                }
                                var treeObject = new TreeObject(textBoxName.Text.Trim(), "text", tree);
                                _provider.BatchTreeObject.Add(treeObject);
                                _typeOfTrees = treeObject.Type;
                                _noOfTrees++;
                            }
                            _groupTreeName = textBoxName.Text.Trim();
                            return true;
                        }
                        if (_mode == 1 && _provider.BatchNumericData != null &&
                            !string.IsNullOrEmpty(maskedTextBoxDegree.Text))
                        {
                            _noOfTrees = 0;
                            foreach (var batchTree in _provider.BatchNumericData)
                            {
                                ITree<double> tree = factory.GetTree<double>(int.Parse(maskedTextBoxDegree.Text));
                                foreach (var node in batchTree)
                                {
                                    tree.Add(node);
                                }
                                var treeObject = new TreeObject(textBoxName.Text.Trim(), "numeric", tree);
                                _provider.BatchTreeObject.Add(treeObject);
                                _typeOfTrees = treeObject.Type;
                                _noOfTrees++;
                            }
                            _groupTreeName = textBoxName.Text.Trim();
                            return true;
                        }
                        if (string.IsNullOrEmpty(maskedTextBoxDegree.Text))
                        {
                            toolTipHelper.ToolTipTitle = "No degree is entered";
                            toolTipHelper.Show("Please key in tree degree first.", maskedTextBoxDegree, 3000);
                            return false;
                        }
                    }
                    //from file, not group
                    if (_fromFile && !_group)
                    {
                        if (_mode == 0 && _provider.TextData != null && !string.IsNullOrEmpty(maskedTextBoxDegree.Text))
                        {
                            ITree<string> tree = factory.GetTree<string>(int.Parse(maskedTextBoxDegree.Text));
                            foreach (var node in _provider.TextData)
                            {
                                tree.Add(node);
                            }
                            var treeObject = new TreeObject(textBoxName.Text.Trim(), "text", tree);
                            _provider.TreeObjects.Add(treeObject);
                            return true;
                        }
                        if (_mode == 1 && _provider.NumericData != null &&
                            !string.IsNullOrEmpty(maskedTextBoxDegree.Text))
                        {
                            ITree<double> tree = factory.GetTree<double>(int.Parse(maskedTextBoxDegree.Text));
                            foreach (var node in _provider.NumericData)
                            {
                                tree.Add(node);
                            }
                            var treeObject = new TreeObject(textBoxName.Text.Trim(), "numeric", tree);
                            _provider.TreeObjects.Add(treeObject);
                            return true;
                        }
                        if (string.IsNullOrEmpty(maskedTextBoxDegree.Text))
                        {
                            toolTipHelper.ToolTipTitle = "No degree is entered";
                            toolTipHelper.Show("Please key in tree degree first.", maskedTextBoxDegree, 3000);
                            return false;
                        }
                    }
                        //not from file, not group
                    else if (!_fromFile && !_group)
                    {
                        if (comboBoxDataType.SelectedItem != null && !string.IsNullOrEmpty(textBoxName.Text) &&
                            !string.IsNullOrEmpty(maskedTextBoxDegree.Text))
                        {
                            int mode = comboBoxDataType.SelectedIndex;
                            switch (mode)
                            {
                                case 0:
                                    ITree<string> textTree = factory.GetTree<string>(int.Parse(maskedTextBoxDegree.Text));
                                    var treeObjectText = new TreeObject(textBoxName.Text.Trim(), "text", textTree);
                                    _provider.TreeObjects.Add(treeObjectText);
                                    break;
                                case 1:
                                    ITree<double> numericTree =
                                        factory.GetTree<double>(int.Parse(maskedTextBoxDegree.Text));
                                    var treeObjectNumeric = new TreeObject(textBoxName.Text.Trim(), "numeric",
                                                                           numericTree);
                                    _provider.TreeObjects.Add(treeObjectNumeric);
                                    break;
                            }
                            return true;
                        }
                        if (comboBoxDataType.SelectedItem == null)
                        {
                            toolTipHelper.ToolTipTitle = "No data type is selected";
                            toolTipHelper.Show("Please select data type from the list first.", comboBoxDataType, 3000);
                            return false;
                        }
                        if (string.IsNullOrEmpty(maskedTextBoxDegree.Text))
                        {
                            toolTipHelper.ToolTipTitle = "No degree is entered";
                            toolTipHelper.Show("Please key in tree degree first.", maskedTextBoxDegree, 3000);
                            return false;
                        }
                    }
                    break;
                case false:
                    //from file, group
                    if (_fromFile && _group)
                    {
                        if (_mode == 0 && _provider.BatchTextData != null)
                        {
                            _noOfTrees = 0;
                            foreach (var batchTree in _provider.BatchTextData)
                            {
                                ITree<string> tree = factory.GetTree<string>();
                                foreach (var node in batchTree)
                                {
                                    tree.Add(node);
                                }
                                var treeObject = new TreeObject(textBoxName.Text.Trim(), "text", tree);
                                _provider.BatchTreeObject.Add(treeObject);
                                _typeOfTrees = treeObject.Type;
                                _noOfTrees++;
                            }
                            _groupTreeName = textBoxName.Text.Trim();
                            return true;
                        }
                        if (_mode == 1 && _provider.BatchNumericData != null)
                        {
                            _noOfTrees = 0;
                            foreach (var batchTree in _provider.BatchNumericData)
                            {
                                ITree<double> tree = factory.GetTree<double>();
                                foreach (var node in batchTree)
                                {
                                    tree.Add(node);
                                }
                                var treeObject = new TreeObject(textBoxName.Text.Trim(), "numeric", tree);
                                _provider.BatchTreeObject.Add(treeObject);
                                _typeOfTrees = treeObject.Type;
                                _noOfTrees++;
                            }
                            _groupTreeName = textBoxName.Text.Trim();
                            return true;
                        }
                    }
                    //from file, not group
                    if (_fromFile && !_group)
                    {
                        if (_mode == 0 && _provider.TextData != null)
                        {
                            ITree<string> tree = factory.GetTree<string>();
                            foreach (var node in _provider.TextData)
                            {
                                tree.Add(node);
                            }
                            var treeObject = new TreeObject(textBoxName.Text.Trim(), "text", tree);
                            _provider.TreeObjects.Add(treeObject);
                            return true;
                        }
                        if (_mode == 1 && _provider.NumericData != null)
                        {
                            ITree<double> tree = factory.GetTree<double>();
                            foreach (var node in _provider.NumericData)
                            {
                                tree.Add(node);
                            }
                            var treeObject = new TreeObject(textBoxName.Text.Trim(), "numeric", tree);
                            _provider.TreeObjects.Add(treeObject);
                            return true;
                        }
                    }
                        //not from file, not group
                    else if (!_fromFile && !_group)
                    {
                        if (comboBoxDataType.SelectedItem != null && !string.IsNullOrEmpty(textBoxName.Text))
                        {
                            int mode = comboBoxDataType.SelectedIndex;
                            switch (mode)
                            {
                                case 0:
                                    ITree<string> textTree = factory.GetTree<string>();
                                    var treeObjectText = new TreeObject(textBoxName.Text.Trim(), "text", textTree);
                                    _provider.TreeObjects.Add(treeObjectText);
                                    break;
                                case 1:
                                    ITree<double> numericTree = factory.GetTree<double>();
                                    var treeObjectNumeric = new TreeObject(textBoxName.Text.Trim(), "numeric",
                                                                           numericTree);
                                    _provider.TreeObjects.Add(treeObjectNumeric);
                                    break;
                            }
                            return true;
                        }
                        toolTipHelper.ToolTipTitle = "No data type is selected";
                        toolTipHelper.Show("Please select data type from the list first.", comboBoxDataType, 3000);
                        return false;
                    }
                    break;
            }
            return false;
        }

        private void PopulateComboBox()
        {
            if (_group == false)
            {
                var owner = (Create) Owner;
                owner.FillSelectedTreeComboBox();
            }
            else
            {
                var owner = (BatchProcess) Owner;
                owner.UpdateLogOnCreate(_noOfTrees, _typeOfTrees, _groupTreeName);
            }
        }

        private void BtnAddClick(object sender, EventArgs e)
        {
            if (PerformAdd() != true) return;
            PopulateComboBox();
            Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData != Keys.Return)
                return false;
            btnAdd.PerformClick();
            return true;
        }

        private void TextBoxNameTextChanged(object sender, EventArgs e)
        {
            btnAdd.Enabled = !string.IsNullOrEmpty(textBoxName.Text.Trim());
        }

        private void MaskedTextBoxDegreeMaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            toolTipHelper.ToolTipTitle = "Invalid input";
            toolTipHelper.Show("We're sorry, but only digits (0-9) are allowed.", maskedTextBoxDegree, 3000);
        }

        private void ComboBoxAvailableTreesSelectedIndexChanged(object sender, EventArgs e)
        {
            var factory = (ITreeFactory) comboBoxAvailableTrees.SelectedItem;
            switch (_fromFile)
            {
                case true:
                    if (factory.NeedDegree)
                    {
                        Width = 315;
                        labelTreeDegree.Visible = true;
                        maskedTextBoxDegree.Visible = true;
                        btnAdd.Location = new Point(86, 124);
                        ShowToolTip();
                    }
                    if (factory.NeedDegree == false)
                    {
                        Width = 175;
                        labelTreeDegree.Visible = false;
                        maskedTextBoxDegree.Visible = false;
                        btnAdd.Location = new Point(12, 124);
                    }
                    break;
                case false:
                    if (factory.NeedDegree)
                    {
                        Width = 315;
                        labelTreeDegree.Visible = true;
                        maskedTextBoxDegree.Visible = true;
                        btnAdd.Location = new Point(86, 124);
                        ShowToolTip();
                    }
                    if (factory.NeedDegree == false)
                    {
                        Width = 315;
                        labelTreeDegree.Visible = false;
                        maskedTextBoxDegree.Visible = false;
                        btnAdd.Location = new Point(86, 124);
                    }
                    break;
            }
        }

    }
}

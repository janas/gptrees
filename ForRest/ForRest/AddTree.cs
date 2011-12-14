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

        private void PerformAdd()
        {
            if (_fromFile && _group == false)
            {
                if (_mode == 0 && _provider.TextData != null)
                {
                    var factory = (ITreeFactory)comboBoxAvailableTrees.SelectedItem;
                    ITree<string> tree = factory.GetTree<string>();
                    foreach (var node in _provider.TextData)
                    {
                        tree.Add(node);
                    }
                    var treeObject = new TreeObject(textBoxName.Text, "text", tree);
                    _provider.TreeObjects.Add(treeObject);
                }
                else if (_mode == 1 && _provider.NumericData != null)
                {
                    var factory = (ITreeFactory)comboBoxAvailableTrees.SelectedItem;
                    ITree<double> tree = factory.GetTree<double>();
                    foreach (var node in _provider.NumericData)
                    {
                        tree.Add(node);
                    }
                    var treeObject = new TreeObject(textBoxName.Text, "numeric", tree);
                    _provider.TreeObjects.Add(treeObject);
                }
            }
            else if (_fromFile && _group)
            {
                if (_mode == 0 && _provider.BatchTextData != null)
                {
                    var factory = (ITreeFactory)comboBoxAvailableTrees.SelectedItem;
                    foreach (var batchTree in _provider.BatchTextData)
                    {
                        ITree<string> tree = factory.GetTree<string>();
                        foreach (var node in batchTree)
                        {
                            tree.Add(node);
                        }
                        var treeObject = new TreeObject(textBoxName.Text, "text", tree);
                        _provider.BatchTreeObject.Add(treeObject);
                    }
                }
                else if (_mode == 1 && _provider.BatchNumericData != null)
                {
                    var factory = (ITreeFactory)comboBoxAvailableTrees.SelectedItem;
                    foreach (var batchTree in _provider.BatchNumericData)
                    {
                        ITree<double> tree = factory.GetTree<double>();
                        foreach (var node in batchTree)
                        {
                            tree.Add(node);
                        }
                        var treeObject = new TreeObject(textBoxName.Text, "numeric", tree);
                        _provider.BatchTreeObject.Add(treeObject);
                    }
                }
            }
            else
            {
                if (comboBoxDataType.SelectedItem != null && textBoxName.Text != null)
                {
                    var factory = (ITreeFactory)comboBoxAvailableTrees.SelectedItem;
                    int mode = comboBoxDataType.SelectedIndex;
                    switch (mode)
                    {
                        case 0:
                            ITree<string> textTree = factory.GetTree<string>();
                            var treeObjectText = new TreeObject(textBoxName.Text, "text", textTree);
                            _provider.TreeObjects.Add(treeObjectText);
                            break;
                        case 1:
                            ITree<double> numericTree = factory.GetTree<double>();
                            var treeObjectNumeric = new TreeObject(textBoxName.Text, "numeric", numericTree);
                            _provider.TreeObjects.Add(treeObjectNumeric);
                            break;
                    }
                }
                else
                    MessageBox.Show("No data type is selected. Please select data typr from the list first.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateComboBox()
        {
            if (_group == false)
            {
                var owner = (Create)Owner;
                owner.FillSelectedTreeComboBox();
            }
            else
            {
                var owner = (BatchProcess)Owner;
                owner.UpdateLogOnCreate();
            }
        }
        
        private void BtnAddClick(object sender, EventArgs e)
        {
            PerformAdd();
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
            if (!string.IsNullOrEmpty(textBoxName.Text))
            {
                btnAdd.Enabled = true;
            }
            else
            {
                btnAdd.Enabled = false;
            }
        }
    }
}

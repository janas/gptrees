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

        private void InitializeAdd()
        {
            Width = 315;
            labelDataType.Visible = true;
            comboBoxDataType.Visible = true;
            btnAdd.Location = new Point(86, 73);
        }

        private void FillTrees()
        {
            string applicationPath = Application.ExecutablePath;
            _provider.CreatePluginList(applicationPath);
            comboBoxAvailableTrees.DataSource = _provider.PluginList;
            comboBoxAvailableTrees.DisplayMember = "Name";  
        }

        private void BtnAddClick(object sender, EventArgs e)
        {
            if (_fromFile)
            {
                if (_mode == 0 && _provider.TextData != null)
                {
                    var factory = (ITreeFactory)comboBoxAvailableTrees.SelectedItem;
                    ITree<string> tree = factory.GetTree<string>();
                    foreach (var node in _provider.TextData)
                    {
                        tree.Add(node);
                    }
                    _provider.TextTrees.Add(tree);
                }
                else if (_mode == 1 && _provider.NumericData != null)
                {
                    var factory = (ITreeFactory)comboBoxAvailableTrees.SelectedItem;
                    ITree<double> tree = factory.GetTree<double>();
                    foreach (var node in _provider.NumericData)
                    {
                        tree.Add(node);
                    }
                    _provider.NumericTrees.Add(tree);
                }
            }
            else
            {
                if (comboBoxDataType.SelectedItem != null)
                {
                    var factory = (ITreeFactory)comboBoxAvailableTrees.SelectedItem;
                    int mode = comboBoxDataType.SelectedIndex;
                    switch (mode)
                    {
                        case 0:
                            ITree<string> textTree = factory.GetTree<string>();
                            _provider.TextTrees.Add(textTree);
                            break;
                        case 1:
                            ITree<double> numericTree = factory.GetTree<double>();
                            _provider.NumericTrees.Add(numericTree);
                            break;
                    }
                }
                else
                    MessageBox.Show("No data type is selected. Please select data typr from the list first.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            PopulateComboBox();
            Close();
        }

        private void PopulateComboBox()
        {
            var owner = (Create) Owner;
            owner.FillSelectedTreeComboBox();
        }
    }
}

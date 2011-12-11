using System;
using System.IO;
using System.Windows.Forms;
using ForRest.Provider.BLL;

namespace ForRest
{
    public partial class Create : Form
    {
        private readonly Provider.Provider _provider;
        private readonly int _mode;

        public Create(Provider.Provider provider, int mode)
        {
            InitializeComponent();
            _provider = provider;
            _mode = mode;
            FillSelectedTreeComboBox();
        }

        public void FillSelectedTreeComboBox()
        {
            comboBoxSelectTree.Items.Clear();
            foreach (var tree in _provider.TextTrees)
            {
                comboBoxSelectTree.Items.Add(tree);
            }
            foreach (var tree in _provider.NumericTrees)
            {
                comboBoxSelectTree.Items.Add(tree);
            }
        }

        private void BtnAddNodeClick(object sender, EventArgs e)
        {
            if (comboBoxSelectTree.SelectedItem != null)
            {
                
            }
            else
                MessageBox.Show("No tree is selected. Please select tree from list first.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void BtnRemoveNodeClick(object sender, EventArgs e)
        {
            if (comboBoxSelectTree.SelectedItem != null)
            {
               
            }
            else
                MessageBox.Show("No tree is selected. Please select tree from list first.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void BtnAddTreeFromFileClick(object sender, EventArgs e)
        {
            if (_provider.NumericData.Count != 0 || _provider.TextData.Count != 0)
            {
                var addTree = new AddTree(_provider, true, _mode) { Owner = this };
                addTree.ShowDialog();    
            }
            else
                MessageBox.Show("No file is loaded. Please load file with data first.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void BtnAddTreeClick(object sender, EventArgs e)
        {
            var addTree = new AddTree(_provider, false) {Owner = this};
            addTree.ShowDialog();
        }
    }
}

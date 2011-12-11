using System.Windows.Forms;

namespace ForRest
{
    public partial class Search : Form
    {
        private readonly Provider.Provider _provider;
        
        public Search(Provider.Provider provider)
        {
            InitializeComponent();
            _provider = provider;
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

        private void BtnSearchClick(object sender, System.EventArgs e)
        {
            if (comboBoxSelectTree.SelectedItem != null)
            {
                
            }
            else
                MessageBox.Show("No tree is selected. Please select tree from list first.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

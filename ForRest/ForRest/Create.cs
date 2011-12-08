using System;
using System.Windows.Forms;
using ForRest.Provider.BLL;

namespace ForRest
{
    public partial class Create : Form
    {
        private Provider.Provider _provider;

        public Create()
        {
            InitializeComponent();
            FillTrees();
        }

        private void FillTrees()
        {
            _provider = new Provider.Provider();
            string applicationPatch = Application.ExecutablePath;
            _provider.CreatePluginList(applicationPatch);
            comboBoxSelectTree.DataSource = _provider.PluginList;
            comboBoxSelectTree.DisplayMember = "PluginName";
        }

        private void BtnAddTreeClick(object sender, EventArgs e)
        {
            var f = (ITreeFactory)comboBoxSelectTree.SelectedItem;
            ITree<double> tree = f.GetTree<double>();
        }

        private void BtnAddNodeClick(object sender, EventArgs e)
        {

        }

        private void BtnRemoveNodeClick(object sender, EventArgs e)
        {

        }

        private void CreateFormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}

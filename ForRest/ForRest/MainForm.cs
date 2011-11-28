using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ForRest
{
    public partial class MainForm : Form
    {
        Provider.Provider provider = new Provider.Provider();
        private int _createState = 0;
        private int _searchState = 0;
        private int _batchProcessState = 0;
        private Create create = new Create();
        private Search search = new Search();

        public MainForm()
        {
            InitializeComponent();
            PrepareDialogs();
        }

        private void PrepareDialogs()
        {
            create.MdiParent = this;
            search.MdiParent = this;
        }

        private void BtnCreateClick(object sender, EventArgs e)
        {
            if (_createState == 0)
            {
                search.Hide();
                create.WindowState = FormWindowState.Maximized;
                create.Show();
                _createState = 1;
            }
            else
            {
                create.Hide();
                _createState = 0;
            }
        }

        private void BtnSearchClick(object sender, EventArgs e)
        {
            if (_searchState == 0)
            {
                create.Hide();
                search.WindowState = FormWindowState.Maximized;
                search.Show();
                _searchState = 1;
            }
            else
            {
                search.Hide();
                _searchState = 0;
            }
        }

        private void BtnAboutClick(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }

        private void BtnOpenClick(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                
            }
        }

        private void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnLoadedModulesClick(object sender, EventArgs e)
        {
            string applicationPatch = Application.ExecutablePath;
            List<string> pluginList = provider.CreateItemsList<string>(applicationPatch);
            LoadedModules loadedModules = new LoadedModules();
            loadedModules.GetData(pluginList);
            loadedModules.ShowDialog();
        }

    }
}

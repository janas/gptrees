using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ForRest
{
    public partial class MainForm : Form
    {
        Provider.Provider provider = new Provider.Provider();
        private int _createState;
        private int _searchState;
        private int _batchProcessState;
        private Create create = new Create();
        private Search search = new Search();
        private BatchProcess batchProcess = new BatchProcess();

        public MainForm()
        {
            InitializeComponent();
            PrepareDialogs();
        }

        private void PrepareDialogs()
        {
            create.MdiParent = this;
            search.MdiParent = this;
            batchProcess.MdiParent = this;
        }

        private void BtnCreateClick(object sender, EventArgs e)
        {
            if (_createState == 0)
            {
                search.Hide();
                _searchState = 0;
                batchProcess.Hide();
                _batchProcessState = 0;
                searchToolStripMenuItem.Checked = false;
                batchProcessToolStripMenuItem.Checked = false;
                create.WindowState = FormWindowState.Maximized;
                createToolStripMenuItem.Checked = true;
                create.Show();
                _createState = 1;
            }
            else
            {
                create.Hide();
                createToolStripMenuItem.Checked = false;
                _createState = 0;
            }
        }

        private void BtnSearchClick(object sender, EventArgs e)
        {
            if (_searchState == 0)
            {
                create.Hide();
                _createState = 0;
                batchProcess.Hide();
                _batchProcessState = 0;
                createToolStripMenuItem.Checked = false;
                batchProcessToolStripMenuItem.Checked = false;
                search.WindowState = FormWindowState.Maximized;
                searchToolStripMenuItem.Checked = true;
                search.Show();
                _searchState = 1;
            }
            else
            {
                search.Hide();
                searchToolStripMenuItem.Checked = false;
                _searchState = 0;
            }
        }

        private void BtnBatchProcessClick(object sender, EventArgs e)
        {
            if (_batchProcessState == 0)
            {
                search.Hide();
                _searchState = 0;
                create.Hide();
                _createState = 0;
                searchToolStripMenuItem.Checked = false;
                createToolStripMenuItem.Checked = false;
                batchProcess.WindowState = FormWindowState.Maximized;
                batchProcessToolStripMenuItem.Checked = true;
                batchProcess.Show();
                _batchProcessState = 1;
            }
            else
            {
                batchProcess.Hide();
                batchProcessToolStripMenuItem.Checked = false;
                _batchProcessState = 0;
            }
        }
        
        private void BtnAboutClick(object sender, EventArgs e)
        {
            var aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }

        private void BtnOpenClick(object sender, EventArgs e)
        {
            var openDialog = new OpenDialog(provider);
            openDialog.ShowDialog();
        }

        private void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnLoadedModulesClick(object sender, EventArgs e)
        {
            string applicationPath = Application.ExecutablePath;
            List<string[]> pluginList = provider.GetPluginDescription(applicationPath);
            //List<string[]> pluginList = provider.GetPluginDescription(applicationPath);
            var loadedModules = new LoadedModules();
            loadedModules.GetData(pluginList);
            loadedModules.ShowDialog();
        }

        private void BtnExportClick(object sender, EventArgs e)
        {
            if (provider.PerformanceSets.Count > 0)
            {
                saveFileDialog.Filter = "CSV Files (*.csv)|*.csv";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.FileName = "PerformanceResultSet";
                saveFileDialog.DefaultExt = "csv";
                DialogResult result = saveFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    provider.WriteResults(provider.PerformanceSets, saveFileDialog.FileName);
                }
            }
            else
            {
                MessageBox.Show("None tree has been processed. Can not save the results. Proceed with tree first!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}

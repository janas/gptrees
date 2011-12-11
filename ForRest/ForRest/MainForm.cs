using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ForRest
{
    public partial class MainForm : Form
    {
        readonly Provider.Provider _provider = new Provider.Provider();
        private int _createState;
        private int _searchState;
        private int _batchProcessState;
        private Create _create;
        private Search _search;
        private BatchProcess _batchProcess;

        public int Mode;

        public MainForm()
        {
            InitializeComponent();
            _provider.CheckDirectoryExists(Application.ExecutablePath);
        }

        private void BtnCreateClick(object sender, EventArgs e)
        {
            if (_createState == 0)
            {
                if (_create != null && !_create.IsDisposed) return;
                _create = new Create(_provider, Mode) { MdiParent = this, WindowState = FormWindowState.Maximized };
                _create.Show();
                _create.BringToFront();
                _createState = 1;
                createToolStripMenuItem.Checked = true;
                if (_search != null && (_search != null || !_search.IsDisposed))
                { _search.Close(); }
                _searchState = 0;
                searchToolStripMenuItem.Checked = false;
                if (_batchProcess != null && (_batchProcess != null || !_batchProcess.IsDisposed))
                { _batchProcess.Close(); }
                _batchProcessState = 0;
                batchProcessToolStripMenuItem.Checked = false;
            }
            else
            {
                _create.Close();
                _createState = 0;
                createToolStripMenuItem.Checked = false;
            }
            
        }

        private void BtnSearchClick(object sender, EventArgs e)
        {
            if (_searchState == 0)
            {
                if (_search != null && !_search.IsDisposed) return;
                _search = new Search(_provider) { MdiParent = this, WindowState = FormWindowState.Maximized };
                _search.Show();
                _search.BringToFront();
                _searchState = 1;
                searchToolStripMenuItem.Checked = true;
                if(_create != null && (_create != null || !_create.IsDisposed))
                { _create.Close(); }
                _createState = 0;
                createToolStripMenuItem.Checked = false;
                if(_batchProcess != null && (_batchProcess != null || !_batchProcess.IsDisposed))
                { _batchProcess.Close(); }
                _batchProcessState = 0;
                batchProcessToolStripMenuItem.Checked = false;
            }
            else
            {
                _search.Close();
                _searchState = 0;
                searchToolStripMenuItem.Checked = false;
            }
        }

        private void BtnBatchProcessClick(object sender, EventArgs e)
        {
            if (_batchProcessState == 0)
            {
                if (_batchProcess != null && !_batchProcess.IsDisposed) return;
                _batchProcess = new BatchProcess { MdiParent = this, WindowState = FormWindowState.Maximized };
                _batchProcess.Show();
                _batchProcess.BringToFront();
                _batchProcessState = 1;
                batchProcessToolStripMenuItem.Checked = true;
                if (_create != null && (_create != null || !_create.IsDisposed))
                { _create.Close(); }
                _createState = 0;
                createToolStripMenuItem.Checked = false;
                if (_search != null && (_search != null || !_search.IsDisposed))
                { _search.Close(); }
                _searchState = 0;
                searchToolStripMenuItem.Checked = false;
            }
            else
            {
                _batchProcess.Close();
                _batchProcessState = 0;
                batchProcessToolStripMenuItem.Checked = false;
            }
        }
        
        private void BtnAboutClick(object sender, EventArgs e)
        {
            var aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }

        private void BtnOpenClick(object sender, EventArgs e)
        {
            var openDialog = new OpenDialog(_provider) {Owner = this};
            openDialog.ShowDialog();
        }

        private void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnLoadedModulesClick(object sender, EventArgs e)
        {
            string applicationPath = Application.ExecutablePath;
            List<string[]> pluginList = _provider.GetPluginDescription(applicationPath);
            var loadedModules = new LoadedModules();
            loadedModules.GetData(pluginList);
            loadedModules.ShowDialog();
        }

        private void BtnExportClick(object sender, EventArgs e)
        {
            if (_provider.PerformanceSets.Count > 0)
            {
                saveFileDialog.Filter = "CSV Files (*.csv)|*.csv";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.FileName = "PerformanceResultSet";
                saveFileDialog.DefaultExt = "csv";
                DialogResult result = saveFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    _provider.WriteResults(_provider.PerformanceSets, saveFileDialog.FileName);
                }
            }
            else
            {
                MessageBox.Show("None tree has been processed. Can not save the results. Proceed with tree first!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnTestTreeClick(object sender, EventArgs e)
        {
            var tester = new Tester();
            tester.Show();
        }
    }
}

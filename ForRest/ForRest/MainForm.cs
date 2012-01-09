// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainForm.cs" company="Warsaw University of Technology">
//   
// </copyright>
// <summary>
//   The main form.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForRest
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    /// <summary>
    /// The main form.
    /// </summary>
    public partial class MainForm : Form
    {
        #region Constants and Fields

        /// <summary>
        /// The mode.
        /// </summary>
        public int Mode;

        /// <summary>
        /// The _provider.
        /// </summary>
        private readonly Provider.Provider _provider = new Provider.Provider();

        /// <summary>
        /// The _batch process.
        /// </summary>
        private BatchProcess _batchProcess;

        /// <summary>
        /// The _batch process state.
        /// </summary>
        private int _batchProcessState;

        /// <summary>
        /// The _create.
        /// </summary>
        private Create _create;

        /// <summary>
        /// The _create state.
        /// </summary>
        private int _createState;

        /// <summary>
        /// The _graph mode.
        /// </summary>
        private int _graphMode;

        /// <summary>
        /// The _search.
        /// </summary>
        private Search _search;

        /// <summary>
        /// The _search state.
        /// </summary>
        private int _searchState;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        /// <param name="isGleeEnabled">
        /// The is glee enabled.
        /// </param>
        public MainForm(bool isGleeEnabled)
        {
            this.InitializeComponent();
            this._provider.CheckDirectoryExists(Application.ExecutablePath);
            this.EnableGleeMode(isGleeEnabled);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// The batch process closing.
        /// </summary>
        public void BatchProcessClosing()
        {
            this._batchProcessState = 0;
            this.batchProcessToolStripMenuItem.Checked = false;
        }

        /// <summary>
        /// The create closing.
        /// </summary>
        public void CreateClosing()
        {
            this._createState = 0;
            this.createToolStripMenuItem.Checked = false;
        }

        /// <summary>
        /// The search closing.
        /// </summary>
        public void SearchClosing()
        {
            this._searchState = 0;
            this.searchToolStripMenuItem.Checked = false;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The btn about click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BtnAboutClick(object sender, EventArgs e)
        {
            var aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }

        /// <summary>
        /// The btn batch process click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BtnBatchProcessClick(object sender, EventArgs e)
        {
            if (this._batchProcessState == 0)
            {
                if (this._batchProcess != null && !this._batchProcess.IsDisposed)
                {
                    return;
                }

                this._batchProcess = new BatchProcess(this._provider)
                    {
                       MdiParent = this, WindowState = FormWindowState.Maximized 
                    };
                this._batchProcess.Show();
                this.ActivateMdiChild(null);
                this.ActivateMdiChild(this._batchProcess);
                this._batchProcess.BringToFront();
                this._batchProcessState = 1;
                this.batchProcessToolStripMenuItem.Checked = true;
                if (this._create != null && (this._create != null || !this._create.IsDisposed))
                {
                    this._create.Close();
                }

                this._createState = 0;
                this.createToolStripMenuItem.Checked = false;
                if (this._search != null && (this._search != null || !this._search.IsDisposed))
                {
                    this._search.Close();
                }

                this._searchState = 0;
                this.searchToolStripMenuItem.Checked = false;
            }
            else
            {
                this._batchProcess.Close();
                this._batchProcessState = 0;
                this.batchProcessToolStripMenuItem.Checked = false;
            }
        }

        /// <summary>
        /// The btn create click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BtnCreateClick(object sender, EventArgs e)
        {
            if (this._createState == 0)
            {
                if (this._create != null && !this._create.IsDisposed)
                {
                    return;
                }

                this._create = new Create(this._provider, this.Mode, this._graphMode)
                    {
                       MdiParent = this, WindowState = FormWindowState.Maximized 
                    };
                this._create.Show();
                this.ActivateMdiChild(null);
                this.ActivateMdiChild(this._create);
                this._create.BringToFront();
                this._createState = 1;
                this.createToolStripMenuItem.Checked = true;
                if (this._search != null && (this._search != null || !this._search.IsDisposed))
                {
                    this._search.Close();
                }

                this._searchState = 0;
                this.searchToolStripMenuItem.Checked = false;
                if (this._batchProcess != null && (this._batchProcess != null || !this._batchProcess.IsDisposed))
                {
                    this._batchProcess.Close();
                }

                this._batchProcessState = 0;
                this.batchProcessToolStripMenuItem.Checked = false;
            }
            else
            {
                this._create.Close();
                this._createState = 0;
                this.createToolStripMenuItem.Checked = false;
            }
        }

        /// <summary>
        /// The btn export click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BtnExportClick(object sender, EventArgs e)
        {
            if (this._provider.PerformanceSets.Count > 0)
            {
                this.saveFileDialog.Filter = "CSV Files (*.csv)|*.csv";
                this.saveFileDialog.FilterIndex = 1;
                this.saveFileDialog.FileName = "PerformanceResultSet";
                this.saveFileDialog.DefaultExt = "csv";
                DialogResult result = this.saveFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this._provider.WriteResults(this._provider.PerformanceSets, this.saveFileDialog.FileName);
                }
            }
            else
            {
                MessageBox.Show(
                    "None tree has been processed. Can not save the results. Proceed with tree first!", 
                    "Error!", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// The btn loaded modules click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BtnLoadedModulesClick(object sender, EventArgs e)
        {
            string applicationPath = Application.ExecutablePath;
            List<string[]> pluginList = this._provider.GetPluginDescription(applicationPath);
            var loadedModules = new LoadedModules();
            loadedModules.GetData(pluginList);
            loadedModules.ShowDialog();
        }

        /// <summary>
        /// The btn open click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BtnOpenClick(object sender, EventArgs e)
        {
            var openDialog = new OpenDialog(this._provider) { Owner = this };
            openDialog.ShowDialog();
            if (this.ActiveMdiChild == null || this.ActiveMdiChild.IsDisposed || this.ActiveMdiChild.Name != "Create")
            {
                return;
            }

            var create = (Create)this.ActiveMdiChild;
            create.Mode = this.Mode;
        }

        /// <summary>
        /// The btn search click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BtnSearchClick(object sender, EventArgs e)
        {
            if (this._searchState == 0)
            {
                if (this._search != null && !this._search.IsDisposed)
                {
                    return;
                }

                this._search = new Search(this._provider, this._graphMode)
                    {
                       MdiParent = this, WindowState = FormWindowState.Maximized 
                    };
                this._search.Show();
                this.ActivateMdiChild(null);
                this.ActivateMdiChild(this._search);
                this._search.BringToFront();
                this._searchState = 1;
                this.searchToolStripMenuItem.Checked = true;
                if (this._create != null && (this._create != null || !this._create.IsDisposed))
                {
                    this._create.Close();
                }

                this._createState = 0;
                this.createToolStripMenuItem.Checked = false;
                if (this._batchProcess != null && (this._batchProcess != null || !this._batchProcess.IsDisposed))
                {
                    this._batchProcess.Close();
                }

                this._batchProcessState = 0;
                this.batchProcessToolStripMenuItem.Checked = false;
            }
            else
            {
                this._search.Close();
                this._searchState = 0;
                this.searchToolStripMenuItem.Checked = false;
            }
        }

        /// <summary>
        /// The enable glee mode.
        /// </summary>
        /// <param name="isGleeEnabled">
        /// The is glee enabled.
        /// </param>
        private void EnableGleeMode(bool isGleeEnabled)
        {
            switch (isGleeEnabled)
            {
                case true:
                    this.gLEEGraphToolStripMenuItem.Visible = true;
                    break;
                default:
                    this.gLEEGraphToolStripMenuItem.Visible = false;
                    break;
            }
        }

        /// <summary>
        /// The exit tool strip menu item click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// The g lee graph tool strip menu item click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void GLeeGraphToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (this._graphMode == 2)
            {
                this.gLEEGraphToolStripMenuItem.Checked = true;
                return;
            }

            if (this.gLEEGraphToolStripMenuItem.Checked)
            {
                this._graphMode = 2;
                this.graphToolStripMenuItem.Checked = false;
                this.treeViewToolStripMenuItem.Checked = false;
            }
            else
            {
                this.gLEEGraphToolStripMenuItem.Checked = true;
                this.treeViewToolStripMenuItem.Checked = false;
                this.graphToolStripMenuItem.Checked = false;
                this._graphMode = 2;
            }

            if (this.ActiveMdiChild != null && !this.ActiveMdiChild.IsDisposed && this.ActiveMdiChild.Name == "Create")
            {
                var create = (Create)this.ActiveMdiChild;
                create.GraphMode = this._graphMode;
                create.ChangeGraphMode();
            }

            if (this.ActiveMdiChild != null && !this.ActiveMdiChild.IsDisposed && this.ActiveMdiChild.Name == "Search")
            {
                var search = (Search)this.ActiveMdiChild;
                search.GraphMode = this._graphMode;
                search.ChangeGraphMode();
            }
        }

        /// <summary>
        /// The graph tool strip menu item click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void GraphToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (this._graphMode == 1)
            {
                this.graphToolStripMenuItem.Checked = true;
                return;
            }

            if (this.graphToolStripMenuItem.Checked)
            {
                this._graphMode = 1;
                this.treeViewToolStripMenuItem.Checked = false;
                this.gLEEGraphToolStripMenuItem.Checked = false;
            }
            else
            {
                this.graphToolStripMenuItem.Checked = true;
                this.treeViewToolStripMenuItem.Checked = false;
                this.gLEEGraphToolStripMenuItem.Checked = false;
                this._graphMode = 1;
            }

            if (this.ActiveMdiChild != null && !this.ActiveMdiChild.IsDisposed && this.ActiveMdiChild.Name == "Create")
            {
                var create = (Create)this.ActiveMdiChild;
                create.GraphMode = this._graphMode;
                create.ChangeGraphMode();
            }

            if (this.ActiveMdiChild != null && !this.ActiveMdiChild.IsDisposed && this.ActiveMdiChild.Name == "Search")
            {
                var search = (Search)this.ActiveMdiChild;
                search.GraphMode = this._graphMode;
                search.ChangeGraphMode();
            }
        }

        /// <summary>
        /// The tree view tool strip menu item click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void TreeViewToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (this._graphMode == 0)
            {
                this.treeViewToolStripMenuItem.Checked = true;
                return;
            }

            if (this.treeViewToolStripMenuItem.Checked)
            {
                this._graphMode = 0;
                this.graphToolStripMenuItem.Checked = false;
                this.gLEEGraphToolStripMenuItem.Checked = false;
            }
            else
            {
                this.treeViewToolStripMenuItem.Checked = true;
                this.graphToolStripMenuItem.Checked = false;
                this.gLEEGraphToolStripMenuItem.Checked = false;
                this._graphMode = 0;
            }

            if (this.ActiveMdiChild != null && !this.ActiveMdiChild.IsDisposed && this.ActiveMdiChild.Name == "Create")
            {
                var create = (Create)this.ActiveMdiChild;
                create.GraphMode = this._graphMode;
                create.ChangeGraphMode();
            }

            if (this.ActiveMdiChild != null && !this.ActiveMdiChild.IsDisposed && this.ActiveMdiChild.Name == "Search")
            {
                var search = (Search)this.ActiveMdiChild;
                search.GraphMode = this._graphMode;
                search.ChangeGraphMode();
            }
        }

        #endregion
    }
}
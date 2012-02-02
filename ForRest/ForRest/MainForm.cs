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
        /// The provider.
        /// </summary>
        private readonly Provider.Provider provider = new Provider.Provider();

        /// <summary>
        /// The batch process.
        /// </summary>
        private BatchProcess batchProcess;

        /// <summary>
        /// The batch process state.
        /// </summary>
        private int batchProcessState;

        /// <summary>
        /// The create.
        /// </summary>
        private Create create;

        /// <summary>
        /// The create state.
        /// </summary>
        private int createState;

        /// <summary>
        /// The graph mode.
        /// </summary>
        private int graphMode;

        /// <summary>
        /// The search.
        /// </summary>
        private Search search;

        /// <summary>
        /// The search state.
        /// </summary>
        private int searchState;

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
            this.provider.CheckDirectoryExists(Application.ExecutablePath);
            this.EnableGleeMode(isGleeEnabled);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// The batch process closing.
        /// </summary>
        public void BatchProcessClosing()
        {
            this.batchProcessState = 0;
            this.batchProcessToolStripMenuItem.Checked = false;
        }

        /// <summary>
        /// The create closing.
        /// </summary>
        public void CreateClosing()
        {
            this.createState = 0;
            this.createToolStripMenuItem.Checked = false;
        }

        /// <summary>
        /// The search closing.
        /// </summary>
        public void SearchClosing()
        {
            this.searchState = 0;
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
            if (this.batchProcessState == 0)
            {
                if (this.batchProcess != null && !this.batchProcess.IsDisposed)
                {
                    return;
                }

                this.batchProcess = new BatchProcess(this.provider)
                    {
                        MdiParent = this, WindowState = FormWindowState.Maximized
                    };
                this.batchProcess.Show();
                this.ActivateMdiChild(null);
                this.ActivateMdiChild(this.batchProcess);
                this.batchProcess.BringToFront();
                this.batchProcessState = 1;
                this.batchProcessToolStripMenuItem.Checked = true;
                if (this.create != null && (this.create != null || !this.create.IsDisposed))
                {
                    this.create.Close();
                }

                this.createState = 0;
                this.createToolStripMenuItem.Checked = false;
                if (this.search != null && (this.search != null || !this.search.IsDisposed))
                {
                    this.search.Close();
                }

                this.searchState = 0;
                this.searchToolStripMenuItem.Checked = false;
            }
            else
            {
                this.batchProcess.Close();
                this.batchProcessState = 0;
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
            if (this.createState == 0)
            {
                if (this.create != null && !this.create.IsDisposed)
                {
                    return;
                }

                this.create = new Create(this.provider, this.Mode, this.graphMode)
                    {
                        MdiParent = this, WindowState = FormWindowState.Maximized
                    };
                this.create.Show();
                this.ActivateMdiChild(null);
                this.ActivateMdiChild(this.create);
                this.create.BringToFront();
                this.createState = 1;
                this.createToolStripMenuItem.Checked = true;
                if (this.search != null && (this.search != null || !this.search.IsDisposed))
                {
                    this.search.Close();
                }

                this.searchState = 0;
                this.searchToolStripMenuItem.Checked = false;
                if (this.batchProcess != null && (this.batchProcess != null || !this.batchProcess.IsDisposed))
                {
                    this.batchProcess.Close();
                }

                this.batchProcessState = 0;
                this.batchProcessToolStripMenuItem.Checked = false;
            }
            else
            {
                this.create.Close();
                this.createState = 0;
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
            if (this.provider.PerformanceSets.Count > 0)
            {
                this.saveFileDialog.Filter = "CSV Files (*.csv)|*.csv";
                this.saveFileDialog.FilterIndex = 1;
                this.saveFileDialog.FileName = "PerformanceResultSet";
                this.saveFileDialog.DefaultExt = "csv";
                DialogResult result = this.saveFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.provider.WriteResults(this.provider.PerformanceSets, this.saveFileDialog.FileName);
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
            List<string[]> pluginList = this.provider.GetPluginDescription(applicationPath);
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
            var openDialog = new OpenDialog(this.provider) { Owner = this };
            openDialog.ShowDialog();
            if (this.ActiveMdiChild == null || this.ActiveMdiChild.IsDisposed || this.ActiveMdiChild.Name != "Create")
            {
                return;
            }

            var activeMdiChild = (Create)this.ActiveMdiChild;
            activeMdiChild.Mode = this.Mode;
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
            if (this.searchState == 0)
            {
                if (this.search != null && !this.search.IsDisposed)
                {
                    return;
                }

                this.search = new Search(this.provider, this.graphMode)
                    {
                        MdiParent = this, WindowState = FormWindowState.Maximized
                    };
                this.search.Show();
                this.ActivateMdiChild(null);
                this.ActivateMdiChild(this.search);
                this.search.BringToFront();
                this.searchState = 1;
                this.searchToolStripMenuItem.Checked = true;
                if (this.create != null && (this.create != null || !this.create.IsDisposed))
                {
                    this.create.Close();
                }

                this.createState = 0;
                this.createToolStripMenuItem.Checked = false;
                if (this.batchProcess != null && (this.batchProcess != null || !this.batchProcess.IsDisposed))
                {
                    this.batchProcess.Close();
                }

                this.batchProcessState = 0;
                this.batchProcessToolStripMenuItem.Checked = false;
            }
            else
            {
                this.search.Close();
                this.searchState = 0;
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
            this.gLEEGraphToolStripMenuItem.Visible = isGleeEnabled;
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
        /// No graph view tool strip menu item click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void NoGraphToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (this.graphMode == 0)
            {
                this.noGraphToolStripMenuItem.Checked = true;
                return;
            }

            if (this.noGraphToolStripMenuItem.Checked)
            {
                this.graphMode = 0;
                this.treeViewToolStripMenuItem.Checked = false;
                this.graphToolStripMenuItem.Checked = false;
                this.gLEEGraphToolStripMenuItem.Checked = false;
            }
            else
            {
                this.noGraphToolStripMenuItem.Checked = true;
                this.treeViewToolStripMenuItem.Checked = false;
                this.graphToolStripMenuItem.Checked = false;
                this.gLEEGraphToolStripMenuItem.Checked = false;
                this.graphMode = 0;
            }

            if (this.ActiveMdiChild != null && !this.ActiveMdiChild.IsDisposed && this.ActiveMdiChild.Name == "Create")
            {
                var activeMdiChild = (Create)this.ActiveMdiChild;
                activeMdiChild.GraphMode = this.graphMode;
                activeMdiChild.ChangeGraphMode();
            }

            if (this.ActiveMdiChild != null && !this.ActiveMdiChild.IsDisposed && this.ActiveMdiChild.Name == "Search")
            {
                var activeMdiChild = (Search)this.ActiveMdiChild;
                activeMdiChild.GraphMode = this.graphMode;
                activeMdiChild.ChangeGraphMode();
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
            if (this.graphMode == 1)
            {
                this.treeViewToolStripMenuItem.Checked = true;
                return;
            }

            if (this.treeViewToolStripMenuItem.Checked)
            {
                this.graphMode = 1;
                this.noGraphToolStripMenuItem.Checked = false;
                this.graphToolStripMenuItem.Checked = false;
                this.gLEEGraphToolStripMenuItem.Checked = false;
            }
            else
            {
                this.treeViewToolStripMenuItem.Checked = true;
                this.noGraphToolStripMenuItem.Checked = false;
                this.graphToolStripMenuItem.Checked = false;
                this.gLEEGraphToolStripMenuItem.Checked = false;
                this.graphMode = 1;
            }

            if (this.ActiveMdiChild != null && !this.ActiveMdiChild.IsDisposed && this.ActiveMdiChild.Name == "Create")
            {
                var activeMdiChild = (Create)this.ActiveMdiChild;
                activeMdiChild.GraphMode = this.graphMode;
                activeMdiChild.ChangeGraphMode();
            }

            if (this.ActiveMdiChild != null && !this.ActiveMdiChild.IsDisposed && this.ActiveMdiChild.Name == "Search")
            {
                var activeMdiChild = (Search)this.ActiveMdiChild;
                activeMdiChild.GraphMode = this.graphMode;
                activeMdiChild.ChangeGraphMode();
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
            if (this.graphMode == 2)
            {
                this.graphToolStripMenuItem.Checked = true;
                return;
            }

            if (this.graphToolStripMenuItem.Checked)
            {
                this.graphMode = 2;
                this.noGraphToolStripMenuItem.Checked = false;
                this.treeViewToolStripMenuItem.Checked = false;
                this.gLEEGraphToolStripMenuItem.Checked = false;
            }
            else
            {
                this.graphToolStripMenuItem.Checked = true;
                this.noGraphToolStripMenuItem.Checked = false;
                this.treeViewToolStripMenuItem.Checked = false;
                this.gLEEGraphToolStripMenuItem.Checked = false;
                this.graphMode = 2;
            }

            if (this.ActiveMdiChild != null && !this.ActiveMdiChild.IsDisposed && this.ActiveMdiChild.Name == "Create")
            {
                var activeMdiChild = (Create)this.ActiveMdiChild;
                activeMdiChild.GraphMode = this.graphMode;
                activeMdiChild.ChangeGraphMode();
            }

            if (this.ActiveMdiChild != null && !this.ActiveMdiChild.IsDisposed && this.ActiveMdiChild.Name == "Search")
            {
                var activeMdiChild = (Search)this.ActiveMdiChild;
                activeMdiChild.GraphMode = this.graphMode;
                activeMdiChild.ChangeGraphMode();
            }
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
            if (this.graphMode == 3)
            {
                this.gLEEGraphToolStripMenuItem.Checked = true;
                return;
            }

            if (this.gLEEGraphToolStripMenuItem.Checked)
            {
                this.graphMode = 3;
                this.noGraphToolStripMenuItem.Checked = false;
                this.graphToolStripMenuItem.Checked = false;
                this.treeViewToolStripMenuItem.Checked = false;
            }
            else
            {
                this.gLEEGraphToolStripMenuItem.Checked = true;
                this.noGraphToolStripMenuItem.Checked = false;
                this.treeViewToolStripMenuItem.Checked = false;
                this.graphToolStripMenuItem.Checked = false;
                this.graphMode = 3;
            }

            if (this.ActiveMdiChild != null && !this.ActiveMdiChild.IsDisposed && this.ActiveMdiChild.Name == "Create")
            {
                var activeMdiChild = (Create)this.ActiveMdiChild;
                activeMdiChild.GraphMode = this.graphMode;
                activeMdiChild.ChangeGraphMode();
            }

            if (this.ActiveMdiChild != null && !this.ActiveMdiChild.IsDisposed && this.ActiveMdiChild.Name == "Search")
            {
                var activeMdiChild = (Search)this.ActiveMdiChild;
                activeMdiChild.GraphMode = this.graphMode;
                activeMdiChild.ChangeGraphMode();
            }
        }

        #endregion
    }
}
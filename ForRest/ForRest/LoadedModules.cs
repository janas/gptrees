// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoadedModules.cs" company="Warsaw University of Technology">
//   
// </copyright>
// <summary>
//   The loaded modules.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForRest
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    /// <summary>
    /// The loaded modules.
    /// </summary>
    public partial class LoadedModules : Form
    {
        #region Constants and Fields

        /// <summary>
        /// The plugins list.
        /// </summary>
        private List<string[]> pluginsList;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadedModules"/> class.
        /// </summary>
        public LoadedModules()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// The get data.
        /// </summary>
        /// <param name="itemsList">
        /// The items list.
        /// </param>
        public void GetData(List<string[]> itemsList)
        {
            this.pluginsList = itemsList;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The list box plugin name selected index changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void ListBoxPluginNameSelectedIndexChanged(object sender, EventArgs e)
        {
            this.textBoxPluginDescription.Clear();
            if (this.listBoxPluginName.SelectedIndex == -1)
            {
                return;
            }

            this.textBoxPluginDescription.AppendText(this.pluginsList[this.listBoxPluginName.SelectedIndex][1]);
            this.textBoxPluginDescription.AppendText("\r\n\r\nAdvanced Information\r\n");
            this.textBoxPluginDescription.AppendText(this.pluginsList[this.listBoxPluginName.SelectedIndex][2]);
        }

        /// <summary>
        /// The load plugin list box.
        /// </summary>
        private void LoadPluginListBox()
        {
            this.listBoxPluginName.Items.Clear();
            foreach (string[] t in this.pluginsList)
            {
                this.listBoxPluginName.Items.Add(t[0]);
            }
        }

        /// <summary>
        /// The loaded modules load.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void LoadedModulesLoad(object sender, EventArgs e)
        {
            this.LoadPluginListBox();
        }

        #endregion
    }
}
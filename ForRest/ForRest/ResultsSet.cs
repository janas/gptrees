// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResultsSet.cs" company="Warsaw University of Technology">
//   
// </copyright>
// <summary>
//   The results set.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForRest
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// The results set.
    /// </summary>
    public partial class ResultsSet : Form
    {
        #region Constants and Fields

        /// <summary>
        /// The provider.
        /// </summary>
        private readonly Provider.Provider provider;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultsSet"/> class.
        /// </summary>
        /// <param name="provider">
        /// The provider.
        /// </param>
        public ResultsSet(Provider.Provider provider)
        {
            this.InitializeComponent();
            this.provider = provider;
            this.dataGridViewResultsSet.ColumnHeadersBorderStyle = ProperColumnHeadersBorderStyle;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets ProperColumnHeadersBorderStyle.
        /// </summary>
        private static DataGridViewHeaderBorderStyle ProperColumnHeadersBorderStyle
        {
            get
            {
                return (SystemFonts.MessageBoxFont.Name == "Segoe UI")
                           ? DataGridViewHeaderBorderStyle.None
                           : DataGridViewHeaderBorderStyle.Raised;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The results set load.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void ResultsSetLoad(object sender, EventArgs e)
        {
            this.dataGridViewResultsSet.DataSource = this.provider.PerformanceSets;
            this.dataGridViewResultsSet.Columns[0].HeaderText = "Tree name";
            this.dataGridViewResultsSet.Columns[1].HeaderText = "Type of the tree";
            this.dataGridViewResultsSet.Columns[2].HeaderText = "Number of nodes";
            this.dataGridViewResultsSet.Columns[3].HeaderText = "Nodes type";
            this.dataGridViewResultsSet.Columns[4].HeaderText = "Search time (in ms)";
        }

        #endregion
    }
}
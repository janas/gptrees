// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BatchProcess.cs" company="Warsaw University of Technology">
//   
// </copyright>
// <summary>
//   The batch process.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForRest
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using System.Windows.Forms;

    using ForRest.BLL;

    /// <summary>
    /// The batch process.
    /// </summary>
    public partial class BatchProcess : Form
    {
        #region Constants and Fields

        /// <summary>
        /// The provider.
        /// </summary>
        private readonly Provider.Provider provider;

        /// <summary>
        /// The numeric items.
        /// </summary>
        private List<double> numericItems;

        /// <summary>
        /// The is help shown.
        /// </summary>
        private bool isHelpShown = true;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BatchProcess"/> class.
        /// </summary>
        /// <param name="provider">
        /// The provider.
        /// </param>
        public BatchProcess(Provider.Provider provider)
        {
            this.InitializeComponent();
            this.PrepareDialog();
            this.InitializeEditBox();
            this.provider = provider;
            this.IsAdd = false;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets Mode.
        /// </summary>
        public int Mode { get; set; }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether IsAdd.
        /// </summary>
        private bool IsAdd { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// The update log on create.
        /// </summary>
        /// <param name="noOfTrees">
        /// The no of trees.
        /// </param>
        /// <param name="treeType">
        /// The tree type.
        /// </param>
        /// <param name="groupTreeName">
        /// The group tree name.
        /// </param>
        public void UpdateLogOnCreate(int noOfTrees, string treeType, string groupTreeName)
        {
            var currentDateTime = DateTime.Now.TimeOfDay.ToString();
            this.textBoxLog.AppendText(
                "[" + currentDateTime.Substring(0, 13) + "]" + "\t" + noOfTrees + " tree(s) created, type of "
                + treeType + ", group tree name " + groupTreeName + "." + Environment.NewLine);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The background worker batch process do work.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BackgroundWorkerBatchProcessDoWork(object sender, DoWorkEventArgs e)
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            var currentDateTime = DateTime.Now.TimeOfDay.ToString();
            backgroundWorkerBatchProcess.ReportProgress(
                0,
                "[" + currentDateTime.Substring(0, 13) + "]" + "\tThread ID " + threadId + " started." + Environment.NewLine);
            var type = e.Argument as string;
            var searchPerformer = new SearchPerformer(this.provider, this.backgroundWorkerBatchProcess);
            if (type != null && type.Equals("Text"))
            {
                searchPerformer.GenericBatchSearch(this.ListBoxToList(), "text");
            }
            else
            {
                searchPerformer.GenericBatchSearch(this.numericItems, "numeric");
            }

            e.Result = Thread.CurrentThread.ManagedThreadId;
        }

        /// <summary>
        /// The background worker batch process progress changed.
        /// </summary>
        /// <param name="sender">
        /// The sender. 
        /// </param>
        /// <param name="e">
        /// The e. 
        /// </param>
        private void BackgroundWorkerBatchProcessProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var progress = (string)e.UserState;
            textBoxLog.AppendText(progress);

            this.progressBarBatchSearch.Value = e.ProgressPercentage;
        }

        /// <summary>
        /// The background worker batch process run worker completed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BackgroundWorkerBatchProcessRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var currentDateTime = DateTime.Now.TimeOfDay.ToString();
            this.textBoxLog.AppendText(
                "[" + currentDateTime.Substring(0, 13) + "]" + "\tThread ID " + e.Result + " is done." + Environment.NewLine);
            
            if (!backgroundWorkerBatchProcess.IsBusy)
            {
                this.EnableExportButton();
                this.listBoxSearchItems.Enabled = true;
                this.btnBatchSearch.Enabled = true;
            }
        }

        /// <summary>
        /// The batch process form closing.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BatchProcessFormClosing(object sender, FormClosingEventArgs e)
        {
            var mainForm = (MainForm)this.MdiParent;
            mainForm.BatchProcessClosing();
        }

        /// <summary>
        /// The btn batch search click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BtnBatchSearchClick(object sender, EventArgs e)
        {
            bool result = this.ConvertToDouble();
            if (this.comboBoxDataType.SelectedItem.Equals("Numeric") && result == false)
            {
                var currentDateTime = DateTime.Now.TimeOfDay.ToString();
                this.textBoxLog.AppendText(
                    "[" + currentDateTime.Substring(0, 13)
                    + "]\tNone value has been processed. Please provide correct input for numeric trees!"
                    + Environment.NewLine);
            }
            else if (this.comboBoxDataType.SelectedItem.Equals("Numeric") && result)
            {
                var currentDateTime = DateTime.Now.TimeOfDay.ToString();
                this.textBoxLog.AppendText(
                    "[" + currentDateTime.Substring(0, 13) + "]\tSuccessfully converted " + this.numericItems.Count
                    + " items out of " + this.listBoxSearchItems.Items.Count + " items." + Environment.NewLine);
                this.listBoxSearchItems.Enabled = false;
                this.btnBatchSearch.Enabled = false;
                this.backgroundWorkerBatchProcess.RunWorkerAsync(this.comboBoxDataType.SelectedItem.Equals("Numeric"));
                Application.DoEvents();
            }
            else if (this.comboBoxDataType.SelectedItem.Equals("Text"))
            {
                this.listBoxSearchItems.Enabled = false;
                this.btnBatchSearch.Enabled = false;
                this.backgroundWorkerBatchProcess.RunWorkerAsync(comboBoxDataType.SelectedItem.ToString());
                Application.DoEvents();
            }
        }

        /// <summary>
        /// The btn create numeric trees click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BtnCreateNumericTreesClick(object sender, EventArgs e)
        {
            if (this.provider.BatchNumericData.Count == 0)
            {
                return;
            }

            var addTree = new AddTree(this.provider, true, 1, true) { Owner = this };
            addTree.ShowDialog();
            this.VerifyLayout();
        }

        /// <summary>
        /// The btn create text trees click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BtnCreateTextTreesClick(object sender, EventArgs e)
        {
            if (this.provider.BatchTextData.Count == 0)
            {
                return;
            }

            var addTree = new AddTree(this.provider, true, 0, true) { Owner = this };
            addTree.ShowDialog();
            this.VerifyLayout();
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
            this.saveFileDialog.Filter = "CSV Files (*.csv)|*.csv";
            this.saveFileDialog.FilterIndex = 1;
            this.saveFileDialog.FileName = "PerformanceResultSet";
            this.saveFileDialog.DefaultExt = "csv";
            DialogResult result = this.saveFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.provider.WriteResults(this.provider.BatchPerformanceSet, this.saveFileDialog.FileName);
            }
        }

        /// <summary>
        /// The btn open files click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BtnOpenFilesClick(object sender, EventArgs e)
        {
            var openDialog = new OpenDialog(this.provider, true) { Owner = this };
            openDialog.ShowDialog();
            var currentDateTime = DateTime.Now.TimeOfDay.ToString();
            this.textBoxLog.AppendText("[" + currentDateTime.Substring(0, 13) + "]" + "\tFiles loaded." + Environment.NewLine);
            this.EnableCreateButtons();
            this.PopulateComboBox();
        }

        /// <summary>
        /// The combo box data type selected index changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void ComboBoxDataTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBoxSearchItems.Enabled == false)
            {
                this.listBoxSearchItems.Enabled = true;
            }

            if (this.isHelpShown)
            {
                this.listBoxSearchItems.Items.Clear();
                this.isHelpShown = false;
            }

            var currentDateTime = DateTime.Now.TimeOfDay.ToString();
            this.textBoxLog.AppendText(
                "[" + currentDateTime.Substring(0, 13) + "]" + "\t" + this.comboBoxDataType.SelectedItem
                + " data type selected." + Environment.NewLine);
        }

        /// <summary>
        /// The convert to double.
        /// </summary>
        /// <returns>
        /// Returns true if number of items in listbox is greather than 0, fale otherwise.
        /// </returns>
        private bool ConvertToDouble()
        {
            this.numericItems = new List<double>();
            foreach (var item in this.listBoxSearchItems.Items)
            {
                double numericValue;
                if (double.TryParse(item.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out numericValue))
                {
                    this.numericItems.Add(numericValue);
                }
            }

            return this.numericItems.Count > 0;
        }

        /// <summary>
        /// Method that converts a listbox items into a string list.
        /// </summary>
        /// <returns>
        /// Returns string list containing listbox items.
        /// </returns>
        private List<string> ListBoxToList()
        {
            return (from object item in this.listBoxSearchItems.Items select item.ToString()).ToList();
        }

        /// <summary>
        /// The create edit box.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        private void CreateEditBox(object sender)
        {
            this.listBoxSearchItems = (ListBox)sender;
            var itemSelected = this.listBoxSearchItems.SelectedIndex;
            Rectangle rectangle = this.listBoxSearchItems.GetItemRectangle(itemSelected);
            var itemText = (string)this.listBoxSearchItems.Items[itemSelected];
            this.editBox.Location = new Point(rectangle.X, rectangle.Y + 15);
            this.editBox.Size = new Size(rectangle.Width, rectangle.Height);
            this.editBox.Show();
            this.listBoxSearchItems.Controls.AddRange(new Control[] { this.editBox });
            this.editBox.Text = itemText;
            this.editBox.Focus();
        }

        /// <summary>
        /// The create edit box add.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        private void CreateEditBoxAdd(object sender)
        {
            this.listBoxSearchItems = (ListBox)sender;
            var itemSelected = this.listBoxSearchItems.Items.Count;
            if (itemSelected == 0)
            {
                var rectangle = new Rectangle(0, 0, 145, 13);
                this.editBox.Location = new Point(rectangle.X, rectangle.Y);
                this.editBox.Size = new Size(rectangle.Width, rectangle.Height);
                this.editBox.Show();
                this.listBoxSearchItems.Controls.AddRange(new Control[] { this.editBox });
                this.editBox.Text = string.Empty;
                this.editBox.Focus();
            }
            else
            {
                itemSelected = itemSelected - 1;
                Rectangle rectangle = this.listBoxSearchItems.GetItemRectangle(itemSelected);
                this.editBox.Location = new Point(rectangle.X, rectangle.Y + 15);
                this.editBox.Size = new Size(rectangle.Width, rectangle.Height);
                this.editBox.Show();
                this.listBoxSearchItems.Controls.AddRange(new Control[] { this.editBox });
                this.editBox.Text = string.Empty;
                this.editBox.Focus();
            }
        }

        /// <summary>
        /// The enable create buttons.
        /// </summary>
        private void EnableCreateButtons()
        {
            if (this.Mode == 0 && !this.btnCreateTextTrees.Enabled)
            {
                this.btnCreateTextTrees.Enabled = true;
            }

            if (this.Mode == 1 && !this.btnCreateNumericTrees.Enabled)
            {
                this.btnCreateNumericTrees.Enabled = true;
            }
        }

        /// <summary>
        /// The enable export button.
        /// </summary>
        private void EnableExportButton()
        {
            if (this.btnExport.Enabled)
            {
                return;
            }

            if (this.provider.BatchPerformanceSet.Count > 0)
            {
                this.btnExport.Enabled = true;
            }
        }

        /// <summary>
        /// The initialize edit box.
        /// </summary>
        private void InitializeEditBox()
        {
            this.editBox.Hide();
            this.listBoxSearchItems.Controls.AddRange(new Control[] { this.editBox });
            this.editBox.Text = string.Empty;
            this.editBox.BackColor = Color.Beige;
            this.editBox.ForeColor = Color.Black;
            this.editBox.BorderStyle = BorderStyle.FixedSingle;
            this.editBox.KeyPress += this.ListBoxSearchItemsEditOver;
            this.editBox.LostFocus += this.ListBoxSearchItemsFocusOver;
        }

        /// <summary>
        /// The list box search items double click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void ListBoxSearchItemsDoubleClick(object sender, EventArgs e)
        {
            var index = this.listBoxSearchItems.SelectedIndex;
            if (index != -1)
            {
                this.IsAdd = false;
                this.CreateEditBox(sender);
            }
            else
            {
                this.IsAdd = true;
                this.CreateEditBoxAdd(sender);
            }
        }

        /// <summary>
        /// The list box search items edit over.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void ListBoxSearchItemsEditOver(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && this.IsAdd)
            {
                if (this.listBoxSearchItems.Items.Contains(this.editBox.Text))
                {
                    return;
                }

                this.listBoxSearchItems.Items.Add(this.editBox.Text);
                this.editBox.Hide();
                this.listBoxSearchItems.SelectedIndex = listBoxSearchItems.Items.Count - 1;
                this.listBoxSearchItems.SelectedIndex = -1;
            }

            if (e.KeyChar == 13 && !this.IsAdd)
            {
                var itemSelected = this.listBoxSearchItems.SelectedIndex;
                this.listBoxSearchItems.Items[itemSelected] = this.editBox.Text;
                this.editBox.Hide();
                this.listBoxSearchItems.SelectedItem = null;
            }

            if (e.KeyChar == 27)
            {
                this.editBox.Hide();
                this.listBoxSearchItems.SelectedIndex = listBoxSearchItems.Items.Count - 1;
                this.listBoxSearchItems.SelectedIndex = -1;
            }

            this.VerifyLayout();
        }

        /// <summary>
        /// The list box search items focus over.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void ListBoxSearchItemsFocusOver(object sender, EventArgs e)
        {
            if (this.IsAdd)
            {
                if (this.listBoxSearchItems.Items.Contains(this.editBox.Text))
                {
                    return;
                }

                this.listBoxSearchItems.Items.Add(this.editBox.Text);
                this.editBox.Hide();
                this.listBoxSearchItems.SelectedIndex = listBoxSearchItems.Items.Count - 1;
                this.listBoxSearchItems.SelectedIndex = -1;
            }

            if (!this.IsAdd)
            {
                var itemSelected = this.listBoxSearchItems.SelectedIndex;
                this.listBoxSearchItems.Items[itemSelected] = this.editBox.Text;
                this.editBox.Hide();
                this.listBoxSearchItems.SelectedItem = null;
            }

            this.VerifyLayout();
        }

        /// <summary>
        /// The list box search items key down.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void ListBoxSearchItemsKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F2)
            {
                this.CreateEditBox(sender);
                this.IsAdd = false;
            }

            if (e.KeyData == Keys.Delete && this.listBoxSearchItems.SelectedIndex != -1)
            {
                this.listBoxSearchItems.Items.RemoveAt(this.listBoxSearchItems.SelectedIndex);
                this.listBoxSearchItems.SelectedItem = null;
                this.IsAdd = false;
            }

            if (e.KeyData == Keys.Escape)
            {
                this.editBox.Hide();
                this.listBoxSearchItems.SelectedItem = null;
                this.IsAdd = false;
            }

            this.VerifyLayout();
        }

        /// <summary>
        /// The list box search items key press.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void ListBoxSearchItemsKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && this.IsAdd)
            {
                this.CreateEditBoxAdd(sender);
            }

            if (e.KeyChar == 13 && !this.IsAdd)
            {
                this.CreateEditBox(sender);
            }

            if (e.KeyChar == 27 && !this.IsAdd)
            {
                this.editBox.Hide();
                this.listBoxSearchItems.SelectedItem = null;
            }

            this.VerifyLayout();
        }

        /// <summary>
        /// The populate combo box.
        /// </summary>
        private void PopulateComboBox()
        {
            if (this.Mode == 0 && comboBoxDataType.Items.Contains("Text"))
            {
                return;
            }

            if (this.Mode == 0 && !comboBoxDataType.Items.Contains("Text"))
            {
                this.comboBoxDataType.Items.Add("Text");
            }

            if (this.Mode == 1 && comboBoxDataType.Items.Contains("Numeric"))
            {
                return;
            }

            if (this.Mode == 1 && !comboBoxDataType.Items.Contains("Numeric"))
            {
                this.comboBoxDataType.Items.Add("Numeric");
            }
        }

        /// <summary>
        /// The prepare dialog.
        /// </summary>
        private void PrepareDialog()
        {
            this.btnCreateNumericTrees.Enabled = false;
            this.btnCreateTextTrees.Enabled = false;
            this.btnBatchSearch.Enabled = false;
            this.btnExport.Enabled = false;
            this.listBoxSearchItems.Enabled = false;
            this.listBoxSearchItems.Items.Add("Enter search data here");
        }

        /// <summary>
        /// The verify layout.
        /// </summary>
        private void VerifyLayout()
        {
            var existBatchTextTree = false;
            var existBatchNumericTree = false;
            foreach (var batchTree in this.provider.BatchTreeObject)
            {
                if (batchTree.Type.Equals("text"))
                {
                    existBatchTextTree = true;
                }

                if (batchTree.Type.Equals("numeric"))
                {
                    existBatchNumericTree = true;
                }

                if (existBatchTextTree && existBatchNumericTree)
                {
                    break;
                }
            }

            if (this.listBoxSearchItems.Enabled && this.listBoxSearchItems.Items.Count > 0
                && (existBatchTextTree || existBatchNumericTree))
            {
                this.btnBatchSearch.Enabled = true;
                return;
            }

            this.btnBatchSearch.Enabled = false;
        }

        #endregion
    }
}
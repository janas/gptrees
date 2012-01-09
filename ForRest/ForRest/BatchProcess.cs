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
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Threading;
    using System.Windows.Forms;

    using ForRest.Provider.BLL;

    /// <summary>
    /// The batch process.
    /// </summary>
    public partial class BatchProcess : Form
    {
        #region Constants and Fields

        /// <summary>
        /// The _numeric items.
        /// </summary>
        private readonly List<double> _numericItems = new List<double>();

        /// <summary>
        /// The _provider.
        /// </summary>
        private readonly Provider.Provider _provider;

        /// <summary>
        /// The _is help shown.
        /// </summary>
        private bool _isHelpShown = true;

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
            this._provider = provider;
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
            this.textBoxLog.AppendText(
                "[" + DateTime.Now.TimeOfDay + "]" + "\t" + noOfTrees + " tree(s) created, type of " + treeType
                + ", group tree name " + groupTreeName + ".\n");
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
            string comboBoxDataTypeSelectedItem = string.Empty;
            int threadId = Thread.CurrentThread.ManagedThreadId;
            if (this.comboBoxDataType.InvokeRequired)
            {
                this.comboBoxDataType.Invoke(
                    new MethodInvoker(
                        delegate { comboBoxDataTypeSelectedItem = this.comboBoxDataType.SelectedItem.ToString(); }));
            }
            else
            {
                comboBoxDataTypeSelectedItem = this.comboBoxDataType.SelectedItem.ToString();
            }

            this.textBoxLog.Invoke(
                (MethodInvoker)
                (() =>
                 this.textBoxLog.AppendText(
                     "[" + DateTime.Now.TimeOfDay + "]" + "\tThread ID " + threadId + " is started.\n")));
            switch (comboBoxDataTypeSelectedItem)
            {
                case "Text":
                    foreach (var item in this.listBoxSearchItems.Items)
                    {
                        foreach (var batchTextTree in this._provider.BatchTreeObject)
                        {
                            if (!batchTextTree.Type.Equals("text"))
                            {
                                continue;
                            }

                            var watch = new Stopwatch();
                            watch.Start();
                            List<int> result = batchTextTree.TextTree.Contains(item.ToString());
                            watch.Stop();
                            if (result != null)
                            {
                                var peroformanceSet = new PerformanceSet
                                    {
                                        TreeName = batchTextTree.Name, 
                                        SearchTime = watch.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture), 
                                        TypeOfNodes = "String", 
                                        TypeOfTree = batchTextTree.TextTree.TreeType, 
                                        NoOfNodes = "notImplemented"
                                    };
                                this._provider.BatchPerformanceSet.Add(peroformanceSet);

                                this.textBoxLog.Invoke(
                                    (MethodInvoker)
                                    (() =>
                                     this.textBoxLog.AppendText(
                                         "[" + DateTime.Now.TimeOfDay + "]" + "\tGroup tree name: " + batchTextTree.Name
                                         + " Value: " + item + " Found in " + watch.ElapsedMilliseconds
                                         + " ms for tree type: " + batchTextTree.TextTree.TreeType + "\n")));
                            }
                            else
                            {
                                var peroformanceSet = new PerformanceSet
                                    {
                                        TreeName = batchTextTree.Name, 
                                        SearchTime = watch.ElapsedMilliseconds + "/Not Found", 
                                        TypeOfNodes = "String", 
                                        TypeOfTree = batchTextTree.TextTree.TreeType, 
                                        NoOfNodes = "notImplemented"
                                    };
                                this._provider.BatchPerformanceSet.Add(peroformanceSet);

                                this.textBoxLog.Invoke(
                                    (MethodInvoker)
                                    (() =>
                                     this.textBoxLog.AppendText(
                                         "[" + DateTime.Now.TimeOfDay + "]" + "\tGroup tree name: " + batchTextTree.Name
                                         + " Value: " + item + " Not found in " + watch.ElapsedMilliseconds
                                         + " ms for tree type: " + batchTextTree.TextTree.TreeType + "\n")));
                            }
                        }
                    }

                    break;
                case "Numeric":
                    foreach (var item in this._numericItems)
                    {
                        foreach (var batchNumericTree in this._provider.BatchTreeObject)
                        {
                            if (!batchNumericTree.Type.Equals("numeric"))
                            {
                                continue;
                            }

                            var watch = new Stopwatch();
                            watch.Start();
                            List<int> result = batchNumericTree.NumericTree.Contains(item);
                            watch.Stop();
                            if (result != null)
                            {
                                var performanceSet = new PerformanceSet
                                    {
                                        TreeName = batchNumericTree.Name, 
                                        SearchTime = watch.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture), 
                                        TypeOfNodes = "Double", 
                                        TypeOfTree = batchNumericTree.NumericTree.TreeType, 
                                        NoOfNodes = "notImplemented"
                                    };
                                this._provider.BatchPerformanceSet.Add(performanceSet);
                                this.textBoxLog.Invoke(
                                    (MethodInvoker)
                                    (() =>
                                     this.textBoxLog.AppendText(
                                         "[" + DateTime.Now.TimeOfDay + "]" + "\tGroup tree name: "
                                         + batchNumericTree.Name + " Value: " + item + " Found in "
                                         + watch.ElapsedMilliseconds + " ms for tree type: "
                                         + batchNumericTree.NumericTree.TreeType + "\n")));
                            }
                            else
                            {
                                var peroformanceSet = new PerformanceSet
                                    {
                                        TreeName = batchNumericTree.Name, 
                                        SearchTime = watch.ElapsedMilliseconds + "/Not Found", 
                                        TypeOfNodes = "Double", 
                                        TypeOfTree = batchNumericTree.NumericTree.TreeType, 
                                        NoOfNodes = "notImplemented"
                                    };
                                this._provider.BatchPerformanceSet.Add(peroformanceSet);
                                this.textBoxLog.Invoke(
                                    (MethodInvoker)
                                    (() =>
                                     this.textBoxLog.AppendText(
                                         "[" + DateTime.Now.TimeOfDay + "]" + "\tGroup tree name: "
                                         + batchNumericTree.Name + " Value: " + item + " Not found in "
                                         + watch.ElapsedMilliseconds + " ms for tree type: "
                                         + batchNumericTree.NumericTree.TreeType + "\n")));
                            }
                        }
                    }

                    break;
            }

            e.Result = Thread.CurrentThread.ManagedThreadId;
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
        private void BackgroundWorkerBatchProcessRunWorkerCompleted(
            object sender, RunWorkerCompletedEventArgs e)
        {
            this.textBoxLog.Invoke(
                (MethodInvoker)
                (() =>
                 this.textBoxLog.AppendText(
                     "[" + DateTime.Now.TimeOfDay + "]" + "\tThread ID " + e.Result + " is done.\n")));
            this.EnableExportButton();
            this.btnBatchSearch.Enabled = true;
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
                this.textBoxLog.AppendText(
                    "[" + DateTime.Now.TimeOfDay
                    + "]\tNone value has been processed. Please provide correct input for numeric trees!\n");
            }
            else if (this.comboBoxDataType.SelectedItem.Equals("Numeric") && result)
            {
                this.textBoxLog.AppendText(
                    "[" + DateTime.Now.TimeOfDay + "]\tSuccessfully converted " + this._numericItems.Count
                    + " items out of " + this.listBoxSearchItems.Items.Count + " items.\n");
                this.backgroundWorkerBatchProcess.RunWorkerAsync();
                this.EnableExportButton();
                this.btnBatchSearch.Enabled = false;
            }
            else if (this.comboBoxDataType.SelectedItem.Equals("Text"))
            {
                this.backgroundWorkerBatchProcess.RunWorkerAsync();
                this.EnableExportButton();
                this.btnBatchSearch.Enabled = false;
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
            if (this._provider.BatchNumericData.Count == 0)
            {
                return;
            }

            var addTree = new AddTree(this._provider, true, 1, true) { Owner = this };
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
            if (this._provider.BatchTextData.Count == 0)
            {
                return;
            }

            var addTree = new AddTree(this._provider, true, 0, true) { Owner = this };
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
                this._provider.WriteResults(this._provider.BatchPerformanceSet, this.saveFileDialog.FileName);
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
            var openDialog = new OpenDialog(this._provider, true) { Owner = this };
            openDialog.ShowDialog();
            this.textBoxLog.AppendText("[" + DateTime.Now.TimeOfDay + "]" + "\tFiles loaded.\n");
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

            if (this._isHelpShown)
            {
                this.listBoxSearchItems.Items.Clear();
                this._isHelpShown = false;
            }

            this.textBoxLog.AppendText(
                "[" + DateTime.Now.TimeOfDay + "]" + "\t" + this.comboBoxDataType.SelectedItem
                + " data type selected.\n");
        }

        /// <summary>
        /// The convert to double.
        /// </summary>
        /// <returns>
        /// Returns true if number of items in listbox is greather than 0, fale otherwise.
        /// </returns>
        private bool ConvertToDouble()
        {
            foreach (var item in this.listBoxSearchItems.Items)
            {
                double numericValue;
                if (double.TryParse(item.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out numericValue))
                {
                    this._numericItems.Add(numericValue);
                }
            }

            return this._numericItems.Count > 0;
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
            switch (this.Mode)
            {
                case 0:
                    if (this.btnCreateTextTrees.Enabled == false)
                    {
                        this.btnCreateTextTrees.Enabled = true;
                    }

                    break;
                case 1:
                    if (this.btnCreateNumericTrees.Enabled == false)
                    {
                        this.btnCreateNumericTrees.Enabled = true;
                    }

                    break;
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

            if (this._provider.BatchPerformanceSet.Count > 0)
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
            if (e.KeyChar == 13)
            {
                switch (this.IsAdd)
                {
                    case true:
                        if (this.listBoxSearchItems.Items.Contains(this.editBox.Text))
                        {
                            return;
                        }

                        this.listBoxSearchItems.Items.Add(this.editBox.Text);
                        this.editBox.Hide();
                        break;
                    case false:
                        var itemSelected = this.listBoxSearchItems.SelectedIndex;
                        this.listBoxSearchItems.Items[itemSelected] = this.editBox.Text;
                        this.editBox.Hide();
                        this.listBoxSearchItems.SelectedItem = null;
                        break;
                }
            }

            if (e.KeyChar == 27)
            {
                this.editBox.Hide();
                this.listBoxSearchItems.SelectedItem = null;
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
            switch (this.IsAdd)
            {
                case true:
                    if (this.listBoxSearchItems.Items.Contains(this.editBox.Text))
                    {
                        return;
                    }

                    this.listBoxSearchItems.Items.Add(this.editBox.Text);
                    this.editBox.Hide();
                    break;
                case false:
                    var itemSelected = this.listBoxSearchItems.SelectedIndex;
                    this.listBoxSearchItems.Items[itemSelected] = this.editBox.Text;
                    this.editBox.Hide();
                    this.listBoxSearchItems.SelectedItem = null;
                    break;
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
            }

            if (e.KeyData == Keys.Delete && this.listBoxSearchItems.SelectedIndex != -1)
            {
                this.listBoxSearchItems.Items.RemoveAt(this.listBoxSearchItems.SelectedIndex);
                this.listBoxSearchItems.SelectedItem = null;
            }

            if (e.KeyData == Keys.Escape)
            {
                this.editBox.Hide();
                this.listBoxSearchItems.SelectedItem = null;
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
            switch (this.IsAdd)
            {
                case true:
                    if (e.KeyChar == 13)
                    {
                        this.CreateEditBoxAdd(sender);
                    }

                    break;
                case false:
                    if (e.KeyChar == 13)
                    {
                        this.CreateEditBox(sender);
                    }

                    if (e.KeyChar == 27)
                    {
                        this.editBox.Hide();
                        this.listBoxSearchItems.SelectedItem = null;
                    }

                    break;
            }

            this.VerifyLayout();
        }

        /// <summary>
        /// The populate combo box.
        /// </summary>
        private void PopulateComboBox()
        {
            switch (this.Mode)
            {
                case 0:
                    this.comboBoxDataType.Items.Add("Text");
                    break;
                case 1:
                    this.comboBoxDataType.Items.Add("Numeric");
                    break;
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
            bool existBatchTextTree = false;
            bool existBatchNumericTree = false;
            foreach (var batchTree in _provider.BatchTreeObject)
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
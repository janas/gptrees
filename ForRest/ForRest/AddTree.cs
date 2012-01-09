// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddTree.cs" company="Warsaw University of Technology">
//   
// </copyright>
// <summary>
//   The add tree form.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForRest
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    using ForRest.Provider.BLL;

    /// <summary>
    /// The add tree form.
    /// </summary>
    public partial class AddTree : Form
    {
        #region Constants and Fields

        /// <summary>
        ///   The _from file.
        /// </summary>
        private readonly bool _fromFile;

        /// <summary>
        ///   The _group.
        /// </summary>
        private readonly bool _group;

        /// <summary>
        ///   The _mode.
        /// </summary>
        private readonly int _mode;

        /// <summary>
        ///   The _provider.
        /// </summary>
        private readonly Provider.Provider _provider;

        /// <summary>
        ///   The _group tree name.
        /// </summary>
        private string _groupTreeName;

        /// <summary>
        ///   The _no of trees.
        /// </summary>
        private int _noOfTrees;

        /// <summary>
        ///   The _type of trees.
        /// </summary>
        private string _typeOfTrees;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AddTree"/> class.
        /// </summary>
        /// <param name="provider">
        /// The provider. 
        /// </param>
        /// <param name="fromFile">
        /// The from file. 
        /// </param>
        public AddTree(Provider.Provider provider, bool fromFile)
        {
            this.InitializeComponent();
            this.InitializeAdd();
            this._provider = provider;
            this._fromFile = fromFile;
            this.FillTrees();
            this.SetTitleLabel();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddTree"/> class.
        /// </summary>
        /// <param name="provider">
        /// The provider. 
        /// </param>
        /// <param name="fromFile">
        /// The from file. 
        /// </param>
        /// <param name="mode">
        /// The mode. 
        /// </param>
        public AddTree(Provider.Provider provider, bool fromFile, int mode)
        {
            this.InitializeComponent();
            this._provider = provider;
            this._fromFile = fromFile;
            this._mode = mode;
            this.FillTrees();
            this.SetTitleLabel();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddTree"/> class.
        /// </summary>
        /// <param name="provider">
        /// The provider. 
        /// </param>
        /// <param name="fromFile">
        /// The from file. 
        /// </param>
        /// <param name="mode">
        /// The mode. 
        /// </param>
        /// <param name="group">
        /// The group. 
        /// </param>
        public AddTree(Provider.Provider provider, bool fromFile, int mode, bool group)
        {
            this.InitializeComponent();
            this._provider = provider;
            this._fromFile = fromFile;
            this._mode = mode;
            this._group = @group;
            this.FillTrees();
            this.SetTitleLabel();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The process cmd key.
        /// </summary>
        /// <param name="msg">
        /// The msg. 
        /// </param>
        /// <param name="keyData">
        /// The key data. 
        /// </param>
        /// <returns>
        /// Returns true if Return button was pressed. 
        /// </returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData != Keys.Return)
            {
                return false;
            }

            this.btnAdd.PerformClick();
            return true;
        }

        /// <summary>
        /// The background worker add tree do work.
        /// </summary>
        /// <param name="sender">
        /// The sender. 
        /// </param>
        /// <param name="e">
        /// The e. 
        /// </param>
        private void BackgroundWorkerAddTreeDoWork(object sender, DoWorkEventArgs e)
        {
            int comboBoxDataTypeSelectedIndex = -1;
            if (this.comboBoxDataType.InvokeRequired)
            {
                this.comboBoxDataType.Invoke(
                    new MethodInvoker(delegate { comboBoxDataTypeSelectedIndex = this.comboBoxDataType.SelectedIndex; }));
            }
            else
            {
                comboBoxDataTypeSelectedIndex = this.comboBoxDataType.SelectedIndex;
            }

            string maskedTextBoxDegreeText = string.Empty;
            if (this.maskedTextBoxDegree.InvokeRequired)
            {
                this.maskedTextBoxDegree.Invoke(
                    new MethodInvoker(delegate { maskedTextBoxDegreeText = this.maskedTextBoxDegree.Text; }));
            }
            else
            {
                maskedTextBoxDegreeText = this.maskedTextBoxDegree.Text;
            }

            string textBoxNameText = string.Empty;
            if (this.textBoxName.InvokeRequired)
            {
                this.textBoxName.Invoke(new MethodInvoker(delegate { textBoxNameText = this.textBoxName.Text; }));
            }
            else
            {
                textBoxNameText = this.textBoxName.Text;
            }

            object comboBoxDataTypeSelectedItem = null;
            if (this.comboBoxDataType.InvokeRequired)
            {
                this.comboBoxDataType.Invoke(
                    new MethodInvoker(delegate { comboBoxDataTypeSelectedItem = this.comboBoxDataType.Text; }));
            }
            else
            {
                comboBoxDataTypeSelectedItem = this.comboBoxDataType.Text;
            }

            var factory = e.Argument as ITreeFactory;
            switch (factory.NeedDegree)
            {
                case true:

                    // from file, group
                    if (this._fromFile && this._group)
                    {
                        if (this._mode == 0 && this._provider.BatchTextData != null
                            && !string.IsNullOrEmpty(maskedTextBoxDegreeText))
                        {
                            this._noOfTrees = 0;
                            foreach (var batchTree in this._provider.BatchTextData)
                            {
                                int count = batchTree.Count;
                                ITree<string> tree = factory.GetTree<string>(int.Parse(maskedTextBoxDegreeText));
                                for (int i = 0; i < batchTree.Count; i++)
                                {
                                    tree.Add(batchTree[i]);
                                    int progress = (i * 100) / count;
                                    this.backgroundWorkerAddTree.ReportProgress(progress);
                                }

                                var treeObject = new TreeObject(textBoxNameText.Trim(), "text", tree);
                                this._provider.BatchTreeObject.Add(treeObject);
                                this._typeOfTrees = treeObject.Type;
                                this._noOfTrees++;
                            }

                            this._groupTreeName = textBoxNameText.Trim();
                            e.Result = true;
                            return;
                        }

                        if (this._mode == 1 && this._provider.BatchNumericData != null
                            && !string.IsNullOrEmpty(maskedTextBoxDegreeText))
                        {
                            this._noOfTrees = 0;
                            foreach (var batchTree in this._provider.BatchNumericData)
                            {
                                int count = batchTree.Count;
                                ITree<double> tree = factory.GetTree<double>(int.Parse(maskedTextBoxDegreeText));
                                for (int i = 0; i < batchTree.Count; i++)
                                {
                                    tree.Add(batchTree[i]);
                                    int progress = (i * 100) / count;
                                    this.backgroundWorkerAddTree.ReportProgress(progress);
                                }

                                var treeObject = new TreeObject(textBoxNameText.Trim(), "numeric", tree);
                                this._provider.BatchTreeObject.Add(treeObject);
                                this._typeOfTrees = treeObject.Type;
                                this._noOfTrees++;
                            }

                            this._groupTreeName = textBoxNameText.Trim();
                            e.Result = true;
                            return;
                        }

                        if (string.IsNullOrEmpty(maskedTextBoxDegreeText))
                        {
                            e.Result = false;
                            return;
                        }
                    }

                    // from file, not group
                    if (this._fromFile && !this._group)
                    {
                        if (this._mode == 0 && this._provider.TextData != null
                            && !string.IsNullOrEmpty(maskedTextBoxDegreeText))
                        {
                            ITree<string> tree = factory.GetTree<string>(int.Parse(maskedTextBoxDegreeText));
                            int count = this._provider.TextData.Count;
                            for (int i = 0; i < count; i++)
                            {
                                tree.Add(this._provider.TextData[i]);
                                int progress = (i * 100) / count;
                                this.backgroundWorkerAddTree.ReportProgress(progress);
                            }

                            var treeObject = new TreeObject(textBoxNameText.Trim(), "text", tree);
                            this._provider.TreeObjects.Add(treeObject);
                            e.Result = true;
                            return;
                        }

                        if (this._mode == 1 && this._provider.NumericData != null
                            && !string.IsNullOrEmpty(maskedTextBoxDegreeText))
                        {
                            ITree<double> tree = factory.GetTree<double>(int.Parse(maskedTextBoxDegreeText));
                            int count = this._provider.NumericData.Count;
                            for (int i = 0; i < count; i++)
                            {
                                tree.Add(this._provider.NumericData[i]);
                                int progress = (i * 100) / count;
                                this.backgroundWorkerAddTree.ReportProgress(progress);
                            }

                            var treeObject = new TreeObject(textBoxNameText.Trim(), "numeric", tree);
                            this._provider.TreeObjects.Add(treeObject);
                            e.Result = true;
                            return;
                        }

                        if (string.IsNullOrEmpty(maskedTextBoxDegreeText))
                        {
                            e.Result = false;
                            return;
                        }
                    }                        
                        // not from file, not group
                    else if (!this._fromFile && !this._group)
                    {
                        if (comboBoxDataTypeSelectedItem != null && !string.IsNullOrEmpty(textBoxNameText)
                            && !string.IsNullOrEmpty(maskedTextBoxDegreeText))
                        {
                            int mode = comboBoxDataTypeSelectedIndex;
                            switch (mode)
                            {
                                case 0:
                                    ITree<string> textTree = factory.GetTree<string>(int.Parse(maskedTextBoxDegreeText));
                                    var treeObjectText = new TreeObject(textBoxNameText.Trim(), "text", textTree);
                                    this._provider.TreeObjects.Add(treeObjectText);
                                    this.progressBarAddTree.Invoke(
                                        (MethodInvoker)
                                        (() => this.progressBarAddTree.Value = this.progressBarAddTree.Maximum));
                                    Application.DoEvents();
                                    Thread.Sleep(500);
                                    break;
                                case 1:
                                    ITree<double> numericTree =
                                        factory.GetTree<double>(int.Parse(maskedTextBoxDegreeText));
                                    var treeObjectNumeric = new TreeObject(
                                        textBoxNameText.Trim(), "numeric", numericTree);
                                    this._provider.TreeObjects.Add(treeObjectNumeric);
                                    this.progressBarAddTree.Invoke(
                                        (MethodInvoker)
                                        (() => this.progressBarAddTree.Value = this.progressBarAddTree.Maximum));
                                    Application.DoEvents();
                                    Thread.Sleep(500);
                                    break;
                            }

                            e.Result = true;
                            return;
                        }

                        if (comboBoxDataTypeSelectedItem == null)
                        {
                            e.Result = false;
                            return;
                        }

                        if (string.IsNullOrEmpty(maskedTextBoxDegreeText))
                        {
                            e.Result = false;
                            return;
                        }
                    }

                    break;
                case false:

                    // from file, group
                    if (this._fromFile && this._group)
                    {
                        if (this._mode == 0 && this._provider.BatchTextData != null)
                        {
                            this._noOfTrees = 0;
                            foreach (var batchTree in this._provider.BatchTextData)
                            {
                                int count = batchTree.Count;
                                ITree<string> tree = factory.GetTree<string>();
                                for (int i = 0; i < batchTree.Count; i++)
                                {
                                    tree.Add(batchTree[i]);
                                    int progress = (i * 100) / count;
                                    this.backgroundWorkerAddTree.ReportProgress(progress);
                                }

                                var treeObject = new TreeObject(textBoxNameText.Trim(), "text", tree);
                                this._provider.BatchTreeObject.Add(treeObject);
                                this._typeOfTrees = treeObject.Type;
                                this._noOfTrees++;
                            }

                            this._groupTreeName = textBoxNameText.Trim();
                            e.Result = true;
                            return;
                        }

                        if (this._mode == 1 && this._provider.BatchNumericData != null)
                        {
                            this._noOfTrees = 0;
                            foreach (var batchTree in this._provider.BatchNumericData)
                            {
                                int count = batchTree.Count;
                                ITree<double> tree = factory.GetTree<double>();
                                for (int i = 0; i < batchTree.Count; i++)
                                {
                                    tree.Add(batchTree[i]);
                                    int progress = (i * 100) / count;
                                    this.backgroundWorkerAddTree.ReportProgress(progress);
                                }

                                var treeObject = new TreeObject(textBoxNameText.Trim(), "numeric", tree);
                                this._provider.BatchTreeObject.Add(treeObject);
                                this._typeOfTrees = treeObject.Type;
                                this._noOfTrees++;
                            }

                            this._groupTreeName = textBoxNameText.Trim();
                            e.Result = true;
                            return;
                        }
                    }

                    // from file, not group
                    if (this._fromFile && !this._group)
                    {
                        if (this._mode == 0 && this._provider.TextData != null)
                        {
                            ITree<string> tree = factory.GetTree<string>();
                            int count = this._provider.TextData.Count;
                            for (int i = 0; i < count; i++)
                            {
                                tree.Add(this._provider.TextData[i]);
                                int progress = (i * 100) / count;
                                this.backgroundWorkerAddTree.ReportProgress(progress);
                            }

                            var treeObject = new TreeObject(textBoxNameText.Trim(), "text", tree);
                            this._provider.TreeObjects.Add(treeObject);
                            e.Result = true;
                            return;
                        }

                        if (this._mode == 1 && this._provider.NumericData != null)
                        {
                            ITree<double> tree = factory.GetTree<double>();
                            int count = this._provider.NumericData.Count;
                            for (int i = 0; i < count; i++)
                            {
                                tree.Add(this._provider.NumericData[i]);
                                int progress = (i * 100) / count;
                                this.backgroundWorkerAddTree.ReportProgress(progress);
                            }

                            var treeObject = new TreeObject(textBoxNameText.Trim(), "numeric", tree);
                            this._provider.TreeObjects.Add(treeObject);
                            e.Result = true;
                            return;
                        }
                    }                        
                        // not from file, not group
                    else if (!this._fromFile && !this._group)
                    {
                        if (comboBoxDataTypeSelectedItem != null && !string.IsNullOrEmpty(textBoxNameText))
                        {
                            int mode = comboBoxDataTypeSelectedIndex;
                            switch (mode)
                            {
                                case 0:
                                    ITree<string> textTree = factory.GetTree<string>();
                                    var treeObjectText = new TreeObject(textBoxNameText.Trim(), "text", textTree);
                                    this._provider.TreeObjects.Add(treeObjectText);
                                    this.progressBarAddTree.Invoke(
                                        (MethodInvoker)
                                        (() => this.progressBarAddTree.Value = this.progressBarAddTree.Maximum));
                                    Application.DoEvents();
                                    Thread.Sleep(500);
                                    break;
                                case 1:
                                    ITree<double> numericTree = factory.GetTree<double>();
                                    var treeObjectNumeric = new TreeObject(
                                        textBoxNameText.Trim(), "numeric", numericTree);
                                    this._provider.TreeObjects.Add(treeObjectNumeric);
                                    this.progressBarAddTree.Invoke(
                                        (MethodInvoker)
                                        (() => this.progressBarAddTree.Value = this.progressBarAddTree.Maximum));
                                    Application.DoEvents();
                                    Thread.Sleep(500);
                                    break;
                            }

                            e.Result = true;
                            return;
                        }

                        e.Result = false;
                        return;
                    }

                    break;
            }

            e.Result = false;
        }

        /// <summary>
        /// The background worker add tree progress changed.
        /// </summary>
        /// <param name="sender">
        /// The sender. 
        /// </param>
        /// <param name="e">
        /// The e. 
        /// </param>
        private void BackgroundWorkerAddTreeProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBarAddTree.Value = e.ProgressPercentage;
            var percent =
                (int)
                (((this.progressBarAddTree.Value - this.progressBarAddTree.Minimum)
                  / (double)(this.progressBarAddTree.Maximum - this.progressBarAddTree.Minimum)) * 100);
            using (Graphics gr = this.progressBarAddTree.CreateGraphics())
            {
                gr.DrawString(
                    percent.ToString() + "%", 
                    SystemFonts.DefaultFont, 
                    Brushes.Black, 
                    new PointF(
                        this.progressBarAddTree.Width / 2
                        - (gr.MeasureString(percent.ToString() + "%", SystemFonts.DefaultFont).Width / 2.0F), 
                        this.progressBarAddTree.Height / 2
                        - (gr.MeasureString(percent.ToString() + "%", SystemFonts.DefaultFont).Height / 2.0F)));
            }
        }

        /// <summary>
        /// The background worker add tree run worker completed.
        /// </summary>
        /// <param name="sender">
        /// The sender. 
        /// </param>
        /// <param name="e">
        /// The e. 
        /// </param>
        private void BackgroundWorkerAddTreeRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var result = (bool)e.Result;
            if (result != true)
            {
                this.btnAdd.Enabled = true;
                return;
            }

            this.PopulateComboBox();
            this.Close();
        }

        /// <summary>
        /// The btn add click.
        /// </summary>
        /// <param name="sender">
        /// The sender. 
        /// </param>
        /// <param name="e">
        /// The e. 
        /// </param>
        private void BtnAddClick(object sender, EventArgs e)
        {
            var factory = (ITreeFactory)this.comboBoxAvailableTrees.SelectedItem;
            switch (factory.NeedDegree)
            {
                case true:

                    // from file, group
                    if (this._fromFile && this._group)
                    {
                        if (string.IsNullOrEmpty(this.maskedTextBoxDegree.Text)
                            || int.Parse(this.maskedTextBoxDegree.Text) < 2)
                        {
                            this.toolTipHelper.ToolTipTitle = "Wrong degree is entered";
                            this.toolTipHelper.Show(
                                "Please key in correct tree degree first.", this.maskedTextBoxDegree, 3000);
                            return;
                        }
                    }

                    // from file, not group
                    if (this._fromFile && !this._group)
                    {
                        if (string.IsNullOrEmpty(this.maskedTextBoxDegree.Text)
                            || int.Parse(this.maskedTextBoxDegree.Text) < 2)
                        {
                            this.toolTipHelper.ToolTipTitle = "Wrong degree is entered";
                            this.toolTipHelper.Show(
                                "Please key in correct tree degree first.", this.maskedTextBoxDegree, 3000);
                            return;
                        }
                    }

                        // not from file, not group
                    else if (!this._fromFile && !this._group)
                    {
                        if (this.comboBoxDataType.SelectedItem == null)
                        {
                            this.toolTipHelper.ToolTipTitle = "No data type is selected";
                            this.toolTipHelper.Show(
                                "Please select data type from the list first.", this.comboBoxDataType, 3000);
                            return;
                        }

                        if (string.IsNullOrEmpty(this.maskedTextBoxDegree.Text)
                            || int.Parse(this.maskedTextBoxDegree.Text) < 2)
                        {
                            this.toolTipHelper.ToolTipTitle = "Wrong degree is entered";
                            this.toolTipHelper.Show(
                                "Please key in correct tree degree first.", this.maskedTextBoxDegree, 3000);
                            return;
                        }
                    }

                    break;
                case false:

                    // not from file, not group
                    if (!this._fromFile && !this._group)
                    {
                        if (this.comboBoxDataType.SelectedItem == null)
                        {
                            this.toolTipHelper.ToolTipTitle = "No data type is selected";
                            this.toolTipHelper.Show(
                                "Please select data type from the list first.", this.comboBoxDataType, 3000);
                            return;
                        }
                    }

                    break;
            }

            this.btnAdd.Enabled = false;
            this.backgroundWorkerAddTree.RunWorkerAsync(factory);
        }

        /// <summary>
        /// The btn add enable disable.
        /// </summary>
        private void BtnAddEnableDisable()
        {
            if (string.IsNullOrEmpty(this.textBoxName.Text.Trim()))
            {
                this.btnAdd.Enabled = false;
                return;
            }

            var factory = (ITreeFactory)this.comboBoxAvailableTrees.SelectedItem;
            switch (this._fromFile)
            {
                case true:
                    if (factory.NeedDegree
                        &&
                        (string.IsNullOrEmpty(this.maskedTextBoxDegree.Text)
                         || int.Parse(this.maskedTextBoxDegree.Text) < 2))
                    {
                        this.btnAdd.Enabled = false;
                        return;
                    }
                    else
                    {
                    }

                    break;
                case false:
                    if (factory.NeedDegree
                        &&
                        (string.IsNullOrEmpty(this.maskedTextBoxDegree.Text)
                         || int.Parse(this.maskedTextBoxDegree.Text) < 2))
                    {
                        this.btnAdd.Enabled = false;
                        return;
                    }
                    if (this.comboBoxDataType.SelectedItem == null)
                    {
                        this.btnAdd.Enabled = false;
                        return;
                    }

                    break;
            }

            this.btnAdd.Enabled = true;
        }

        /// <summary>
        /// The combo box available trees selected index changed.
        /// </summary>
        /// <param name="sender">
        /// The sender. 
        /// </param>
        /// <param name="e">
        /// The e. 
        /// </param>
        private void ComboBoxAvailableTreesSelectedIndexChanged(object sender, EventArgs e)
        {
            var factory = (ITreeFactory)this.comboBoxAvailableTrees.SelectedItem;
            switch (this._fromFile)
            {
                case true:
                    if (factory.NeedDegree)
                    {
                        this.Width = 315;
                        this.labelTreeDegree.Visible = true;
                        this.maskedTextBoxDegree.Visible = true;
                        this.btnAdd.Location = new Point(86, 140);
                        this.ShowToolTip();
                    }

                    if (factory.NeedDegree == false)
                    {
                        this.Width = 175;
                        this.labelTreeDegree.Visible = false;
                        this.maskedTextBoxDegree.Visible = false;
                        this.btnAdd.Location = new Point(12, 140);
                    }

                    break;
                case false:
                    if (factory.NeedDegree)
                    {
                        this.Width = 315;
                        this.labelTreeDegree.Visible = true;
                        this.maskedTextBoxDegree.Visible = true;
                        this.btnAdd.Location = new Point(86, 140);
                        this.ShowToolTip();
                    }

                    if (factory.NeedDegree == false)
                    {
                        this.Width = 315;
                        this.labelTreeDegree.Visible = false;
                        this.maskedTextBoxDegree.Visible = false;
                        this.btnAdd.Location = new Point(86, 140);
                    }

                    break;
            }

            this.BtnAddEnableDisable();
        }

        /// <summary>
        /// The fill trees.
        /// </summary>
        private void FillTrees()
        {
            string applicationPath = Application.ExecutablePath;
            this._provider.CreatePluginList(applicationPath);
            this.comboBoxAvailableTrees.DataSource = this._provider.PluginList;
            this.comboBoxAvailableTrees.DisplayMember = "Name";
        }

        /// <summary>
        /// The initialize add.
        /// </summary>
        private void InitializeAdd()
        {
            this.Width = 315;
            this.labelDataType.Visible = true;
            this.comboBoxDataType.Visible = true;
            this.btnAdd.Location = new Point(86, 140);
        }

        /// <summary>
        /// The masked text box degree mask input rejected.
        /// </summary>
        /// <param name="sender">
        /// The sender. 
        /// </param>
        /// <param name="e">
        /// The e. 
        /// </param>
        private void MaskedTextBoxDegreeMaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            this.toolTipHelper.ToolTipTitle = "Invalid input";
            this.toolTipHelper.Show("We're sorry, but only digits (0-9) are allowed.", this.maskedTextBoxDegree, 3000);
        }

        /// <summary>
        /// The populate combo box.
        /// </summary>
        private void PopulateComboBox()
        {
            if (this._group == false)
            {
                var owner = (Create)this.Owner;
                owner.FillSelectedTreeComboBox();
            }
            else
            {
                var owner = (BatchProcess)this.Owner;
                owner.UpdateLogOnCreate(this._noOfTrees, this._typeOfTrees, this._groupTreeName);
            }
        }

        /// <summary>
        /// The set title label.
        /// </summary>
        private void SetTitleLabel()
        {
            this.labelMode.ResetText();
            if (!this._fromFile && !this._group)
            {
                this.labelMode.Text = "Adding tree manually";
            }

            if (this._fromFile && !this._group)
            {
                switch (this._mode)
                {
                    case 0:
                        this.labelMode.Text = "Adding text tree from file";
                        break;
                    case 1:
                        this.labelMode.Text = "Adding numeric tree from file";
                        break;
                }
            }

            if (this._fromFile && this._group)
            {
                switch (this._mode)
                {
                    case 0:
                        this.labelMode.Text = "Adding text trees in batch";
                        break;
                    case 1:
                        this.labelMode.Text = "Adding numeric trees in batch";
                        break;
                }
            }
        }

        /// <summary>
        /// The show tool tip.
        /// </summary>
        private void ShowToolTip()
        {
            this.toolTipHelper.ToolTipTitle = "Please enter tree degree";
            this.toolTipHelper.Show("Please key in tree degree first.", this.maskedTextBoxDegree, 3000);
        }

        /// <summary>
        /// The text box name text changed.
        /// </summary>
        /// <param name="sender">
        /// The sender. 
        /// </param>
        /// <param name="e">
        /// The e. 
        /// </param>
        private void TextBoxNameTextChanged(object sender, EventArgs e)
        {
            this.BtnAddEnableDisable();
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
            this.BtnAddEnableDisable();
        }

        /// <summary>
        /// The masked text box degree text changed.
        /// </summary>
        /// <param name="sender">
        /// The sender. 
        /// </param>
        /// <param name="e">
        /// The e. 
        /// </param>
        private void MaskedTextBoxDegreeTextChanged(object sender, EventArgs e)
        {
            this.BtnAddEnableDisable();
        }

        #endregion
    }
}
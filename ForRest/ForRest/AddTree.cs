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
    using System.Windows.Forms;

    using ForRest.BLL;
    using ForRest.Provider.BLL;

    /// <summary>
    /// The add tree form.
    /// </summary>
    public partial class AddTree : Form
    {
        #region Constants and Fields

        /// <summary>
        ///   The from file.
        /// </summary>
        private readonly bool fromFile;

        /// <summary>
        ///   The group.
        /// </summary>
        private readonly bool @group;

        /// <summary>
        ///   The provider.
        /// </summary>
        private readonly Provider.Provider provider;

        /// <summary>
        ///   The mode.
        /// </summary>
        private int mode;
        
        /// <summary>
        ///   The group tree name.
        /// </summary>
        private string groupTreeName;

        /// <summary>
        ///   The no of trees.
        /// </summary>
        private int noOfTrees;

        /// <summary>
        ///   The type of trees.
        /// </summary>
        private string typeOfTrees;

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
            this.provider = provider;
            this.fromFile = fromFile;
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
            this.provider = provider;
            this.fromFile = fromFile;
            this.mode = mode;
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
            this.provider = provider;
            this.fromFile = fromFile;
            this.mode = mode;
            this.@group = @group;
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
            var factory = e.Argument as ITreeFactory;
            var treeBuilder = new TreeBuilder(this.provider, this.textBoxName.Text.Trim(), this.backgroundWorkerAddTree);
            
                if (this.fromFile)
                {
                    if (factory != null && factory.NeedDegree)
                    {
                        if (!this.group)
                        {
                            if (this.mode == 0)
                            {
                                treeBuilder.BuildTreeFromFile<string>(factory, int.Parse(maskedTextBoxDegree.Text));
                            }

                            if (this.mode == 1)
                            {
                                treeBuilder.BuildTreeFromFile<double>(factory, int.Parse(maskedTextBoxDegree.Text));
                            }
                        }
                        else
                        {
                            if (this.mode == 0)
                            {
                                treeBuilder.BuildBatchTree<string>(factory, int.Parse(maskedTextBoxDegree.Text));
                            }

                            if (this.mode == 1)
                            {
                                treeBuilder.BuildBatchTree<double>(factory, int.Parse(maskedTextBoxDegree.Text));
                            }

                            this.typeOfTrees = treeBuilder.TypeOfTrees;
                            this.noOfTrees = treeBuilder.NumberOfTrees;
                            this.groupTreeName = this.textBoxName.Text.Trim();
                        }
                    }
                    else
                    {
                        if (!this.group)
                        {
                            if (this.mode == 0)
                            {
                                treeBuilder.BuildTreeFromFile<string>(factory);
                            }

                            if (this.mode == 1)
                            {
                                treeBuilder.BuildTreeFromFile<double>(factory);
                            }
                        }
                        else
                        {
                            if (this.mode == 0)
                            {
                                treeBuilder.BuildBatchTree<string>(factory);
                            }

                            if (this.mode == 1)
                            {
                                treeBuilder.BuildBatchTree<double>(factory);
                            }

                            this.typeOfTrees = treeBuilder.TypeOfTrees;
                            this.noOfTrees = treeBuilder.NumberOfTrees;
                            this.groupTreeName = this.textBoxName.Text.Trim();
                        }
                    }
                }
                else
                {
                    if (factory != null && factory.NeedDegree)
                    {
                        if (this.mode == 0)
                        {
                            treeBuilder.BuildTree<string>(factory, int.Parse(maskedTextBoxDegree.Text));
                        }

                        if (this.mode == 1)
                        {
                            treeBuilder.BuildTree<double>(factory, int.Parse(maskedTextBoxDegree.Text));
                        }
                    }
                    else
                    {
                        if (this.mode == 0)
                        {
                            treeBuilder.BuildTree<string>(factory);
                        }

                        if (this.mode == 1)
                        {
                            treeBuilder.BuildTree<double>(factory);
                        }
                    }
                }

                e.Result = true;
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
                comboBoxAvailableTrees.Enabled = true;
                comboBoxDataType.Enabled = true;
                textBoxName.Enabled = true;
                maskedTextBoxDegree.Enabled = true;
                btnAdd.Enabled = true;
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
            if (factory.NeedDegree)
            {
                {
                    // from file, group; from file not group
                    if ((this.fromFile && this.@group) || (this.fromFile && !this.@group))
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
                    else if (!this.fromFile && !this.@group)
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
                }
            }
            else
            {
                // not from file, not group
                if (!this.fromFile && !this.@group)
                {
                    if (this.comboBoxDataType.SelectedItem == null)
                    {
                        this.toolTipHelper.ToolTipTitle = "No data type is selected";
                        this.toolTipHelper.Show(
                            "Please select data type from the list first.", this.comboBoxDataType, 3000);
                        return;
                    }
                }
            }

            comboBoxAvailableTrees.Enabled = false;
            comboBoxDataType.Enabled = false;
            textBoxName.Enabled = false;
            maskedTextBoxDegree.Enabled = false;
            btnAdd.Enabled = false;
            this.backgroundWorkerAddTree.RunWorkerAsync(factory);
            Application.DoEvents();
        }

        /// <summary>
        /// The Add button enable/disable method.
        /// </summary>
        private void BtnAddEnableDisable()
        {
            if (string.IsNullOrEmpty(this.textBoxName.Text.Trim()))
            {
                this.btnAdd.Enabled = false;
                return;
            }

            var factory = (ITreeFactory)this.comboBoxAvailableTrees.SelectedItem;
            if (this.fromFile)
            {
                if (factory.NeedDegree
                    &&
                    (string.IsNullOrEmpty(this.maskedTextBoxDegree.Text) || int.Parse(this.maskedTextBoxDegree.Text) < 2))
                {
                    this.btnAdd.Enabled = false;
                    return;
                }
            }
            else
            {
                if (factory.NeedDegree
                    &&
                    (string.IsNullOrEmpty(this.maskedTextBoxDegree.Text) || int.Parse(this.maskedTextBoxDegree.Text) < 2))
                {
                    this.btnAdd.Enabled = false;
                    return;
                }

                if (this.comboBoxDataType.SelectedItem == null)
                {
                    this.btnAdd.Enabled = false;
                    return;
                }
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
            if (this.fromFile)
            {
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
            }
            else
            {
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
            }

            this.BtnAddEnableDisable();
        }

        /// <summary>
        /// The fill trees method.
        /// </summary>
        private void FillTrees()
        {
            string applicationPath = Application.ExecutablePath;
            this.provider.CreatePluginList(applicationPath);
            this.comboBoxAvailableTrees.DataSource = this.provider.PluginList;
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
            if (this.@group == false)
            {
                var owner = (Create)this.Owner;
                owner.FillSelectedTreeComboBox();
            }
            else
            {
                var owner = (BatchProcess)this.Owner;
                owner.UpdateLogOnCreate(this.noOfTrees, this.typeOfTrees, this.groupTreeName);
            }
        }

        /// <summary>
        /// The set title label.
        /// </summary>
        private void SetTitleLabel()
        {
            this.labelMode.ResetText();
            if (!this.fromFile && !this.@group)
            {
                this.labelMode.Text = "Adding tree manually";
            }

            if (this.fromFile && !this.@group)
            {
                switch (this.mode)
                {
                    case 0:
                        this.labelMode.Text = "Adding text tree from file";
                        break;
                    case 1:
                        this.labelMode.Text = "Adding numeric tree from file";
                        break;
                }
            }

            if (this.fromFile && this.@group)
            {
                switch (this.mode)
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
            this.mode = comboBoxDataType.SelectedIndex;
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
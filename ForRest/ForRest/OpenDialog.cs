// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OpenDialog.cs" company="Warsaw University of Technology">
//   
// </copyright>
// <summary>
//   The open dialog.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForRest
{
    using System;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    /// <summary>
    /// The open dialog.
    /// </summary>
    public partial class OpenDialog : Form
    {
        #region Constants and Fields

        /// <summary>
        /// The multiselect.
        /// </summary>
        private readonly bool multiselect;

        /// <summary>
        /// The provider.
        /// </summary>
        private readonly Provider.Provider provider = new Provider.Provider();

        /// <summary>
        /// The data type.
        /// </summary>
        private string dtType;

        /// <summary>
        /// The file path.
        /// </summary>
        private string filePath;

        /// <summary>
        /// The file paths.
        /// </summary>
        private string[] filePaths;

        /// <summary>
        /// The sep.
        /// </summary>
        private char sep;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenDialog"/> class.
        /// </summary>
        /// <param name="provider">
        /// The provider.
        /// </param>
        public OpenDialog(Provider.Provider provider)
        {
            this.InitializeComponent();
            this.provider = provider;
            this.multiselect = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenDialog"/> class.
        /// </summary>
        /// <param name="provider">
        /// The provider.
        /// </param>
        /// <param name="multiselect">
        /// The multiselect.
        /// </param>
        public OpenDialog(Provider.Provider provider, bool multiselect)
        {
            this.InitializeComponent();
            this.provider = provider;
            this.multiselect = multiselect;
        }

        #endregion

        #region Enums

        /// <summary>
        /// The data type.
        /// </summary>
        private enum DataType
        {
            /// <summary>
            /// The text.
            /// </summary>
            Text = 0,

            /// <summary>
            /// The numeric.
            /// </summary>
            Numeric
        }

        /// <summary>
        /// The separator.
        /// </summary>
        private enum Separator
        {
            /// <summary>
            /// The comma.
            /// </summary>
            Comma = 0,

            /// <summary>
            /// The semicolon.
            /// </summary>
            Semicolon,

            /// <summary>
            /// The colon.
            /// </summary>
            Colon
        }

        #endregion

        #region Methods

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
            if (this.comboBoxDataType.SelectedItem != null && this.comboBoxSeparator.SelectedItem != null)
            {
                if (this.multiselect == false)
                {
                    this.openFileDialog.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
                    this.openFileDialog.FilterIndex = 1;
                    this.openFileDialog.FileName = string.Empty;
                    DialogResult result = this.openFileDialog.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        this.filePath = this.openFileDialog.FileName;
                        this.textBoxFile.Clear();
                        this.textBoxFile.Text = this.filePath;
                        var separator = (Separator)this.comboBoxSeparator.SelectedIndex;
                        switch (separator)
                        {
                            case Separator.Comma:
                                this.sep = ',';
                                break;
                            case Separator.Colon:
                                this.sep = ':';
                                break;
                            case Separator.Semicolon:
                                this.sep = ';';
                                break;
                        }

                        var dataType = (DataType)this.comboBoxDataType.SelectedIndex;
                        switch (dataType)
                        {
                            case DataType.Numeric:
                                this.dtType = "Numeric";
                                break;
                            case DataType.Text:
                                this.dtType = "Text";
                                break;
                        }
                    }

                    if (result == DialogResult.Cancel)
                    {
                        this.textBoxFile.Text = "Canceled by user!";
                        this.labelError.ResetText();
                        this.labelError.Text = "No file loaded!";
                    }
                }
                else
                {
                    this.openFileDialog.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
                    this.openFileDialog.Multiselect = true;
                    this.openFileDialog.FilterIndex = 1;
                    this.openFileDialog.FileName = string.Empty;
                    DialogResult result = this.openFileDialog.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        this.filePaths = this.openFileDialog.FileNames;
                        this.textBoxFile.Clear();
                        this.textBoxFile.Text = "Multiple files selected";
                        var separator = (Separator)this.comboBoxSeparator.SelectedIndex;
                        switch (separator)
                        {
                            case Separator.Comma:
                                this.sep = ',';
                                break;
                            case Separator.Colon:
                                this.sep = ':';
                                break;
                            case Separator.Semicolon:
                                this.sep = ';';
                                break;
                        }

                        var dataType = (DataType)this.comboBoxDataType.SelectedIndex;
                        switch (dataType)
                        {
                            case DataType.Numeric:
                                this.dtType = "Numeric";
                                break;
                            case DataType.Text:
                                this.dtType = "Text";
                                break;
                        }
                    }

                    if (result == DialogResult.Cancel)
                    {
                        this.textBoxFile.Text = "Canceled by user!";
                        this.labelError.ResetText();
                        this.labelError.Text = "No file loaded!";
                    }
                }
            }

            this.ProcessFile();
        }

        /// <summary>
        /// The process file.
        /// </summary>
        private void ProcessFile()
        {
            if (this.multiselect == false)
            {
                if (this.dtType != null && this.filePath != null)
                {
                    if (this.dtType.Equals("Text"))
                    {
                        var owner = (MainForm)this.Owner;
                        owner.Mode = 0;
                        this.provider.TextData = this.provider.LoadTextData(this.filePath, this.sep);
                    }
                    else if (this.dtType.Equals("Numeric"))
                    {
                        var owner = (MainForm)this.Owner;
                        owner.Mode = 1;
                        this.provider.NumericData = this.provider.LoadNumericData(this.filePath, this.sep);
                    }

                    if (this.provider.TextData.Count > 0 || this.provider.NumericData.Count > 0)
                    {
                        this.pictureBoxLoadStatus.BackColor = Color.Green;
                        this.labelError.ResetText();
                        this.labelError.Text = "File processed successfully!";
                        Application.DoEvents();
                        this.btnOpen.Enabled = false;
                        Thread.Sleep(1000);
                        this.Close();
                    }
                    else
                    {
                        this.labelError.Text = "Invalid file selected!";
                    }
                }
                else
                {
                    this.labelError.Text = "File is not processed!";
                }
            }
            else
            {
                if (this.dtType != null && this.filePaths != null)
                {
                    if (this.dtType.Equals("Text"))
                    {
                        var owner = (BatchProcess)this.Owner;
                        owner.Mode = 0;
                        foreach (var path in this.filePaths)
                        {
                            this.provider.BatchTextData.Add(this.provider.LoadTextData(path, this.sep));
                        }
                    }
                    else if (this.dtType.Equals("Numeric"))
                    {
                        var owner = (BatchProcess)this.Owner;
                        owner.Mode = 1;
                        foreach (var path in this.filePaths)
                        {
                            this.provider.BatchNumericData.Add(this.provider.LoadNumericData(path, this.sep));
                        }
                    }

                    if (this.provider.BatchTextData.Count > 0 || this.provider.BatchNumericData.Count > 0)
                    {
                        this.pictureBoxLoadStatus.BackColor = Color.Green;
                        this.labelError.ResetText();
                        this.labelError.Text = "Files processed successfully!";
                        Application.DoEvents();
                        this.btnOpen.Enabled = false;
                        Thread.Sleep(1000);
                        this.Close();
                    }
                    else
                    {
                        this.labelError.Text = "Invalid file selected!";
                    }
                }
                else
                {
                    this.labelError.Text = "Files are not processed!";
                }
            }
        }

        /// <summary>
        /// The combo box data type_ selected index changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void ComboBoxDataTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxDataType.SelectedItem != null && this.comboBoxSeparator.SelectedItem != null)
            {
                this.btnOpen.Enabled = true;
            }
        }

        /// <summary>
        /// The combo box separator_ selected index changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void ComboBoxSeparatorSelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxDataType.SelectedItem != null && this.comboBoxSeparator.SelectedItem != null)
            {
                this.btnOpen.Enabled = true;
            }
        }

        #endregion
    }
}
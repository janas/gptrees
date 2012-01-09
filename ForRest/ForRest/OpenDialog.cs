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
        /// The _multiselect.
        /// </summary>
        private readonly bool _multiselect;

        /// <summary>
        /// The _provider.
        /// </summary>
        private readonly Provider.Provider _provider = new Provider.Provider();

        /// <summary>
        /// The _dt type.
        /// </summary>
        private string _dtType;

        /// <summary>
        /// The _file path.
        /// </summary>
        private string _filePath;

        /// <summary>
        /// The _file paths.
        /// </summary>
        private string[] _filePaths;

        /// <summary>
        /// The _sep.
        /// </summary>
        private char _sep;

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
            this._provider = provider;
            this._multiselect = false;
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
            this._provider = provider;
            this._multiselect = multiselect;
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
                if (this._multiselect == false)
                {
                    this.openFileDialog.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
                    this.openFileDialog.FilterIndex = 1;
                    this.openFileDialog.FileName = string.Empty;
                    DialogResult result = this.openFileDialog.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        this._filePath = this.openFileDialog.FileName;
                        this.textBoxFile.Clear();
                        this.textBoxFile.Text = this._filePath;
                        var separator = (Separator)this.comboBoxSeparator.SelectedIndex;
                        switch (separator)
                        {
                            case Separator.Comma:
                                this._sep = ',';
                                break;
                            case Separator.Colon:
                                this._sep = ':';
                                break;
                            case Separator.Semicolon:
                                this._sep = ';';
                                break;
                        }

                        var dataType = (DataType)this.comboBoxDataType.SelectedIndex;
                        switch (dataType)
                        {
                            case DataType.Numeric:
                                this._dtType = "Numeric";
                                break;
                            case DataType.Text:
                                this._dtType = "Text";
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
                        this._filePaths = this.openFileDialog.FileNames;
                        this.textBoxFile.Clear();
                        this.textBoxFile.Text = "Multiple files selected";
                        var separator = (Separator)this.comboBoxSeparator.SelectedIndex;
                        switch (separator)
                        {
                            case Separator.Comma:
                                this._sep = ',';
                                break;
                            case Separator.Colon:
                                this._sep = ':';
                                break;
                            case Separator.Semicolon:
                                this._sep = ';';
                                break;
                        }

                        var dataType = (DataType)this.comboBoxDataType.SelectedIndex;
                        switch (dataType)
                        {
                            case DataType.Numeric:
                                this._dtType = "Numeric";
                                break;
                            case DataType.Text:
                                this._dtType = "Text";
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
            if (this._multiselect == false)
            {
                if (this._dtType != null && this._filePath != null)
                {
                    if (this._dtType.Equals("Text"))
                    {
                        var owner = (MainForm)this.Owner;
                        owner.Mode = 0;
                        this._provider.TextData = this._provider.LoadTextData(this._filePath, this._sep);
                    }
                    else if (this._dtType.Equals("Numeric"))
                    {
                        var owner = (MainForm)this.Owner;
                        owner.Mode = 1;
                        this._provider.NumericData = this._provider.LoadNumericData(this._filePath, this._sep);
                    }

                    if (this._provider.TextData.Count > 0 || this._provider.NumericData.Count > 0)
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
                if (this._dtType != null && this._filePaths != null)
                {
                    if (this._dtType.Equals("Text"))
                    {
                        var owner = (BatchProcess)this.Owner;
                        owner.Mode = 0;
                        foreach (var filePath in this._filePaths)
                        {
                            this._provider.BatchTextData.Add(this._provider.LoadTextData(filePath, this._sep));
                        }
                    }
                    else if (this._dtType.Equals("Numeric"))
                    {
                        var owner = (BatchProcess)this.Owner;
                        owner.Mode = 1;
                        foreach (var filePath in this._filePaths)
                        {
                            this._provider.BatchNumericData.Add(this._provider.LoadNumericData(filePath, this._sep));
                        }
                    }

                    if (this._provider.BatchTextData.Count > 0 || this._provider.BatchNumericData.Count > 0)
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
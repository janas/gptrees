using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace ForRest
{
    public partial class OpenDialog : Form
    {
        private enum Separator
        {
            Comma = 0,
            Semicolon,
            Colon
        }

        private enum DataType
        {
            Text = 0,
            Numeric
        }

        private char _sep;
        private string _dtType;
        private string _filePath;
        private string[] _filePaths;

        private readonly bool _multiselect;
        private readonly Provider.Provider _provider = new Provider.Provider();

        public OpenDialog(Provider.Provider provider)
        {
            InitializeComponent();
            _provider = provider;
        }

        public OpenDialog(Provider.Provider provider, bool multiselect)
        {
            InitializeComponent();
            _provider = provider;
            _multiselect = multiselect;
        }

        private void BtnOpenClick(object sender, EventArgs e)
        {
            if (comboBoxDataType.SelectedItem != null && comboBoxSeparator.SelectedItem != null)
            {
                if (_multiselect == false)
                {
                    openFileDialog.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
                    openFileDialog.FilterIndex = 1;
                    openFileDialog.FileName = "";
                    DialogResult result = openFileDialog.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        _filePath = openFileDialog.FileName;
                        textBoxFile.Clear();
                        textBoxFile.Text = _filePath;
                        var separator = (Separator) comboBoxSeparator.SelectedIndex;
                        switch (separator)
                        {
                            case Separator.Comma:
                                _sep = ',';
                                break;
                            case Separator.Colon:
                                _sep = ':';
                                break;
                            case Separator.Semicolon:
                                _sep = ';';
                                break;
                        }
                        var dataType = (DataType) comboBoxDataType.SelectedIndex;
                        switch (dataType)
                        {
                            case DataType.Numeric:
                                _dtType = "Numeric";
                                break;
                            case DataType.Text:
                                _dtType = "Text";
                                break;
                        }
                    }
                    if (result == DialogResult.Cancel)
                    {
                        textBoxFile.Text = "Canceled by user!";
                        labelError.ResetText();
                        labelError.Text = "No file loaded!";
                    }
                }
                else
                {
                    openFileDialog.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
                    openFileDialog.Multiselect = true;
                    openFileDialog.FilterIndex = 1;
                    openFileDialog.FileName = "";
                    DialogResult result = openFileDialog.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        _filePaths = openFileDialog.FileNames;
                        textBoxFile.Clear();
                        textBoxFile.Text = "Multiple files selected";
                        var separator = (Separator) comboBoxSeparator.SelectedIndex;
                        switch (separator)
                        {
                            case Separator.Comma:
                                _sep = ',';
                                break;
                            case Separator.Colon:
                                _sep = ':';
                                break;
                            case Separator.Semicolon:
                                _sep = ';';
                                break;
                        }
                        var dataType = (DataType) comboBoxDataType.SelectedIndex;
                        switch (dataType)
                        {
                            case DataType.Numeric:
                                _dtType = "Numeric";
                                break;
                            case DataType.Text:
                                _dtType = "Text";
                                break;
                        }
                    }
                    if (result == DialogResult.Cancel)
                    {
                        textBoxFile.Text = "Canceled by user!";
                        labelError.ResetText();
                        labelError.Text = "No file loaded!";
                    }
                }
            }
        }

        private void BtnProcessClick(object sender, EventArgs e)
        {
            if (_multiselect == false)
            {
                if (_dtType != null && _filePath != null)
                {
                    if (_dtType.Equals("Text"))
                    {
                        var owner = (MainForm) Owner;
                        owner.Mode = 0;
                        _provider.TextData = _provider.LoadTextData(_filePath, _sep);
                    }
                    else if (_dtType.Equals("Numeric"))
                    {
                        var owner = (MainForm) Owner;
                        owner.Mode = 1;
                        _provider.NumericData = _provider.LoadNumericData(_filePath, _sep);
                    }
                    if (_provider.TextData.Count > 0 || _provider.NumericData.Count > 0)
                    {
                        pictureBoxLoadStatus.BackColor = Color.Green;
                        labelError.ResetText();
                        labelError.Text = "File processed successfully!";
                        Application.DoEvents();
                        Thread.Sleep(1000);
                        Close();
                    }
                }
                else
                {
                    labelError.Text = "File is not processed!";
                }
            }
            else
            {
                if (_dtType != null && _filePaths != null)
                {
                    if (_dtType.Equals("Text"))
                    {
                        var owner = (BatchProcess) Owner;
                        owner.Mode = 0;
                        foreach (var filePath in _filePaths)
                        {
                            _provider.BatchTextData.Add(_provider.LoadTextData(filePath, _sep));
                        }
                    }
                    else if (_dtType.Equals("Numeric"))
                    {
                        var owner = (BatchProcess) Owner;
                        owner.Mode = 1;
                        foreach (var filePath in _filePaths)
                        {
                            _provider.BatchNumericData.Add(_provider.LoadNumericData(filePath, _sep));
                        }
                    }
                    if (_provider.BatchTextData.Count > 0 || _provider.BatchNumericData.Count > 0)
                    {
                        pictureBoxLoadStatus.BackColor = Color.Green;
                        labelError.ResetText();
                        labelError.Text = "Files processed successfully!";
                        Application.DoEvents();
                        Thread.Sleep(1000);
                        Close();
                    }
                }
                else
                {
                    labelError.Text = "Files are not processed!";
                }
            }
        }
    }
}

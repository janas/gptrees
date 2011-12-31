using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ForRest.Provider.BLL;

namespace ForRest
{
    public partial class AddTree : Form
    {
        private readonly Provider.Provider _provider;
        private readonly bool _fromFile;
        private readonly bool _group;
        private readonly int _mode;
        private int _noOfTrees;
        private string _typeOfTrees;
        private string _groupTreeName;

        public AddTree(Provider.Provider provider, bool fromFile)
        {
            InitializeComponent();
            InitializeAdd();
            _provider = provider;
            _fromFile = fromFile;
            FillTrees();
            SetTitleLabel();
        }

        public AddTree(Provider.Provider provider, bool fromFile, int mode)
        {
            InitializeComponent();
            _provider = provider;
            _fromFile = fromFile;
            _mode = mode;
            FillTrees();
            SetTitleLabel();
        }

        public AddTree(Provider.Provider provider, bool fromFile, int mode, bool group)
        {
            InitializeComponent();
            _provider = provider;
            _fromFile = fromFile;
            _mode = mode;
            _group = @group;
            FillTrees();
            SetTitleLabel();
        }

        private void InitializeAdd()
        {
            Width = 315;
            labelDataType.Visible = true;
            comboBoxDataType.Visible = true;
            btnAdd.Location = new Point(86, 140);
        }

        private void SetTitleLabel()
        {
            labelMode.ResetText();
            if (!_fromFile && !_group)
            {
                labelMode.Text = "Adding tree manually";
            }
            if (_fromFile && !_group)
            {
                switch (_mode)
                {
                    case 0:
                        labelMode.Text = "Adding text tree from file";
                        break;
                    case 1:
                        labelMode.Text = "Adding numeric tree from file";
                        break;
                }
            }
            if (_fromFile && _group)
            {
                switch (_mode)
                {
                    case 0:
                        labelMode.Text = "Adding text trees in batch";
                        break;
                    case 1:
                        labelMode.Text = "Adding numeric trees in batch";
                        break;
                }
            }
        }

        private void FillTrees()
        {
            string applicationPath = Application.ExecutablePath;
            _provider.CreatePluginList(applicationPath);
            comboBoxAvailableTrees.DataSource = _provider.PluginList;
            comboBoxAvailableTrees.DisplayMember = "Name";
        }

        private void ShowToolTip()
        {
            toolTipHelper.ToolTipTitle = "Please enter tree degree";
            toolTipHelper.Show("Please key in tree degree first.", maskedTextBoxDegree, 3000);
        }

        private void PopulateComboBox()
        {
            if (_group == false)
            {
                var owner = (Create) Owner;
                owner.FillSelectedTreeComboBox();
            }
            else
            {
                var owner = (BatchProcess) Owner;
                owner.UpdateLogOnCreate(_noOfTrees, _typeOfTrees, _groupTreeName);
            }
        }

        private void BtnAddClick(object sender, EventArgs e)
        {
            var factory = (ITreeFactory) comboBoxAvailableTrees.SelectedItem;
            switch (factory.NeedDegree)
            {
                case true:
                    //from file, group
                    if (_fromFile && _group)
                    {
                        if (string.IsNullOrEmpty(maskedTextBoxDegree.Text) || int.Parse(maskedTextBoxDegree.Text) < 2)
                        {
                            toolTipHelper.ToolTipTitle = "Wrong degree is entered";
                            toolTipHelper.Show("Please key in correct tree degree first.", maskedTextBoxDegree, 3000);
                            return;
                        }
                    }
                    //from file, not group
                    if (_fromFile && !_group)
                    {
                        if (string.IsNullOrEmpty(maskedTextBoxDegree.Text) || int.Parse(maskedTextBoxDegree.Text) < 2)
                        {
                            toolTipHelper.ToolTipTitle = "Wrong degree is entered";
                            toolTipHelper.Show("Please key in correct tree degree first.", maskedTextBoxDegree, 3000);
                            return;
                        }
                    }
                        //not from file, not group
                    else if (!_fromFile && !_group)
                    {
                        if (comboBoxDataType.SelectedItem == null)
                        {
                            toolTipHelper.ToolTipTitle = "No data type is selected";
                            toolTipHelper.Show("Please select data type from the list first.", comboBoxDataType, 3000);
                            return;
                        }
                        if (string.IsNullOrEmpty(maskedTextBoxDegree.Text) || int.Parse(maskedTextBoxDegree.Text) < 2)
                        {
                            toolTipHelper.ToolTipTitle = "Wrong degree is entered";
                            toolTipHelper.Show("Please key in correct tree degree first.", maskedTextBoxDegree, 3000);
                            return;
                        }
                    }
                    break;
                case false:
                    //not from file, not group
                    if (!_fromFile && !_group)
                    {
                        if (comboBoxDataType.SelectedItem == null)
                        {
                            toolTipHelper.ToolTipTitle = "No data type is selected";
                            toolTipHelper.Show("Please select data type from the list first.", comboBoxDataType, 3000);
                            return;
                        }
                    }
                    break;
            }
            btnAdd.Enabled = false;
            backgroundWorkerAddTree.RunWorkerAsync(factory);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData != Keys.Return)
                return false;
            btnAdd.PerformClick();
            return true;
        }

        private void TextBoxNameTextChanged(object sender, EventArgs e)
        {
            btnAdd.Enabled = !string.IsNullOrEmpty(textBoxName.Text.Trim());
        }

        private void MaskedTextBoxDegreeMaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            toolTipHelper.ToolTipTitle = "Invalid input";
            toolTipHelper.Show("We're sorry, but only digits (0-9) are allowed.", maskedTextBoxDegree, 3000);
        }

        private void ComboBoxAvailableTreesSelectedIndexChanged(object sender, EventArgs e)
        {
            var factory = (ITreeFactory) comboBoxAvailableTrees.SelectedItem;
            switch (_fromFile)
            {
                case true:
                    if (factory.NeedDegree)
                    {
                        Width = 315;
                        labelTreeDegree.Visible = true;
                        maskedTextBoxDegree.Visible = true;
                        btnAdd.Location = new Point(86, 140);
                        ShowToolTip();
                    }
                    if (factory.NeedDegree == false)
                    {
                        Width = 175;
                        labelTreeDegree.Visible = false;
                        maskedTextBoxDegree.Visible = false;
                        btnAdd.Location = new Point(12, 140);
                    }
                    break;
                case false:
                    if (factory.NeedDegree)
                    {
                        Width = 315;
                        labelTreeDegree.Visible = true;
                        maskedTextBoxDegree.Visible = true;
                        btnAdd.Location = new Point(86, 140);
                        ShowToolTip();
                    }
                    if (factory.NeedDegree == false)
                    {
                        Width = 315;
                        labelTreeDegree.Visible = false;
                        maskedTextBoxDegree.Visible = false;
                        btnAdd.Location = new Point(86, 140);
                    }
                    break;
            }
        }

        private void BackgroundWorkerAddTreeDoWork(object sender, DoWorkEventArgs e)
        {
            int comboBoxDataTypeSelectedIndex = -1;
            if (comboBoxDataType.InvokeRequired)
            {
                comboBoxDataType.Invoke(new MethodInvoker
                                            (delegate
                                                 { comboBoxDataTypeSelectedIndex = comboBoxDataType.SelectedIndex; }));
            }
            else
            {
                comboBoxDataTypeSelectedIndex = comboBoxDataType.SelectedIndex;
            }
            string maskedTextBoxDegreeText = string.Empty;
            if (maskedTextBoxDegree.InvokeRequired)
            {
                maskedTextBoxDegree.Invoke(new MethodInvoker
                                               (delegate { maskedTextBoxDegreeText = maskedTextBoxDegree.Text; }));
            }
            else
            {
                maskedTextBoxDegreeText = maskedTextBoxDegree.Text;
            }
            string textBoxNameText = string.Empty;
            if (textBoxName.InvokeRequired)
            {
                textBoxName.Invoke(new MethodInvoker
                                       (delegate { textBoxNameText = textBoxName.Text; }));
            }
            else
            {
                textBoxNameText = textBoxName.Text;
            }
            object comboBoxDataTypeSelectedItem = null;
            if (comboBoxDataType.InvokeRequired)
            {
                comboBoxDataType.Invoke(new MethodInvoker
                                            (delegate { comboBoxDataTypeSelectedItem = comboBoxDataType.Text; }));
            }
            else
            {
                comboBoxDataTypeSelectedItem = comboBoxDataType.Text;
            }

            var factory = e.Argument as ITreeFactory;
            switch (factory.NeedDegree)
            {
                case true:
                    //from file, group
                    if (_fromFile && _group)
                    {
                        if (_mode == 0 && _provider.BatchTextData != null &&
                            !string.IsNullOrEmpty(maskedTextBoxDegreeText))
                        {
                            _noOfTrees = 0;
                            foreach (var batchTree in _provider.BatchTextData)
                            {
                                int count = batchTree.Count;
                                ITree<string> tree = factory.GetTree<string>(int.Parse(maskedTextBoxDegreeText));
                                for (int i = 0; i < batchTree.Count; i++)
                                {
                                    tree.Add(batchTree[i]);
                                    int progress = (i*100)/count;
                                    backgroundWorkerAddTree.ReportProgress(progress);
                                }
                                var treeObject = new TreeObject(textBoxNameText.Trim(), "text", tree);
                                _provider.BatchTreeObject.Add(treeObject);
                                _typeOfTrees = treeObject.Type;
                                _noOfTrees++;
                            }
                            _groupTreeName = textBoxNameText.Trim();
                            e.Result = true;
                            return;
                        }
                        if (_mode == 1 && _provider.BatchNumericData != null &&
                            !string.IsNullOrEmpty(maskedTextBoxDegreeText))
                        {
                            _noOfTrees = 0;
                            foreach (var batchTree in _provider.BatchNumericData)
                            {
                                int count = batchTree.Count;
                                ITree<double> tree = factory.GetTree<double>(int.Parse(maskedTextBoxDegreeText));
                                for (int i = 0; i < batchTree.Count; i++)
                                {
                                    tree.Add(batchTree[i]);
                                    int progress = (i*100)/count;
                                    backgroundWorkerAddTree.ReportProgress(progress);
                                }
                                var treeObject = new TreeObject(textBoxNameText.Trim(), "numeric", tree);
                                _provider.BatchTreeObject.Add(treeObject);
                                _typeOfTrees = treeObject.Type;
                                _noOfTrees++;
                            }
                            _groupTreeName = textBoxNameText.Trim();
                            e.Result = true;
                            return;
                        }
                        if (string.IsNullOrEmpty(maskedTextBoxDegreeText))
                        {
                            e.Result = false;
                            return;
                        }
                    }
                    //from file, not group
                    if (_fromFile && !_group)
                    {
                        if (_mode == 0 && _provider.TextData != null && !string.IsNullOrEmpty(maskedTextBoxDegreeText))
                        {
                            ITree<string> tree = factory.GetTree<string>(int.Parse(maskedTextBoxDegreeText));
                            int count = _provider.TextData.Count;
                            for (int i = 0; i < count; i++)
                            {
                                tree.Add(_provider.TextData[i]);
                                int progress = (i*100)/count;
                                backgroundWorkerAddTree.ReportProgress(progress);
                            }
                            var treeObject = new TreeObject(textBoxNameText.Trim(), "text", tree);
                            _provider.TreeObjects.Add(treeObject);
                            e.Result = true;
                            return;
                        }
                        if (_mode == 1 && _provider.NumericData != null &&
                            !string.IsNullOrEmpty(maskedTextBoxDegreeText))
                        {
                            ITree<double> tree = factory.GetTree<double>(int.Parse(maskedTextBoxDegreeText));
                            int count = _provider.NumericData.Count;
                            for (int i = 0; i < count; i++)
                            {
                                tree.Add(_provider.NumericData[i]);
                                int progress = (i*100)/count;
                                backgroundWorkerAddTree.ReportProgress(progress);
                            }
                            var treeObject = new TreeObject(textBoxNameText.Trim(), "numeric", tree);
                            _provider.TreeObjects.Add(treeObject);
                            e.Result = true;
                            return;
                        }
                        if (string.IsNullOrEmpty(maskedTextBoxDegreeText))
                        {
                            e.Result = false;
                            return;
                        }
                    }
                        //not from file, not group
                    else if (!_fromFile && !_group)
                    {
                        if (comboBoxDataTypeSelectedItem != null && !string.IsNullOrEmpty(textBoxNameText) &&
                            !string.IsNullOrEmpty(maskedTextBoxDegreeText))
                        {
                            int mode = comboBoxDataTypeSelectedIndex;
                            switch (mode)
                            {
                                case 0:
                                    ITree<string> textTree = factory.GetTree<string>(int.Parse(maskedTextBoxDegreeText));
                                    var treeObjectText = new TreeObject(textBoxNameText.Trim(), "text", textTree);
                                    _provider.TreeObjects.Add(treeObjectText);
                                    break;
                                case 1:
                                    ITree<double> numericTree =
                                        factory.GetTree<double>(int.Parse(maskedTextBoxDegreeText));
                                    var treeObjectNumeric = new TreeObject(textBoxNameText.Trim(), "numeric",
                                                                           numericTree);
                                    _provider.TreeObjects.Add(treeObjectNumeric);
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
                    //from file, group
                    if (_fromFile && _group)
                    {
                        if (_mode == 0 && _provider.BatchTextData != null)
                        {
                            _noOfTrees = 0;
                            foreach (var batchTree in _provider.BatchTextData)
                            {
                                int count = batchTree.Count;
                                ITree<string> tree = factory.GetTree<string>();
                                for (int i = 0; i < batchTree.Count; i++)
                                {
                                    tree.Add(batchTree[i]);
                                    int progress = (i*100)/count;
                                    backgroundWorkerAddTree.ReportProgress(progress);
                                }
                                var treeObject = new TreeObject(textBoxNameText.Trim(), "text", tree);
                                _provider.BatchTreeObject.Add(treeObject);
                                _typeOfTrees = treeObject.Type;
                                _noOfTrees++;
                            }
                            _groupTreeName = textBoxNameText.Trim();
                            e.Result = true;
                            return;
                        }
                        if (_mode == 1 && _provider.BatchNumericData != null)
                        {
                            _noOfTrees = 0;
                            foreach (var batchTree in _provider.BatchNumericData)
                            {
                                int count = batchTree.Count;
                                ITree<double> tree = factory.GetTree<double>();
                                for (int i = 0; i < batchTree.Count; i++)
                                {
                                    tree.Add(batchTree[i]);
                                    int progress = (i*100)/count;
                                    backgroundWorkerAddTree.ReportProgress(progress);
                                }
                                var treeObject = new TreeObject(textBoxNameText.Trim(), "numeric", tree);
                                _provider.BatchTreeObject.Add(treeObject);
                                _typeOfTrees = treeObject.Type;
                                _noOfTrees++;
                            }
                            _groupTreeName = textBoxNameText.Trim();
                            e.Result = true;
                            return;
                        }
                    }
                    //from file, not group
                    if (_fromFile && !_group)
                    {
                        if (_mode == 0 && _provider.TextData != null)
                        {
                            ITree<string> tree = factory.GetTree<string>();
                            int count = _provider.TextData.Count;
                            for (int i = 0; i < count; i++)
                            {
                                tree.Add(_provider.TextData[i]);
                                int progress = (i*100)/count;
                                backgroundWorkerAddTree.ReportProgress(progress);
                            }
                            var treeObject = new TreeObject(textBoxNameText.Trim(), "text", tree);
                            _provider.TreeObjects.Add(treeObject);
                            e.Result = true;
                            return;
                        }
                        if (_mode == 1 && _provider.NumericData != null)
                        {
                            ITree<double> tree = factory.GetTree<double>();
                            int count = _provider.NumericData.Count;
                            for (int i = 0; i < count; i++)
                            {
                                tree.Add(_provider.NumericData[i]);
                                int progress = (i*100)/count;
                                backgroundWorkerAddTree.ReportProgress(progress);
                            }
                            var treeObject = new TreeObject(textBoxNameText.Trim(), "numeric", tree);
                            _provider.TreeObjects.Add(treeObject);
                            e.Result = true;
                            return;
                        }
                    }
                        //not from file, not group
                    else if (!_fromFile && !_group)
                    {
                        if (comboBoxDataTypeSelectedItem != null && !string.IsNullOrEmpty(textBoxNameText))
                        {
                            int mode = comboBoxDataTypeSelectedIndex;
                            switch (mode)
                            {
                                case 0:
                                    ITree<string> textTree = factory.GetTree<string>();
                                    var treeObjectText = new TreeObject(textBoxNameText.Trim(), "text", textTree);
                                    _provider.TreeObjects.Add(treeObjectText);
                                    break;
                                case 1:
                                    ITree<double> numericTree = factory.GetTree<double>();
                                    var treeObjectNumeric = new TreeObject(textBoxNameText.Trim(), "numeric",
                                                                           numericTree);
                                    _provider.TreeObjects.Add(treeObjectNumeric);
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

        private void BackgroundWorkerAddTreeProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBarAddTree.Value = e.ProgressPercentage;
            var percent = (int) (((progressBarAddTree.Value - progressBarAddTree.Minimum)/
                                  (double) (progressBarAddTree.Maximum - progressBarAddTree.Minimum))*100);
            using (Graphics gr = progressBarAddTree.CreateGraphics())
            {
                gr.DrawString(percent.ToString() + "%",
                              SystemFonts.DefaultFont,
                              Brushes.Black,
                              new PointF(progressBarAddTree.Width/2 - (gr.MeasureString(percent.ToString() + "%",
                                                                                        SystemFonts.DefaultFont).Width/
                                                                       2.0F),
                                         progressBarAddTree.Height/2 - (gr.MeasureString(percent.ToString() + "%",
                                                                                         SystemFonts.DefaultFont).Height/
                                                                        2.0F)));
            }
        }

        private void BackgroundWorkerAddTreeRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var result = (bool) e.Result;
            if (result != true)
            {
                btnAdd.Enabled = true;
                return;
            }
            PopulateComboBox();
            Close();
        }
    }
}

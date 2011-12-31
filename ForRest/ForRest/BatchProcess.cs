using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using ForRest.Provider.BLL;

namespace ForRest
{
    public partial class BatchProcess : Form
    {
        private readonly Provider.Provider _provider;
        private readonly List<double> _numericItems = new List<double>();
        private bool IsAdd { get; set; }
        private bool _isHelpShown = true;

        public int Mode { get; set; }

        public BatchProcess(Provider.Provider provider)
        {
            InitializeComponent();
            PrepareDialog();
            InitializeEditBox();
            _provider = provider;
            IsAdd = false;
        }

        public void UpdateLogOnCreate(int noOfTrees, string treeType, string groupTreeName)
        {
            textBoxLog.AppendText("[" + DateTime.Now.TimeOfDay + "]" + "\t" + noOfTrees + " tree(s) created, type of " +
                                  treeType + ", group tree name " + groupTreeName + ".\n");
        }

        private void PrepareDialog()
        {
            btnCreateNumericTrees.Enabled = false;
            btnCreateTextTrees.Enabled = false;
            btnBatchSearch.Enabled = false;
            btnExport.Enabled = false;
            listBoxSearchItems.Enabled = false;
            listBoxSearchItems.Items.Add("Enter search data here");
        }

        private void PopulateComboBox()
        {
            switch (Mode)
            {
                case 0:
                    comboBoxDataType.Items.Add("Text");
                    break;
                case 1:
                    comboBoxDataType.Items.Add("Numeric");
                    break;
            }
        }

        private bool ConvertToDouble()
        {
            foreach (var item in listBoxSearchItems.Items)
            {

                double numericValue;
                if (double.TryParse(item.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out numericValue))
                    _numericItems.Add(numericValue);
            }
            return _numericItems.Count > 0;
        }

        private void EnableCreateButtons()
        {
            switch (Mode)
            {
                case 0:
                    if (btnCreateTextTrees.Enabled == false)
                        btnCreateTextTrees.Enabled = true;
                    break;
                case 1:
                    if (btnCreateNumericTrees.Enabled == false)
                        btnCreateNumericTrees.Enabled = true;
                    break;
            }
        }

        private void EnableExportButton()
        {
            if (btnExport.Enabled) return;
            if (_provider.BatchPerformanceSet.Count > 0)
            {
                btnExport.Enabled = true;
            }
        }

        private void ToggleBatchSearchButton()
        {
            btnBatchSearch.Enabled = !btnBatchSearch.Enabled;
        }

        private void InitializeEditBox()
        {
            editBox.Hide();
            listBoxSearchItems.Controls.AddRange(new Control[] {editBox});
            editBox.Text = "";
            editBox.BackColor = Color.Beige;
            editBox.ForeColor = Color.Black;
            editBox.BorderStyle = BorderStyle.FixedSingle;
            editBox.KeyPress += ListBoxSearchItemsEditOver;
            editBox.LostFocus += ListBoxSearchItemsFocusOver;
        }

        private void CreateEditBox(object sender)
        {
            listBoxSearchItems = (ListBox) sender;
            var itemSelected = listBoxSearchItems.SelectedIndex;
            Rectangle rectangle = listBoxSearchItems.GetItemRectangle(itemSelected);
            var itemText = (string) listBoxSearchItems.Items[itemSelected];
            editBox.Location = new Point(rectangle.X, rectangle.Y + 15);
            editBox.Size = new Size(rectangle.Width, rectangle.Height);
            editBox.Show();
            listBoxSearchItems.Controls.AddRange(new Control[] {editBox});
            editBox.Text = itemText;
            editBox.Focus();
        }

        private void CreateEditBoxAdd(object sender)
        {
            listBoxSearchItems = (ListBox) sender;
            var itemSelected = listBoxSearchItems.Items.Count;
            if (itemSelected == 0)
            {
                var rectangle = new Rectangle(0, 0, 145, 13);
                editBox.Location = new Point(rectangle.X, rectangle.Y);
                editBox.Size = new Size(rectangle.Width, rectangle.Height);
                editBox.Show();
                listBoxSearchItems.Controls.AddRange(new Control[] {editBox});
                editBox.Text = string.Empty;
                editBox.Focus();
            }
            else
            {
                itemSelected = itemSelected - 1;
                Rectangle rectangle = listBoxSearchItems.GetItemRectangle(itemSelected);
                editBox.Location = new Point(rectangle.X, rectangle.Y + 15);
                editBox.Size = new Size(rectangle.Width, rectangle.Height);
                editBox.Show();
                listBoxSearchItems.Controls.AddRange(new Control[] {editBox});
                editBox.Text = string.Empty;
                editBox.Focus();
            }
        }

        private void BtnOpenFilesClick(object sender, EventArgs e)
        {
            var openDialog = new OpenDialog(_provider, true) {Owner = this};
            openDialog.ShowDialog();
            textBoxLog.AppendText("[" + DateTime.Now.TimeOfDay + "]" + "\tFiles loaded.\n");
            EnableCreateButtons();
            PopulateComboBox();
        }

        private void BtnCreateTextTreesClick(object sender, EventArgs e)
        {
            if (_provider.BatchTextData.Count == 0) return;
            var addTree = new AddTree(_provider, true, 0, true) {Owner = this};
            addTree.ShowDialog();
        }

        private void BtnCreateNumericTreesClick(object sender, EventArgs e)
        {
            if (_provider.BatchNumericData.Count == 0) return;
            var addTree = new AddTree(_provider, true, 1, true) {Owner = this};
            addTree.ShowDialog();
        }

        private void BtnBatchSearchClick(object sender, EventArgs e)
        {
            bool result = ConvertToDouble();
            if (comboBoxDataType.SelectedItem.Equals("Numeric") && result == false)
            {
                textBoxLog.AppendText("[" + DateTime.Now.TimeOfDay +
                                      "]\tNone value has been processed. Please provide correct input for numeric trees!\n");
            }
            else if (comboBoxDataType.SelectedItem.Equals("Numeric") && result)
            {
                textBoxLog.AppendText("[" + DateTime.Now.TimeOfDay + "]\tSuccessfully converted " + _numericItems.Count +
                                      " items out of " +
                                      listBoxSearchItems.Items.Count + " items.\n");
                backgroundWorkerBatchProcess.RunWorkerAsync();
                EnableExportButton();
                ToggleBatchSearchButton();
            }
            else if (comboBoxDataType.SelectedItem.Equals("Text"))
            {
                backgroundWorkerBatchProcess.RunWorkerAsync();
                EnableExportButton();
                ToggleBatchSearchButton();
            }
        }

        private void ListBoxSearchItemsDoubleClick(object sender, EventArgs e)
        {
            var index = listBoxSearchItems.SelectedIndex;
            if (index != -1)
            {
                IsAdd = false;
                CreateEditBox(sender);
            }
            else
            {
                IsAdd = true;
                CreateEditBoxAdd(sender);
            }
        }

        private void ListBoxSearchItemsKeyPress(object sender, KeyPressEventArgs e)
        {
            switch (IsAdd)
            {
                case true:
                    if (e.KeyChar == 13)
                    {
                        CreateEditBoxAdd(sender);
                    }
                    break;
                case false:
                    if (e.KeyChar == 13)
                    {
                        CreateEditBox(sender);
                    }
                    if (e.KeyChar == 27)
                    {
                        editBox.Hide();
                        listBoxSearchItems.SelectedItem = null;
                    }
                    break;
            }
        }

        private void ListBoxSearchItemsKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F2)
            {
                CreateEditBox(sender);
            }
            if (e.KeyData == Keys.Delete)
            {
                listBoxSearchItems.Items.RemoveAt(listBoxSearchItems.SelectedIndex);
                listBoxSearchItems.SelectedItem = null;
            }
            if (e.KeyData == Keys.Escape)
            {
                editBox.Hide();
                listBoxSearchItems.SelectedItem = null;
            }
        }

        private void ListBoxSearchItemsFocusOver(object sender, EventArgs e)
        {
            switch (IsAdd)
            {
                case true:
                    if (listBoxSearchItems.Items.Contains(editBox.Text)) return;
                    listBoxSearchItems.Items.Add(editBox.Text);
                    editBox.Hide();
                    break;
                case false:
                    var itemSelected = listBoxSearchItems.SelectedIndex;
                    listBoxSearchItems.Items[itemSelected] = editBox.Text;
                    editBox.Hide();
                    listBoxSearchItems.SelectedItem = null;
                    break;
            }
        }

        private void ListBoxSearchItemsEditOver(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                switch (IsAdd)
                {
                    case true:
                        if (listBoxSearchItems.Items.Contains(editBox.Text)) return;
                        listBoxSearchItems.Items.Add(editBox.Text);
                        editBox.Hide();
                        break;
                    case false:
                        var itemSelected = listBoxSearchItems.SelectedIndex;
                        listBoxSearchItems.Items[itemSelected] = editBox.Text;
                        editBox.Hide();
                        listBoxSearchItems.SelectedItem = null;
                        break;
                }
            }
            if (e.KeyChar == 27)
            {
                editBox.Hide();
                listBoxSearchItems.SelectedItem = null;
            }
        }

        private void ComboBoxDataTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxSearchItems.Enabled == false)
            {
                listBoxSearchItems.Enabled = true;
            }
            if (_isHelpShown)
            {
                listBoxSearchItems.Items.Clear();
                _isHelpShown = false;
            }
            textBoxLog.AppendText("[" + DateTime.Now.TimeOfDay + "]" + "\t" + comboBoxDataType.SelectedItem +
                                  " data type selected.\n");
            btnBatchSearch.Enabled = true;
        }

        private void BtnExportClick(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "CSV Files (*.csv)|*.csv";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.FileName = "PerformanceResultSet";
            saveFileDialog.DefaultExt = "csv";
            DialogResult result = saveFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                _provider.WriteResults(_provider.BatchPerformanceSet, saveFileDialog.FileName);
            }
        }

        private void BackgroundWorkerBatchProcessDoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            textBoxLog.AppendText("[" + DateTime.Now.TimeOfDay + "]" + "\tThread ID " +
                                  Thread.CurrentThread.ManagedThreadId + " is started.\n");
            switch (comboBoxDataType.SelectedItem.ToString())
            {
                case "Text":
                    foreach (var item in listBoxSearchItems.Items)
                    {
                        foreach (var batchTextTree in _provider.BatchTreeObject)
                        {
                            if (!batchTextTree.Type.Equals("text")) continue;
                            var watch = new Stopwatch();
                            watch.Start();
                            List<int> result = batchTextTree.TextTree.Contains(item.ToString());
                            watch.Stop();
                            if (result != null)
                            {
                                var peroformanceSet = new PerformanceSet
                                                          {
                                                              TreeName = batchTextTree.Name,
                                                              SearchTime =
                                                                  watch.ElapsedMilliseconds.ToString(
                                                                      CultureInfo.InvariantCulture),
                                                              TypeOfNodes = "String",
                                                              TypeOfTree = batchTextTree.TextTree.GetType().ToString(),
                                                              NoOfNodes = "notImplemented"
                                                          };
                                _provider.BatchPerformanceSet.Add(peroformanceSet);
                                textBoxLog.AppendText("[" + DateTime.Now.TimeOfDay + "]" + "\tGroup tree name: " +
                                                      batchTextTree.Name + " Value: " + item +
                                                      " Found in " + watch.ElapsedMilliseconds + " ms for tree type: " +
                                                      batchTextTree.TextTree.GetType() + "\n");
                            }
                            else
                            {
                                var peroformanceSet = new PerformanceSet
                                                          {
                                                              TreeName = batchTextTree.Name,
                                                              SearchTime = watch.ElapsedMilliseconds + "/Not Found",
                                                              TypeOfNodes = "String",
                                                              TypeOfTree = batchTextTree.TextTree.GetType().ToString(),
                                                              NoOfNodes = "notImplemented"
                                                          };
                                _provider.BatchPerformanceSet.Add(peroformanceSet);
                                textBoxLog.AppendText("[" + DateTime.Now.TimeOfDay + "]" + "\tGroup tree name: " +
                                                      batchTextTree.Name + " Value: " + item +
                                                      " Not found in " + watch.ElapsedMilliseconds +
                                                      " ms for tree type: " + batchTextTree.TextTree.GetType() + "\n");
                            }
                        }
                    }
                    break;
                case "Numeric":
                    foreach (var item in _numericItems)
                    {
                        foreach (var batchNumericTree in _provider.BatchTreeObject)
                        {
                            if (!batchNumericTree.Type.Equals("numeric")) continue;
                            var watch = new Stopwatch();
                            watch.Start();
                            List<int> result = batchNumericTree.NumericTree.Contains(item);
                            watch.Stop();
                            if (result != null)
                            {
                                var performanceSet = new PerformanceSet
                                                         {
                                                             TreeName = batchNumericTree.Name,
                                                             SearchTime =
                                                                 watch.ElapsedMilliseconds.ToString(
                                                                     CultureInfo.InvariantCulture),
                                                             TypeOfNodes = "Double",
                                                             TypeOfTree =
                                                                 batchNumericTree.NumericTree.GetType().ToString(),
                                                             NoOfNodes = "notImplemented"
                                                         };
                                _provider.BatchPerformanceSet.Add(performanceSet);
                                textBoxLog.AppendText("[" + DateTime.Now.TimeOfDay + "]" + "\tGroup tree name: " +
                                                      batchNumericTree.Name + " Value: " + item +
                                                      " Found in " + watch.ElapsedMilliseconds + " ms for tree type: " +
                                                      batchNumericTree.NumericTree.GetType() + "\n");
                            }
                            else
                            {
                                var peroformanceSet = new PerformanceSet
                                                          {
                                                              TreeName = batchNumericTree.Name,
                                                              SearchTime = watch.ElapsedMilliseconds + "/Not Found",
                                                              TypeOfNodes = "Double",
                                                              TypeOfTree =
                                                                  batchNumericTree.NumericTree.GetType().ToString(),
                                                              NoOfNodes = "notImplemented"
                                                          };
                                _provider.BatchPerformanceSet.Add(peroformanceSet);
                                textBoxLog.AppendText("[" + DateTime.Now.TimeOfDay + "]" + "\tGroup tree name: " +
                                                      batchNumericTree.Name + " Value: " + item +
                                                      " Not found in " + watch.ElapsedMilliseconds +
                                                      " ms for tree type: " + batchNumericTree.NumericTree.GetType() +
                                                      "\n");
                            }
                        }
                    }
                    break;
            }
            e.Result = Thread.CurrentThread.ManagedThreadId;
        }

        private void BackgroundWorkerBatchProcessRunWorkerCompleted(object sender,
                                                                    System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            textBoxLog.AppendText("[" + DateTime.Now.TimeOfDay + "]" + "\tThread ID " + e.Result + " is done.\n");
            EnableExportButton();
            ToggleBatchSearchButton();
        }
    }
}

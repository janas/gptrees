using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;
using ForRest.Provider.BLL;

namespace ForRest
{
    public partial class Search : Form
    {
        private readonly Provider.Provider _provider;

        public int GraphMode { get; set; }
        
        public Search(Provider.Provider provider, int graphMode)
        {
            InitializeComponent();
            _provider = provider;
            GraphMode = graphMode;
            FillSelectedTreeComboBox();
        }

        public void FillSelectedTreeComboBox()
        {
            comboBoxSelectTree.Items.Clear();
            foreach (var treeObject in _provider.TreeObjects)
            {
                comboBoxSelectTree.Items.Add(treeObject);
                comboBoxSelectTree.DisplayMember = "Name";
            }
        }

        private void BtnSearchClick(object sender, System.EventArgs e)
        {
            if (comboBoxSelectTree.SelectedItem != null)
            {
                var treeObject = (TreeObject)comboBoxSelectTree.SelectedItem;
                if (treeObject.Type.Equals("text") && textBoxSearchFor.Text != null)
                {
                    var watch = new Stopwatch();
                    string textValue = textBoxSearchFor.Text;
                    watch.Start();
                    List<int> result = treeObject.TextTree.Contains(textValue);
                    watch.Stop();
                    if(result != null)
                    {
                        var peroformanceSet = new PerformanceSet();
                        labelTime.ResetText();
                        labelTime.Text = watch.ElapsedMilliseconds.ToString() + " ms";
                        peroformanceSet.TreeName = treeObject.Name;
                        peroformanceSet.SearchTime = watch.ElapsedMilliseconds.ToString();
                        peroformanceSet.TypeOfNodes = "String";
                        peroformanceSet.TypeOfTree = treeObject.TextTree.GetType().ToString();
                        peroformanceSet.NoOfNodes = "notImplemented";
                        _provider.PerformanceSets.Add(peroformanceSet);
                    }
                    else
                    {
                        var peroformanceSet = new PerformanceSet();
                        labelTime.ResetText();
                        labelTime.Text = "0 ms/error";
                        peroformanceSet.TreeName = treeObject.Name;
                        peroformanceSet.SearchTime = watch.ElapsedMilliseconds.ToString();
                        peroformanceSet.TypeOfNodes = "String";
                        peroformanceSet.TypeOfTree = treeObject.TextTree.GetType().ToString();
                        peroformanceSet.NoOfNodes = "notImplemented";
                        _provider.PerformanceSets.Add(peroformanceSet);
                    }
                }
                else if (treeObject.Type.Equals("numeric") && textBoxSearchFor.Text != null)
                {
                    var watch = new Stopwatch();
                    double numericValue = double.Parse(textBoxSearchFor.Text, NumberStyles.Any, CultureInfo.InvariantCulture);
                    watch.Start();
                    List<int> result = treeObject.NumericTree.Contains(numericValue);
                    watch.Stop();
                    if (result != null)
                    {
                        var peroformanceSet = new PerformanceSet();
                        labelTime.ResetText();
                        labelTime.Text = watch.ElapsedMilliseconds.ToString() + " ms";
                        peroformanceSet.TreeName = treeObject.Name;
                        peroformanceSet.SearchTime = watch.ElapsedMilliseconds.ToString();
                        peroformanceSet.TypeOfNodes = "Double";
                        peroformanceSet.TypeOfTree = treeObject.NumericTree.GetType().ToString();
                        peroformanceSet.NoOfNodes = "notImplemented";
                        _provider.PerformanceSets.Add(peroformanceSet);
                    }
                    else
                    {
                        var peroformanceSet = new PerformanceSet();
                        labelTime.ResetText();
                        labelTime.Text = "0 ms/error";
                        peroformanceSet.TreeName = treeObject.Name;
                        peroformanceSet.SearchTime = watch.ElapsedMilliseconds.ToString();
                        peroformanceSet.TypeOfNodes = "Double";
                        peroformanceSet.TypeOfTree = treeObject.NumericTree.GetType().ToString();
                        peroformanceSet.NoOfNodes = "notImplemented";
                        _provider.PerformanceSets.Add(peroformanceSet);
                    }
                }
            }
            else
                MessageBox.Show("No tree is selected. Please select tree from list first.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void BtnResetResultsSetClick(object sender, System.EventArgs e)
        {
            _provider.PerformanceSets.Clear();
        }
    }
}

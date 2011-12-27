using System.Drawing;
using System.Windows.Forms;

namespace ForRest
{
    public partial class ResultsSet : Form
    {
        private readonly Provider.Provider _provider;

        public ResultsSet(Provider.Provider provider)
        {
            InitializeComponent();
            _provider = provider;
            dataGridViewResultsSet.ColumnHeadersBorderStyle = ProperColumnHeadersBorderStyle;
        }

        private static DataGridViewHeaderBorderStyle ProperColumnHeadersBorderStyle
        {
            get
            {
                return (SystemFonts.MessageBoxFont.Name == "Segoe UI")
                           ? DataGridViewHeaderBorderStyle.None
                           : DataGridViewHeaderBorderStyle.Raised;
            }
        }

        private void ResultsSetLoad(object sender, System.EventArgs e)
        {
            dataGridViewResultsSet.DataSource = _provider.PerformanceSets;
            //dataGridViewResultsSet.DataSource = _provider.BatchPerformanceSet;
            dataGridViewResultsSet.Columns[0].HeaderText = "Tree name";
            dataGridViewResultsSet.Columns[1].HeaderText = "Type of the tree";
            dataGridViewResultsSet.Columns[2].HeaderText = "Number of nodes";
            dataGridViewResultsSet.Columns[3].HeaderText = "Nodes type";
            dataGridViewResultsSet.Columns[4].HeaderText = "Search time (in ms)";
        }
    }
}

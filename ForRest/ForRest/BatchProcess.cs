using System.Windows.Forms;

namespace ForRest
{
    public partial class BatchProcess : Form
    {
        public BatchProcess()
        {
            InitializeComponent();
        }

        private void BatchProcessFormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}

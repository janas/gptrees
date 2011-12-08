using System.Windows.Forms;

namespace ForRest
{
    public partial class Search : Form
    {
        public Search()
        {
            InitializeComponent();
        }

        private void SearchFormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void BtnSearchClick(object sender, System.EventArgs e)
        {
            
        }
    }
}

using System;
using System.Windows.Forms;

namespace ForRest
{
    public partial class BatchProcess : Form
    {
        private readonly Provider.Provider _provider;

        public int Mode;

        public BatchProcess(Provider.Provider provider)
        {
            InitializeComponent();
            PrepareDialog();
            _provider = provider;
        }

        public void UpdateLogOnCreate()
        {
            textBoxLog.AppendText("[" + DateTime.Now.TimeOfDay + "]" + "\tTree created.\n");
        }

        private void PrepareDialog()
        {
            btnCreateNumericTrees.Enabled = false;
            btnCreateTextTrees.Enabled = false;
            btnBatchSearch.Enabled = false;
        }

        private void BtnOpenFilesClick(object sender, EventArgs e)
        {
            var openDialog = new OpenDialog(_provider, true) { Owner = this };
            openDialog.ShowDialog();
            textBoxLog.AppendText("[" + DateTime.Now.TimeOfDay + "]" + "\tFiles loaded.\n");
            EnableCreateButtons(Mode);
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
            var addTree = new AddTree(_provider, true, 1, true) { Owner = this };
            addTree.ShowDialog();
        }

        private void BtnBatchSearchClick(object sender, EventArgs e)
        {

        }

        private void EnableCreateButtons(int mode)
        {
            switch (mode)
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
    }
}

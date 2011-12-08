using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ForRest
{
    public partial class Create : Form
    {
        public Create()
        {
            InitializeComponent();
            FillTrees();
        }

        private void FillTrees()
        {
            comboBoxSelectTree.Items.Add("asdasd");
        }

        private void BtnAddTreeClick(object sender, EventArgs e)
        {

        }

        private void BtnAddNodeClick(object sender, EventArgs e)
        {

        }

        private void BtnRemoveNodeClick(object sender, EventArgs e)
        {

        }

        private void CreateFormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}

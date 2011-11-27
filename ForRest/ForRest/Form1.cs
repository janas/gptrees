using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ForRest
{
    public partial class Form1 : Form
    {
        
        Provider.Provider provider = new Provider.Provider();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                List<string> lst = new List<string>();
                List<double> dbl = new List<double>();
                DateTime start = DateTime.Now;
                lst = provider.LoadTextData(openFileDialog1.FileName, ';');
                DateTime end = DateTime.Now;
                TimeSpan tt = end - start;
                for (int i = 0; i < lst.Count; i++)
                {
                    textBox1.AppendText(lst[i]);
                    textBox1.AppendText("\n");
                }
                //textBox1.AppendText(lst.Count + "\n");
                //textBox1.AppendText(tt.TotalMilliseconds.ToString());
            }
        }

        private void btnLoadPlugins_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            /*string applicationPatch = Application.ExecutablePath;
            List<string> temp = provider.CreateItemsList(applicationPatch);
            for(int i =0; i<temp.Count; i++)
            {
                listBox1.Items.Add(temp[i]);
            }*/
            listBox1.Items.Add(provider.test());
        }
    }
}

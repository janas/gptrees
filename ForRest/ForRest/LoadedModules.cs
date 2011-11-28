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
    public partial class LoadedModules : Form
    {
        private List<string> pluginsList;
        public LoadedModules()
        {
            InitializeComponent();
        }

        public void GetData(List<string> itemsList)
        {
            pluginsList = itemsList;
        }

        private void LoadPluginListBox()
        {
            listBoxPluginName.Items.Clear();
            for (int i = 0; i < pluginsList.Count; i++)
            {
                listBoxPluginName.Items.Add(pluginsList[i]);
            } 
        }

        private void LoadedModulesLoad(object sender, EventArgs e)
        {
            LoadPluginListBox();
        }
    }
}

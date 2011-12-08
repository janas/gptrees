using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ForRest
{
    public partial class LoadedModules : Form
    {
        private List<string[]> _pluginsList;
        public LoadedModules()
        {
            InitializeComponent();
        }

        public void GetData(List<string[]> itemsList)
        {
            _pluginsList = itemsList;
        }

        private void LoadPluginListBox()
        {
            listBoxPluginName.Items.Clear();
            for (int i = 0; i < _pluginsList.Count; i++)
            {
                listBoxPluginName.Items.Add(_pluginsList[i][0]);
            } 
        }

        private void LoadedModulesLoad(object sender, EventArgs e)
        {
                LoadPluginListBox();
        }

        private void ListBoxPluginNameSelectedIndexChanged(object sender, EventArgs e)
        {
                textBoxPluginDescription.Clear();
                textBoxPluginDescription.AppendText(_pluginsList[listBoxPluginName.SelectedIndex][1]);
                textBoxPluginDescription.AppendText("\r\n\r\nAdvanced Information\r\n");
                textBoxPluginDescription.AppendText(_pluginsList[listBoxPluginName.SelectedIndex][2]);
        }
    }
}

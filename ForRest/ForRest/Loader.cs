using System;
using System.IO;
using System.Timers;
using System.Windows.Forms;

namespace ForRest
{
    public partial class Loader : Form
    {
        public Loader()
        {
            InitializeComponent();
            PrepareLabels();
        }

        private void PrepareLabels()
        {
            labelError.Visible = false;
            labelFound1.Visible = false;
            labelNotFound1.Visible = false;
        }

        private void LoaderLoad(object sender, EventArgs e)
        {
            string providerDll = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "ForRest.Provider.dll");
            if (File.Exists(providerDll))
            {
                labelFound1.Visible = true;
            }
            if (!File.Exists(providerDll))
            {
                labelNotFound1.Visible = true;
                labelError.Visible = true;
            }
            CloseApplication();
        }

        private void CloseApplication()
        {
            var timer = new System.Timers.Timer(5000);
            timer.Elapsed += TimerElapsed;
            timer.Enabled = true;
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}

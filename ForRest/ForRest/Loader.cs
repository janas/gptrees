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
            labelFound2.Visible = false;
            labelFound3.Visible = false;
            labelFound4.Visible = false;
            labelNotFound1.Visible = false;
            labelNotFound2.Visible = false;
            labelNotFound3.Visible = false;
            labelNotFound4.Visible = false;
        }

        private void LoaderLoad(object sender, EventArgs e)
        {
            string providerDll = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "ForRest.Provider.dll");
            string gleeDll = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Microsoft.GLEE.dll");
            string gleeDrawingDll = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath),
                                                 "Microsoft.GLEE.Drawing.dll");
            string gleeDrawingGDIdll = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath),
                                                    "Microsoft.GLEE.GraphViewerGDI.dll");
            if (File.Exists(providerDll))
            {
                labelFound1.Visible = true;
            }
            if (!File.Exists(providerDll))
            {
                labelNotFound1.Visible = true;
                labelError.Visible = true;
            }
            if (File.Exists(gleeDll))
            {
                labelFound2.Visible = true;
            }
            if (!File.Exists(gleeDll))
            {
                labelNotFound2.Visible = true;
                labelError.Visible = true;
            }
            if (File.Exists(gleeDrawingDll))
            {
                labelFound3.Visible = true;
            }
            if (!File.Exists(gleeDrawingDll))
            {
                labelNotFound3.Visible = true;
                labelError.Visible = true;
            }
            if (File.Exists(gleeDrawingGDIdll))
            {
                labelFound4.Visible = true;
            }
            if (!File.Exists(gleeDrawingGDIdll))
            {
                labelNotFound4.Visible = true;
                labelError.Visible = true;
            }
            CloseApplication();
        }

        private void CloseApplication()
        {
            var timer = new System.Timers.Timer(2000);
            timer.Elapsed += TimerElapsed;
            timer.Enabled = true;
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}

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
        }

        private void LoaderLoad(object sender, EventArgs e)
        {
            string providerDll = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "ForRest.Provider.dll");
            string gleeDll = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Microsoft.GLEE.dll");
            string gleeDrawingDll = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath),
                                                 "Microsoft.GLEE.Drawing.dll");
            string gleeGdiDll = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath),
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
            }
            if (File.Exists(gleeDrawingDll))
            {
                labelFound3.Visible = true;
            }
            if (!File.Exists(gleeDrawingDll))
            {
                labelNotFound3.Visible = true;
            }
            if (File.Exists(gleeGdiDll))
            {
                labelFound4.Visible = true;
            }
            if (!File.Exists(gleeGdiDll))
            {
                labelNotFound4.Visible = true;
            }
            CloseApplication();
        }

        private void ShowLabel()
        {
            labelMeassage.ResetText();
            labelMeassage.Text = "One or more of Microsoft GLEE libraries were not found." + Environment.NewLine +
                                 "GLEE mode will not be accessible";
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

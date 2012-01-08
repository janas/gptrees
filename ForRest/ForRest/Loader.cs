using System;
using System.IO;
using System.Timers;
using System.Windows.Forms;

namespace ForRest
{
    public partial class Loader : Form
    {
        private bool _runApplication;

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
                labelMeassage.Visible = true;
                _runApplication = true;
            }
            if (!File.Exists(providerDll))
            {
                labelNotFound1.Visible = true;
                labelError.Visible = true;
                _runApplication = false;
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

        private void CloseApplication()
        {
            var timer = new System.Timers.Timer(5000);
            switch (_runApplication)
            {
                case false:
                    timer.Elapsed += TimerElapsedFalse;
                    break;
                case true:
                    timer.Elapsed += TimerElapsedTrue;
                    break;
            }
            timer.Enabled = true;
        }

        private void TimerElapsedFalse(object sender, ElapsedEventArgs e)
        {
            Invoke((MethodInvoker)(Close));
            DialogResult=DialogResult.No;
        }

        private void TimerElapsedTrue(object sender, ElapsedEventArgs e)
        {
            Invoke((MethodInvoker) (Close));
            DialogResult = DialogResult.Yes;
        }
    }
}

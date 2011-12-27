using System;
using System.IO;
using System.Windows.Forms;

namespace ForRest
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            LoadApplication();
        }

        private static void LoadApplication()
        {
            string providerDll = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "ForRest.Provider.dll");
            string gleeDll = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Microsoft.GLEE.dll");
            string gleeDrawingDll = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath),
                                                 "Microsoft.GLEE.Drawing.dll");
            string gleeDrawingGDIdll = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath),
                                                    "Microsoft.GLEE.GraphViewerGDI.dll");
            if (File.Exists(providerDll) && File.Exists(gleeDll) && File.Exists(gleeDrawingDll) &&
                File.Exists(gleeDrawingGDIdll))
            {
                Application.Run(new MainForm());
            }
            else
            {
                Application.Run(new Loader());
            }
        }
    }
}

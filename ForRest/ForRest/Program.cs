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
            if (File.Exists(providerDll))
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

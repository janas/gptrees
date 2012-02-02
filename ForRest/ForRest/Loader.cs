// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Loader.cs" company="Warsaw University of Technology">
//   
// </copyright>
// <summary>
//   The loader.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForRest
{
    using System;
    using System.IO;
    using System.Timers;
    using System.Windows.Forms;

    using Timer = System.Timers.Timer;

    /// <summary>
    /// The loader.
    /// </summary>
    public partial class Loader : Form
    {
        #region Constants and Fields

        /// <summary>
        /// The _run application.
        /// </summary>
        private bool runApplication;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Loader"/> class.
        /// </summary>
        public Loader()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The close application.
        /// </summary>
        private void CloseApplication()
        {
            var timer = new Timer(5000);
            switch (this.runApplication)
            {
                case false:
                    timer.Elapsed += this.TimerElapsedFalse;
                    break;
                case true:
                    timer.Elapsed += this.TimerElapsedTrue;
                    break;
            }

            timer.Enabled = true;
        }

        /// <summary>
        /// The loader load.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void LoaderLoad(object sender, EventArgs e)
        {
            string providerDll = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "ForRest.Provider.dll");
            string gleeDll = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Microsoft.GLEE.dll");
            string gleeDrawingDll = Path.Combine(
                Path.GetDirectoryName(Application.ExecutablePath), "Microsoft.GLEE.Drawing.dll");
            string gleeGdiDll = Path.Combine(
                Path.GetDirectoryName(Application.ExecutablePath), "Microsoft.GLEE.GraphViewerGDI.dll");
            if (File.Exists(providerDll))
            {
                this.labelFound1.Visible = true;
                this.labelMeassage.Visible = true;
                this.runApplication = true;
            }

            if (!File.Exists(providerDll))
            {
                this.labelNotFound1.Visible = true;
                this.labelError.Visible = true;
                this.runApplication = false;
            }

            if (File.Exists(gleeDll))
            {
                this.labelFound2.Visible = true;
            }

            if (!File.Exists(gleeDll))
            {
                this.labelNotFound2.Visible = true;
            }

            if (File.Exists(gleeDrawingDll))
            {
                this.labelFound3.Visible = true;
            }

            if (!File.Exists(gleeDrawingDll))
            {
                this.labelNotFound3.Visible = true;
            }

            if (File.Exists(gleeGdiDll))
            {
                this.labelFound4.Visible = true;
            }

            if (!File.Exists(gleeGdiDll))
            {
                this.labelNotFound4.Visible = true;
            }

            this.CloseApplication();
        }

        /// <summary>
        /// The timer elapsed false.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void TimerElapsedFalse(object sender, ElapsedEventArgs e)
        {
            this.Invoke((MethodInvoker)this.Close);
            this.DialogResult = DialogResult.No;
        }

        /// <summary>
        /// The timer elapsed true.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void TimerElapsedTrue(object sender, ElapsedEventArgs e)
        {
            this.Invoke((MethodInvoker)this.Close);
            this.DialogResult = DialogResult.Yes;
        }

        #endregion
    }
}
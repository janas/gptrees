using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ForRest
{
    partial class Loader
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelProviderDll = new System.Windows.Forms.Label();
            this.labelChecking = new System.Windows.Forms.Label();
            this.labelNotFound1 = new System.Windows.Forms.Label();
            this.labelError = new System.Windows.Forms.Label();
            this.labelFound1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelProviderDll
            // 
            this.labelProviderDll.AutoSize = true;
            this.labelProviderDll.Location = new System.Drawing.Point(64, 43);
            this.labelProviderDll.Name = "labelProviderDll";
            this.labelProviderDll.Size = new System.Drawing.Size(137, 13);
            this.labelProviderDll.TabIndex = 0;
            this.labelProviderDll.Text = "ForRest.Provider.dll exists...";
            // 
            // labelChecking
            // 
            this.labelChecking.AutoSize = true;
            this.labelChecking.Location = new System.Drawing.Point(27, 18);
            this.labelChecking.Name = "labelChecking";
            this.labelChecking.Size = new System.Drawing.Size(55, 13);
            this.labelChecking.TabIndex = 4;
            this.labelChecking.Text = "Checking:";
            // 
            // labelNotFound1
            // 
            this.labelNotFound1.AutoSize = true;
            this.labelNotFound1.ForeColor = System.Drawing.Color.Red;
            this.labelNotFound1.Location = new System.Drawing.Point(325, 43);
            this.labelNotFound1.Name = "labelNotFound1";
            this.labelNotFound1.Size = new System.Drawing.Size(54, 13);
            this.labelNotFound1.TabIndex = 9;
            this.labelNotFound1.Text = "Not found";
            // 
            // labelError
            // 
            this.labelError.AutoSize = true;
            this.labelError.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelError.Location = new System.Drawing.Point(76, 78);
            this.labelError.Name = "labelError";
            this.labelError.Size = new System.Drawing.Size(286, 13);
            this.labelError.TabIndex = 13;
            this.labelError.Text = "Provider was not found. Application will exit now.";
            // 
            // labelFound1
            // 
            this.labelFound1.AutoSize = true;
            this.labelFound1.ForeColor = System.Drawing.Color.Green;
            this.labelFound1.Location = new System.Drawing.Point(325, 43);
            this.labelFound1.Name = "labelFound1";
            this.labelFound1.Size = new System.Drawing.Size(37, 13);
            this.labelFound1.TabIndex = 15;
            this.labelFound1.Text = "Found";
            // 
            // Loader
            // 
            this.ClientSize = new System.Drawing.Size(440, 120);
            this.Controls.Add(this.labelFound1);
            this.Controls.Add(this.labelError);
            this.Controls.Add(this.labelNotFound1);
            this.Controls.Add(this.labelChecking);
            this.Controls.Add(this.labelProviderDll);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Loader";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Loader";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.LoaderLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label labelProviderDll;
        private Label labelChecking;
        private Label labelNotFound1;
        private Label labelError;
        private Label labelFound1;
    }
}
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
            this.labelGleeDll = new System.Windows.Forms.Label();
            this.labelGleeDrawingDll = new System.Windows.Forms.Label();
            this.labelGleeGraphViewerDll = new System.Windows.Forms.Label();
            this.labelChecking = new System.Windows.Forms.Label();
            this.labelFound3 = new System.Windows.Forms.Label();
            this.labelFound4 = new System.Windows.Forms.Label();
            this.labelNotFound1 = new System.Windows.Forms.Label();
            this.labelNotFound2 = new System.Windows.Forms.Label();
            this.labelNotFound3 = new System.Windows.Forms.Label();
            this.labelNotFound4 = new System.Windows.Forms.Label();
            this.labelError = new System.Windows.Forms.Label();
            this.labelFound2 = new System.Windows.Forms.Label();
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
            // labelGleeDll
            // 
            this.labelGleeDll.AutoSize = true;
            this.labelGleeDll.Location = new System.Drawing.Point(64, 70);
            this.labelGleeDll.Name = "labelGleeDll";
            this.labelGleeDll.Size = new System.Drawing.Size(132, 13);
            this.labelGleeDll.TabIndex = 1;
            this.labelGleeDll.Text = "Microsoft.GLEE.dll exists...";
            // 
            // labelGleeDrawingDll
            // 
            this.labelGleeDrawingDll.AutoSize = true;
            this.labelGleeDrawingDll.Location = new System.Drawing.Point(64, 97);
            this.labelGleeDrawingDll.Name = "labelGleeDrawingDll";
            this.labelGleeDrawingDll.Size = new System.Drawing.Size(174, 13);
            this.labelGleeDrawingDll.TabIndex = 2;
            this.labelGleeDrawingDll.Text = "Microsoft.GLEE.Drawing.dll exists...";
            // 
            // labelGleeGraphViewerDll
            // 
            this.labelGleeGraphViewerDll.AutoSize = true;
            this.labelGleeGraphViewerDll.Location = new System.Drawing.Point(64, 124);
            this.labelGleeGraphViewerDll.Name = "labelGleeGraphViewerDll";
            this.labelGleeGraphViewerDll.Size = new System.Drawing.Size(215, 13);
            this.labelGleeGraphViewerDll.TabIndex = 3;
            this.labelGleeGraphViewerDll.Text = "Microsoft.GLEE.GraphViewerGDI.dll exists...";
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
            // labelFound3
            // 
            this.labelFound3.AutoSize = true;
            this.labelFound3.ForeColor = System.Drawing.Color.Green;
            this.labelFound3.Location = new System.Drawing.Point(325, 97);
            this.labelFound3.Name = "labelFound3";
            this.labelFound3.Size = new System.Drawing.Size(37, 13);
            this.labelFound3.TabIndex = 7;
            this.labelFound3.Text = "Found";
            // 
            // labelFound4
            // 
            this.labelFound4.AutoSize = true;
            this.labelFound4.ForeColor = System.Drawing.Color.Green;
            this.labelFound4.Location = new System.Drawing.Point(325, 124);
            this.labelFound4.Name = "labelFound4";
            this.labelFound4.Size = new System.Drawing.Size(37, 13);
            this.labelFound4.TabIndex = 8;
            this.labelFound4.Text = "Found";
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
            // labelNotFound2
            // 
            this.labelNotFound2.AutoSize = true;
            this.labelNotFound2.ForeColor = System.Drawing.Color.Red;
            this.labelNotFound2.Location = new System.Drawing.Point(325, 70);
            this.labelNotFound2.Name = "labelNotFound2";
            this.labelNotFound2.Size = new System.Drawing.Size(54, 13);
            this.labelNotFound2.TabIndex = 10;
            this.labelNotFound2.Text = "Not found";
            // 
            // labelNotFound3
            // 
            this.labelNotFound3.AutoSize = true;
            this.labelNotFound3.ForeColor = System.Drawing.Color.Red;
            this.labelNotFound3.Location = new System.Drawing.Point(325, 97);
            this.labelNotFound3.Name = "labelNotFound3";
            this.labelNotFound3.Size = new System.Drawing.Size(54, 13);
            this.labelNotFound3.TabIndex = 11;
            this.labelNotFound3.Text = "Not found";
            // 
            // labelNotFound4
            // 
            this.labelNotFound4.AutoSize = true;
            this.labelNotFound4.ForeColor = System.Drawing.Color.Red;
            this.labelNotFound4.Location = new System.Drawing.Point(325, 124);
            this.labelNotFound4.Name = "labelNotFound4";
            this.labelNotFound4.Size = new System.Drawing.Size(54, 13);
            this.labelNotFound4.TabIndex = 12;
            this.labelNotFound4.Text = "Not found";
            // 
            // labelError
            // 
            this.labelError.AutoSize = true;
            this.labelError.Location = new System.Drawing.Point(99, 176);
            this.labelError.Name = "labelError";
            this.labelError.Size = new System.Drawing.Size(280, 13);
            this.labelError.TabIndex = 13;
            this.labelError.Text = "One or more files were not found. Application will exit now.";
            // 
            // labelFound2
            // 
            this.labelFound2.AutoSize = true;
            this.labelFound2.ForeColor = System.Drawing.Color.Green;
            this.labelFound2.Location = new System.Drawing.Point(325, 70);
            this.labelFound2.Name = "labelFound2";
            this.labelFound2.Size = new System.Drawing.Size(37, 13);
            this.labelFound2.TabIndex = 14;
            this.labelFound2.Text = "Found";
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
            this.ClientSize = new System.Drawing.Size(444, 207);
            this.Controls.Add(this.labelFound1);
            this.Controls.Add(this.labelFound2);
            this.Controls.Add(this.labelError);
            this.Controls.Add(this.labelNotFound4);
            this.Controls.Add(this.labelNotFound3);
            this.Controls.Add(this.labelNotFound2);
            this.Controls.Add(this.labelNotFound1);
            this.Controls.Add(this.labelFound4);
            this.Controls.Add(this.labelFound3);
            this.Controls.Add(this.labelChecking);
            this.Controls.Add(this.labelGleeGraphViewerDll);
            this.Controls.Add(this.labelGleeDrawingDll);
            this.Controls.Add(this.labelGleeDll);
            this.Controls.Add(this.labelProviderDll);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Loader";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Loader";
            this.Load += new System.EventHandler(this.LoaderLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label labelProviderDll;
        private Label labelGleeDll;
        private Label labelGleeDrawingDll;
        private Label labelGleeGraphViewerDll;
        private Label labelChecking;
        private Label labelFound3;
        private Label labelFound4;
        private Label labelNotFound1;
        private Label labelNotFound2;
        private Label labelNotFound3;
        private Label labelNotFound4;
        private Label labelError;
        private Label labelFound2;
        private Label labelFound1;
    }
}
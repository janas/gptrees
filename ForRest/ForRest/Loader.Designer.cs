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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelGleeDll = new System.Windows.Forms.Label();
            this.labelDrawingDll = new System.Windows.Forms.Label();
            this.labelGraphViewerDll = new System.Windows.Forms.Label();
            this.labelFound2 = new System.Windows.Forms.Label();
            this.labelFound3 = new System.Windows.Forms.Label();
            this.labelFound4 = new System.Windows.Forms.Label();
            this.labelNotFound2 = new System.Windows.Forms.Label();
            this.labelNotFound3 = new System.Windows.Forms.Label();
            this.labelNotFound4 = new System.Windows.Forms.Label();
            this.labelMeassage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            this.labelNotFound1.Location = new System.Drawing.Point(357, 43);
            this.labelNotFound1.Name = "labelNotFound1";
            this.labelNotFound1.Size = new System.Drawing.Size(54, 13);
            this.labelNotFound1.TabIndex = 9;
            this.labelNotFound1.Text = "Not found";
            this.labelNotFound1.Visible = false;
            // 
            // labelError
            // 
            this.labelError.AutoSize = true;
            this.labelError.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelError.Location = new System.Drawing.Point(94, 148);
            this.labelError.Name = "labelError";
            this.labelError.Size = new System.Drawing.Size(286, 13);
            this.labelError.TabIndex = 13;
            this.labelError.Text = "Provider was not found. Application will exit now.";
            this.labelError.Visible = false;
            // 
            // labelFound1
            // 
            this.labelFound1.AutoSize = true;
            this.labelFound1.ForeColor = System.Drawing.Color.Green;
            this.labelFound1.Location = new System.Drawing.Point(357, 43);
            this.labelFound1.Name = "labelFound1";
            this.labelFound1.Size = new System.Drawing.Size(37, 13);
            this.labelFound1.TabIndex = 15;
            this.labelFound1.Text = "Found";
            this.labelFound1.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ForRest.Properties.Resources.Tree_32;
            this.pictureBox1.Location = new System.Drawing.Point(25, 43);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(33, 36);
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // labelGleeDll
            // 
            this.labelGleeDll.AutoSize = true;
            this.labelGleeDll.Location = new System.Drawing.Point(64, 66);
            this.labelGleeDll.Name = "labelGleeDll";
            this.labelGleeDll.Size = new System.Drawing.Size(132, 13);
            this.labelGleeDll.TabIndex = 17;
            this.labelGleeDll.Text = "Microsoft.GLEE.dll exists...";
            // 
            // labelDrawingDll
            // 
            this.labelDrawingDll.AutoSize = true;
            this.labelDrawingDll.Location = new System.Drawing.Point(64, 89);
            this.labelDrawingDll.Name = "labelDrawingDll";
            this.labelDrawingDll.Size = new System.Drawing.Size(174, 13);
            this.labelDrawingDll.TabIndex = 18;
            this.labelDrawingDll.Text = "Microsoft.GLEE.Drawing.dll exists...";
            // 
            // labelGraphViewerDll
            // 
            this.labelGraphViewerDll.AutoSize = true;
            this.labelGraphViewerDll.Location = new System.Drawing.Point(64, 112);
            this.labelGraphViewerDll.Name = "labelGraphViewerDll";
            this.labelGraphViewerDll.Size = new System.Drawing.Size(215, 13);
            this.labelGraphViewerDll.TabIndex = 19;
            this.labelGraphViewerDll.Text = "Microsoft.GLEE.GraphViewerGDI.dll exists...";
            // 
            // labelFound2
            // 
            this.labelFound2.AutoSize = true;
            this.labelFound2.ForeColor = System.Drawing.Color.Green;
            this.labelFound2.Location = new System.Drawing.Point(357, 66);
            this.labelFound2.Name = "labelFound2";
            this.labelFound2.Size = new System.Drawing.Size(37, 13);
            this.labelFound2.TabIndex = 20;
            this.labelFound2.Text = "Found";
            this.labelFound2.Visible = false;
            // 
            // labelFound3
            // 
            this.labelFound3.AutoSize = true;
            this.labelFound3.ForeColor = System.Drawing.Color.Green;
            this.labelFound3.Location = new System.Drawing.Point(357, 89);
            this.labelFound3.Name = "labelFound3";
            this.labelFound3.Size = new System.Drawing.Size(37, 13);
            this.labelFound3.TabIndex = 21;
            this.labelFound3.Text = "Found";
            this.labelFound3.Visible = false;
            // 
            // labelFound4
            // 
            this.labelFound4.AutoSize = true;
            this.labelFound4.ForeColor = System.Drawing.Color.Green;
            this.labelFound4.Location = new System.Drawing.Point(357, 112);
            this.labelFound4.Name = "labelFound4";
            this.labelFound4.Size = new System.Drawing.Size(37, 13);
            this.labelFound4.TabIndex = 22;
            this.labelFound4.Text = "Found";
            this.labelFound4.Visible = false;
            // 
            // labelNotFound2
            // 
            this.labelNotFound2.AutoSize = true;
            this.labelNotFound2.ForeColor = System.Drawing.Color.Red;
            this.labelNotFound2.Location = new System.Drawing.Point(357, 66);
            this.labelNotFound2.Name = "labelNotFound2";
            this.labelNotFound2.Size = new System.Drawing.Size(54, 13);
            this.labelNotFound2.TabIndex = 23;
            this.labelNotFound2.Text = "Not found";
            this.labelNotFound2.Visible = false;
            // 
            // labelNotFound3
            // 
            this.labelNotFound3.AutoSize = true;
            this.labelNotFound3.ForeColor = System.Drawing.Color.Red;
            this.labelNotFound3.Location = new System.Drawing.Point(357, 89);
            this.labelNotFound3.Name = "labelNotFound3";
            this.labelNotFound3.Size = new System.Drawing.Size(54, 13);
            this.labelNotFound3.TabIndex = 24;
            this.labelNotFound3.Text = "Not found";
            this.labelNotFound3.Visible = false;
            // 
            // labelNotFound4
            // 
            this.labelNotFound4.AutoSize = true;
            this.labelNotFound4.ForeColor = System.Drawing.Color.Red;
            this.labelNotFound4.Location = new System.Drawing.Point(357, 112);
            this.labelNotFound4.Name = "labelNotFound4";
            this.labelNotFound4.Size = new System.Drawing.Size(54, 13);
            this.labelNotFound4.TabIndex = 25;
            this.labelNotFound4.Text = "Not found";
            this.labelNotFound4.Visible = false;
            // 
            // labelMeassage
            // 
            this.labelMeassage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelMeassage.Location = new System.Drawing.Point(12, 139);
            this.labelMeassage.Name = "labelMeassage";
            this.labelMeassage.Size = new System.Drawing.Size(436, 31);
            this.labelMeassage.TabIndex = 26;
            this.labelMeassage.Text = "labelMessage";
            this.labelMeassage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelMeassage.Visible = false;
            // 
            // Loader
            // 
            this.ClientSize = new System.Drawing.Size(460, 195);
            this.Controls.Add(this.labelMeassage);
            this.Controls.Add(this.labelNotFound4);
            this.Controls.Add(this.labelNotFound3);
            this.Controls.Add(this.labelNotFound2);
            this.Controls.Add(this.labelFound4);
            this.Controls.Add(this.labelFound3);
            this.Controls.Add(this.labelFound2);
            this.Controls.Add(this.labelGraphViewerDll);
            this.Controls.Add(this.labelDrawingDll);
            this.Controls.Add(this.labelGleeDll);
            this.Controls.Add(this.pictureBox1);
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label labelProviderDll;
        private Label labelChecking;
        private Label labelNotFound1;
        private Label labelError;
        private Label labelFound1;
        private PictureBox pictureBox1;
        private Label labelGleeDll;
        private Label labelDrawingDll;
        private Label labelGraphViewerDll;
        private Label labelFound2;
        private Label labelFound3;
        private Label labelFound4;
        private Label labelNotFound2;
        private Label labelNotFound3;
        private Label labelNotFound4;
        private Label labelMeassage;
    }
}
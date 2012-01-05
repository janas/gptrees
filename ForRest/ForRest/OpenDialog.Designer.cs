namespace ForRest
{
    partial class OpenDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OpenDialog));
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.comboBoxSeparator = new System.Windows.Forms.ComboBox();
            this.comboBoxDataType = new System.Windows.Forms.ComboBox();
            this.textBoxFile = new System.Windows.Forms.TextBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.labelSeparator = new System.Windows.Forms.Label();
            this.labelDataType = new System.Windows.Forms.Label();
            this.labelError = new System.Windows.Forms.Label();
            this.groupBoxLoadStatus = new System.Windows.Forms.GroupBox();
            this.pictureBoxLoadStatus = new System.Windows.Forms.PictureBox();
            this.groupBoxLoadStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoadStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.Title = "Open CSV File";
            // 
            // comboBoxSeparator
            // 
            this.comboBoxSeparator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSeparator.FormattingEnabled = true;
            this.comboBoxSeparator.Items.AddRange(new object[] {
            ", (comma)",
            "; (semicolon)",
            ": (colon)"});
            this.comboBoxSeparator.Location = new System.Drawing.Point(115, 12);
            this.comboBoxSeparator.Name = "comboBoxSeparator";
            this.comboBoxSeparator.Size = new System.Drawing.Size(100, 21);
            this.comboBoxSeparator.TabIndex = 0;
            this.comboBoxSeparator.SelectedIndexChanged += new System.EventHandler(this.comboBoxSeparator_SelectedIndexChanged);
            // 
            // comboBoxDataType
            // 
            this.comboBoxDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDataType.FormattingEnabled = true;
            this.comboBoxDataType.Items.AddRange(new object[] {
            "Text",
            "Numeric"});
            this.comboBoxDataType.Location = new System.Drawing.Point(115, 43);
            this.comboBoxDataType.Name = "comboBoxDataType";
            this.comboBoxDataType.Size = new System.Drawing.Size(100, 21);
            this.comboBoxDataType.TabIndex = 2;
            this.comboBoxDataType.SelectedIndexChanged += new System.EventHandler(this.comboBoxDataType_SelectedIndexChanged);
            // 
            // textBoxFile
            // 
            this.textBoxFile.Location = new System.Drawing.Point(14, 98);
            this.textBoxFile.Name = "textBoxFile";
            this.textBoxFile.Size = new System.Drawing.Size(295, 20);
            this.textBoxFile.TabIndex = 3;
            this.textBoxFile.WordWrap = false;
            // 
            // btnOpen
            // 
            this.btnOpen.Enabled = false;
            this.btnOpen.Location = new System.Drawing.Point(322, 96);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(92, 22);
            this.btnOpen.TabIndex = 4;
            this.btnOpen.Text = "Open...";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.BtnOpenClick);
            // 
            // labelSeparator
            // 
            this.labelSeparator.AutoSize = true;
            this.labelSeparator.Location = new System.Drawing.Point(15, 15);
            this.labelSeparator.Name = "labelSeparator";
            this.labelSeparator.Size = new System.Drawing.Size(89, 13);
            this.labelSeparator.TabIndex = 5;
            this.labelSeparator.Text = "Select Separator:";
            // 
            // labelDataType
            // 
            this.labelDataType.AutoSize = true;
            this.labelDataType.Location = new System.Drawing.Point(16, 46);
            this.labelDataType.Name = "labelDataType";
            this.labelDataType.Size = new System.Drawing.Size(93, 13);
            this.labelDataType.TabIndex = 6;
            this.labelDataType.Text = "Select Data Type:";
            // 
            // labelError
            // 
            this.labelError.Location = new System.Drawing.Point(16, 82);
            this.labelError.Name = "labelError";
            this.labelError.Size = new System.Drawing.Size(293, 13);
            this.labelError.TabIndex = 7;
            this.labelError.Text = "No file loaded!";
            this.labelError.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBoxLoadStatus
            // 
            this.groupBoxLoadStatus.Controls.Add(this.pictureBoxLoadStatus);
            this.groupBoxLoadStatus.Location = new System.Drawing.Point(322, 43);
            this.groupBoxLoadStatus.Name = "groupBoxLoadStatus";
            this.groupBoxLoadStatus.Size = new System.Drawing.Size(92, 44);
            this.groupBoxLoadStatus.TabIndex = 8;
            this.groupBoxLoadStatus.TabStop = false;
            this.groupBoxLoadStatus.Text = "File Processing";
            // 
            // pictureBoxLoadStatus
            // 
            this.pictureBoxLoadStatus.BackColor = System.Drawing.Color.Crimson;
            this.pictureBoxLoadStatus.Location = new System.Drawing.Point(7, 17);
            this.pictureBoxLoadStatus.Name = "pictureBoxLoadStatus";
            this.pictureBoxLoadStatus.Size = new System.Drawing.Size(79, 21);
            this.pictureBoxLoadStatus.TabIndex = 9;
            this.pictureBoxLoadStatus.TabStop = false;
            // 
            // OpenDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 132);
            this.Controls.Add(this.groupBoxLoadStatus);
            this.Controls.Add(this.labelError);
            this.Controls.Add(this.labelDataType);
            this.Controls.Add(this.labelSeparator);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.textBoxFile);
            this.Controls.Add(this.comboBoxDataType);
            this.Controls.Add(this.comboBoxSeparator);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OpenDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Open CSV File";
            this.groupBoxLoadStatus.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoadStatus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ComboBox comboBoxSeparator;
        private System.Windows.Forms.ComboBox comboBoxDataType;
        private System.Windows.Forms.TextBox textBoxFile;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Label labelSeparator;
        private System.Windows.Forms.Label labelDataType;
        private System.Windows.Forms.Label labelError;
        private System.Windows.Forms.GroupBox groupBoxLoadStatus;
        private System.Windows.Forms.PictureBox pictureBoxLoadStatus;
    }
}
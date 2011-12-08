namespace ForRest
{
    partial class LoadedModules
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoadedModules));
            this.listBoxPluginName = new System.Windows.Forms.ListBox();
            this.labelListOfModules = new System.Windows.Forms.Label();
            this.groupBoxPluginName = new System.Windows.Forms.GroupBox();
            this.groupBoxPluginDescription = new System.Windows.Forms.GroupBox();
            this.textBoxPluginDescription = new System.Windows.Forms.TextBox();
            this.groupBoxPluginName.SuspendLayout();
            this.groupBoxPluginDescription.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxPluginName
            // 
            this.listBoxPluginName.FormattingEnabled = true;
            this.listBoxPluginName.HorizontalScrollbar = true;
            this.listBoxPluginName.Location = new System.Drawing.Point(8, 19);
            this.listBoxPluginName.Name = "listBoxPluginName";
            this.listBoxPluginName.Size = new System.Drawing.Size(405, 82);
            this.listBoxPluginName.TabIndex = 0;
            this.listBoxPluginName.SelectedIndexChanged += new System.EventHandler(this.ListBoxPluginNameSelectedIndexChanged);
            // 
            // labelListOfModules
            // 
            this.labelListOfModules.AutoSize = true;
            this.labelListOfModules.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelListOfModules.Location = new System.Drawing.Point(9, 20);
            this.labelListOfModules.Name = "labelListOfModules";
            this.labelListOfModules.Size = new System.Drawing.Size(125, 13);
            this.labelListOfModules.TabIndex = 2;
            this.labelListOfModules.Text = "List of available modules:";
            // 
            // groupBoxPluginName
            // 
            this.groupBoxPluginName.Controls.Add(this.listBoxPluginName);
            this.groupBoxPluginName.Location = new System.Drawing.Point(12, 49);
            this.groupBoxPluginName.Name = "groupBoxPluginName";
            this.groupBoxPluginName.Size = new System.Drawing.Size(419, 110);
            this.groupBoxPluginName.TabIndex = 3;
            this.groupBoxPluginName.TabStop = false;
            this.groupBoxPluginName.Text = "Plugin Name";
            // 
            // groupBoxPluginDescription
            // 
            this.groupBoxPluginDescription.Controls.Add(this.textBoxPluginDescription);
            this.groupBoxPluginDescription.Location = new System.Drawing.Point(12, 177);
            this.groupBoxPluginDescription.Name = "groupBoxPluginDescription";
            this.groupBoxPluginDescription.Size = new System.Drawing.Size(419, 134);
            this.groupBoxPluginDescription.TabIndex = 4;
            this.groupBoxPluginDescription.TabStop = false;
            this.groupBoxPluginDescription.Text = "Plugin Description";
            // 
            // textBoxPluginDescription
            // 
            this.textBoxPluginDescription.Location = new System.Drawing.Point(8, 19);
            this.textBoxPluginDescription.Multiline = true;
            this.textBoxPluginDescription.Name = "textBoxPluginDescription";
            this.textBoxPluginDescription.ReadOnly = true;
            this.textBoxPluginDescription.Size = new System.Drawing.Size(405, 109);
            this.textBoxPluginDescription.TabIndex = 0;
            // 
            // LoadedModules
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 327);
            this.Controls.Add(this.groupBoxPluginDescription);
            this.Controls.Add(this.groupBoxPluginName);
            this.Controls.Add(this.labelListOfModules);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoadedModules";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Loaded Modules";
            this.Load += new System.EventHandler(this.LoadedModulesLoad);
            this.groupBoxPluginName.ResumeLayout(false);
            this.groupBoxPluginDescription.ResumeLayout(false);
            this.groupBoxPluginDescription.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxPluginName;
        private System.Windows.Forms.Label labelListOfModules;
        private System.Windows.Forms.GroupBox groupBoxPluginName;
        private System.Windows.Forms.GroupBox groupBoxPluginDescription;
        private System.Windows.Forms.TextBox textBoxPluginDescription;
    }
}
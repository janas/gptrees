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
            this.listBoxPluginDescription = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listBoxPluginName
            // 
            this.listBoxPluginName.FormattingEnabled = true;
            this.listBoxPluginName.Location = new System.Drawing.Point(36, 41);
            this.listBoxPluginName.Name = "listBoxPluginName";
            this.listBoxPluginName.Size = new System.Drawing.Size(150, 186);
            this.listBoxPluginName.TabIndex = 0;
            // 
            // listBoxPluginDescription
            // 
            this.listBoxPluginDescription.FormattingEnabled = true;
            this.listBoxPluginDescription.Location = new System.Drawing.Point(223, 41);
            this.listBoxPluginDescription.Name = "listBoxPluginDescription";
            this.listBoxPluginDescription.Size = new System.Drawing.Size(150, 186);
            this.listBoxPluginDescription.TabIndex = 1;
            // 
            // LoadedModules
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 262);
            this.Controls.Add(this.listBoxPluginDescription);
            this.Controls.Add(this.listBoxPluginName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoadedModules";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Loaded Modules";
            this.Load += new System.EventHandler(this.LoadedModulesLoad);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxPluginName;
        private System.Windows.Forms.ListBox listBoxPluginDescription;
    }
}
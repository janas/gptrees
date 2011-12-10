namespace ForRest
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.batchProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadedModulesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripBtnOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnExport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripBtnCreate = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnSearch = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnBatchProcess = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripBtnLoadedModules = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnAbout = new System.Windows.Forms.ToolStripButton();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.modeToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.testToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(824, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem,
            this.exportToFileToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openFileToolStripMenuItem.Text = "&Open File";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.BtnOpenClick);
            // 
            // exportToFileToolStripMenuItem
            // 
            this.exportToFileToolStripMenuItem.Name = "exportToFileToolStripMenuItem";
            this.exportToFileToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exportToFileToolStripMenuItem.Text = "&Export to File";
            this.exportToFileToolStripMenuItem.Click += new System.EventHandler(this.BtnExportClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
            // 
            // modeToolStripMenuItem
            // 
            this.modeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.batchProcessToolStripMenuItem});
            this.modeToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.modeToolStripMenuItem.Name = "modeToolStripMenuItem";
            this.modeToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.modeToolStripMenuItem.Text = "&Mode";
            // 
            // createToolStripMenuItem
            // 
            this.createToolStripMenuItem.CheckOnClick = true;
            this.createToolStripMenuItem.Name = "createToolStripMenuItem";
            this.createToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.createToolStripMenuItem.Text = "&Create";
            this.createToolStripMenuItem.Click += new System.EventHandler(this.BtnCreateClick);
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.CheckOnClick = true;
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.searchToolStripMenuItem.Text = "&Search";
            this.searchToolStripMenuItem.Click += new System.EventHandler(this.BtnSearchClick);
            // 
            // batchProcessToolStripMenuItem
            // 
            this.batchProcessToolStripMenuItem.CheckOnClick = true;
            this.batchProcessToolStripMenuItem.Name = "batchProcessToolStripMenuItem";
            this.batchProcessToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.batchProcessToolStripMenuItem.Text = "&Batch Process";
            this.batchProcessToolStripMenuItem.Click += new System.EventHandler(this.BtnBatchProcessClick);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadedModulesToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // loadedModulesToolStripMenuItem
            // 
            this.loadedModulesToolStripMenuItem.Name = "loadedModulesToolStripMenuItem";
            this.loadedModulesToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.loadedModulesToolStripMenuItem.Text = "&Loaded Modules";
            this.loadedModulesToolStripMenuItem.Click += new System.EventHandler(this.BtnLoadedModulesClick);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.BtnAboutClick);
            // 
            // toolStrip
            // 
            this.toolStrip.AutoSize = false;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripBtnOpen,
            this.toolStripBtnExport,
            this.toolStripSeparator2,
            this.toolStripBtnCreate,
            this.toolStripBtnSearch,
            this.toolStripBtnBatchProcess,
            this.toolStripSeparator3,
            this.toolStripBtnLoadedModules,
            this.toolStripBtnAbout});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(824, 54);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip";
            // 
            // toolStripBtnOpen
            // 
            this.toolStripBtnOpen.AutoSize = false;
            this.toolStripBtnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnOpen.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnOpen.Image")));
            this.toolStripBtnOpen.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripBtnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnOpen.Name = "toolStripBtnOpen";
            this.toolStripBtnOpen.Size = new System.Drawing.Size(51, 51);
            this.toolStripBtnOpen.Text = "Open";
            this.toolStripBtnOpen.ToolTipText = "Open File";
            this.toolStripBtnOpen.Click += new System.EventHandler(this.BtnOpenClick);
            // 
            // toolStripBtnExport
            // 
            this.toolStripBtnExport.AutoSize = false;
            this.toolStripBtnExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnExport.Image = global::ForRest.Properties.Resources.Actions_floppy_save_icon;
            this.toolStripBtnExport.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripBtnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnExport.Name = "toolStripBtnExport";
            this.toolStripBtnExport.Size = new System.Drawing.Size(51, 51);
            this.toolStripBtnExport.Text = "Export to File";
            this.toolStripBtnExport.ToolTipText = "Export to File";
            this.toolStripBtnExport.Click += new System.EventHandler(this.BtnExportClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 54);
            // 
            // toolStripBtnCreate
            // 
            this.toolStripBtnCreate.AutoSize = false;
            this.toolStripBtnCreate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnCreate.Image = global::ForRest.Properties.Resources.Actions_edit_icon;
            this.toolStripBtnCreate.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripBtnCreate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnCreate.Name = "toolStripBtnCreate";
            this.toolStripBtnCreate.Size = new System.Drawing.Size(51, 51);
            this.toolStripBtnCreate.Text = "Create";
            this.toolStripBtnCreate.Click += new System.EventHandler(this.BtnCreateClick);
            // 
            // toolStripBtnSearch
            // 
            this.toolStripBtnSearch.AutoSize = false;
            this.toolStripBtnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnSearch.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnSearch.Image")));
            this.toolStripBtnSearch.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripBtnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnSearch.Name = "toolStripBtnSearch";
            this.toolStripBtnSearch.Size = new System.Drawing.Size(51, 51);
            this.toolStripBtnSearch.Text = "Search";
            this.toolStripBtnSearch.Click += new System.EventHandler(this.BtnSearchClick);
            // 
            // toolStripBtnBatchProcess
            // 
            this.toolStripBtnBatchProcess.AutoSize = false;
            this.toolStripBtnBatchProcess.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnBatchProcess.Image = global::ForRest.Properties.Resources.Actions_edit_copy_icon;
            this.toolStripBtnBatchProcess.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripBtnBatchProcess.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnBatchProcess.Name = "toolStripBtnBatchProcess";
            this.toolStripBtnBatchProcess.Size = new System.Drawing.Size(51, 51);
            this.toolStripBtnBatchProcess.Text = "Batch Process";
            this.toolStripBtnBatchProcess.Click += new System.EventHandler(this.BtnBatchProcessClick);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 54);
            // 
            // toolStripBtnLoadedModules
            // 
            this.toolStripBtnLoadedModules.AutoSize = false;
            this.toolStripBtnLoadedModules.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnLoadedModules.Image = global::ForRest.Properties.Resources.Actions_gear_icon;
            this.toolStripBtnLoadedModules.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripBtnLoadedModules.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnLoadedModules.Name = "toolStripBtnLoadedModules";
            this.toolStripBtnLoadedModules.Size = new System.Drawing.Size(51, 51);
            this.toolStripBtnLoadedModules.Text = "Loaded Modules";
            this.toolStripBtnLoadedModules.Click += new System.EventHandler(this.BtnLoadedModulesClick);
            // 
            // toolStripBtnAbout
            // 
            this.toolStripBtnAbout.AutoSize = false;
            this.toolStripBtnAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnAbout.Image = global::ForRest.Properties.Resources.Actions_info_icon;
            this.toolStripBtnAbout.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripBtnAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnAbout.Name = "toolStripBtnAbout";
            this.toolStripBtnAbout.Size = new System.Drawing.Size(51, 51);
            this.toolStripBtnAbout.Text = "About";
            this.toolStripBtnAbout.Click += new System.EventHandler(this.BtnAboutClick);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Title = "Export to CSV File";
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.treeToolStripMenuItem});
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.testToolStripMenuItem.Text = "&Test";
            // 
            // treeToolStripMenuItem
            // 
            this.treeToolStripMenuItem.Name = "treeToolStripMenuItem";
            this.treeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.treeToolStripMenuItem.Text = "&Tree";
            this.treeToolStripMenuItem.Click += new System.EventHandler(this.BtnTestTreeClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 562);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ForRest";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem batchProcessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadedModulesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripBtnOpen;
        private System.Windows.Forms.ToolStripButton toolStripBtnExport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripBtnCreate;
        private System.Windows.Forms.ToolStripButton toolStripBtnSearch;
        private System.Windows.Forms.ToolStripButton toolStripBtnBatchProcess;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripBtnLoadedModules;
        private System.Windows.Forms.ToolStripButton toolStripBtnAbout;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem treeToolStripMenuItem;
    }
}
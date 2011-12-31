namespace ForRest
{
    partial class BatchProcess
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BatchProcess));
            this.groupBoxControlsBatchProcess = new System.Windows.Forms.GroupBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.listBoxSearchItems = new System.Windows.Forms.ListBox();
            this.groupBoxDataType = new System.Windows.Forms.GroupBox();
            this.comboBoxDataType = new System.Windows.Forms.ComboBox();
            this.btnCreateNumericTrees = new System.Windows.Forms.Button();
            this.btnCreateTextTrees = new System.Windows.Forms.Button();
            this.btnBatchSearch = new System.Windows.Forms.Button();
            this.btnOpenFiles = new System.Windows.Forms.Button();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.editBox = new System.Windows.Forms.TextBox();
            this.groupBoxLog = new System.Windows.Forms.GroupBox();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.backgroundWorkerBatchProcess = new System.ComponentModel.BackgroundWorker();
            this.groupBoxControlsBatchProcess.SuspendLayout();
            this.groupBoxDataType.SuspendLayout();
            this.groupBoxLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxControlsBatchProcess
            // 
            this.groupBoxControlsBatchProcess.Controls.Add(this.btnExport);
            this.groupBoxControlsBatchProcess.Controls.Add(this.listBoxSearchItems);
            this.groupBoxControlsBatchProcess.Controls.Add(this.groupBoxDataType);
            this.groupBoxControlsBatchProcess.Controls.Add(this.btnCreateNumericTrees);
            this.groupBoxControlsBatchProcess.Controls.Add(this.btnCreateTextTrees);
            this.groupBoxControlsBatchProcess.Controls.Add(this.btnBatchSearch);
            this.groupBoxControlsBatchProcess.Controls.Add(this.btnOpenFiles);
            this.groupBoxControlsBatchProcess.Location = new System.Drawing.Point(12, 12);
            this.groupBoxControlsBatchProcess.Name = "groupBoxControlsBatchProcess";
            this.groupBoxControlsBatchProcess.Size = new System.Drawing.Size(160, 438);
            this.groupBoxControlsBatchProcess.TabIndex = 0;
            this.groupBoxControlsBatchProcess.TabStop = false;
            this.groupBoxControlsBatchProcess.Text = "Controls";
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(6, 61);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(149, 36);
            this.btnExport.TabIndex = 8;
            this.btnExport.Text = "Export results to CSV file";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.BtnExportClick);
            // 
            // listBoxSearchItems
            // 
            this.listBoxSearchItems.FormattingEnabled = true;
            this.listBoxSearchItems.Location = new System.Drawing.Point(6, 251);
            this.listBoxSearchItems.Name = "listBoxSearchItems";
            this.listBoxSearchItems.Size = new System.Drawing.Size(149, 134);
            this.listBoxSearchItems.TabIndex = 7;
            this.listBoxSearchItems.DoubleClick += new System.EventHandler(this.ListBoxSearchItemsDoubleClick);
            this.listBoxSearchItems.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListBoxSearchItemsKeyDown);
            this.listBoxSearchItems.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ListBoxSearchItemsKeyPress);
            // 
            // groupBoxDataType
            // 
            this.groupBoxDataType.Controls.Add(this.comboBoxDataType);
            this.groupBoxDataType.Location = new System.Drawing.Point(6, 201);
            this.groupBoxDataType.Name = "groupBoxDataType";
            this.groupBoxDataType.Size = new System.Drawing.Size(149, 44);
            this.groupBoxDataType.TabIndex = 6;
            this.groupBoxDataType.TabStop = false;
            this.groupBoxDataType.Text = "Select Data Type";
            // 
            // comboBoxDataType
            // 
            this.comboBoxDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDataType.FormattingEnabled = true;
            this.comboBoxDataType.Location = new System.Drawing.Point(6, 17);
            this.comboBoxDataType.Name = "comboBoxDataType";
            this.comboBoxDataType.Size = new System.Drawing.Size(136, 21);
            this.comboBoxDataType.TabIndex = 1;
            this.comboBoxDataType.SelectedIndexChanged += new System.EventHandler(this.ComboBoxDataTypeSelectedIndexChanged);
            // 
            // btnCreateNumericTrees
            // 
            this.btnCreateNumericTrees.Location = new System.Drawing.Point(6, 145);
            this.btnCreateNumericTrees.Name = "btnCreateNumericTrees";
            this.btnCreateNumericTrees.Size = new System.Drawing.Size(149, 36);
            this.btnCreateNumericTrees.TabIndex = 4;
            this.btnCreateNumericTrees.Text = "Create Numeric Trees";
            this.btnCreateNumericTrees.UseVisualStyleBackColor = true;
            this.btnCreateNumericTrees.Click += new System.EventHandler(this.BtnCreateNumericTreesClick);
            // 
            // btnCreateTextTrees
            // 
            this.btnCreateTextTrees.Location = new System.Drawing.Point(6, 103);
            this.btnCreateTextTrees.Name = "btnCreateTextTrees";
            this.btnCreateTextTrees.Size = new System.Drawing.Size(149, 36);
            this.btnCreateTextTrees.TabIndex = 3;
            this.btnCreateTextTrees.Text = "Create Text Trees";
            this.btnCreateTextTrees.UseVisualStyleBackColor = true;
            this.btnCreateTextTrees.Click += new System.EventHandler(this.BtnCreateTextTreesClick);
            // 
            // btnBatchSearch
            // 
            this.btnBatchSearch.Location = new System.Drawing.Point(5, 396);
            this.btnBatchSearch.Name = "btnBatchSearch";
            this.btnBatchSearch.Size = new System.Drawing.Size(149, 36);
            this.btnBatchSearch.TabIndex = 2;
            this.btnBatchSearch.Text = "Batch Search";
            this.btnBatchSearch.UseVisualStyleBackColor = true;
            this.btnBatchSearch.Click += new System.EventHandler(this.BtnBatchSearchClick);
            // 
            // btnOpenFiles
            // 
            this.btnOpenFiles.Location = new System.Drawing.Point(6, 19);
            this.btnOpenFiles.Name = "btnOpenFiles";
            this.btnOpenFiles.Size = new System.Drawing.Size(149, 36);
            this.btnOpenFiles.TabIndex = 0;
            this.btnOpenFiles.Text = "Open CSV files";
            this.btnOpenFiles.UseVisualStyleBackColor = true;
            this.btnOpenFiles.Click += new System.EventHandler(this.BtnOpenFilesClick);
            // 
            // textBoxLog
            // 
            this.textBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLog.BackColor = System.Drawing.Color.White;
            this.textBoxLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxLog.Location = new System.Drawing.Point(6, 19);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.Size = new System.Drawing.Size(363, 413);
            this.textBoxLog.TabIndex = 1;
            // 
            // editBox
            // 
            this.editBox.Location = new System.Drawing.Point(0, 0);
            this.editBox.Name = "editBox";
            this.editBox.Size = new System.Drawing.Size(100, 20);
            this.editBox.TabIndex = 0;
            // 
            // groupBoxLog
            // 
            this.groupBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxLog.Controls.Add(this.textBoxLog);
            this.groupBoxLog.Location = new System.Drawing.Point(197, 12);
            this.groupBoxLog.Name = "groupBoxLog";
            this.groupBoxLog.Size = new System.Drawing.Size(375, 438);
            this.groupBoxLog.TabIndex = 2;
            this.groupBoxLog.TabStop = false;
            this.groupBoxLog.Text = "Actions Log";
            // 
            // backgroundWorkerBatchProcess
            // 
            this.backgroundWorkerBatchProcess.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorkerBatchProcessDoWork);
            this.backgroundWorkerBatchProcess.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorkerBatchProcessRunWorkerCompleted);
            // 
            // BatchProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 462);
            this.Controls.Add(this.groupBoxLog);
            this.Controls.Add(this.groupBoxControlsBatchProcess);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(600, 500);
            this.Name = "BatchProcess";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "Batch Process";
            this.groupBoxControlsBatchProcess.ResumeLayout(false);
            this.groupBoxDataType.ResumeLayout(false);
            this.groupBoxLog.ResumeLayout(false);
            this.groupBoxLog.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxControlsBatchProcess;
        private System.Windows.Forms.Button btnOpenFiles;
        private System.Windows.Forms.Button btnBatchSearch;
        private System.Windows.Forms.ComboBox comboBoxDataType;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.GroupBox groupBoxLog;
        private System.Windows.Forms.Button btnCreateNumericTrees;
        private System.Windows.Forms.Button btnCreateTextTrees;
        private System.Windows.Forms.GroupBox groupBoxDataType;
        private System.Windows.Forms.ListBox listBoxSearchItems;
        private System.Windows.Forms.TextBox editBox;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.ComponentModel.BackgroundWorker backgroundWorkerBatchProcess;
    }
}
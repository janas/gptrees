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
            this.btnBatchSearch = new System.Windows.Forms.Button();
            this.btnCreateTrees = new System.Windows.Forms.Button();
            this.btnOpenFiles = new System.Windows.Forms.Button();
            this.groupBoxControlsBatchProcess.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxControlsBatchProcess
            // 
            this.groupBoxControlsBatchProcess.Controls.Add(this.btnBatchSearch);
            this.groupBoxControlsBatchProcess.Controls.Add(this.btnCreateTrees);
            this.groupBoxControlsBatchProcess.Controls.Add(this.btnOpenFiles);
            this.groupBoxControlsBatchProcess.Location = new System.Drawing.Point(12, 12);
            this.groupBoxControlsBatchProcess.Name = "groupBoxControlsBatchProcess";
            this.groupBoxControlsBatchProcess.Size = new System.Drawing.Size(479, 184);
            this.groupBoxControlsBatchProcess.TabIndex = 0;
            this.groupBoxControlsBatchProcess.TabStop = false;
            this.groupBoxControlsBatchProcess.Text = "Controls";
            // 
            // btnBatchSearch
            // 
            this.btnBatchSearch.Location = new System.Drawing.Point(186, 91);
            this.btnBatchSearch.Name = "btnBatchSearch";
            this.btnBatchSearch.Size = new System.Drawing.Size(138, 36);
            this.btnBatchSearch.TabIndex = 2;
            this.btnBatchSearch.Text = "Batch Search";
            this.btnBatchSearch.UseVisualStyleBackColor = true;
            // 
            // btnCreateTrees
            // 
            this.btnCreateTrees.Location = new System.Drawing.Point(186, 32);
            this.btnCreateTrees.Name = "btnCreateTrees";
            this.btnCreateTrees.Size = new System.Drawing.Size(138, 36);
            this.btnCreateTrees.TabIndex = 1;
            this.btnCreateTrees.Text = "Create Trees";
            this.btnCreateTrees.UseVisualStyleBackColor = true;
            // 
            // btnOpenFiles
            // 
            this.btnOpenFiles.Location = new System.Drawing.Point(23, 32);
            this.btnOpenFiles.Name = "btnOpenFiles";
            this.btnOpenFiles.Size = new System.Drawing.Size(138, 36);
            this.btnOpenFiles.TabIndex = 0;
            this.btnOpenFiles.Text = "Open CSV files";
            this.btnOpenFiles.UseVisualStyleBackColor = true;
            // 
            // BatchProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 362);
            this.Controls.Add(this.groupBoxControlsBatchProcess);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BatchProcess";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "Batch Process";
            this.groupBoxControlsBatchProcess.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxControlsBatchProcess;
        private System.Windows.Forms.Button btnOpenFiles;
        private System.Windows.Forms.Button btnCreateTrees;
        private System.Windows.Forms.Button btnBatchSearch;
    }
}
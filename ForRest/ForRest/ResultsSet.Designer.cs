namespace ForRest
{
    partial class ResultsSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResultsSet));
            this.dataGridViewResultsSet = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResultsSet)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewResultsSet
            // 
            this.dataGridViewResultsSet.AllowUserToAddRows = false;
            this.dataGridViewResultsSet.AllowUserToDeleteRows = false;
            this.dataGridViewResultsSet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewResultsSet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewResultsSet.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewResultsSet.Name = "dataGridViewResultsSet";
            this.dataGridViewResultsSet.ReadOnly = true;
            this.dataGridViewResultsSet.Size = new System.Drawing.Size(544, 262);
            this.dataGridViewResultsSet.TabIndex = 0;
            // 
            // ResultsSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 262);
            this.Controls.Add(this.dataGridViewResultsSet);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "ResultsSet";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Results Set";
            this.Load += new System.EventHandler(this.ResultsSetLoad);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResultsSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewResultsSet;
    }
}
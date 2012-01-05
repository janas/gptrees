namespace ForRest
{
    partial class Search
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Search));
            this.comboBoxSelectTree = new System.Windows.Forms.ComboBox();
            this.groupBoxResults = new System.Windows.Forms.GroupBox();
            this.labelNodes = new System.Windows.Forms.Label();
            this.labelTime = new System.Windows.Forms.Label();
            this.labelNodesVisited = new System.Windows.Forms.Label();
            this.labelTimeElapsed = new System.Windows.Forms.Label();
            this.groupBoxSelectTree = new System.Windows.Forms.GroupBox();
            this.groupBoxSearchFor = new System.Windows.Forms.GroupBox();
            this.textBoxSearchFor = new System.Windows.Forms.TextBox();
            this.groupBoxControlsSearch = new System.Windows.Forms.GroupBox();
            this.btnShowResultsSet = new System.Windows.Forms.Button();
            this.btnResetResultsSet = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.toolTipHelperSearch = new System.Windows.Forms.ToolTip(this.components);
            this.backgroundWorkerSearch = new System.ComponentModel.BackgroundWorker();
            this.groupBoxResults.SuspendLayout();
            this.groupBoxSelectTree.SuspendLayout();
            this.groupBoxSearchFor.SuspendLayout();
            this.groupBoxControlsSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxSelectTree
            // 
            this.comboBoxSelectTree.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSelectTree.FormattingEnabled = true;
            this.comboBoxSelectTree.Location = new System.Drawing.Point(6, 19);
            this.comboBoxSelectTree.Name = "comboBoxSelectTree";
            this.comboBoxSelectTree.Size = new System.Drawing.Size(136, 21);
            this.comboBoxSelectTree.TabIndex = 0;
            this.comboBoxSelectTree.SelectedIndexChanged += new System.EventHandler(this.ComboBoxSelectTreeSelectedIndexChanged);
            // 
            // groupBoxResults
            // 
            this.groupBoxResults.Controls.Add(this.labelNodes);
            this.groupBoxResults.Controls.Add(this.labelTime);
            this.groupBoxResults.Controls.Add(this.labelNodesVisited);
            this.groupBoxResults.Controls.Add(this.labelTimeElapsed);
            this.groupBoxResults.Location = new System.Drawing.Point(5, 277);
            this.groupBoxResults.Name = "groupBoxResults";
            this.groupBoxResults.Size = new System.Drawing.Size(149, 77);
            this.groupBoxResults.TabIndex = 1;
            this.groupBoxResults.TabStop = false;
            this.groupBoxResults.Text = "Results";
            // 
            // labelNodes
            // 
            this.labelNodes.AutoSize = true;
            this.labelNodes.Location = new System.Drawing.Point(86, 54);
            this.labelNodes.Name = "labelNodes";
            this.labelNodes.Size = new System.Drawing.Size(36, 13);
            this.labelNodes.TabIndex = 2;
            this.labelNodes.Text = "nodes";
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Location = new System.Drawing.Point(86, 26);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(26, 13);
            this.labelTime.TabIndex = 2;
            this.labelTime.Text = "time";
            // 
            // labelNodesVisited
            // 
            this.labelNodesVisited.AutoSize = true;
            this.labelNodesVisited.Location = new System.Drawing.Point(5, 54);
            this.labelNodesVisited.Name = "labelNodesVisited";
            this.labelNodesVisited.Size = new System.Drawing.Size(75, 13);
            this.labelNodesVisited.TabIndex = 2;
            this.labelNodesVisited.Text = "Nodes Visited:";
            // 
            // labelTimeElapsed
            // 
            this.labelTimeElapsed.AutoSize = true;
            this.labelTimeElapsed.Location = new System.Drawing.Point(6, 26);
            this.labelTimeElapsed.Name = "labelTimeElapsed";
            this.labelTimeElapsed.Size = new System.Drawing.Size(74, 13);
            this.labelTimeElapsed.TabIndex = 2;
            this.labelTimeElapsed.Text = "Time Elapsed:";
            // 
            // groupBoxSelectTree
            // 
            this.groupBoxSelectTree.Controls.Add(this.comboBoxSelectTree);
            this.groupBoxSelectTree.Location = new System.Drawing.Point(6, 19);
            this.groupBoxSelectTree.Name = "groupBoxSelectTree";
            this.groupBoxSelectTree.Size = new System.Drawing.Size(149, 51);
            this.groupBoxSelectTree.TabIndex = 2;
            this.groupBoxSelectTree.TabStop = false;
            this.groupBoxSelectTree.Text = "Select Tree";
            // 
            // groupBoxSearchFor
            // 
            this.groupBoxSearchFor.Controls.Add(this.textBoxSearchFor);
            this.groupBoxSearchFor.Location = new System.Drawing.Point(6, 76);
            this.groupBoxSearchFor.Name = "groupBoxSearchFor";
            this.groupBoxSearchFor.Size = new System.Drawing.Size(149, 50);
            this.groupBoxSearchFor.TabIndex = 3;
            this.groupBoxSearchFor.TabStop = false;
            this.groupBoxSearchFor.Text = "Search For";
            // 
            // textBoxSearchFor
            // 
            this.textBoxSearchFor.Location = new System.Drawing.Point(6, 19);
            this.textBoxSearchFor.Name = "textBoxSearchFor";
            this.textBoxSearchFor.Size = new System.Drawing.Size(136, 20);
            this.textBoxSearchFor.TabIndex = 0;
            // 
            // groupBoxControlsSearch
            // 
            this.groupBoxControlsSearch.Controls.Add(this.btnShowResultsSet);
            this.groupBoxControlsSearch.Controls.Add(this.btnResetResultsSet);
            this.groupBoxControlsSearch.Controls.Add(this.btnSearch);
            this.groupBoxControlsSearch.Controls.Add(this.groupBoxSelectTree);
            this.groupBoxControlsSearch.Controls.Add(this.groupBoxResults);
            this.groupBoxControlsSearch.Controls.Add(this.groupBoxSearchFor);
            this.groupBoxControlsSearch.Location = new System.Drawing.Point(12, 12);
            this.groupBoxControlsSearch.Name = "groupBoxControlsSearch";
            this.groupBoxControlsSearch.Size = new System.Drawing.Size(160, 361);
            this.groupBoxControlsSearch.TabIndex = 4;
            this.groupBoxControlsSearch.TabStop = false;
            this.groupBoxControlsSearch.Text = "Controls";
            // 
            // btnShowResultsSet
            // 
            this.btnShowResultsSet.Location = new System.Drawing.Point(5, 183);
            this.btnShowResultsSet.Name = "btnShowResultsSet";
            this.btnShowResultsSet.Size = new System.Drawing.Size(149, 36);
            this.btnShowResultsSet.TabIndex = 5;
            this.btnShowResultsSet.Text = "Show Results Set";
            this.btnShowResultsSet.UseVisualStyleBackColor = true;
            this.btnShowResultsSet.Click += new System.EventHandler(this.BtnShowResultsSetClick);
            // 
            // btnResetResultsSet
            // 
            this.btnResetResultsSet.Location = new System.Drawing.Point(6, 225);
            this.btnResetResultsSet.Name = "btnResetResultsSet";
            this.btnResetResultsSet.Size = new System.Drawing.Size(149, 36);
            this.btnResetResultsSet.TabIndex = 5;
            this.btnResetResultsSet.Text = "Reset Results Set";
            this.btnResetResultsSet.UseVisualStyleBackColor = true;
            this.btnResetResultsSet.Click += new System.EventHandler(this.BtnResetResultsSetClick);
            // 
            // btnSearch
            // 
            this.btnSearch.Enabled = false;
            this.btnSearch.Location = new System.Drawing.Point(5, 141);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(149, 36);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearchClick);
            // 
            // backgroundWorkerSearch
            // 
            this.backgroundWorkerSearch.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorkerSearchDoWork);
            this.backgroundWorkerSearch.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorkerSearchRunWorkerCompleted);
            // 
            // Search
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 462);
            this.Controls.Add(this.groupBoxControlsSearch);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(600, 500);
            this.Name = "Search";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "Search";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SearchFormClosing);
            this.Resize += new System.EventHandler(this.SearchResize);
            this.groupBoxResults.ResumeLayout(false);
            this.groupBoxResults.PerformLayout();
            this.groupBoxSelectTree.ResumeLayout(false);
            this.groupBoxSearchFor.ResumeLayout(false);
            this.groupBoxSearchFor.PerformLayout();
            this.groupBoxControlsSearch.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxSelectTree;
        private System.Windows.Forms.GroupBox groupBoxResults;
        private System.Windows.Forms.Label labelNodesVisited;
        private System.Windows.Forms.Label labelTimeElapsed;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Label labelNodes;
        private System.Windows.Forms.GroupBox groupBoxSelectTree;
        private System.Windows.Forms.GroupBox groupBoxSearchFor;
        private System.Windows.Forms.TextBox textBoxSearchFor;
        private System.Windows.Forms.GroupBox groupBoxControlsSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnResetResultsSet;
        private System.Windows.Forms.Button btnShowResultsSet;
        private System.Windows.Forms.ToolTip toolTipHelperSearch;
        private System.ComponentModel.BackgroundWorker backgroundWorkerSearch;
    }
}
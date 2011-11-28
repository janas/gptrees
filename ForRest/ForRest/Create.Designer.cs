namespace ForRest
{
    partial class Create
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Create));
            this.btnAddTree = new System.Windows.Forms.Button();
            this.btnAddNode = new System.Windows.Forms.Button();
            this.btnRemoveNode = new System.Windows.Forms.Button();
            this.groupBoxSelectTree = new System.Windows.Forms.GroupBox();
            this.comboBoxSelectTree = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBoxSelectTree.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddTree
            // 
            this.btnAddTree.Location = new System.Drawing.Point(6, 75);
            this.btnAddTree.Name = "btnAddTree";
            this.btnAddTree.Size = new System.Drawing.Size(138, 36);
            this.btnAddTree.TabIndex = 0;
            this.btnAddTree.Text = "Add Tree";
            this.btnAddTree.UseVisualStyleBackColor = true;
            this.btnAddTree.Click += new System.EventHandler(this.BtnAddTreeClick);
            // 
            // btnAddNode
            // 
            this.btnAddNode.Location = new System.Drawing.Point(6, 126);
            this.btnAddNode.Name = "btnAddNode";
            this.btnAddNode.Size = new System.Drawing.Size(138, 36);
            this.btnAddNode.TabIndex = 1;
            this.btnAddNode.Text = "Add Node";
            this.btnAddNode.UseVisualStyleBackColor = true;
            this.btnAddNode.Click += new System.EventHandler(this.BtnAddNodeClick);
            // 
            // btnRemoveNode
            // 
            this.btnRemoveNode.Location = new System.Drawing.Point(6, 178);
            this.btnRemoveNode.Name = "btnRemoveNode";
            this.btnRemoveNode.Size = new System.Drawing.Size(138, 36);
            this.btnRemoveNode.TabIndex = 2;
            this.btnRemoveNode.Text = "Remove Node";
            this.btnRemoveNode.UseVisualStyleBackColor = true;
            this.btnRemoveNode.Click += new System.EventHandler(this.BtnRemoveNodeClick);
            // 
            // groupBoxSelectTree
            // 
            this.groupBoxSelectTree.Controls.Add(this.comboBoxSelectTree);
            this.groupBoxSelectTree.Location = new System.Drawing.Point(6, 19);
            this.groupBoxSelectTree.Name = "groupBoxSelectTree";
            this.groupBoxSelectTree.Size = new System.Drawing.Size(138, 50);
            this.groupBoxSelectTree.TabIndex = 3;
            this.groupBoxSelectTree.TabStop = false;
            this.groupBoxSelectTree.Text = "Select Tree";
            // 
            // comboBoxSelectTree
            // 
            this.comboBoxSelectTree.FormattingEnabled = true;
            this.comboBoxSelectTree.Location = new System.Drawing.Point(6, 19);
            this.comboBoxSelectTree.Name = "comboBoxSelectTree";
            this.comboBoxSelectTree.Size = new System.Drawing.Size(126, 21);
            this.comboBoxSelectTree.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBoxSelectTree);
            this.groupBox1.Controls.Add(this.btnRemoveNode);
            this.groupBox1.Controls.Add(this.btnAddTree);
            this.groupBox1.Controls.Add(this.btnAddNode);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(155, 230);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // Create
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 362);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Create";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "Create";
            this.groupBoxSelectTree.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAddTree;
        private System.Windows.Forms.Button btnAddNode;
        private System.Windows.Forms.Button btnRemoveNode;
        private System.Windows.Forms.GroupBox groupBoxSelectTree;
        private System.Windows.Forms.ComboBox comboBoxSelectTree;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
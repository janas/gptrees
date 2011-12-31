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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Create));
            this.btnAddTreeFromFile = new System.Windows.Forms.Button();
            this.btnAddNode = new System.Windows.Forms.Button();
            this.btnRemoveNode = new System.Windows.Forms.Button();
            this.groupBoxSelectTree = new System.Windows.Forms.GroupBox();
            this.comboBoxSelectTree = new System.Windows.Forms.ComboBox();
            this.groupBoxControlsCreate = new System.Windows.Forms.GroupBox();
            this.labelResult = new System.Windows.Forms.Label();
            this.labelTextBoxValue = new System.Windows.Forms.Label();
            this.btnRemoveTree = new System.Windows.Forms.Button();
            this.textBoxValue = new System.Windows.Forms.TextBox();
            this.btnAddTree = new System.Windows.Forms.Button();
            this.toolTipHelperCreate = new System.Windows.Forms.ToolTip(this.components);
            this.groupBoxSelectTree.SuspendLayout();
            this.groupBoxControlsCreate.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddTreeFromFile
            // 
            this.btnAddTreeFromFile.Location = new System.Drawing.Point(6, 128);
            this.btnAddTreeFromFile.Name = "btnAddTreeFromFile";
            this.btnAddTreeFromFile.Size = new System.Drawing.Size(149, 36);
            this.btnAddTreeFromFile.TabIndex = 0;
            this.btnAddTreeFromFile.Text = "Add Tree From File";
            this.btnAddTreeFromFile.UseVisualStyleBackColor = true;
            this.btnAddTreeFromFile.Click += new System.EventHandler(this.BtnAddTreeFromFileClick);
            // 
            // btnAddNode
            // 
            this.btnAddNode.Location = new System.Drawing.Point(6, 263);
            this.btnAddNode.Name = "btnAddNode";
            this.btnAddNode.Size = new System.Drawing.Size(149, 36);
            this.btnAddNode.TabIndex = 1;
            this.btnAddNode.Text = "Add Node";
            this.btnAddNode.UseVisualStyleBackColor = true;
            this.btnAddNode.Click += new System.EventHandler(this.BtnAddNodeClick);
            // 
            // btnRemoveNode
            // 
            this.btnRemoveNode.Location = new System.Drawing.Point(6, 305);
            this.btnRemoveNode.Name = "btnRemoveNode";
            this.btnRemoveNode.Size = new System.Drawing.Size(149, 36);
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
            this.groupBoxSelectTree.Size = new System.Drawing.Size(149, 49);
            this.groupBoxSelectTree.TabIndex = 3;
            this.groupBoxSelectTree.TabStop = false;
            this.groupBoxSelectTree.Text = "Select Tree";
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
            // groupBoxControlsCreate
            // 
            this.groupBoxControlsCreate.Controls.Add(this.labelResult);
            this.groupBoxControlsCreate.Controls.Add(this.labelTextBoxValue);
            this.groupBoxControlsCreate.Controls.Add(this.btnRemoveTree);
            this.groupBoxControlsCreate.Controls.Add(this.textBoxValue);
            this.groupBoxControlsCreate.Controls.Add(this.btnAddTree);
            this.groupBoxControlsCreate.Controls.Add(this.btnAddTreeFromFile);
            this.groupBoxControlsCreate.Controls.Add(this.groupBoxSelectTree);
            this.groupBoxControlsCreate.Controls.Add(this.btnRemoveNode);
            this.groupBoxControlsCreate.Controls.Add(this.btnAddNode);
            this.groupBoxControlsCreate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBoxControlsCreate.Location = new System.Drawing.Point(12, 12);
            this.groupBoxControlsCreate.Name = "groupBoxControlsCreate";
            this.groupBoxControlsCreate.Size = new System.Drawing.Size(160, 370);
            this.groupBoxControlsCreate.TabIndex = 4;
            this.groupBoxControlsCreate.TabStop = false;
            this.groupBoxControlsCreate.Text = "Controls";
            // 
            // labelResult
            // 
            this.labelResult.AutoSize = true;
            this.labelResult.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelResult.Location = new System.Drawing.Point(6, 344);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(0, 13);
            this.labelResult.TabIndex = 5;
            // 
            // labelTextBoxValue
            // 
            this.labelTextBoxValue.AutoSize = true;
            this.labelTextBoxValue.Location = new System.Drawing.Point(10, 221);
            this.labelTextBoxValue.Name = "labelTextBoxValue";
            this.labelTextBoxValue.Size = new System.Drawing.Size(140, 13);
            this.labelTextBoxValue.TabIndex = 5;
            this.labelTextBoxValue.Text = "Enter value here to process:";
            // 
            // btnRemoveTree
            // 
            this.btnRemoveTree.Location = new System.Drawing.Point(6, 170);
            this.btnRemoveTree.Name = "btnRemoveTree";
            this.btnRemoveTree.Size = new System.Drawing.Size(149, 36);
            this.btnRemoveTree.TabIndex = 5;
            this.btnRemoveTree.Text = "Remove Tree";
            this.btnRemoveTree.UseVisualStyleBackColor = true;
            this.btnRemoveTree.Click += new System.EventHandler(this.BtnRemoveTreeClick);
            // 
            // textBoxValue
            // 
            this.textBoxValue.Location = new System.Drawing.Point(6, 237);
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.Size = new System.Drawing.Size(149, 20);
            this.textBoxValue.TabIndex = 5;
            // 
            // btnAddTree
            // 
            this.btnAddTree.Location = new System.Drawing.Point(6, 86);
            this.btnAddTree.Name = "btnAddTree";
            this.btnAddTree.Size = new System.Drawing.Size(149, 36);
            this.btnAddTree.TabIndex = 5;
            this.btnAddTree.Text = "Add Tree";
            this.btnAddTree.UseVisualStyleBackColor = true;
            this.btnAddTree.Click += new System.EventHandler(this.BtnAddTreeClick);
            // 
            // Create
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 462);
            this.Controls.Add(this.groupBoxControlsCreate);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(600, 500);
            this.Name = "Create";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "Create";
            this.groupBoxSelectTree.ResumeLayout(false);
            this.groupBoxControlsCreate.ResumeLayout(false);
            this.groupBoxControlsCreate.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAddTreeFromFile;
        private System.Windows.Forms.Button btnAddNode;
        private System.Windows.Forms.Button btnRemoveNode;
        private System.Windows.Forms.GroupBox groupBoxSelectTree;
        private System.Windows.Forms.ComboBox comboBoxSelectTree;
        private System.Windows.Forms.GroupBox groupBoxControlsCreate;
        private System.Windows.Forms.Button btnAddTree;
        private System.Windows.Forms.TextBox textBoxValue;
        private System.Windows.Forms.Label labelTextBoxValue;
        private System.Windows.Forms.Label labelResult;
        private System.Windows.Forms.Button btnRemoveTree;
        private System.Windows.Forms.ToolTip toolTipHelperCreate;
    }
}
namespace conver
{
    partial class FrmReplaceFiles
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
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.richTextBox4 = new System.Windows.Forms.RichTextBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtResultFiles = new System.Windows.Forms.RichTextBox();
            this.btnReplace = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.groupBox9);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.groupBox6);
            this.splitContainer3.Size = new System.Drawing.Size(800, 450);
            this.splitContainer3.SplitterDistance = 111;
            this.splitContainer3.TabIndex = 39;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.btnReplace);
            this.groupBox9.Controls.Add(this.richTextBox4);
            this.groupBox9.Controls.Add(this.checkBox4);
            this.groupBox9.Controls.Add(this.checkBox2);
            this.groupBox9.Controls.Add(this.richTextBox3);
            this.groupBox9.Controls.Add(this.richTextBox1);
            this.groupBox9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox9.Location = new System.Drawing.Point(0, 0);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(800, 111);
            this.groupBox9.TabIndex = 37;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Replace Folder Path";
            // 
            // richTextBox4
            // 
            this.richTextBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox4.Location = new System.Drawing.Point(6, 113);
            this.richTextBox4.Multiline = false;
            this.richTextBox4.Name = "richTextBox4";
            this.richTextBox4.Size = new System.Drawing.Size(788, 0);
            this.richTextBox4.TabIndex = 35;
            this.richTextBox4.Text = "";
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Checked = true;
            this.checkBox4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox4.Location = new System.Drawing.Point(6, 88);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(178, 19);
            this.checkBox4.TabIndex = 0;
            this.checkBox4.Text = "Using The Rules Below";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(6, 63);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(194, 19);
            this.checkBox2.TabIndex = 0;
            this.checkBox2.Text = "Skip File Path/Name Like:";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // richTextBox3
            // 
            this.richTextBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox3.Location = new System.Drawing.Point(227, 61);
            this.richTextBox3.Multiline = false;
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.Size = new System.Drawing.Size(503, 32);
            this.richTextBox3.TabIndex = 34;
            this.richTextBox3.Text = "test.txt";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(6, 22);
            this.richTextBox1.Multiline = false;
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(788, 33);
            this.richTextBox1.TabIndex = 33;
            this.richTextBox1.Text = "";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtResultFiles);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Location = new System.Drawing.Point(0, 0);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(800, 335);
            this.groupBox6.TabIndex = 4;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Replace Files Results";
            // 
            // txtResultFiles
            // 
            this.txtResultFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResultFiles.Location = new System.Drawing.Point(3, 18);
            this.txtResultFiles.Margin = new System.Windows.Forms.Padding(4);
            this.txtResultFiles.Name = "txtResultFiles";
            this.txtResultFiles.ReadOnly = true;
            this.txtResultFiles.Size = new System.Drawing.Size(794, 314);
            this.txtResultFiles.TabIndex = 8;
            this.txtResultFiles.Text = "";
            // 
            // btnReplace
            // 
            this.btnReplace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReplace.Location = new System.Drawing.Point(736, 61);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(58, 32);
            this.btnReplace.TabIndex = 36;
            this.btnReplace.Text = "OK";
            this.btnReplace.UseVisualStyleBackColor = true;
            // 
            // FrmReplaceFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer3);
            this.Name = "FrmReplaceFiles";
            this.Text = "FrmReplaceFiles";
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.RichTextBox richTextBox4;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.RichTextBox richTextBox3;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RichTextBox txtResultFiles;
        private System.Windows.Forms.Button btnReplace;
    }
}
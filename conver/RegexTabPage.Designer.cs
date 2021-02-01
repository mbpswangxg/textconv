namespace conver
{
    partial class RegexTabPage
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.spcFrame = new System.Windows.Forms.SplitContainer();
            this.tabResult = new System.Windows.Forms.TabControl();
            this.tpMatch = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkMultiline = new System.Windows.Forms.CheckBox();
            this.chkIgnoreCase = new System.Windows.Forms.CheckBox();
            this.txtPattern = new System.Windows.Forms.RichTextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtInput = new System.Windows.Forms.RichTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.treeMatch = new System.Windows.Forms.TreeView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtRangeTo = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRangeFrom = new System.Windows.Forms.RichTextBox();
            this.chkRange = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtReplaceResult = new System.Windows.Forms.RichTextBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.txtReplacement = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.spcFrame)).BeginInit();
            this.spcFrame.Panel1.SuspendLayout();
            this.spcFrame.Panel2.SuspendLayout();
            this.spcFrame.SuspendLayout();
            this.tabResult.SuspendLayout();
            this.tpMatch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.SuspendLayout();
            // 
            // spcFrame
            // 
            this.spcFrame.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spcFrame.Location = new System.Drawing.Point(4, 5);
            this.spcFrame.Margin = new System.Windows.Forms.Padding(4);
            this.spcFrame.Name = "spcFrame";
            // 
            // spcFrame.Panel1
            // 
            this.spcFrame.Panel1.Controls.Add(this.tabResult);
            this.spcFrame.Panel1MinSize = 200;
            // 
            // spcFrame.Panel2
            // 
            this.spcFrame.Panel2.Controls.Add(this.tabControl1);
            this.spcFrame.Panel2MinSize = 200;
            this.spcFrame.Size = new System.Drawing.Size(1266, 794);
            this.spcFrame.SplitterDistance = 685;
            this.spcFrame.SplitterWidth = 5;
            this.spcFrame.TabIndex = 1;
            this.spcFrame.TabStop = false;
            // 
            // tabResult
            // 
            this.tabResult.Controls.Add(this.tpMatch);
            this.tabResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabResult.Location = new System.Drawing.Point(0, 0);
            this.tabResult.Margin = new System.Windows.Forms.Padding(4);
            this.tabResult.Name = "tabResult";
            this.tabResult.SelectedIndex = 0;
            this.tabResult.Size = new System.Drawing.Size(685, 794);
            this.tabResult.TabIndex = 3;
            this.tabResult.TabStop = false;
            // 
            // tpMatch
            // 
            this.tpMatch.Controls.Add(this.splitContainer1);
            this.tpMatch.Location = new System.Drawing.Point(4, 25);
            this.tpMatch.Margin = new System.Windows.Forms.Padding(4);
            this.tpMatch.Name = "tpMatch";
            this.tpMatch.Padding = new System.Windows.Forms.Padding(4);
            this.tpMatch.Size = new System.Drawing.Size(677, 765);
            this.tpMatch.TabIndex = 0;
            this.tpMatch.Text = "Input/Match";
            this.tpMatch.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(4, 4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1MinSize = 100;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2MinSize = 100;
            this.splitContainer1.Size = new System.Drawing.Size(669, 757);
            this.splitContainer1.SplitterDistance = 171;
            this.splitContainer1.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkMultiline);
            this.groupBox1.Controls.Add(this.chkIgnoreCase);
            this.groupBox1.Controls.Add(this.txtPattern);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(669, 171);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pattern";
            // 
            // chkMultiline
            // 
            this.chkMultiline.AutoSize = true;
            this.chkMultiline.Checked = true;
            this.chkMultiline.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMultiline.Location = new System.Drawing.Point(203, -1);
            this.chkMultiline.Name = "chkMultiline";
            this.chkMultiline.Size = new System.Drawing.Size(81, 19);
            this.chkMultiline.TabIndex = 4;
            this.chkMultiline.Text = "Multiline";
            this.chkMultiline.UseVisualStyleBackColor = true;
            // 
            // chkIgnoreCase
            // 
            this.chkIgnoreCase.AutoSize = true;
            this.chkIgnoreCase.Checked = true;
            this.chkIgnoreCase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIgnoreCase.Location = new System.Drawing.Point(96, -1);
            this.chkIgnoreCase.Name = "chkIgnoreCase";
            this.chkIgnoreCase.Size = new System.Drawing.Size(101, 19);
            this.chkIgnoreCase.TabIndex = 4;
            this.chkIgnoreCase.Text = "IgnoreCase";
            this.chkIgnoreCase.UseVisualStyleBackColor = true;
            // 
            // txtPattern
            // 
            this.txtPattern.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPattern.Location = new System.Drawing.Point(3, 18);
            this.txtPattern.Margin = new System.Windows.Forms.Padding(4);
            this.txtPattern.Name = "txtPattern";
            this.txtPattern.Size = new System.Drawing.Size(663, 150);
            this.txtPattern.TabIndex = 3;
            this.txtPattern.Text = "";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer2.Panel1MinSize = 200;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer2.Panel2MinSize = 200;
            this.splitContainer2.Size = new System.Drawing.Size(669, 582);
            this.splitContainer2.SplitterDistance = 327;
            this.splitContainer2.SplitterWidth = 10;
            this.splitContainer2.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtInput);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(327, 582);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Input";
            // 
            // txtInput
            // 
            this.txtInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInput.Location = new System.Drawing.Point(3, 18);
            this.txtInput.Margin = new System.Windows.Forms.Padding(4);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(321, 561);
            this.txtInput.TabIndex = 2;
            this.txtInput.Text = "";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.treeMatch);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(332, 582);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Match Groups";
            // 
            // treeMatch
            // 
            this.treeMatch.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.treeMatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeMatch.Location = new System.Drawing.Point(3, 18);
            this.treeMatch.Margin = new System.Windows.Forms.Padding(4);
            this.treeMatch.Name = "treeMatch";
            this.treeMatch.Size = new System.Drawing.Size(326, 561);
            this.treeMatch.TabIndex = 3;
            this.treeMatch.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(576, 794);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Controls.Add(this.groupBox10);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(568, 765);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "Replacement";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.tableLayoutPanel1);
            this.groupBox4.Controls.Add(this.chkRange);
            this.groupBox4.Location = new System.Drawing.Point(6, 86);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(556, 89);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.txtRangeTo, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtRangeFrom, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(7, 25);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(542, 58);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // txtRangeTo
            // 
            this.txtRangeTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRangeTo.Location = new System.Drawing.Point(285, 4);
            this.txtRangeTo.Margin = new System.Windows.Forms.Padding(4);
            this.txtRangeTo.Name = "txtRangeTo";
            this.txtRangeTo.Size = new System.Drawing.Size(253, 50);
            this.txtRangeTo.TabIndex = 9;
            this.txtRangeTo.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(264, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 58);
            this.label2.TabIndex = 8;
            this.label2.Text = "~";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtRangeFrom
            // 
            this.txtRangeFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRangeFrom.Location = new System.Drawing.Point(4, 4);
            this.txtRangeFrom.Margin = new System.Windows.Forms.Padding(4);
            this.txtRangeFrom.Name = "txtRangeFrom";
            this.txtRangeFrom.Size = new System.Drawing.Size(253, 50);
            this.txtRangeFrom.TabIndex = 1;
            this.txtRangeFrom.Text = "";
            // 
            // chkRange
            // 
            this.chkRange.AutoSize = true;
            this.chkRange.Checked = true;
            this.chkRange.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRange.Location = new System.Drawing.Point(7, 3);
            this.chkRange.Name = "chkRange";
            this.chkRange.Size = new System.Drawing.Size(431, 19);
            this.chkRange.TabIndex = 0;
            this.chkRange.Text = "Skip The Match In Then Range From Pattern And End Pattern ";
            this.chkRange.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.txtReplaceResult);
            this.groupBox5.Location = new System.Drawing.Point(6, 179);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(554, 570);
            this.groupBox5.TabIndex = 13;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Replace Result";
            // 
            // txtReplaceResult
            // 
            this.txtReplaceResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReplaceResult.Location = new System.Drawing.Point(3, 18);
            this.txtReplaceResult.Margin = new System.Windows.Forms.Padding(4);
            this.txtReplaceResult.Name = "txtReplaceResult";
            this.txtReplaceResult.ReadOnly = true;
            this.txtReplaceResult.Size = new System.Drawing.Size(548, 549);
            this.txtReplaceResult.TabIndex = 7;
            this.txtReplaceResult.Text = "";
            // 
            // groupBox10
            // 
            this.groupBox10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox10.Controls.Add(this.txtReplacement);
            this.groupBox10.Location = new System.Drawing.Point(6, 6);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(556, 77);
            this.groupBox10.TabIndex = 6;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Replacement String";
            // 
            // txtReplacement
            // 
            this.txtReplacement.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReplacement.Location = new System.Drawing.Point(7, 19);
            this.txtReplacement.Margin = new System.Windows.Forms.Padding(4);
            this.txtReplacement.Name = "txtReplacement";
            this.txtReplacement.Size = new System.Drawing.Size(542, 51);
            this.txtReplacement.TabIndex = 5;
            this.txtReplacement.Text = "";
            // 
            // RegexTabPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.spcFrame);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "RegexTabPage";
            this.Size = new System.Drawing.Size(1274, 803);
            this.spcFrame.Panel1.ResumeLayout(false);
            this.spcFrame.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcFrame)).EndInit();
            this.spcFrame.ResumeLayout(false);
            this.tabResult.ResumeLayout(false);
            this.tpMatch.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer spcFrame;
        private System.Windows.Forms.TabControl tabResult;
        private System.Windows.Forms.TabPage tpMatch;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox txtPattern;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox txtInput;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RichTextBox txtReplaceResult;
        private System.Windows.Forms.TreeView treeMatch;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox txtReplacement;
        private System.Windows.Forms.CheckBox chkIgnoreCase;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RichTextBox txtRangeTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox txtRangeFrom;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.CheckBox chkMultiline;
        private System.Windows.Forms.CheckBox chkRange;
    }
}

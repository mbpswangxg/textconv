namespace conver
{
    partial class Regexer
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtResultLog = new System.Windows.Forms.RichTextBox();
            this.txtFileFilter = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdoFilterDo = new System.Windows.Forms.RadioButton();
            this.rdoFilterSkip = new System.Windows.Forms.RadioButton();
            this.chkFileFilter = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtInputFile = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtReplaceResult = new System.Windows.Forms.RichTextBox();
            this.txtReplacement = new System.Windows.Forms.RichTextBox();
            this.txtPattern = new System.Windows.Forms.RichTextBox();
            this.txtInput = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoRangeDo = new System.Windows.Forms.RadioButton();
            this.rdoRangeSkip = new System.Windows.Forms.RadioButton();
            this.chkRange = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.txtRangeTo = new System.Windows.Forms.RichTextBox();
            this.txtRangeFrom = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtInputFolder = new System.Windows.Forms.RichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tcRight = new System.Windows.Forms.TabControl();
            this.tabMatches = new System.Windows.Forms.TabPage();
            this.treeMatch = new System.Windows.Forms.TreeView();
            this.label9 = new System.Windows.Forms.Label();
            this.txtReplacementFile = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.spcFrame)).BeginInit();
            this.spcFrame.Panel1.SuspendLayout();
            this.spcFrame.Panel2.SuspendLayout();
            this.spcFrame.SuspendLayout();
            this.tabResult.SuspendLayout();
            this.tpMatch.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tcRight.SuspendLayout();
            this.tabMatches.SuspendLayout();
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
            // 
            // spcFrame.Panel2
            // 
            this.spcFrame.Panel2.Controls.Add(this.tcRight);
            this.spcFrame.Size = new System.Drawing.Size(1039, 786);
            this.spcFrame.SplitterDistance = 702;
            this.spcFrame.SplitterWidth = 3;
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
            this.tabResult.Size = new System.Drawing.Size(702, 786);
            this.tabResult.TabIndex = 3;
            this.tabResult.TabStop = false;
            // 
            // tpMatch
            // 
            this.tpMatch.Controls.Add(this.tableLayoutPanel1);
            this.tpMatch.Location = new System.Drawing.Point(4, 25);
            this.tpMatch.Margin = new System.Windows.Forms.Padding(4);
            this.tpMatch.Name = "tpMatch";
            this.tpMatch.Padding = new System.Windows.Forms.Padding(4);
            this.tpMatch.Size = new System.Drawing.Size(694, 757);
            this.tpMatch.TabIndex = 0;
            this.tpMatch.Text = "Match/Replace Result";
            this.tpMatch.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.txtResultLog, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.txtFileFilter, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.txtInputFile, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.txtReplaceResult, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.txtReplacement, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtPattern, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtInput, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.txtInputFolder, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtReplacementFile, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 10;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(686, 749);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // txtResultLog
            // 
            this.txtResultLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResultLog.Enabled = false;
            this.txtResultLog.Location = new System.Drawing.Point(156, 590);
            this.txtResultLog.Margin = new System.Windows.Forms.Padding(4);
            this.txtResultLog.Name = "txtResultLog";
            this.txtResultLog.ReadOnly = true;
            this.txtResultLog.Size = new System.Drawing.Size(525, 154);
            this.txtResultLog.TabIndex = 10;
            this.txtResultLog.Text = "";
            // 
            // txtFileFilter
            // 
            this.txtFileFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFileFilter.Location = new System.Drawing.Point(156, 508);
            this.txtFileFilter.Margin = new System.Windows.Forms.Padding(4);
            this.txtFileFilter.Name = "txtFileFilter";
            this.txtFileFilter.Size = new System.Drawing.Size(525, 73);
            this.txtFileFilter.TabIndex = 9;
            this.txtFileFilter.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdoFilterDo);
            this.groupBox2.Controls.Add(this.rdoFilterSkip);
            this.groupBox2.Controls.Add(this.chkFileFilter);
            this.groupBox2.Location = new System.Drawing.Point(4, 507);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(112, 60);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "FileFilter";
            // 
            // rdoFilterDo
            // 
            this.rdoFilterDo.AutoSize = true;
            this.rdoFilterDo.Location = new System.Drawing.Point(64, 37);
            this.rdoFilterDo.Name = "rdoFilterDo";
            this.rdoFilterDo.Size = new System.Drawing.Size(43, 19);
            this.rdoFilterDo.TabIndex = 2;
            this.rdoFilterDo.TabStop = true;
            this.rdoFilterDo.Text = "do";
            this.rdoFilterDo.UseVisualStyleBackColor = true;
            // 
            // rdoFilterSkip
            // 
            this.rdoFilterSkip.AutoSize = true;
            this.rdoFilterSkip.Checked = true;
            this.rdoFilterSkip.Location = new System.Drawing.Point(6, 37);
            this.rdoFilterSkip.Name = "rdoFilterSkip";
            this.rdoFilterSkip.Size = new System.Drawing.Size(52, 19);
            this.rdoFilterSkip.TabIndex = 1;
            this.rdoFilterSkip.TabStop = true;
            this.rdoFilterSkip.Text = "skip";
            this.rdoFilterSkip.UseVisualStyleBackColor = true;
            // 
            // chkFileFilter
            // 
            this.chkFileFilter.AutoSize = true;
            this.chkFileFilter.Location = new System.Drawing.Point(6, 18);
            this.chkFileFilter.Name = "chkFileFilter";
            this.chkFileFilter.Size = new System.Drawing.Size(102, 19);
            this.chkFileFilter.TabIndex = 0;
            this.chkFileFilter.Text = "FilterCheck";
            this.chkFileFilter.UseVisualStyleBackColor = true;
            this.chkFileFilter.CheckedChanged += new System.EventHandler(this.chkFileFilter_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 473);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(134, 19);
            this.label6.TabIndex = 27;
            this.label6.Text = "InputFolderPath";
            // 
            // txtInputFile
            // 
            this.txtInputFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInputFile.Location = new System.Drawing.Point(156, 446);
            this.txtInputFile.Margin = new System.Windows.Forms.Padding(4);
            this.txtInputFile.Multiline = false;
            this.txtInputFile.Name = "txtInputFile";
            this.txtInputFile.Size = new System.Drawing.Size(525, 22);
            this.txtInputFile.TabIndex = 6;
            this.txtInputFile.Text = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 442);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 19);
            this.label5.TabIndex = 25;
            this.label5.Text = "InputFilePath";
            // 
            // txtReplaceResult
            // 
            this.txtReplaceResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReplaceResult.Location = new System.Drawing.Point(156, 364);
            this.txtReplaceResult.Margin = new System.Windows.Forms.Padding(4);
            this.txtReplaceResult.Name = "txtReplaceResult";
            this.txtReplaceResult.ReadOnly = true;
            this.txtReplaceResult.Size = new System.Drawing.Size(525, 73);
            this.txtReplaceResult.TabIndex = 5;
            this.txtReplaceResult.Text = "";
            // 
            // txtReplacement
            // 
            this.txtReplacement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReplacement.Location = new System.Drawing.Point(156, 169);
            this.txtReplacement.Margin = new System.Windows.Forms.Padding(4);
            this.txtReplacement.Name = "txtReplacement";
            this.txtReplacement.Size = new System.Drawing.Size(525, 73);
            this.txtReplacement.TabIndex = 2;
            this.txtReplacement.Text = "";
            // 
            // txtPattern
            // 
            this.txtPattern.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPattern.Location = new System.Drawing.Point(156, 87);
            this.txtPattern.Margin = new System.Windows.Forms.Padding(4);
            this.txtPattern.Name = "txtPattern";
            this.txtPattern.Size = new System.Drawing.Size(525, 73);
            this.txtPattern.TabIndex = 1;
            this.txtPattern.Text = "";
            // 
            // txtInput
            // 
            this.txtInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInput.Location = new System.Drawing.Point(156, 5);
            this.txtInput.Margin = new System.Windows.Forms.Padding(4);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(525, 73);
            this.txtInput.TabIndex = 0;
            this.txtInput.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "pattern";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 165);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "replacement";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoRangeDo);
            this.groupBox1.Controls.Add(this.rdoRangeSkip);
            this.groupBox1.Controls.Add(this.chkRange);
            this.groupBox1.Location = new System.Drawing.Point(4, 281);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(112, 62);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "RangePattern";
            // 
            // rdoRangeDo
            // 
            this.rdoRangeDo.AutoSize = true;
            this.rdoRangeDo.Location = new System.Drawing.Point(64, 37);
            this.rdoRangeDo.Name = "rdoRangeDo";
            this.rdoRangeDo.Size = new System.Drawing.Size(43, 19);
            this.rdoRangeDo.TabIndex = 2;
            this.rdoRangeDo.TabStop = true;
            this.rdoRangeDo.Text = "do";
            this.rdoRangeDo.UseVisualStyleBackColor = true;
            // 
            // rdoRangeSkip
            // 
            this.rdoRangeSkip.AutoSize = true;
            this.rdoRangeSkip.Checked = true;
            this.rdoRangeSkip.Location = new System.Drawing.Point(6, 37);
            this.rdoRangeSkip.Name = "rdoRangeSkip";
            this.rdoRangeSkip.Size = new System.Drawing.Size(52, 19);
            this.rdoRangeSkip.TabIndex = 1;
            this.rdoRangeSkip.TabStop = true;
            this.rdoRangeSkip.Text = "skip";
            this.rdoRangeSkip.UseVisualStyleBackColor = true;
            // 
            // chkRange
            // 
            this.chkRange.AutoSize = true;
            this.chkRange.Location = new System.Drawing.Point(6, 18);
            this.chkRange.Name = "chkRange";
            this.chkRange.Size = new System.Drawing.Size(105, 19);
            this.chkRange.TabIndex = 0;
            this.chkRange.Text = "rangeCheck";
            this.chkRange.UseVisualStyleBackColor = true;
            this.chkRange.CheckedChanged += new System.EventHandler(this.chkRange_CheckedChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel2.Controls.Add(this.txtRangeTo, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtRangeFrom, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(155, 281);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(527, 75);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // txtRangeTo
            // 
            this.txtRangeTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRangeTo.Location = new System.Drawing.Point(293, 4);
            this.txtRangeTo.Margin = new System.Windows.Forms.Padding(4);
            this.txtRangeTo.Name = "txtRangeTo";
            this.txtRangeTo.Size = new System.Drawing.Size(230, 67);
            this.txtRangeTo.TabIndex = 1;
            this.txtRangeTo.Text = "";
            // 
            // txtRangeFrom
            // 
            this.txtRangeFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRangeFrom.Location = new System.Drawing.Point(4, 4);
            this.txtRangeFrom.Margin = new System.Windows.Forms.Padding(4);
            this.txtRangeFrom.Name = "txtRangeFrom";
            this.txtRangeFrom.Size = new System.Drawing.Size(229, 67);
            this.txtRangeFrom.TabIndex = 0;
            this.txtRangeFrom.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(240, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 75);
            this.label1.TabIndex = 7;
            this.label1.Text = "~";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 360);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 15);
            this.label4.TabIndex = 23;
            this.label4.Text = "Replace Result";
            // 
            // txtInputFolder
            // 
            this.txtInputFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInputFolder.Location = new System.Drawing.Point(155, 476);
            this.txtInputFolder.Multiline = false;
            this.txtInputFolder.Name = "txtInputFolder";
            this.txtInputFolder.Size = new System.Drawing.Size(527, 24);
            this.txtInputFolder.TabIndex = 7;
            this.txtInputFolder.Text = "";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 586);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 15);
            this.label7.TabIndex = 30;
            this.label7.Text = "Result Log";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 15);
            this.label8.TabIndex = 20;
            this.label8.Text = "Input";
            // 
            // tcRight
            // 
            this.tcRight.Controls.Add(this.tabMatches);
            this.tcRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcRight.Location = new System.Drawing.Point(0, 0);
            this.tcRight.Margin = new System.Windows.Forms.Padding(4);
            this.tcRight.Name = "tcRight";
            this.tcRight.SelectedIndex = 0;
            this.tcRight.Size = new System.Drawing.Size(334, 786);
            this.tcRight.TabIndex = 1;
            this.tcRight.TabStop = false;
            // 
            // tabMatches
            // 
            this.tabMatches.Controls.Add(this.treeMatch);
            this.tabMatches.Location = new System.Drawing.Point(4, 25);
            this.tabMatches.Margin = new System.Windows.Forms.Padding(4);
            this.tabMatches.Name = "tabMatches";
            this.tabMatches.Padding = new System.Windows.Forms.Padding(4);
            this.tabMatches.Size = new System.Drawing.Size(326, 757);
            this.tabMatches.TabIndex = 0;
            this.tabMatches.Text = "Matches";
            this.tabMatches.UseVisualStyleBackColor = true;
            // 
            // treeMatch
            // 
            this.treeMatch.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.treeMatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeMatch.Location = new System.Drawing.Point(4, 4);
            this.treeMatch.Margin = new System.Windows.Forms.Padding(4);
            this.treeMatch.Name = "treeMatch";
            this.treeMatch.Size = new System.Drawing.Size(318, 749);
            this.treeMatch.TabIndex = 0;
            this.treeMatch.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 247);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(109, 15);
            this.label9.TabIndex = 31;
            this.label9.Text = "replacement file";
            // 
            // txtReplacementFile
            // 
            this.txtReplacementFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReplacementFile.Location = new System.Drawing.Point(155, 250);
            this.txtReplacementFile.Multiline = false;
            this.txtReplacementFile.Name = "txtReplacementFile";
            this.txtReplacementFile.Size = new System.Drawing.Size(527, 24);
            this.txtReplacementFile.TabIndex = 32;
            this.txtReplacementFile.Text = "";
            // 
            // Regexer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.spcFrame);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Regexer";
            this.Size = new System.Drawing.Size(1047, 795);
            this.spcFrame.Panel1.ResumeLayout(false);
            this.spcFrame.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcFrame)).EndInit();
            this.spcFrame.ResumeLayout(false);
            this.tabResult.ResumeLayout(false);
            this.tpMatch.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tcRight.ResumeLayout(false);
            this.tabMatches.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer spcFrame;
        private System.Windows.Forms.RichTextBox txtPattern;
        private System.Windows.Forms.TabControl tabResult;
        private System.Windows.Forms.TabPage tpMatch;
        private System.Windows.Forms.RichTextBox txtInput;
        private System.Windows.Forms.TabControl tcRight;
        private System.Windows.Forms.TabPage tabMatches;
        private System.Windows.Forms.TreeView treeMatch;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RichTextBox txtReplacement;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkRange;
        private System.Windows.Forms.RadioButton rdoRangeSkip;
        private System.Windows.Forms.RadioButton rdoRangeDo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.RichTextBox txtRangeTo;
        private System.Windows.Forms.RichTextBox txtRangeFrom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox txtReplaceResult;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox txtInputFile;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox txtInputFolder;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdoFilterDo;
        private System.Windows.Forms.RadioButton rdoFilterSkip;
        private System.Windows.Forms.CheckBox chkFileFilter;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RichTextBox txtResultLog;
        private System.Windows.Forms.RichTextBox txtFileFilter;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RichTextBox txtReplacementFile;
    }
}

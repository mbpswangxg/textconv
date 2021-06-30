
namespace XPathUI
{
    partial class XPathTabPage
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.groupHistory = new System.Windows.Forms.GroupBox();
            this.lblFilePath = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.RichTextBox();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.chkOptionalParams = new System.Windows.Forms.CheckBox();
            this.txtReplacement = new System.Windows.Forms.RichTextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tabInput = new System.Windows.Forms.TabControl();
            this.tabInputContent = new System.Windows.Forms.TabPage();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.txtFileContent = new System.Windows.Forms.RichTextBox();
            this.treeResult = new System.Windows.Forms.TreeView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtFileFilter = new System.Windows.Forms.TextBox();
            this.treeFiles = new System.Windows.Forms.TreeView();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.txtReplaceResult = new System.Windows.Forms.RichTextBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.txtReplaceLog = new System.Windows.Forms.RichTextBox();
            this.splitContainer6 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupHistory.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabInput.SuspendLayout();
            this.tabInputContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).BeginInit();
            this.splitContainer6.Panel1.SuspendLayout();
            this.splitContainer6.Panel2.SuspendLayout();
            this.splitContainer6.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1087, 711);
            this.splitContainer1.SplitterDistance = 76;
            this.splitContainer1.TabIndex = 5;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.groupHistory);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.tabControl2);
            this.splitContainer3.Panel2Collapsed = true;
            this.splitContainer3.Size = new System.Drawing.Size(1087, 76);
            this.splitContainer3.SplitterDistance = 615;
            this.splitContainer3.TabIndex = 6;
            // 
            // groupHistory
            // 
            this.groupHistory.Controls.Add(this.lblFilePath);
            this.groupHistory.Controls.Add(this.txtFilter);
            this.groupHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupHistory.Location = new System.Drawing.Point(0, 0);
            this.groupHistory.Name = "groupHistory";
            this.groupHistory.Size = new System.Drawing.Size(1087, 76);
            this.groupHistory.TabIndex = 0;
            this.groupHistory.TabStop = false;
            this.groupHistory.Text = "XPath";
            // 
            // lblFilePath
            // 
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.Location = new System.Drawing.Point(103, 1);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(0, 12);
            this.lblFilePath.TabIndex = 6;
            // 
            // txtFilter
            // 
            this.txtFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFilter.Location = new System.Drawing.Point(3, 15);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(1081, 58);
            this.txtFilter.TabIndex = 5;
            this.txtFilter.Text = "//input[@type=\'button\'];//title;//a";
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(96, 100);
            this.tabControl2.TabIndex = 3;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.splitContainer5);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage3.Size = new System.Drawing.Size(88, 74);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Actions";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // splitContainer5
            // 
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.Location = new System.Drawing.Point(2, 2);
            this.splitContainer5.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer5.Name = "splitContainer5";
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.groupBox10);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.groupBox4);
            this.splitContainer5.Size = new System.Drawing.Size(84, 70);
            this.splitContainer5.SplitterDistance = 40;
            this.splitContainer5.SplitterWidth = 3;
            this.splitContainer5.TabIndex = 0;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.chkOptionalParams);
            this.groupBox10.Controls.Add(this.txtReplacement);
            this.groupBox10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox10.Location = new System.Drawing.Point(0, 0);
            this.groupBox10.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox10.Size = new System.Drawing.Size(40, 70);
            this.groupBox10.TabIndex = 6;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Commands";
            // 
            // chkOptionalParams
            // 
            this.chkOptionalParams.AutoSize = true;
            this.chkOptionalParams.Checked = true;
            this.chkOptionalParams.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOptionalParams.Location = new System.Drawing.Point(152, -1);
            this.chkOptionalParams.Name = "chkOptionalParams";
            this.chkOptionalParams.Size = new System.Drawing.Size(66, 16);
            this.chkOptionalParams.TabIndex = 6;
            this.chkOptionalParams.Text = "Optional";
            this.chkOptionalParams.UseVisualStyleBackColor = true;
            // 
            // txtReplacement
            // 
            this.txtReplacement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReplacement.Location = new System.Drawing.Point(2, 14);
            this.txtReplacement.Name = "txtReplacement";
            this.txtReplacement.Size = new System.Drawing.Size(36, 54);
            this.txtReplacement.TabIndex = 5;
            this.txtReplacement.Text = "";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.richTextBox1);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(41, 70);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Optional Parameters";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(2, 14);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(37, 54);
            this.richTextBox1.TabIndex = 5;
            this.richTextBox1.Text = "";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer6);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer2.Panel2Collapsed = true;
            this.splitContainer2.Size = new System.Drawing.Size(1087, 631);
            this.splitContainer2.SplitterDistance = 616;
            this.splitContainer2.TabIndex = 1;
            // 
            // tabInput
            // 
            this.tabInput.Controls.Add(this.tabInputContent);
            this.tabInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabInput.Location = new System.Drawing.Point(0, 0);
            this.tabInput.Name = "tabInput";
            this.tabInput.SelectedIndex = 0;
            this.tabInput.Size = new System.Drawing.Size(1087, 531);
            this.tabInput.TabIndex = 2;
            // 
            // tabInputContent
            // 
            this.tabInputContent.Controls.Add(this.splitContainer4);
            this.tabInputContent.Location = new System.Drawing.Point(4, 22);
            this.tabInputContent.Name = "tabInputContent";
            this.tabInputContent.Padding = new System.Windows.Forms.Padding(3);
            this.tabInputContent.Size = new System.Drawing.Size(1079, 505);
            this.tabInputContent.TabIndex = 0;
            this.tabInputContent.Text = "Html/Xml";
            this.tabInputContent.UseVisualStyleBackColor = true;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(3, 3);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.txtFileContent);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.treeResult);
            this.splitContainer4.Size = new System.Drawing.Size(1073, 499);
            this.splitContainer4.SplitterDistance = 712;
            this.splitContainer4.TabIndex = 2;
            // 
            // txtFileContent
            // 
            this.txtFileContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFileContent.Location = new System.Drawing.Point(0, 0);
            this.txtFileContent.Name = "txtFileContent";
            this.txtFileContent.Size = new System.Drawing.Size(712, 499);
            this.txtFileContent.TabIndex = 1;
            this.txtFileContent.Text = "";
            // 
            // treeResult
            // 
            this.treeResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeResult.Location = new System.Drawing.Point(0, 0);
            this.treeResult.Name = "treeResult";
            this.treeResult.Size = new System.Drawing.Size(357, 499);
            this.treeResult.TabIndex = 0;
            this.treeResult.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeResult_AfterSelect);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtFileFilter);
            this.groupBox2.Controls.Add(this.treeFiles);
            this.groupBox2.Controls.Add(this.txtFilePath);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1087, 96);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Files";
            // 
            // txtFileFilter
            // 
            this.txtFileFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFileFilter.BackColor = System.Drawing.SystemColors.Info;
            this.txtFileFilter.Location = new System.Drawing.Point(1005, -3);
            this.txtFileFilter.Name = "txtFileFilter";
            this.txtFileFilter.Size = new System.Drawing.Size(78, 19);
            this.txtFileFilter.TabIndex = 8;
            this.txtFileFilter.Text = "html|jsp";
            this.txtFileFilter.TextChanged += new System.EventHandler(this.txtFilePath_TextChanged);
            // 
            // treeFiles
            // 
            this.treeFiles.CheckBoxes = true;
            this.treeFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeFiles.Location = new System.Drawing.Point(3, 15);
            this.treeFiles.Name = "treeFiles";
            this.treeFiles.Size = new System.Drawing.Size(1081, 78);
            this.treeFiles.TabIndex = 0;
            this.treeFiles.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeFiles_AfterSelect);
            this.treeFiles.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeFiles_NodeMouseDoubleClick);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilePath.BackColor = System.Drawing.SystemColors.Info;
            this.txtFilePath.Location = new System.Drawing.Point(47, -2);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(952, 19);
            this.txtFilePath.TabIndex = 2;
            this.txtFilePath.Text = "C:\\tmp\\wangx\\_projects\\jae\\develop\\jae\\jae\\src\\main\\jssp\\src\\jae";
            this.txtFilePath.TextChanged += new System.EventHandler(this.txtFilePath_TextChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tabControl3);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(96, 100);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Actions Result";
            // 
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.tabPage4);
            this.tabControl3.Controls.Add(this.tabPage5);
            this.tabControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl3.Location = new System.Drawing.Point(3, 15);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(90, 82);
            this.tabControl3.TabIndex = 15;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.txtReplaceResult);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(453, 527);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "ReplaceResult";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // txtReplaceResult
            // 
            this.txtReplaceResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReplaceResult.Location = new System.Drawing.Point(3, 3);
            this.txtReplaceResult.Name = "txtReplaceResult";
            this.txtReplaceResult.ReadOnly = true;
            this.txtReplaceResult.Size = new System.Drawing.Size(447, 521);
            this.txtReplaceResult.TabIndex = 7;
            this.txtReplaceResult.Text = "";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.txtReplaceLog);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(453, 527);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "Replace Log";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // txtReplaceLog
            // 
            this.txtReplaceLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReplaceLog.Location = new System.Drawing.Point(3, 3);
            this.txtReplaceLog.Name = "txtReplaceLog";
            this.txtReplaceLog.ReadOnly = true;
            this.txtReplaceLog.Size = new System.Drawing.Size(447, 521);
            this.txtReplaceLog.TabIndex = 8;
            this.txtReplaceLog.Text = "";
            // 
            // splitContainer6
            // 
            this.splitContainer6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer6.Location = new System.Drawing.Point(0, 0);
            this.splitContainer6.Name = "splitContainer6";
            this.splitContainer6.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer6.Panel1
            // 
            this.splitContainer6.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer6.Panel2
            // 
            this.splitContainer6.Panel2.Controls.Add(this.tabInput);
            this.splitContainer6.Size = new System.Drawing.Size(1087, 631);
            this.splitContainer6.SplitterDistance = 96;
            this.splitContainer6.TabIndex = 3;
            // 
            // XPathTabPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "XPathTabPage";
            this.Size = new System.Drawing.Size(1087, 711);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.groupHistory.ResumeLayout(false);
            this.groupHistory.PerformLayout();
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabInput.ResumeLayout(false);
            this.tabInputContent.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.tabControl3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.splitContainer6.Panel1.ResumeLayout(false);
            this.splitContainer6.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).EndInit();
            this.splitContainer6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.RichTextBox txtFileContent;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.TreeView treeFiles;
        private System.Windows.Forms.GroupBox groupHistory;
        private System.Windows.Forms.TreeView treeResult;
        private System.Windows.Forms.RichTextBox txtFilter;
        private System.Windows.Forms.TabControl tabInput;
        private System.Windows.Forms.TabPage tabInputContent;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.CheckBox chkOptionalParams;
        private System.Windows.Forms.RichTextBox txtReplacement;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox txtFileFilter;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.RichTextBox txtReplaceResult;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.RichTextBox txtReplaceLog;
        private System.Windows.Forms.Label lblFilePath;
        private System.Windows.Forms.SplitContainer splitContainer6;
    }
}

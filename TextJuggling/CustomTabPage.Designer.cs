
namespace conver
{
    partial class CustomTabPage
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
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupHistory = new System.Windows.Forms.GroupBox();
            this.txtFileContent = new System.Windows.Forms.RichTextBox();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtJugglingXml = new System.Windows.Forms.RichTextBox();
            this.txtJugglingXmlPath = new System.Windows.Forms.TextBox();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.treeApplication = new System.Windows.Forms.TreeView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.treeOrder = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.treeModule = new System.Windows.Forms.TreeView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtNewJugglingXml = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupHistory.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(1087, 711);
            this.splitContainer1.SplitterDistance = 442;
            this.splitContainer1.TabIndex = 5;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupHistory);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox5);
            this.splitContainer2.Size = new System.Drawing.Size(442, 711);
            this.splitContainer2.SplitterDistance = 355;
            this.splitContainer2.TabIndex = 1;
            // 
            // groupHistory
            // 
            this.groupHistory.Controls.Add(this.txtFileContent);
            this.groupHistory.Controls.Add(this.txtFilePath);
            this.groupHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupHistory.Location = new System.Drawing.Point(0, 0);
            this.groupHistory.Name = "groupHistory";
            this.groupHistory.Size = new System.Drawing.Size(442, 355);
            this.groupHistory.TabIndex = 0;
            this.groupHistory.TabStop = false;
            this.groupHistory.Text = "Juggling.structure.text";
            // 
            // txtFileContent
            // 
            this.txtFileContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFileContent.Location = new System.Drawing.Point(3, 15);
            this.txtFileContent.Name = "txtFileContent";
            this.txtFileContent.Size = new System.Drawing.Size(436, 337);
            this.txtFileContent.TabIndex = 5;
            this.txtFileContent.Text = "";
            // 
            // txtFilePath
            // 
            this.txtFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilePath.BackColor = System.Drawing.SystemColors.Info;
            this.txtFilePath.Location = new System.Drawing.Point(131, -2);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(308, 19);
            this.txtFilePath.TabIndex = 2;
            this.txtFilePath.TextChanged += new System.EventHandler(this.txtFilePath_TextChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtJugglingXml);
            this.groupBox5.Controls.Add(this.txtJugglingXmlPath);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(0, 0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(442, 352);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Juggling";
            // 
            // txtJugglingXml
            // 
            this.txtJugglingXml.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtJugglingXml.Location = new System.Drawing.Point(3, 15);
            this.txtJugglingXml.Name = "txtJugglingXml";
            this.txtJugglingXml.Size = new System.Drawing.Size(436, 334);
            this.txtJugglingXml.TabIndex = 5;
            this.txtJugglingXml.Text = "";
            // 
            // txtJugglingXmlPath
            // 
            this.txtJugglingXmlPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtJugglingXmlPath.BackColor = System.Drawing.SystemColors.Info;
            this.txtJugglingXmlPath.Location = new System.Drawing.Point(131, -2);
            this.txtJugglingXmlPath.Name = "txtJugglingXmlPath";
            this.txtJugglingXmlPath.Size = new System.Drawing.Size(308, 19);
            this.txtJugglingXmlPath.TabIndex = 2;
            this.txtJugglingXmlPath.TextChanged += new System.EventHandler(this.txtJugglingXmlPath_TextChanged);
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.groupBox4);
            this.splitContainer3.Size = new System.Drawing.Size(641, 711);
            this.splitContainer3.SplitterDistance = 272;
            this.splitContainer3.TabIndex = 6;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(272, 711);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.treeApplication);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 240);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(266, 231);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Applications";
            // 
            // treeApplication
            // 
            this.treeApplication.CheckBoxes = true;
            this.treeApplication.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeApplication.Location = new System.Drawing.Point(3, 15);
            this.treeApplication.Name = "treeApplication";
            this.treeApplication.Size = new System.Drawing.Size(260, 213);
            this.treeApplication.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.treeOrder);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 477);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(266, 231);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Modules Order";
            // 
            // treeOrder
            // 
            this.treeOrder.CheckBoxes = true;
            this.treeOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeOrder.Location = new System.Drawing.Point(3, 15);
            this.treeOrder.Name = "treeOrder";
            this.treeOrder.Size = new System.Drawing.Size(260, 213);
            this.treeOrder.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.treeModule);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(266, 231);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Modules";
            // 
            // treeModule
            // 
            this.treeModule.CheckBoxes = true;
            this.treeModule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeModule.FullRowSelect = true;
            this.treeModule.Location = new System.Drawing.Point(3, 15);
            this.treeModule.Name = "treeModule";
            this.treeModule.Size = new System.Drawing.Size(260, 213);
            this.treeModule.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtNewJugglingXml);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(365, 711);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "New Juggling";
            // 
            // txtNewJugglingXml
            // 
            this.txtNewJugglingXml.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNewJugglingXml.Location = new System.Drawing.Point(3, 15);
            this.txtNewJugglingXml.Name = "txtNewJugglingXml";
            this.txtNewJugglingXml.Size = new System.Drawing.Size(359, 693);
            this.txtNewJugglingXml.TabIndex = 5;
            this.txtNewJugglingXml.Text = "";
            // 
            // CustomTabPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "CustomTabPage";
            this.Size = new System.Drawing.Size(1087, 711);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupHistory.ResumeLayout(false);
            this.groupHistory.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.GroupBox groupHistory;
        private System.Windows.Forms.RichTextBox txtFileContent;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RichTextBox txtNewJugglingXml;
        private System.Windows.Forms.TreeView treeApplication;
        private System.Windows.Forms.TreeView treeOrder;
        private System.Windows.Forms.TreeView treeModule;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RichTextBox txtJugglingXml;
        private System.Windows.Forms.TextBox txtJugglingXmlPath;
    }
}

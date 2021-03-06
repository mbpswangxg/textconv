using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace conver
{
    public partial class FrmMDI : Form
    {
        private int childFormNumber = 0;
        
        public FrmMDI()
        {
            InitializeComponent();
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.save_Click);
            this.saveToolStripButton.Click += new System.EventHandler(this.save_Click);
        }
        private void FrmMDI_Load(object sender, EventArgs e)
        {
            ShowNewForm(sender, e);
        }

        #region User Menu

        private void ShowNewForm(object sender, EventArgs e)
        {
            bool isXpath = false;
            if(sender is ToolStripButton)
            {
                ToolStripButton btn1 = (ToolStripButton)sender;
                if (btn1.Name.Contains("XPath"))
                {
                    isXpath = true;
                }
            }else if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem btn1 = (ToolStripMenuItem)sender;
                if (btn1.Name.Contains("XPath"))
                {
                    isXpath = true;
                }
            }
            if (isXpath)
            {
                tabWindow.SelectedTab = NewTabPage("XPath", childFormNumber++);
            }
            else
            {
                tabWindow.SelectedTab = NewTabPage("Regex", childFormNumber++);
            }
        }
        private TabPage NewTabPage(string pageText, int index) 
        {
            string pageName = pageText;
            if (index > 0)
            {
                pageName = pageText + index;
            }
            tabWindow.SuspendLayout();
            tabWindow.TabPages.Add(pageName, pageName);
            TabPage page = tabWindow.TabPages[pageName];
            if (pageText.Contains("XPath"))
            {
                XPathTabPage r = new XPathTabPage();
                r.Dock = DockStyle.Fill;
                r.TabStop = false;
                r.Name = "XPathTabPage";

                this.KeyDown += r.FrmRegex_KeyDown;
                this.btnMatch.Click += r.btnMatch_Click;
                page.Controls.Add(r);
            }
            else
            {
                RegexTabPage r = new RegexTabPage();
                r.Dock = DockStyle.Fill;
                r.TabStop = false;
                r.Name = "RegexTabPage";

                this.KeyDown += r.FrmRegex_KeyDown;
                this.btnMatch.Click += r.btnMatch_Click;
                this.btnReplace.Click += r.btnReplace_Click;
                this.btnReplaceFile.Click += r.btnReplaceFile_Click;
                page.Controls.Add(r);
            }
            
            tabWindow.ResumeLayout();
            return page;
        }
        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Application.StartupPath;
            openFileDialog.Filter = "Yml (*.yml)|*.yml|Yaml (*.yaml)|*.yaml|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                
                //Open a new TabPage.
                FileInfo fi = new FileInfo(openFileDialog.FileName);
                int idx = tabWindow.TabPages.IndexOfKey(fi.Name);
                if (idx >= 0)
                {
                    tabWindow.SelectedTab = tabWindow.TabPages[idx];
                }
                else 
                {
                    //string content = File.ReadAllText(fi.FullName);
                    if (fi.Name.Contains("XPath"))
                    {
                        tabWindow.SelectedTab = NewTabPage(fi.Name, 0);
                    }
                    else
                    {
                        tabWindow.SelectedTab = NewTabPage(fi.Name, 0);
                    }
                    
                    tabWindow.SelectedTab.ToolTipText = fi.FullName;
                }

                Control[] ctrs = tabWindow.SelectedTab.Controls.Find("RegexTabPage", false);
                RegexTabPage regexTab = (RegexTabPage)ctrs.First();
                regexTab.LoadRules(openFileDialog.FileName);
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Application.StartupPath;
            saveFileDialog.Filter = "Yml (*.yml)|*.yml|Yaml (*.yaml)|*.yaml|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                Control[] ctrs = tabWindow.SelectedTab.Controls.Find("RegexTabPage", false);
                RegexTabPage regexTab =(RegexTabPage) ctrs.First();
                regexTab.SaveRules(saveFileDialog.FileName);
                FileInfo fi = new FileInfo(saveFileDialog.FileName);
                tabWindow.SelectedTab.Text = fi.Name;
                tabWindow.SelectedTab.ToolTipText = fi.FullName;
            }
        }
        private void save_Click(object sender, EventArgs e)
        {
            string filepath = tabWindow.SelectedTab.ToolTipText;
            if (File.Exists(filepath))
            {
                Control[] ctrs = tabWindow.SelectedTab.Controls.Find("RegexTabPage", false);
                RegexTabPage regexTab = (RegexTabPage)ctrs.First();
                regexTab.SaveRules(filepath);
                ShowMessage("Replace Rule Saved In: " + filepath);
            }
            else
            {
                SaveAsToolStripMenuItem_Click(sender, e);
            }
        }
        private void mnuCloseTab_Click(object sender, EventArgs e)
        {
            TabPage closingPage = tabWindow.SelectedTab;
            tabWindow.TabPages.Remove(closingPage);
            closingPage.Dispose();
        }
        private void ShowMessage(string message)
        {
            lblStatusMessage.Text = message;
            tbMessage.Text = message;

        }
        #endregion

        #region System Menu
        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        #endregion
    }
}

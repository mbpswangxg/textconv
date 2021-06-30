using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;
using TextJuggling;

namespace conver
{
    public partial class FrmMain : Form
    {
        private int childFormNumber = 0;
        
        public FrmMain()
        {
            InitializeComponent();
        }
        private void FrmMDI_Load(object sender, EventArgs e)
        {
            ShowNewForm(sender, e);
        }

        #region User Menu

        private void ShowNewForm(object sender, EventArgs e)
        {
            tabWindow.SelectedTab = NewTabPage("Tab", childFormNumber++);
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
            {
                CustomTabPage r = new CustomTabPage();
                r.Dock = DockStyle.Fill;
                r.TabStop = false;
                r.Name = "RegexTabPage";

                this.KeyDown += r.FrmRegex_KeyDown;
                this.btnMatch.Click += r.btnMatch_Click;
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
    }
}

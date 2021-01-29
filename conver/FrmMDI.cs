using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        }
        private void FrmMDI_Load(object sender, EventArgs e)
        {
            ShowNewForm(sender, e);
        }

        #region User Menu
        private void ShowNewForm(object sender, EventArgs e)
        {  
            tabWindow.SuspendLayout();
            TabPage page = new TabPage( "Regex" + childFormNumber++);
            tabWindow.TabPages.Add(page);
            RegexTabPage r = new RegexTabPage();
            r.Dock = DockStyle.Fill;
            r.TabStop = false;

            this.KeyDown += r.FrmRegex_KeyDown;
            this.btnMatch.Click += r.btnMatch_Click;
            this.btnReplace.Click += r.btnReplace_Click;

            page.Controls.Add(r);
            tabWindow.ResumeLayout();
            tabWindow.SelectedTab = page;
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text File (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text File (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }
        
        private void mnuCloseTab_Click(object sender, EventArgs e)
        {
            TabPage closingPage = tabWindow.SelectedTab;
            tabWindow.TabPages.Remove(closingPage);
            closingPage.Dispose();
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

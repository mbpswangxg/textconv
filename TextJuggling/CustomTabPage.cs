using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Text.Common;
using TextJuggling;

namespace conver
{
    public partial class CustomTabPage : UserControl
    {
        private JugglingStructureManager structManager;
        private JugglingXmlManager xmlManager;
        public CustomTabPage()
        {
            InitializeComponent();
            structManager = new JugglingStructureManager();
            xmlManager = new JugglingXmlManager();
            txtFilePath.Text = Config.GetAppSettingValue("juggling.structure");
            txtJugglingXmlPath.Text = Config.GetAppSettingValue("juggling.xml");
        }

        /// <summary>
        /// F5-F8 Key down.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FrmRegex_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    btnMatch_Click(sender, e);
                    break;
                case Keys.F6:
                    //btnReplace_Click(sender, e);
                    break;
                default:
                    return;
            }
        }
        public void btnMatch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFileContent.Text)) return;
            if (structManager.Applications.Count == 0) return;
            if (xmlManager.Applications.Count == 0) return;
            List<string> newLines = xmlManager.ExportBy(structManager);
            txtNewJugglingXml.AppendText(string.Join("\n", newLines));
        }
        
        private void txtFilePath_TextChanged(object sender, EventArgs e)
        {
            if (!File.Exists(txtFilePath.Text)) return;

            txtFileContent.Text = File.ReadAllText(txtFilePath.Text, Config.Encoding);
            structManager.Parse(txtFilePath.Text);

            treeApplication.Nodes.Clear();
            structManager.FillTree(treeApplication.Nodes, structManager.Applications);

            //treeModule.Nodes.Clear();
            //structManager.FillTree(treeModule.Nodes, structManager.Modules);
        }
        private void txtJugglingXmlPath_TextChanged(object sender, EventArgs e)
        {
            if (!File.Exists(txtJugglingXmlPath.Text)) return;

            txtJugglingXml.Text = File.ReadAllText(txtJugglingXmlPath.Text, Config.Encoding);
            xmlManager.Parse(txtJugglingXmlPath.Text);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

using System.Windows.Forms;

using HtmlAgilityPack;
using System.Text;
using Text.Common;

namespace XPathUI
{
    public partial class XPathTabPage : UserControl
    {
        private HashSet<string> rules = new HashSet<string>();
        private HighLight hlFileContent;
         
        public XPathTabPage()
        {
            InitializeComponent();
            hlFileContent = new HighLight(txtFileContent);

            txtFilePath.Text = Config.GetAppSettingValue("dest.file");
            LoadFileTree();
        }

        #region SaveRules, LoadRules 
        /// <summary>
        /// Do regex match, show the result on tree.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SaveRules(string filepath)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(string.Format("folderPath:{0}", txtFilePath.Text));
                sb.AppendLine(string.Format("fileExt:{0}", txtFileFilter.Text));
                sb.AppendLine(string.Format("xpathPattern:{0}", txtFilter.Text));
                File.WriteAllText(filepath, sb.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n" + e.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Do regex match, show the result on tree.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void LoadRules(string filepath)
        {
            try
            {
                string[] lines = File.ReadAllLines(filepath);
                foreach(string line in lines)
                {
                    Match m = Regex.Match(line, @"(\w+):(.+)");
                    if (m.Success)
                    {
                        if (m.Groups[1].Value.Equals("folderPath"))
                        {
                            txtFilePath.Text = m.Groups[2].Value;
                        }
                        else if (m.Groups[1].Value.Equals("fileExt"))
                        {
                            txtFileFilter.Text = m.Groups[2].Value;
                        }
                        else if (m.Groups[1].Value.Equals("xpathPattern"))
                        {
                            txtFilter.Text = m.Groups[2].Value;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n" + e.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        #endregion

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
            
            try
            {
                hlFileContent.Reset2Default();

                Dictionary<string, HtmlNodeCollection> kvNodes = HtmlParser.GetNodes(txtFileContent.Text, txtFilter.Text);
                treeResult.Nodes.Clear();
                foreach (KeyValuePair<string,HtmlNodeCollection> kv in kvNodes)
                {
                    TreeNode treeNode = treeResult.Nodes.Add(kv.Key, kv.Key);
                    foreach(HtmlNode n1 in kv.Value)
                    {
                        XPathUItHtmlNode(treeNode, n1);
                        hlFileContent.Highlight(n1.StreamPosition, n1.OuterHtml.Length, Color.Black, Color.YellowGreen);
                    }
                }

                if (!rules.Contains(txtFilter.Text))
                {
                    rules.Add(txtFilter.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void SaveBeforeClosed(object sender, EventArgs e)
        {

        }
        private void XPathUItHtmlNode(TreeNode treeNode, HtmlNode htmlNode)
        {
            TreeNode sNode = treeNode.Nodes.Add(string.Format("{0}:{1}", htmlNode.StreamPosition, htmlNode.Name), htmlNode.OuterHtml);
            foreach(HtmlNode hNode in htmlNode.ChildNodes)
            {
                XPathUItHtmlNode(sNode, hNode);
            }
        }
        //private void LoadHistory()
        //{
        //    if (!File.Exists(HtmlParser.HistoryFile)) return;
        //    string[] lines = File.ReadAllLines(HtmlParser.HistoryFile);
        //    rules.Union(lines);
        //}

        private void txtFilePath_TextChanged(object sender, EventArgs e)
        {
            LoadFileTree();
            if (Regex.IsMatch(txtFilePath.Text, @"^http[s]?:"))
            {
                //lblFilePath.Text = txtFilePath.Text;
                txtFileContent.Text = HtmlParser.GetHtml(txtFilePath.Text);
            }
        }
        private void LoadFileTree()
        {
            treeFiles.Nodes.Clear();
            FillTree(txtFilePath.Text, treeFiles.Nodes);
        }
        private void FillTree(string folderPath, TreeNodeCollection nodes)
        {
            if (Regex.IsMatch(folderPath, @"^http[s]?:")) return;

            if (!Directory.Exists(folderPath)) return;
            
            List<string> lstFiles = new List<string>(Directory.GetFiles(folderPath));
            
            string pattern = txtFileFilter.Text;
            if (!pattern.EndsWith("$"))
            {
                pattern = pattern + "$";
            }
            lstFiles = lstFiles.FindAll(f => Regex.IsMatch(f, pattern));
            // ファイル存在するフォルダだけを表示
            if (lstFiles.Count > 0)
            {
                TreeNode topNode = nodes.Add(folderPath, folderPath.Replace(txtFilePath.Text, "."));
                foreach (string file in lstFiles)
                {
                    topNode.Nodes.Add(file, file.Replace(txtFilePath.Text, "."));
                }
            }

            string[] folders = Directory.GetDirectories(folderPath);
            foreach (string sfolder in folders)
            {
                FillTree(sfolder, nodes);
            }

        }
        private void treeFiles_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            bool ctrClick = (ModifierKeys & Keys.Control) == Keys.Control;
            if (ctrClick && e.Node.Nodes.Count > 0)
            {
                txtFilePath.Text = e.Node.Name;
                return;
            }

            bool sftClick = (ModifierKeys & Keys.Shift) == Keys.Shift;
            if (sftClick)
            {
                txtFilePath.Text = StringUtils.ReplaceMatch(txtFilePath.Text, @"\\[^\\]+\\?$", "");
                return;
            }

            if (File.Exists(e.Node.Name))
            {
                txtFileContent.Text = File.ReadAllText(e.Node.Name, Config.Encoding);
                tabInput.SelectedTab = tabInput.TabPages["tabInputContent"];
            }
        }
        private void treeFiles_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (File.Exists(e.Node.Name))
            {
                lblFilePath.Text = e.Node.Name;
                txtFileContent.Text = File.ReadAllText(lblFilePath.Text);
            }
        }

        private void treeResult_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string sindex = StringUtils.GetMatchGroup(e.Node.Name, @"^(\d+)",1);
            if (!string.IsNullOrEmpty(sindex))
            {
                int index = int.Parse(sindex);
                txtFileContent.Select(index, e.Node.Text.Length);
                txtFileContent.ScrollToCaret();
            }
        }
    }
}

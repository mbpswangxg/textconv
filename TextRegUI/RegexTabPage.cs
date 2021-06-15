using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Text.RegularExpressions;
using TextConv;
using YamlDotNet.Serialization;
using System.IO;

namespace conver
{
    public partial class RegexTabPage : UserControl
    {
        private ReplaceRuleItem ruleItem = new ReplaceRuleItem();
        private ReplaceRule rule = new ReplaceRule();
        private bool replaced = false;
        public RegexOptions RegexOptions {
            get
            {
                RegexOptions options = RegexOptions.None;
                if (chkIgnoreCase.Checked)
                {
                    options |= RegexOptions.IgnoreCase;
                }
                if (chkMultiline.Checked)
                {
                    options |= RegexOptions.Multiline;
                }
                return options;
            }
        }
       
        public RegexTabPage()
        {
            InitializeComponent();
            txtFilePath.Text = Config.GetAppSettingValue2("inputFolder", txtFilePath.Text);
            txtFilePath_TextChanged(txtFilePath, null);
            rule.rules.Add(ruleItem);
        }

        #region
        /// <summary>
        /// Do regex match, show the result on tree.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SaveRules(string filepath)
        {
            using (StreamWriter writer = File.CreateText(filepath))
            {
                UI2Data();
                writer.Write(rule.ToString());
            }
        }
        
        /// <summary>
        /// Do regex match, show the result on tree.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void LoadRules(string filepath)
        {
            rule = YmlLoader.LoadFromFile<ReplaceRule>(filepath);
            Data2UI();
        }
        
        private void UI2Data()
        {
            ruleItem.pattern = txtPattern.Text;
            ruleItem.replacement = txtReplacement.Text;
            
            rule.filefilter = txtFileFilter.Text;
        }
        private void Data2UI()
        {
            ruleItem = rule.rules[0];
            txtPattern.Text = ruleItem.pattern;
            txtReplacement.Text = ruleItem.replacement;
         
            txtFileFilter.Text = rule.filefilter;
        }

        /// <summary>
        /// Do regex match, show the result on tree.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnMatch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtInput.Text)) return;
            if (string.IsNullOrEmpty(txtPattern.Text)) return;

            UI2Data(); 
            
            HighLight hl = new HighLight(txtInput);
            hl.Reset2Default();
            treeMatch.Nodes.Clear();

            List<Match> matches = new List<Match>();

            Regex reg = new Regex(txtPattern.Text, RegexOptions);
            matches.AddRange(reg.Matches(txtInput.Text).Cast<Match>());
            BindMatchTree(matches, hl, reg, "Pattern");

            tabResult.SelectedTab = tpMatch;
            replaced = false;
        }

        /// <summary>
        /// Do replace, show the result on replaced textbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnReplace_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtInput.Text)) return;
            if (string.IsNullOrEmpty(txtReplacement.Text)) return;

            //Match first
            btnMatch_Click(sender, e);

            txtReplaceResult.Text = rule.ReplaceText(txtInput.Text);
            HighLight hl = new HighLight(txtReplaceResult);
            hl.Reset2Default();            
            foreach (string result in ruleItem.Results())
            {
                string[] newV = Regex.Split(result, @"\t");
                hl.Highlight(newV.Last());
            }
            replaced = true;
        }

        /// <summary>
        /// Show Replace files window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnReplaceFile_Click(object sender, EventArgs e)
        {
            txtReplaceLog.Clear();
            if (string.IsNullOrEmpty(ruleItem.pattern)) return;

            int x = ReplaceFiles(treeFiles.Nodes);
            // if no checked files, replace the default selectedNode. 
            if(x == 0 && treeFiles.SelectedNode != null)
            {
                rule.ReplaceFile(treeFiles.SelectedNode.Name);
                AppendReplaceLog(treeFiles.SelectedNode);
            }
            
        }
        private int ReplaceFiles(TreeNodeCollection nodes)
        {
            int x = 0;
            foreach (TreeNode node in nodes)
            {
                if (node.Nodes.Count > 0) 
                {
                    x += ReplaceFiles(node.Nodes);
                    continue;
                }
                if (!node.Checked) continue;
                x++;
                rule.ReplaceFile(node.Name);
                AppendReplaceLog(node);
            }
            return x;
        }
        private void AppendReplaceLog(TreeNode node)
        {
            foreach (var item in rule.rules)
            {
                //置換無しのファイルは出力しない
                if (item.Results().Count == 0) continue;

                txtReplaceLog.AppendText(string.Format("{0}:\npattern: {1}\nreplacement: {2}\n---------Replacement Result------------------\n{3}\n", 
                    node.Name,item.pattern, item.replacement, string.Join("\n", item.Results())));
            }
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
                    btnReplace_Click(sender, e);
                    break;
                case Keys.F7:
                    //btnOpen_Click(sender, e);
                    break;
                case Keys.F8:
                    btnReplaceFile_Click(sender, e);
                    break;
                default:
                return;
            }
        }
        #endregion

        #region Bind MatchCollection to Tree
        private void BindMatchTree(List<Match> matches, HighLight hl, Regex reg, string caption)
        {
            BindMatchTree(matches, hl, reg, caption, Color.Black, Color.Yellow);
        }
        /// <summary>
        /// Show the match result on tree.
        /// </summary>
        /// <param name="matches"></param>
        private void BindMatchTree(List<Match> matches, HighLight hl, Regex reg, string caption, Color foreColor, Color backColor)
        {
            int index = 0;
            foreach (Match match in matches)
            {
                hl.Highlight(match, foreColor, backColor);

                TreeNode nodeMatch = treeMatch.Nodes.Add(string.Format("{0} Match[{1}]:{2}", caption, index, reg.ToString()));
                nodeMatch.Name = string.Format("{0}:{1}", match.Index, match.Length);
                BindGroup(nodeMatch, match, reg);
                index++;
            }
        }

        /// <summary>
        /// Show the match result on tree node.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="match"></param>
        private static void BindGroup(TreeNode node, Match match, Regex reg)
        {
            for (int i = 0; i < match.Groups.Count; i++)
            {
                Group group = match.Groups[i];
                string groupName = reg.GroupNameFromNumber(i);
                string Value = string.Format("[{0}:{1}]{2}", i, groupName, group.Value);
                TreeNode nodeGroup = node.Nodes.Add(Value);
                nodeGroup.Name = string.Format("{0}:{1}", group.Index, group.Length);
                BindCapture(nodeGroup, group);
            }
        }
        /// <summary>
        /// Show the match group result on tree node.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="group"></param>
        private static void BindCapture(TreeNode node, Group group)
        {
            foreach (Capture capture in group.Captures)
            {
                string Value = string.Format("[{0}:{1}]:{2}",
                    capture.Index, capture.Length, capture.Value);
                TreeNode nodeCapture = node.Nodes.Add(Value);
                nodeCapture.Name = string.Format("{0}:{1}", capture.Index, capture.Length);
            }
        }
        #endregion

        private void treeResult_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Match match = Regex.Match(e.Node.Name, @"^(\d+):(\d+)");
            if (match.Success)
            {
                int index = int.Parse(match.Groups[1].Value);
                int length = int.Parse(match.Groups[2].Value);

                txtInput.Select(index, length);
                txtInput.ScrollToCaret();
                if (replaced)
                {
                    txtReplaceResult.Select(index, length);
                    txtReplaceResult.ScrollToCaret();
                }
            }
        }

        private void txtFilePath_TextChanged(object sender, EventArgs e)
        {
            treeFiles.Nodes.Clear();
            FillTree(txtFilePath.Text, treeFiles.Nodes);
        }
        private void FillTree(string folderPath, TreeNodeCollection nodes)
        {
            if (!Directory.Exists(folderPath)) return;

            List<string> lstFiles = new List<string>(Directory.GetFiles(folderPath));
            if (!string.IsNullOrEmpty(txtFileFilter.Text))
            {
                string pattern = txtFileFilter.Text;
                if (!pattern.EndsWith("$"))
                {
                    pattern = pattern + "$";
                }
                lstFiles = lstFiles.FindAll(f => Regex.IsMatch(f, pattern));
            }
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
                txtInput.Text = File.ReadAllText(e.Node.Name, Config.Encoding);
                tabInput.SelectedTab = tabInput.TabPages["tabInputContent"];
            }
        }

        private void treeFiles_AfterCheck(object sender, TreeViewEventArgs e)
        {
            foreach(TreeNode snode in e.Node.Nodes)
            {
                snode.Checked = e.Node.Checked;
            }
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (TreeNode snode in treeFiles.Nodes)
            {
                snode.Checked = !snode.Checked;
            }
        }

        private void chkShowMatchGroup_CheckedChanged(object sender, EventArgs e)
        {
            splitContainer2.Panel2Collapsed = !chkShowMatchGroup.Checked;
        }

        private void chkShowReplacement_CheckedChanged(object sender, EventArgs e)
        {
            spcFrame.Panel2Collapsed = !chkShowReplacement.Checked;
        }

        private void chkOptionalParams_CheckedChanged(object sender, EventArgs e)
        {
            splitContainer5.Panel2Collapsed = !chkOptionalParams.Checked;
        }
    }
}

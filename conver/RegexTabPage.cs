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
        private string replaceRuleText = string.Empty;
        public RegexOptions RegexOptions
        {
            get
            {
                RegexOptions regOptions = RegexOptions.None;
                if (chkIgnoreCase.Checked)
                {
                    regOptions = regOptions | RegexOptions.IgnoreCase;
                }
                if (chkMultiline.Checked)
                {
                    regOptions = regOptions | RegexOptions.Multiline;
                }

                return regOptions;
            }
        }

        public RegexTabPage()
        {
            InitializeComponent();
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
                writer.Write(txtReplaceRule.Text);
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
            txtReplaceRule.Text = File.ReadAllText(filepath);
            replaceRuleText = txtReplaceRule.Text;
        }
        private void LoadRuleFromInput()
        {
            if (string.IsNullOrEmpty(txtReplaceRule.Text)) return;
            if (txtReplaceRule.Text.Equals(this.replaceRuleText)) return;

            this.replaceRuleText = txtReplaceRule.Text;
            var deserializer = new Deserializer();

            try
            {
                rule = deserializer.Deserialize<ReplaceRule>(txtReplaceRule.Text);
                rule.Init();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n" + e.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
            Data2UI();
        }
        private void UI2Data()
        {
            ruleItem.pattern = txtPattern.Text;
            ruleItem.replacement = txtReplacement.Text;
            
            ruleItem.IgnoreCase = chkIgnoreCase.Checked;
            ruleItem.Multiline = chkMultiline.Checked;

            ruleItem.rangeFrom = txtRangeFrom.Text;
            ruleItem.rangeTo = txtRangeTo.Text;
            ruleItem.rangeSkip = chkRange.Checked;

            rule.filefilter = txtFileFilter.Text;
            rule.fileSkip = chkFileSkip.Checked;
        }
        private void Data2UI()
        {
            ruleItem = rule.rules[0];
            txtPattern.Text = ruleItem.pattern;
            txtReplacement.Text = ruleItem.replacement;
         
            chkIgnoreCase.Checked = ruleItem.IgnoreCase;
            chkMultiline.Checked = ruleItem.Multiline;

            txtRangeFrom.Text = ruleItem.rangeFrom;
            txtRangeTo.Text = ruleItem.rangeTo;
            chkRange.Checked = ruleItem.rangeSkip;

            txtFileFilter.Text = rule.filefilter;
            chkFileSkip.Checked = rule.fileSkip;
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

            HighLight hl = new HighLight(txtInput);
            hl.Reset2Default();
            treeMatch.Nodes.Clear();

            List<Match> matches = new List<Match>();

            Regex reg = new Regex(txtPattern.Text, RegexOptions);
            matches.AddRange(reg.Matches(txtInput.Text).Cast<Match>());
            BindMatchTree(matches, hl, reg, "Pattern");

            if (!string.IsNullOrEmpty(txtRangeFrom.Text))
            {
                Regex reg1 = new Regex(txtRangeFrom.Text, RegexOptions);
                matches.Clear();
                matches.AddRange(reg1.Matches(txtInput.Text).Cast<Match>());
                BindMatchTree(matches, hl, reg1, "RangeFrom", Color.Black, Color.LawnGreen);
            }
            if (!string.IsNullOrEmpty(txtRangeTo.Text))
            {
                Regex reg1 = new Regex(txtRangeTo.Text, RegexOptions);
                matches.Clear();
                matches.AddRange(reg1.Matches(txtInput.Text).Cast<Match>());
                BindMatchTree(matches, hl, reg1, "RangeTo", Color.Black, Color.LawnGreen);
            }

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

            LoadRuleFromInput();

            txtReplaceResult.Text = ruleItem.replaceText(txtInput.Text);
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
                txtReplaceLog.AppendText(string.Format("{0}:{1}\n", node.Name, string.Join("\n", item.Results())));
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

        private void chkRange_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRange.Checked)
            {
                chkRange.Text = "Skip(Undo) Replace The Match In the Range.";
            }
            else
            {
                chkRange.Text = "Do Replace The Match In the Range.";
            }
        }

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
                lstFiles = lstFiles.FindAll(f => Regex.IsMatch(f, txtFileFilter.Text));
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
                txtFilePath.Text = UtilWxg.ReplaceMatch(txtFilePath.Text, @"\\[^\\]+\\?$", "");
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
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using TextConv;
using YamlDotNet.Serialization;
using System.IO;
using YamlDotNet.Serialization.NamingConventions;

namespace conver
{
    public partial class RegexTabPage : UserControl
    {
        /// <summary>
        /// Replace Pattern Format
        /// </summary>
        private const string replaceformat = @"";
        private ReplaceItem repItem = new ReplaceItem();
        private List<ReplaceItem> rules = new List<ReplaceItem>();

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
        }

        #region
        /// <summary>
        /// Do regex match, show the result on tree.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SaveRules(string filepath)
        {
            var serializer = new Serializer();
            using (StreamWriter writer = File.CreateText(filepath))
            {
                UI2Data();
                serializer.Serialize(writer, repItem);
            }

            string ruleFile= Config.GetAppSettingValue2("ruleCmdFileName", "rule_cmdstr.txt");
            string cmdFolder = Config.GetAppSettingValue("ruleCmdFolder");
            if (string.IsNullOrEmpty(cmdFolder))
            {
                cmdFolder = Application.StartupPath;
            }
            if (!Directory.Exists(cmdFolder))
            {
                Directory.CreateDirectory(cmdFolder);
            }
            if (!cmdFolder.EndsWith("\\"))
            {
                cmdFolder = cmdFolder + @"\";
            }
            repItem.AppendToCommandFile(cmdFolder + ruleFile);
        }
        
        /// <summary>
        /// Do regex match, show the result on tree.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void LoadRules(string filepath)
        {
            var deserializer = new Deserializer();

            try
            {
                using (StreamReader reader = File.OpenText(filepath))
                {
                    repItem = deserializer.Deserialize<ReplaceItem>(reader);
                    Data2UI(repItem);
                }

            }catch(Exception e)
            {
                MessageBox.Show(e.Message+ "\n"+ e.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        private void UI2Data()
        {
            repItem.Name = txtRuleName.Text;
            if (string.IsNullOrEmpty(repItem.Name))
            {
                repItem.Name = "cmdkey";
            }
            repItem.Desc = txtRuleDesc.Text;

            repItem.pattern = txtPattern.Text;
            repItem.replacement = txtReplacement.Text;
            repItem.inputContent = txtInput.Text;

            repItem.IgnoreCase = chkIgnoreCase.Checked;
            repItem.Multiline = chkMultiline.Checked;

            repItem.rangeFrom = txtRangeFrom.Text;
            repItem.rangeTo = txtRangeTo.Text;
            repItem.rangeSkip = chkRange.Checked;

            repItem.destFolder = txtCommand.Text;
            
        }
        private void Data2UI(ReplaceItem repItem)
        {
            txtRuleName.Text = repItem.Name;
            txtRuleDesc.Text = repItem.Desc;

            txtPattern.Text = repItem.pattern;
            txtReplacement.Text = repItem.replacement;
            txtInput.Text = repItem.inputContent;

            chkIgnoreCase.Checked = repItem.IgnoreCase;
            chkMultiline.Checked = repItem.Multiline;

            txtRangeFrom.Text = repItem.rangeFrom;
            txtRangeTo.Text = repItem.rangeTo;
            chkRange.Checked = repItem.rangeSkip;

            txtCommand.Text = repItem.destFolder;
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

            UI2Data();

            txtReplaceResult.Text = repItem.replaceText(txtInput.Text);
            HighLight hl = new HighLight(txtReplaceResult);
            hl.Reset2Default();            
            foreach (string result in repItem.repResults)
            {
                hl.Highlight(result);
            }
        }

        /// <summary>
        /// Show Replace files window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnReplaceFile_Click(object sender, EventArgs e)
        {
            MessageBox.Show("btnReplaceFile_Click");
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

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {   
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog(this) == DialogResult.OK)
            {
                txtCommand.Text = folderDialog.SelectedPath;
            }

        }

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
            }
        }
    }
}

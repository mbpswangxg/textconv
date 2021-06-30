using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Text.Common;
using System.Windows.Forms;

namespace TextJuggling
{
    public class ImEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public bool Selected { get; set; }
        public Range<int, int> Range = new Range<int, int>();

        public void Init(string content)
        {
            string footer = StringUtils.GetMatchValue(content, @"\([\w\.]+@[\w-_\.]+\)");
            this.Id = StringUtils.GetMatchGroup(footer, @"([\w\.]+)@([\w\.]+)", 1);
            this.Version = StringUtils.GetMatchGroup(footer, @"([\w\.]+)@([\w\.]+)", 2);
            this.Selected = Regex.IsMatch(content, @"\[o\].+");

            string header = content.Replace(footer, string.Empty);
            header = StringUtils.ReplaceMatch(header, @"[^\[]*\[[ox]\]", string.Empty);
            this.Name = header.Trim();
        }
    }
    public class ImApplication : ImEntity
    {
        public string Category { get; set; }
        public List<ImEntity> Children = new List<ImEntity>();
        
        public void InitChild(string content)
        {
            ImEntity child = new ImEntity();
            child.Init(content);
            this.Children.Add(child);
        }
    }
    public class ImModule : ImEntity
    {
        public bool isPark { get; set; }
        public ImApplication Application { get; set; }
        public List<ImEntity> Dependencies = new List<ImEntity>();
        public List<ImEntity> Children = new List<ImEntity>();
    }
    public class ImModuleOrder : ImEntity
    {
        public int Seq { get; set; }
    }
    public class Range<F, T>
    {
        public F Start { get; set; }
        public T End { get; set; }
        public Range()
        {
            //設定なし
        }
        public Range(F from, T to)
        {
            Start = from;
            End = to;
        }
    }
    
}

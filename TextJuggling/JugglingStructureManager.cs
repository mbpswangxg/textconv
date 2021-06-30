using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Text.Common;
namespace TextJuggling
{
    public class JugglingStructureManager
    {
        public List<ImApplication> Applications = new List<ImApplication>();
        public List<ImModule> Modules = new List<ImModule>();
        public List<ImModuleOrder> ModuleOrders = new List<ImModuleOrder>();
        private List<string> buffer = new List<string>();

        public void Parse(string structTextFile)
        {
            buffer.Clear();
            buffer.AddRange(File.ReadAllLines(structTextFile, Config.Encoding));
            
            ImApplication app = null;
            ImModule module = null;
            foreach (string line in buffer)
            {
                //選択されたモジュールのみ対象となります。
                //if (!Regex.IsMatch(line, @"\[o\].+")) continue;

                if (Regex.IsMatch(line, @"^\[o\].+"))
                {
                    app = new ImApplication();
                    app.Init(line);
                    Applications.Add(app);
                }
                else if (app != null)
                {
                    //app.InitChild(line);
                    module = new ImModule();
                    module.Init(line);
                    module.Application = app;
                    app.Children.Add(module);
                    Modules.Add(module);
                }
            }
        }
        public void FillTree<T>(TreeNodeCollection nodes, List<T> imEntities)
        {
            foreach (T app in imEntities)
            {
                ImApplication application = app as ImApplication;
                TreeNode node = new TreeNode(string.Format("{0}@{1}", application.Name, application.Version));
                nodes.Add(node);
                foreach(ImEntity child in application.Children)
                {
                    TreeNode nodeChild = new TreeNode(string.Format("{0}@{1}", child.Name, child.Version));
                    node.Nodes.Add(nodeChild);
                }
            }
        }
    }
}

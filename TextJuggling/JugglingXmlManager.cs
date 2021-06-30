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
    public class JugglingXmlManager
    {
        public List<ImApplication> Applications = new List<ImApplication>();
        public List<ImModule> Modules = new List<ImModule>();
        public List<ImModuleOrder> ModuleOrders = new List<ImModuleOrder>();
        private List<string> buffer = new List<string>();
        private Range<int, int> modulesRange = new Range<int, int>();
        private Range<int, int> appRange = new Range<int, int>();
        private Range<int, int> orderRange = new Range<int, int>();

        public void Parse(string xmlFile)
        {
            buffer.Clear();
            buffer.AddRange(File.ReadAllLines(xmlFile, Config.Encoding));

            Range<int, int> latest = new Range<int, int>();
            //structrue情報を取得
            string line = string.Empty;
            for (int i = 0; i < buffer.Count; i++)
            {
                line = buffer[i];
                if (Regex.IsMatch(line, @"\<[\w:]*structure"))
                {
                    latest.Start = i;
                }
                else if (latest.Start > 0 && Regex.IsMatch(line, @"\<\/[\w:]*structure\>"))
                {
                    latest.End = i;
                }
            }
            if (latest.Start == 0 || latest.Start > latest.End) return;

            
            for (int i = latest.Start; i <= latest.End; i++)
            {
                line = buffer[i];
                //各範囲情報の取得
                if (ParseSingle(line, i, "modules", modulesRange)) continue; 
                if (ParseSingle(line, i, "applications", appRange)) continue;
                if (ParseSingle(line, i, "module-order", orderRange)) continue;
            }
            ParseModulesRange(modulesRange, "module");
            ParseApplicationRange(appRange, "application");
            ParseOrderRange(orderRange, "module");
        }
        /// <summary>
        /// モジュール情報
        /// </summary>
        /// <param name="rangeEntity"></param>
        /// <param name="lines"></param>
        private void ParseModulesRange(Range<int, int> Range, string rangeName)
        {
            ImModule module = null;
            string line = string.Empty;
            for (int i = Range.Start + 1; i < Range.End; i++)
            {
                line = buffer[i];
                //各範囲情報の取得
                if (Regex.IsMatch(line, string.Format(@"\<[\w:]*{0}", rangeName)))
                {
                    module = new ImModule();
                    module.Range.Start = i;
                    module.Id = StringUtils.GetPropertyValue(line, "id");
                    module.Version = StringUtils.GetPropertyValue(line, "version");
                    module.Selected = bool.Parse(StringUtils.GetPropertyValue(line, "selected"));
                    module.isPark = Regex.IsMatch(line, string.Format(@"\<[\w:]*{0}-park", rangeName));
                    this.Modules.Add(module);
                    continue;
                }
                else if (module.Range.Start > 0 && Regex.IsMatch(line, string.Format(@"\<\/[\w:]*{0}\>", rangeName)))
                {
                    module.Range.End = i;
                    continue;
                }
                if (Regex.IsMatch(line, string.Format(@"\<[\w:]*{0}\>", "name")))
                {
                    module.Name = StringUtils.GetNodeText(line);
                    continue;
                }
                if (Regex.IsMatch(line, string.Format(@"\<[\w:]*{0}", "reference")))
                {
                    ImEntity child = new ImEntity();
                    child.Id = StringUtils.GetPropertyValue(line, "id");
                    child.Version = StringUtils.GetPropertyValue(line, "version");
                    module.Children.Add(child);
                    continue;
                }
                if (Regex.IsMatch(line, string.Format(@"\<[\w:]*{0}", "dependency")))
                {
                    ImEntity child = new ImEntity();
                    child.Id = StringUtils.GetPropertyValue(line, "id");
                    child.Version = StringUtils.GetPropertyValue(line, "version");
                    module.Dependencies.Add(child);
                    continue;
                }
            }
        }
        /// <summary>
        /// Application情報
        /// </summary>
        /// <param name="rangeEntity"></param>
        /// <param name="lines"></param>
        private void ParseApplicationRange(Range<int, int> Range, string rangeName) 
        {
            ImApplication app = null;
            string line = string.Empty;
            for (int i = Range.Start + 1; i < Range.End; i++)
            {
                line = buffer[i];
                //各範囲情報の取得                
                if (Regex.IsMatch(line, string.Format(@"\<[\w:]*{0}", rangeName)))
                {
                    app = new ImApplication();
                    app.Range.Start = i;
                    app.Id = StringUtils.GetPropertyValue(line, "id");
                    app.Version = StringUtils.GetPropertyValue(line, "version");
                    app.Selected = bool.Parse(StringUtils.GetPropertyValue(line, "selected"));

                    this.Applications.Add(app);
                    continue;
                }
                else if (app.Range.Start > 0 && Regex.IsMatch(line, string.Format(@"\<\/[\w:]*{0}\>", rangeName)))
                {
                    app.Range.End = i;
                    continue;
                }
                if (Regex.IsMatch(line, string.Format(@"\<[\w:]*{0}\>", "name")))
                {
                    app.Name = StringUtils.GetNodeText(line);
                    continue;
                }
            }
        }
        /// <summary>
        /// モジュール並び順情報
        /// </summary>
        /// <param name="orderRange"></param>
        /// <param name="lines"></param>
        private void ParseOrderRange(Range<int, int> Range, string rangeName)
        {
            ImModuleOrder order = null;
            string line = string.Empty;
            for (int i = Range.Start + 1; i < Range.End; i++)
            {
                line = buffer[i];
                if (Regex.IsMatch(line, string.Format(@"\<[\w:]*{0}", rangeName)))
                {
                    order = new ImModuleOrder();
                    order.Range.Start = i;
                    order.Id = StringUtils.GetPropertyValue(line, "id");
                    order.Version = StringUtils.GetPropertyValue(line, "version");

                    this.ModuleOrders.Add(order);
                    continue;
                }
            }
        }
        private bool ParseSingle(string line, int i, string pattern, Range<int, int> Range)
        {
            if (Range.End > 0) return false;
            
            if (Regex.IsMatch(line, string.Format(@"\<[\w:]*{0}", pattern)))
            {
                Range.Start = i;
                return true;
            }
            else if (Range.Start > 0 && Regex.IsMatch(line, string.Format(@"\<\/[\w:]*{0}\>", pattern)))
            {
                Range.End = i;
                return true;
            }
            return false;
        }

        public List<string> ExportBy(JugglingStructureManager structureManager)
        {
            List<ImModule> selectedModules = this.Modules.FindAll(
                m => structureManager.Modules.Exists(sm => sm.Id.Equals(m.Id) || sm.Name.Equals(m.Name))).ToList();

            List<ImApplication> selectedApplications = this.Applications.FindAll(
                a => structureManager.Applications.Exists(sa => sa.Selected && (sa.Id.Equals(a.Id) || sa.Name.Equals(a.Name)))).ToList();

            List<ImModuleOrder> ords = this.ModuleOrders.FindAll(
                o => selectedModules.Exists(od => od.Id.Equals(o.Id) && od.Version.Equals(o.Version)));

            List<string> exportLines = new List<string>();
            for (int i= 0; i < buffer.Count; i++)
            {
                //modules
                if (modulesRange.Start < i && i < modulesRange.End)
                {
                    if (selectedModules.Exists(m => m.Range.Start <= i && i <= m.Range.End))
                    {
                        exportLines.Add(buffer[i]);
                    }
                    continue;
                }
                //applications
                if (appRange.Start < i && i < appRange.End)
                {
                    if (selectedApplications.Exists(m => m.Range.Start <= i && i <= m.Range.End))
                    {
                        exportLines.Add(buffer[i]);
                    }
                    continue;
                }
                
                ////modules
                //if (orderRange.Start < i && i < orderRange.End)
                //{
                //    if (ords.Exists(m => m.Range.Start <= i && i <= m.Range.End))
                //    {
                //        exportLines.Add(buffer[i]);
                //    }
                //    continue;
                //}

                exportLines.Add(buffer[i]);
            }
           
            return exportLines;
        }
    }
}

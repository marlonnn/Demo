using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo.International
{
    public class BaseFunction
    {
        /// <summary>
        /// if this is Chinese edition which need register process
        /// </summary>
        public static bool ChineseEdition
        {
            get { return s_chineseEdition ?? (bool)(s_chineseEdition = File.Exists(Application.StartupPath + "\\zh-CN\\Demo.InternationalForm.resources.dll")); }
        }

        private static bool? s_chineseEdition;

        public static void RefreshUICulture(System.ComponentModel.ComponentResourceManager resources, Control ctr)
        {
            foreach (Control c in ctr.Controls)
            {
                resources.ApplyResources(c, c.Name);
                RefreshUICulture(resources, c);

                if (c is RibbonBar)
                {
                    RefreshBaseItemsUICulture(resources, (c as RibbonBar).Items);
                }
                else if (c is RibbonControl)
                {
                    RefreshBaseItemsUICulture(resources, (c as RibbonControl).Items);
                }
                else if (c is Bar)
                {
                    RefreshBaseItemsUICulture(resources, (c as Bar).Items);
                }
                else if (c is ButtonX)
                {
                    RefreshBaseItemsUICulture(resources, (c as ButtonX).SubItems);
                }
                else if (c is ToolStrip)
                {
                    RefreshToolStripUICulture(resources, (c as ToolStrip).Items);
                }
            }
        }

        public static void RefreshBaseItemsUICulture(System.ComponentModel.ComponentResourceManager resources, SubItemsCollection items)
        {
            foreach (BaseItem item in items)
            {
                resources.ApplyResources(item, item.Name);
                RefreshBaseItemsUICulture(resources, item.SubItems);
            }
        }

        public static void RefreshToolStripUICulture(System.ComponentModel.ComponentResourceManager resources, ToolStripItemCollection items)
        {
            foreach (ToolStripItem item in items)
            {
                resources.ApplyResources(item, item.Name);
                if (item is ToolStripDropDownItem)
                    RefreshToolStripUICulture(resources, (item as ToolStripDropDownItem).DropDownItems);
            }
        }
    }
}

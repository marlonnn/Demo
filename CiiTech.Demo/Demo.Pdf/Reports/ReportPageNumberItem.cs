using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Pdf.Reports
{
    [Serializable]
    public class ReportPageNumberItem : ReportItemBase
    {
        private int itemStyle;
        /// <summary>
        /// 1:1,2,3...;2: 1/3,2/3...
        /// </summary>
        public int ItemStyle
        {
            get { return itemStyle; }
            set { itemStyle = value; }
        }

    }
}

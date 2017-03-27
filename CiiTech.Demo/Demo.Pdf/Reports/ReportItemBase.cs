using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Pdf.Reports
{
    [Serializable]
    public class ReportItemBase : IComparable<ReportItemBase>
    {
        private Rectangle _bounds;

        /// <summary>
        /// gets or sets report ctrl bounds on the report page when scale factor is 1
        /// </summary>
        public Rectangle Bounds
        {
            get { return _bounds; }
            set { _bounds = value; }
        }

        [NonSerialized]
        private bool _selected;  // by default, new item is selected

        /// <summary>
        /// gets or sets if report item is selected, this value is not serialized
        /// </summary>
        public bool Selected
        {
            get { return _selected; }
            set { _selected = value; }
        }

        private int _level;

        /// <summary>
        /// gets or sets ctrl order in report page
        /// </summary>
        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }

        public int CompareTo(ReportItemBase other)
        {
            return Level.CompareTo(other.Level);
        }

        public virtual void UpdateContent()
        {

        }
        /// <summary>
        /// check this item is right
        /// </summary>
        /// <param name="o">sample or specimen</param>
        /// <returns></returns>
        public virtual bool CheckIsRight(object o)
        {
            return true;
        }
    }
}

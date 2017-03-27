using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Pdf.Reports
{
    [Serializable]
    public class ReportPictureItem : ReportItemBase
    {
        private Image _picture;

        public Image Picture
        {
            get { return _picture; }
            set { _picture = value; }
        }
        // [NonSerialized]
        private Point _OldImgLocation;
        public Point OldImgLocation
        {
            get { return _OldImgLocation; }
            set { _OldImgLocation = value; }
        }
        //  [NonSerialized]
        private Size _OldImgSize;//=new Size(100,100);
        public Size OldImgSize
        {
            get { return _OldImgSize; }
            set { _OldImgSize = value; }
        }
        private Size _NewImgSize;
        public Size NewImgSize
        {
            get { return _NewImgSize; }
            set { _NewImgSize = value; }
        }
    }
}

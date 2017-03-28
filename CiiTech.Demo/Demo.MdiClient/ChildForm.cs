using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo.Mdi
{
    public partial class ChildForm : DevComponents.DotNetBar.Office2007Form
    {
        public ChildForm()
        {
            InitializeComponent();
        }

        public ChildForm(Size size, int i)
        {
            InitializeComponent();

            this.Size = size;
            this.Text = i.ToString();
            this.MouseDown += ChildForm_MouseDown;

        }

        private void ChildForm_MouseDown(object sender, MouseEventArgs e)
        {
        }

        internal class NonClientInfo
        {
            public int CaptionTotalHeight = 26;   // force to 26 to fix in .Net 4.5 problem
            public int BottomBorder = 1;
            public int LeftBorder = 1;
            public int RightBorder = 1;

            public int TotalWidth
            {
                get { return LeftBorder + RightBorder; }
            }

            public int TotalHeight
            {
                get { return CaptionTotalHeight + BottomBorder; }
            }
        }

        /// <summary>
        /// 可以自定义修改Non Client Area背景
        /// </summary>
        /// <param name="g"></param>
        /// <param name="r"></param>
        protected override void PaintNonClientBackgroundImage(Graphics g, Rectangle r)
        {
            //base.PaintNonClientBackgroundImage(g, r);

            //if (MdiParent != null && MdiParent.ActiveMdiChild == this)
            //{
            //    Point pnt = new Point(0, 0);
            //    Point corner = this.PointToScreen(pnt);
            //    Point origin = this.Parent.PointToScreen(pnt);
            //    int titleBarHeight = corner.Y - origin.Y - this.Location.Y;

            //    Rectangle rect = new Rectangle(1, 1, Width - 2, titleBarHeight - 2);
            //    if (rect.Width > 0 && rect.Height > 0)
            //    {
            //        LinearGradientBrush brush = new LinearGradientBrush(rect, Color.FromArgb(174, 200, 225),
            //            Color.FromArgb(96, 143, 189), LinearGradientMode.Horizontal);
            //        g.FillRectangle(brush, rect);
            //        brush.Dispose();
            //    }
            //}
        }
    }
}

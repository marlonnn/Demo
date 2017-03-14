using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Demo.SplashScreen
{
   
    public partial class SplashScreenForm : Form
    {
        private static SplashScreenForm _instance;

        /// <summary>
        /// show the splash screen form
        /// </summary>
        public static void ShowSplash()
        {
            if (_instance == null || _instance.IsDisposed)
            {
                _instance = new SplashScreenForm();
            }
            _instance.Show();
            _instance.Refresh();
            System.Threading.Thread.Sleep(2000);
        }

        /// <summary>
        /// hide but not close splash, to fix auto close login form problem
        /// </summary>
        public static void HideSplash()
        {
            if (_instance != null && !_instance.IsDisposed) _instance.Visible = false;
        }

        /// <summary>
        /// close the splash screen form
        /// </summary>
        public static void CloseSplash()
        {
            if (_instance != null)
            {
                _instance.Dispose();
                _instance = null;
            }
        }

        private SplashScreenForm()
        {
            InitializeComponent();
        }

        private void labelDisplay_Paint(object sender, PaintEventArgs e)
        {
            DrawRoundRect(e.Graphics, this.label1);
        }

        private void DrawRoundRect(Graphics graphics, Label label)
        {
            int X = Convert.ToInt32(label.Width.ToString()) - 1;
            int Y = Convert.ToInt32(label.Height.ToString()) - 1;
            Point[] points = 
			{
                new Point(0,0),
                new Point(0,1),
                new Point(1,0),
				new Point(X-2,0),
				new Point(X-1,0),
				new Point(X-1,1),
				new Point(X,0),
				new Point(X,1),
				new Point(X,2),
				new Point(X,3),
				new Point(X,4),
				new Point(X,Y-3),
				new Point(X,Y-2),
				new Point(X,Y-1),
				new Point(X,Y),
				new Point(X-1,Y-1),
				new Point(X-1,Y),
				new Point(X-2,Y),
				new Point(1,Y),
				new Point(0,Y),
				new Point(0,Y-1),
			};
            GraphicsPath path = new GraphicsPath();
            path.AddLines(points);

            Pen pen = new Pen(Color.White, 1);
            pen.DashStyle = DashStyle.Solid;
            graphics.DrawPath(pen, path);
        }
    }
}

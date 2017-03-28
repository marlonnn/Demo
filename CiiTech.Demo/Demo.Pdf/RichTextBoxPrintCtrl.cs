using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo.Pdf
{
    public class RichTextBoxPrintCtrl : RichTextBox
    {
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        private struct CHARRANGE
        {
            public int cpMin;
            public int cpMax;
        }

        private struct FORMATRANGE
        {
            public IntPtr hdc;
            public IntPtr hdcTarget;
            public RichTextBoxPrintCtrl.RECT rc;
            public RichTextBoxPrintCtrl.RECT rcPage;
            public RichTextBoxPrintCtrl.CHARRANGE chrg;
        }
        private const double AnInch = 14.4;
        private const int WM_USER = 1024;
        private const int EM_FORMATRANGE = 1081;
        [DebuggerNonUserCode]
        public RichTextBoxPrintCtrl()
        {
        }

        [DllImport("USER32", CharSet = CharSet.Ansi, EntryPoint = "SendMessageA", ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
        public int Print(int charFrom, int charTo, PrintPageEventArgs e)
        {
            checked
            {
                RichTextBoxPrintCtrl.CHARRANGE chrg;
                chrg.cpMin = charFrom;
                chrg.cpMax = charTo;
                Rectangle rectangle = e.MarginBounds;
                RichTextBoxPrintCtrl.RECT rc;
                rc.Top = (int)Math.Round(unchecked((double)rectangle.Top * 14.4));
                rectangle = e.MarginBounds;
                rc.Bottom = (int)Math.Round(unchecked((double)rectangle.Bottom * 14.4));
                rectangle = e.MarginBounds;
                rc.Left = (int)Math.Round(unchecked((double)rectangle.Left * 14.4));
                rectangle = e.MarginBounds;
                rc.Right = (int)Math.Round(unchecked((double)rectangle.Right * 14.4));
                rectangle = e.PageBounds;
                RichTextBoxPrintCtrl.RECT rcPage;
                rcPage.Top = (int)Math.Round(unchecked((double)rectangle.Top * 14.4));
                rectangle = e.PageBounds;
                rcPage.Bottom = (int)Math.Round(unchecked((double)rectangle.Bottom * 14.4));
                rectangle = e.PageBounds;
                rcPage.Left = (int)Math.Round(unchecked((double)rectangle.Left * 14.4));
                rectangle = e.PageBounds;
                rcPage.Right = (int)Math.Round(unchecked((double)rectangle.Right * 14.4));
                IntPtr hdc = e.Graphics.GetHdc();
                RichTextBoxPrintCtrl.FORMATRANGE fORMATRANGE;
                fORMATRANGE.chrg = chrg;
                fORMATRANGE.hdc = hdc;
                fORMATRANGE.hdcTarget = hdc;
                fORMATRANGE.rc = rc;
                fORMATRANGE.rcPage = rcPage;
                IntPtr intPtr = IntPtr.Zero;
                IntPtr zero = IntPtr.Zero;
                zero = new IntPtr(1);
                IntPtr intPtr2 = IntPtr.Zero;
                intPtr2 = Marshal.AllocCoTaskMem(Marshal.SizeOf(fORMATRANGE));
                Marshal.StructureToPtr(fORMATRANGE, intPtr2, false);
                intPtr = RichTextBoxPrintCtrl.SendMessage(this.Handle, 1081, zero, intPtr2);
                Marshal.FreeCoTaskMem(intPtr2);
                e.Graphics.ReleaseHdc(hdc);
                return intPtr.ToInt32();
            }
        }

        public void Draw(Graphics g, Rectangle bounds)
        {
            if (this.IsDisposed) return;

            RichTextBox textBox;
            textBox = this; // TextBox;           
            BaseDraw(g, bounds, textBox);
        }

        private void BaseDraw(Graphics g, Rectangle bounds, RichTextBox textBox)
        {
            bounds.X += textBox.Left; // +1;
            bounds.Y += textBox.Top;
            // bounds.Size = textBox.Size;
            bounds.Width = textBox.Width - 2;
            bounds.Height = textBox.Height - 2;

            // remove background color
            string rtf = textBox.Rtf;
            textBox.SelectAll();
            textBox.SelectionProtected = false;
            textBox.SelectionBackColor = Color.White;

            textBox.Print(g, bounds);

            // restore         
            textBox.Rtf = rtf;
        }

    }
}

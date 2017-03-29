using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Collections;

namespace Demo.Pdf
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class ExtensionMethods
    {

        #region Rich Text Box Print

        //Convert the unit used by the .NET framework (1/100 inch) 
        //and the unit used by Win32 API calls (twips 1/1440 inch)
        private const double anInch = 14.4;

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct CHARRANGE
        {
            public int cpMin;         //First character of range (0 for start of doc)
            public int cpMax;           //Last character of range (-1 for end of doc)
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct FORMATRANGE
        {
            public IntPtr hdc;             //Actual DC to draw on
            public IntPtr hdcTarget;       //Target DC for determining text formatting
            public RECT rc;                //Region of the DC to draw to (in twips)
            public RECT rcPage;            //Region of the whole DC (page size) (in twips)
            public CHARRANGE chrg;         //Range of text to draw (see earlier declaration)
        }

        private const int WM_USER = 0x0400;
        private const int EM_FORMATRANGE = WM_USER + 57;

        [DllImport("USER32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        // Render the contents of the RichTextBox for printing
        //	Return the last character printed + 1 (printing start from this point for next page)
        public static int Print(this System.Windows.Forms.RichTextBox rtb, Graphics g, Rectangle bounds)
        {
            int charFrom = 0;
            int charTo = rtb.TextLength;
            //Calculate the area to render and print
            RECT rectToPrint;
            
            rectToPrint.Top = (int)Math.Round((bounds.Top + g.Transform.OffsetY) * anInch);
            rectToPrint.Bottom = (int)Math.Round((bounds.Bottom + g.Transform.OffsetY) * anInch);
            rectToPrint.Left = (int)Math.Round((bounds.Left + g.Transform.OffsetX) * anInch);
            rectToPrint.Right = (int)Math.Round((bounds.Right + g.Transform.OffsetX) * anInch);

            //Calculate the size of the page
            RECT rectPage;
            rectPage.Top = 0;
            rectPage.Bottom = int.MaxValue;
            rectPage.Left = 0;
            rectPage.Right = int.MaxValue;

            IntPtr hdc = g.GetHdc();

            FORMATRANGE fmtRange;
            fmtRange.chrg.cpMax = charTo;				//Indicate character from to character to 
            fmtRange.chrg.cpMin = charFrom;
            fmtRange.hdc = hdc;                    //Use the same DC for measuring and rendering
            fmtRange.hdcTarget = hdc;              //Point at printer hDC
            fmtRange.rc = rectToPrint;             //Indicate the area on page to print
            fmtRange.rcPage = rectPage;            //Indicate size of page

            IntPtr res = IntPtr.Zero;

            IntPtr wparam = IntPtr.Zero;
            wparam = new IntPtr(1);

            //Get the pointer to the FORMATRANGE structure in memory
            IntPtr lparam = IntPtr.Zero;
            lparam = Marshal.AllocCoTaskMem(Marshal.SizeOf(fmtRange));
            Marshal.StructureToPtr(fmtRange, lparam, false);

            //Send the rendered data for printing 
            res = SendMessage(rtb.Handle, EM_FORMATRANGE, wparam, lparam);

            //Free the block of memory allocated
            Marshal.FreeCoTaskMem(lparam);

            //Release the device context handle obtained by a previous call
            g.ReleaseHdc(hdc);

            //Return last + 1 character printer
            return res.ToInt32();
        }

        #endregion
    }

    /// <summary>
    /// Provides a resource type to use for localized enumeration.
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum, AllowMultiple = false)]
    public sealed class LocalizedResourceAttribute : Attribute
    {
        #region Class members

        /// <summary>
        /// The resource type.
        /// </summary>
        private Type m_type;

        #endregion

        #region Class constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedResourceAttribute"/> class.
        /// </summary>
        /// <param name="type">The description to store in this attribute.
        /// </param>
        public LocalizedResourceAttribute(Type type)
            : base()
        {
            this.m_type = type;
        }

        #endregion

        #region Class properties

        /// <summary>
        /// Gets the resource type stored in this attribute.
        /// </summary>
        /// <value>The resource type stored in the attribute.</value>
        public Type Type
        {
            get
            {
                return this.m_type;
            }
        }

        #endregion
    }

    /// <summary>
    /// Provides enum's value localized resource description key.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class LocalizedDescriptionAttribute : Attribute
    {
        #region Class members

        /// <summary>
        /// The localized description key.
        /// </summary>
        private String m_description;

        #endregion

        #region Class constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedDescriptionAttribute"/> class.
        /// </summary>
        /// <param name="description">The localized description key to store in this attribute.
        /// </param>
        public LocalizedDescriptionAttribute(String description)
            : base()
        {
            this.m_description = description;
        }

        #endregion

        #region Class properties

        /// <summary>
        /// Gets the localized desciption key stored in this attribute.
        /// </summary>
        /// <value>The localized description key stored in the attribute.</value>
        public String Description
        {
            get
            {
                return this.m_description;
            }
        }

        #endregion
    }
}

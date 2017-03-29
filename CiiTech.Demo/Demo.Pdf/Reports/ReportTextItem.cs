using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo.Pdf.Reports
{
    [Serializable]
    public class ReportTextItem : ReportItemBase
    {
        [NonSerialized]
        private RichTextBoxEx _rtb;

        public RichTextBoxEx TextBox
        {
            get { return RTB; }
        }

        private RichTextBoxEx RTB
        {
            get
            {
                if (_rtb == null)
                {
                    Constructor();
                }
                return _rtb;
            }
        }

        private void Constructor()
        {
            _rtb = new RichTextBoxEx();
            _rtb.Font = new Font("Microsoft Sans Serif", 9);
            _rtb.Multiline = true;
            _rtb.AcceptsTab = true;
            _rtb.Rtf = _rtf;
            _rtb.Disposed += new EventHandler(_rtb_Disposed);
        }

        ~ReportTextItem()
        {
            if (_rtb != null)
            {
                _rtb.Dispose();
            }
        }

        public void SetRTBFormat(Font font, string text)
        {
            RTB.Font = font;
            RTB.Text = text;
        }

        private void _rtb_Disposed(object sender, EventArgs e)
        {
            UpdateContent();
            _rtb = null;
        }

        public ReportTextItem()
        {
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext sc)
        {
        }

        [OnSerializing]
        private void OnSerializing(StreamingContext sc)
        {
            UpdateContent();
        }

        /// <summary>
        /// using a rich text box to maintain RTF text
        /// </summary>
        private string _rtf;

        public string Text
        {
            get { return RTB.Text; }
            set { RTB.Text = value; }
        }

        private void SetNewValueFont(RichTextBoxEx RTB, int pos, int length)
        {
            if (RTB == null || pos < 0) return;

            RTB.Select(pos, 1);

            Font font = RTB.SelectionFont;

            if (font == null) return;

            string fontName = font.Name;
            float fontSize = font.Size;

            if (fontName == "宋体" || fontName == "SimSun") fontName = "Microsoft Sans Serif";

            RTB.Select(pos, length);

            try { RTB.SelectionFont = new Font(fontName, fontSize); }
            catch { }
        }

        private Font GetFont(RichTextBoxEx RTB, int pos)
        {
            if (RTB == null || pos < 0) return null;
            RTB.Select(pos, 1);
            return RTB.SelectionFont;
        }

        public override void UpdateContent()
        {
            if (_rtb != null)
                _rtf = _rtb.Rtf;
        }

        public void ResetStringLenght()
        {
            return;     // Logan, do not reset string length
            Graphics g = _rtb.CreateGraphics();

            int len = 0;
            for (int i = 0; i < Text.Length; i++)
            {
                _rtb.Select(i, 1);
                if (_rtb.SelectionFont != null)
                {
                    len += (int)(g.MeasureString(_rtb.SelectedText, _rtb.SelectionFont).Width);
                }
            }
            //int len =(int)( g.MeasureString(Text, _rtb.Font).Width);
            //len+=80;
            Rectangle rect = new Rectangle(Bounds.Location, new Size(len, Bounds.Height));
            Bounds = rect;
            g.Dispose();
        }

        public struct TextSelection
        {
            public int SelectionStart
            {
                get;
                set;
            }

            public int SelectionLength
            {
                get;
                set;
            }

            public int SelectionEnd
            {
                get { return SelectionStart + SelectionLength; }
            }

            public TextSelection(int start, int length) : this()
            {
                this.SelectionStart = start;
                this.SelectionLength = length;
            }
        }
    }
}

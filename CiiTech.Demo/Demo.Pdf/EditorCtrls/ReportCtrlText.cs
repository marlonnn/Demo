using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Demo.Pdf.Reports;

namespace Demo.Pdf.EditorCtrls
{
    public interface IReportCtrlText
    {
        RichTextBoxEx TextBox { get; }

        Command CommandUndo { get; }
        Command CommandCut { get; }
        Command CommandCopy { get; }
        Command CommandPaste { get; }
        Command CommandDelete { get; }
        Command CommandStatus { get; }
        Command CommandFontBold { get; }
        Command CommandFontItalic { get; }
        Command CommandFontUnderline { get; }
        Command CommandFontStrike { get; }
        Command CommandAlignLeft { get; }
        Command CommandAlignCenter { get; }
        Command CommandAlignRight { get; }
        Command CommandTextColor { get; }
        Command CommandFind { get; }
        Command CommandFont { get; }
        Command CommandFontSize { get; }
        Command CommandZoom { get; }
        Command CommandIndent { get; }
        Command CommandOutdent { get; }
        Command CommandBullets { get; }
    }
    public partial class ReportCtrlText : ReportCtrl
    {
        public ReportCtrlText()
        {
            InitializeComponent();
        }

        public ReportCtrlText(ReportTextItem reportItem)
            : base(reportItem)
        {
            this.components = new System.ComponentModel.Container();
            Disposed += new EventHandler(ReportCtrlText_Disposed);
        }

        private void ReportCtrlText_Disposed(object sender, EventArgs e)
        {

        }
    }
}

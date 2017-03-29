using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Demo.Pdf.Reports;

namespace Demo.Pdf.EditorCtrls
{
    public partial class ReportPageUI : UserControl
    {
        protected const int BORDER_SIZE = 6;
        private ReportPage _reportPage;
        private Color _borderColor = Color.Tomato;

        private Point _pointDown = new Point();
        private Point _pointOld = new Point();

        bool _isSelected;
        public bool IsSelected
        {
            set
            {
                _isSelected = value;
                if (_isSelected)
                {
                    _borderColor = Color.Tomato;
                }
                else
                {
                    _borderColor = Color.Black;
                }
                this.Invalidate();
            }
            get
            {
                return _isSelected;
            }
        }

        public ReportPage ReportPage
        {
            get
            {
                return _reportPage;
            }
        }

        public ReportPageUI(ReportPage reportPage)
        {
            InitializeComponent();
            DoubleBuffered = true;

            _reportPage = reportPage;
            _reportPage.ReportItemArrayChanged += _reportPage_ReportItemArrayChanged;

            LoadReportCtrls();
        }

        /// <summary>
        /// handle report item array changed event to add or remove report control 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _reportPage_ReportItemArrayChanged(object sender, ArrayChangedEventArgs<ReportItemBase> e)
        {
            if (e.ChangeType == ArrayChangedType.ItemAdded)
                LoadReportControl(e.Item);
            else if (e.ChangeType == ArrayChangedType.ItemRemoved)
                RemoveReportControl(e.Item);
        }

        private void RemoveReportControl(ReportItemBase item)
        {
            // find report ctrl
            var reportCtrl = GetReportCtrl(item);
            if (reportCtrl == null) return;

            // remove report ctrl from controls array and dispose it
            reportCtrl.Dispose();
        }

        /// <summary>
        /// find report ctrl of specified report item
        /// </summary>
        /// <returns></returns>
        public ReportCtrl GetReportCtrl(ReportItemBase item)
        {
            return Controls.OfType<ReportCtrl>().FirstOrDefault(c => c.ReportItem == item);
        }

        private void LoadReportCtrls()
        {
            foreach (ReportItemBase reportItemBase in _reportPage.ReportItems)
            {
                LoadReportControl(reportItemBase);
            }
        }

        private void LoadReportControl(ReportItemBase reportItemBase)
        {
            ReportCtrl reportCtrl = null;
            if (reportItemBase is ReportTextItem)
            {
                reportCtrl = new ReportCtrlText((ReportTextItem)reportItemBase);
            }
            //if (reportItemBase is ReportPlotItem)
            //{
            //    reportCtrl = new ReportCtrlPlot((ReportPlotItem)reportItemBase);
            //}
            //else if (reportItemBase is ReportStatItem)
            //{
            //    reportCtrl = new ReportCtrlStat((ReportStatItem)reportItemBase);
            //}
            //else if (reportItemBase is ReportTextItem)
            //{
            //    reportCtrl = new ReportCtrlText((ReportTextItem)reportItemBase);
            //}
            //else if (reportItemBase is ReportSampleStatItem)
            //{
            //    reportCtrl = new ReportCtrlSampleStat((ReportSampleStatItem)reportItemBase);
            //}
            //else if (reportItemBase is ReportCompItem)
            //{
            //    reportCtrl = new ReportCtrlComp((ReportCompItem)reportItemBase);
            //}
            //else if (reportItemBase is ReportPMTItem)
            //{
            //    reportCtrl = new ReportCtrlPMT((ReportPMTItem)reportItemBase);
            //}
            //else if (reportItemBase is ReportPictureItem)
            //{
            //    reportCtrl = new ReportCtrlPicture((ReportPictureItem)reportItemBase);
            //}
            //else if (reportItemBase is ReportShapeItem)
            //{
            //    reportCtrl = new ReportCtrlShape((ReportShapeItem)reportItemBase);
            //}
            //else if (reportItemBase is ReportTableItem)
            //{
            //    reportCtrl = new ReportCtrlTable((ReportTableItem)reportItemBase);
            //}

            // add report ctrl to controls array
            this.Controls.Add(reportCtrl);
            try
            {
                this.Controls.SetChildIndex(reportCtrl, reportItemBase.Level);
            }
            catch { }

            // set control bounds
            //if (ParentForm is ReportForm) reportCtrl.SetBounds(((ReportForm)ParentForm).Report.Factor / 100.0);
            // set the initial selected state
            reportCtrl.IsSelected = reportItemBase.Selected;
        }
    }
}

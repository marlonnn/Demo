using Demo.Pdf.EditorCtrls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Pdf.Reports
{
    [Serializable]
    public class Report
    {
        private List<ReportPage> _reportPages;
        public List<ReportPage> ReportPages
        {
            get { return _reportPages; }
            private set { _reportPages = value; }
        }

        private string _Name = "Report";
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        private float _factor = 100;
        public float Factor
        {
            get
            {
                return _factor;
            }
            set
            {
                _factor = value;
            }
        }

        private bool _landscape;
        public bool Landscape
        {
            get
            {
                return _landscape;
            }
            set
            {
                _landscape = value;
            }
        }

        public bool IsEmpty
        {
            get { return _reportPages.All(page => page.ReportItems.Count <= 0); }
        }

        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public static Size DefaultReportPlotSize = new Size(181, 181);

        private const int TOP_LINE_POS = 126;

        [NonSerialized]
        private const int MarginTop = 50;
        [NonSerialized]
        private const int MarginBottom = 100;
        [NonSerialized]
        private const int MarginLeft = 97;
        [NonSerialized]
        private const int MarginRight = 86;
        [NonSerialized]
        private const int ReportItemYDistance = 36;
        [NonSerialized]
        private const int DeltaX1 = 2;
        [NonSerialized]
        private const int DeltaX2 = 10;
        [NonSerialized]
        private int _pageWidth;
        [NonSerialized]
        private int _pageHeight;
        [NonSerialized]
        private int _specimenPlotIndex;
        [NonSerialized]
        private bool _onlyShowSpecimenPlots;

        public Report()
        {
            _reportPages = new List<ReportPage>();
            _id = 0;// Program.ExpDoc.Reports.NextID();

            _pageWidth = Landscape ? ReportForm.PAGE_HEIGHT : ReportForm.PAGE_WIDTH;
            _pageHeight = Landscape ? ReportForm.PAGE_WIDTH : ReportForm.PAGE_HEIGHT;
        }
    }
}

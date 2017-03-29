using Demo.Pdf.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo.Pdf.EditorCtrls
{
    public class ReportOperateManage
    {
        Report _report;
        List<ReportPageUI> _pages;
        public List<ReportPageUI> PageUIs
        {
            get { return _pages; }
        }

        public ReportOperateManage(Report report, List<ReportPageUI> pages)
        {
            _report = report;
            _pages = pages;
        }
        public event EventHandler<PageChangeEventArgs> PageChange;
        private void OnPageChange(PageChangeEventArgs pea)
        {
            EventHandler<PageChangeEventArgs> pc = PageChange;
            if (pc != null)
            {
                pc(this, pea);
            }
        }

        /// <summary>
        /// get all report control
        /// </summary>
        /// <param name="reportPageUI">reportPageUI为空时，代表返回所有的control，，否则为当前页的control</param>
        /// <returns></returns>
        public List<ReportCtrl> GetAllSeletedReportControls(ReportPageUI reportPageUI)
        {
            List<ReportCtrl> list = (from a in GetAllReportControls(reportPageUI) where a.IsSelected select a).ToList();
            return list;
        }

        /// <summary>
        /// if null express no ctr 
        /// </summary>
        /// <returns></returns>
        public ReportCtrl GetControlCtrl()
        {
            foreach (ReportCtrl rc in GetAllReportControls(null))
            {
                if (rc.IsControlCtr)
                    return rc;
            }
            return null;
        }

        /// <summary>
        /// get all report control
        /// </summary>
        /// <param name="reportPageUI">reportPageUI为空时，代表返回所有的control，，否则为当前页的control</param>
        /// <returns></returns>
        public List<ReportCtrl> GetAllReportControls(ReportPageUI reportPageUI)
        {
            List<ReportCtrl> list = new List<ReportCtrl>();

            if (reportPageUI == null) // if reportPageUI is null ,return all reportcontrols
            {
                foreach (ReportPageUI rpu in _pages)
                {
                    foreach (Control con in rpu.Controls)
                    {
                        ReportCtrl rptc = con as ReportCtrl;
                        if (rptc != null)
                        {
                            list.Add(rptc);
                        }
                    }
                }
            }
            else  // return reportcontrols in this reportpageui
            {
                foreach (Control con in reportPageUI.Controls)
                {
                    ReportCtrl rptc = con as ReportCtrl;
                    if (rptc != null)
                    {
                        list.Add(rptc);
                    }
                }
            }
            return list;
        }

        // to get the index of current report page, if no this page return -1;
        public int GetCurrentReportPageIndex()
        {
            for (int i = 0; i < _pages.Count; i++)
            {
                if (_pages[i].IsSelected)
                    return i;
            }
            return -1;
        }

        public void SetSeletectedPage(int pageIndex)
        {
            foreach (ReportPageUI rpu in _pages)
            {
                rpu.IsSelected = false;
            }
            if (pageIndex >= 0 && pageIndex < _pages.Count)
            {
                _pages[pageIndex].IsSelected = true;
                //触发page change 事件
                PageChangeEventArgs pageChangeEvent = new PageChangeEventArgs(pageIndex + 1, _pages.Count);
                OnPageChange(pageChangeEvent);
            }
            else
            {
                SetSeletectedPage();
            }
        }

        //make a page is current page automaticly
        public void SetSeletectedPage()
        {
            foreach (ReportPageUI rpu in _pages)
            {
                rpu.IsSelected = false;
            }
            for (int i = _pages.Count - 1; i >= 0; i--)
            {
                if (_pages[i].Top < 585)
                {
                    _pages[i].IsSelected = true;
                    //触发page change 事件
                    PageChangeEventArgs pageChangeEvent = new PageChangeEventArgs(i + 1, _pages.Count);
                    OnPageChange(pageChangeEvent);
                    break;
                }
            }
        }

    }
    public class PageChangeEventArgs : EventArgs
    {
        public int CurrPage
        {
            private set;
            get;
        }
        public int TotalPage
        {
            private set;
            get;
        }
        public PageChangeEventArgs(int currPage, int totalPage)
        {
            CurrPage = currPage;
            TotalPage = totalPage;
        }
    }
}

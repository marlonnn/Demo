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
    public partial class ReportCtrl : UserControl
    {
        private enum SelectionMode
        {
            None,
            NetSelection,   // group selection is active
            Move,           // object(s) are moves
            Size            // object is resized
        }

        #region private

        private const int MIN_SIZE = 10; //对控件缩放的最小值 
        public const int BORDER_SIZE = 6; //调整大小触模柄方框大小，也等于边框的大小  
        protected Color _borderColor = Color.Black;
        private Color _HandlerColor = Color.Black;
        protected Control _subctrl; //report control's child control
        private SelectionMode selectMode = SelectionMode.None;
        private Point lastPoint = new Point(0, 0);
        private Point startPoint = new Point(0, 0);
        private int _currentHandleNumber = 1;
        protected Rectangle _MouseMoveRectangle;
        bool _isMouseMove;

        protected Rectangle _OldBounds;
        #endregion
        public bool IsPrint { set; get; }

        #region properties 

        /// <summary>
        /// gets or sets if control is selected, will invalidate control if value changed
        /// </summary>
        public bool IsSelected
        {
            set
            {
                ReportItem.Selected = value;

                if (this.ParentForm == null) return;

                if (!IsSelected && IsActive)
                {
                    IsActive = false;
                }
                if (IsSelected && ((ReportForm)this.ParentForm).ReportOperateManage.GetControlCtrl() == null)
                {
                    IsControlCtr = true;
                }
                else
                {
                    IsControlCtr = false;
                    List<ReportCtrl> list = ((ReportForm)this.ParentForm).ReportOperateManage.GetAllSeletedReportControls(null);
                    foreach (ReportCtrl rc in list)
                    {
                        if (rc != this && ((ReportForm)this.ParentForm).ReportOperateManage.GetControlCtrl() == null)
                        {
                            rc.IsControlCtr = true;
                            break;
                        }
                    }

                }
                this.Invalidate();
            }
            get
            {
                return ReportItem.Selected;
            }
        }

        protected bool _isActive;
        public virtual bool IsActive
        {
            set
            {
                _isActive = value;

                if (this.ParentForm == null) return;

                if (_isActive)
                {
                    IsSelected = true;
                    _subctrl.Enabled = true;
                    _borderColor = Color.Red;
                }
                else
                {
                    _subctrl.Enabled = false;
                    _borderColor = Color.Black;
                }
            }
            get
            {
                return _isActive;
            }
        }
        public virtual bool IsEditable
        {
            get
            {
                return this.ParentForm != null
                    && ((ReportForm)this.ParentForm).Report != null
                    && (int)((ReportForm)this.ParentForm).Report.Factor == 100;
            }
        }
        public int HandleCount
        {
            get
            {
                return 8;
            }
        }
        bool _isControlCtr;
        public bool IsControlCtr
        {
            set
            {
                _isControlCtr = value;
                if (_isControlCtr)
                {
                    _HandlerColor = Color.White;
                }
                else
                {
                    _HandlerColor = Color.Black;
                }
            }
            get
            {
                return _isControlCtr;
            }
        }

        #endregion

        public ReportItemBase ReportItem
        {
            get;
            protected set;
        }

        public Rectangle Oldbounds
        {
            get
            {
                return _OldBounds;
            }
        }
        public ReportCtrl()
        {
            InitializeComponent();
        }

        public ReportCtrl(ReportItemBase reportItem) : this()
        {
            ReportItem = reportItem;
            AllowDrop = true;
            //this.MouseMove += new MouseEventHandler(ReportCtrl_MouseMove);
            //this.MouseDown += new MouseEventHandler(ReportCtrl_MouseDown);
            ////this.DoubleClick += new EventHandler(ReportCtrl_DoubleClick);
            //this.MouseUp += new MouseEventHandler(ReportCtrl_MouseUp);
            //this.Load += new EventHandler(ReportCtrl_Load);
            //this.Disposed += new EventHandler(ReportCtrl_Disposed);

            //if (components == null) components = new Container();
            //this.ContextMenuStrip = new ContextMenuStrip(components);
            //this.ContextMenuStrip.Opening += new CancelEventHandler(ContextMenuStrip_Opening);

            //ReportCtrl_Load(null, null);
        }

        public void ResetSubCtrlLocation()
        {
            if (_subctrl == null) return;
            _subctrl.Location = new Point(BORDER_SIZE, BORDER_SIZE);
        }
    }
}

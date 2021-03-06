﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo.Pdf.EditorCtrls
{
    public partial class ReportLayout : DevComponents.DotNetBar.PanelEx
    {
        public ReportLayout()
        {
            InitializeComponent();
        }

        public ReportLayout(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        protected override Point ScrollToControl(Control activeControl)
        {
            return this.AutoScrollPosition;
        }
    }
}

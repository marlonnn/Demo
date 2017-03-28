using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo.Mdi
{
    public partial class Form : DevComponents.DotNetBar.Office2007Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetSystemMetrics(int nIndex);

        private int _childFHeight = 250;
        private int _childFWidth = 250;

        private int _colsPerRow = 4;

        private List<ChildForm> _childForms;
        private int _formIndex = 1;

        private List<int> _topLists;
        private int _scrollGap = 0;
        private int _formCount = 0;

        public Form()
        {
            IsMdiContainer = true;
            InitializeComponent();

            _childForms = new List<ChildForm>();
            _topLists = new List<int>();
            this.MouseWheel += Form_MouseWheel;
            this.FormClosed += Form_FormClosed;
        }

        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void Form_MouseWheel(object sender, MouseEventArgs e)
        {
            if (ModifierKeys == Keys.Control)
            {
                //Zoom out/in
            }
            else if (MdiClient.Bounds.Contains(e.Location))
            {
                ScrollForms(e.Delta);
                YAxisOffset();
            }
        }

        private void ScrollForms(int delta)
        {
            if (_childForms.Count == 0) return;
            List<ChildForm> forms = _childForms;
            ChildForm lastForm = forms.Last();
            if (lastForm.Bottom + delta < MdiClient.Height) delta = MdiClient.Height - lastForm.Bottom;

            ChildForm firstForm = forms.First();
            if (firstForm.Top + delta > 0) delta = -firstForm.Top;

            foreach (ChildForm form in forms)
            {
                form.Top += delta;
            }
        }

        public MdiClient MdiClient
        {
            get { return this.Controls.OfType<MdiClient>().Select(ctrl => ctrl as MdiClient).FirstOrDefault(); }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            MdiClient.Paint += MdiClientOnPaint;
        }

        private void MdiClientOnPaint(object sender, PaintEventArgs paintEventArgs)
        {
            // Create a brush
            Rectangle rect = this.ClientRectangle;
            rect.Inflate(2, 2);// to completely fill the client area
            SolidBrush brush = new SolidBrush(Color.SkyBlue);
            // Fill the client area
            paintEventArgs.Graphics.FillRectangle(brush, rect);
        }

        private void ChildForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ChildForm ChildForm = (ChildForm)sender;
            _childForms.Remove(ChildForm);
        }

        private void YAxisOffset()
        {
            var currentForm = _childForms[_childForms.Count - 1];
            var activeForm = (ChildForm)this.ActiveMdiChild;

            int index = _childForms.IndexOf(activeForm);
            int lastTop = _topLists[index];
            _scrollGap = lastTop - activeForm.Top;
        }

        private Point CalculateChildLocation()
        {
            //int formCount = _childForms.Count;
            int currentCols = (_formCount - 1) % _colsPerRow;//current columns
            var currentRows = (_formCount - 1) / _colsPerRow;
            //int currentRows = (v1 == 0) ? v1 : (v1 + 1);

            Point point = new Point(currentCols * _childFWidth, currentRows * _childFHeight - _scrollGap);
            _topLists.Add(currentRows * _childFHeight);
            return point;
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            _formCount++;
            ChildForm ChildForm = new ChildForm(new Size(_childFWidth, _childFHeight), _formIndex);
            _childForms.Add(ChildForm);
            ChildForm.Visible = false;
            ChildForm.MdiParent = Program.MainForm;
            ChildForm.Activate();
            ChildForm.Visible = true;
            ChildForm.Location = CalculateChildLocation();
            ChildForm.Show();
            _formIndex++;
            ChildForm.FormClosed += ChildForm_FormClosed;
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            int index = _childForms.Count;
            if (index > 0)
            {
                ChildForm ChildForm = _childForms[index - 1];
                _childForms.RemoveAt(index - 1);
                _topLists.RemoveAt(index - 1);
                ChildForm.Close();
                _formIndex--;
                _formCount--;
            }
        }
    }
}

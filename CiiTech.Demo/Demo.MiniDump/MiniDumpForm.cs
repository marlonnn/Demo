using MiniDump;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo.MiniDump
{
    public partial class MiniDumpForm : Form
    {
        public MiniDumpForm()
        {
            InitializeComponent();
        }

        private void buttonException_Click(object sender, EventArgs e)
        {
            throw new Exception("Minidump form exceptin message!");
        }
    }
}

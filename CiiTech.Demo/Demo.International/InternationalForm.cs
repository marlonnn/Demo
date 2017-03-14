using DevComponents.DotNetBar;
using DevComponents.Editors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo.International
{
    public partial class InternationalForm : Office2007RibbonForm
    {
        private System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InternationalForm));

        public InternationalForm()
        {
            InitializeComponent();
            this.Load += Form_Load;
            this.comboBoxLanguage.SelectedIndexChanged += comboBoxLanguage_SelectedIndexChanged;
        }

        private void comboBoxLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            var v = ((ComboItem)comboBoxLanguage.SelectedItem).Value.ToString(); 
            Program.SysConfig.UICulture = ((ComboItem)comboBoxLanguage.SelectedItem).Value.ToString();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            Program.SysConfig.PropertyChanged += SysConfig_PropertyChanged;
        }

        private void SysConfig_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == SysConfig.GetPropertyName(() => Program.SysConfig.UICulture))
            {
                BaseFunction.RefreshUICulture(resources, this);
            }
        }
    }
}

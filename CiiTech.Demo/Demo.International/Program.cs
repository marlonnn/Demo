using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo.International
{
    static class Program
    {
        private static SysConfig sysConfig;

        /// <summary>
        /// The configure information about software and hardware
        /// </summary>
        public static SysConfig SysConfig
        {
            get
            {
                if (sysConfig == null)
                {
                    sysConfig = new SysConfig();
                }
                return sysConfig;
            }
        }

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new InternationalForm());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo.SplashScreen
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {

            SplashScreenForm.ShowSplash();


            SplashScreenForm.CloseSplash();
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static string MajorVersion
        {
            get
            {
                return GetMajorVersion(Application.ProductVersion);
            }
        }

        public static string GetMajorVersion(string version)
        {
            if (version != null && version.Length > 5) version = version.Substring(0, version.Length - 5);
            return version;
        }
    }
}

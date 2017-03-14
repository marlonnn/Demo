using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.International
{
    public class SysConfig
    {
        private string uiCulture;

        public string UICulture
        {
            get
            {
                if (string.IsNullOrEmpty(uiCulture))
                {
                    UICulture = GetSysDefaultCulture().Name;
                }

                return uiCulture;
            }
            set
            {
                //if (value == "zh-CN" && !BaseFunction.ChineseEdition)
                //{
                //    value = "en-US";
                //}
                if (uiCulture == value) // compare after previous check
                    return;
                uiCulture = value;

                if (Program.SysConfig != null)
                {
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(uiCulture);
                    MessageBoxManager.Reset();  // after UI culture changes

                    OnPropertyChanged(() => UICulture);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged<T>(Expression<Func<T>> propertyId)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(((MemberExpression)propertyId.Body).Member.Name));
            }
        }

        /// <summary>
        /// return system default culture according to control panel language and region format setting
        /// will return en-US if not ChineseEdition but zh-CN culture
        /// </summary>
        /// <returns></returns>
        public CultureInfo GetSysDefaultCulture()
        {
            CultureInfo sysDefault = System.Threading.Thread.CurrentThread.CurrentCulture;
            if (sysDefault.Name == "zh-CN" && !BaseFunction.ChineseEdition)
            {
                sysDefault = new CultureInfo("en-US");
            }
            return sysDefault;
        }

        public static string GetPropertyName<TValue>(Expression<Func<TValue>> propertyId)
        {
            return ((MemberExpression)propertyId.Body).Member.Name;
        }
    }
}

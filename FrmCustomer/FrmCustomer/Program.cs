using ClassLibraryCustomerData;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrmCustomer
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // init Culture Info
            CultureInfo myCultureInfo = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = myCultureInfo;
            // init Resource Manager
            Localization.UpdateLanguage("en-US");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    sealed class Localization
    {
        private static ResourceManager resMgr;

        public static void UpdateLanguage(string langID)
        {
            try
            {
                //Set Language  
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(langID);

                // Init ResourceManager  
                resMgr = new ResourceManager("FrmCustomer.LangResources", Assembly.GetExecutingAssembly());
            }
            catch (Exception ex)
            {
            }
        }

        public static string getString(String pattern)
        {
            return resMgr.GetString(pattern);
        }
    }

}


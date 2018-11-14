using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_Insert_Del_UpdateView_Data_Grid_
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// public static string uprawnienia = "admin";
        [STAThread]
        static void Main()
        {

            //CultureInfo myCI = new CultureInfo(ConfigurationManager.AppSettings["region"].ToString(), false);
            #region settings
            // Clones myCI and modifies the DTFI and NFI instances associated with the clone.
            //CultureInfo myCIclone = (CultureInfo)myCI.Clone();
            //myCIclone.DateTimeFormat.AMDesignator = "a.m.";
            //myCIclone.DateTimeFormat.DateSeparator = "-";
            //myCIclone.NumberFormat.NumberDecimalSeparator = ".";
            //myCIclone.NumberFormat.CurrencySymbol = "USD";
            //myCIclone.NumberFormat.NumberDecimalDigits = 4;
            //CultureInfo pl = new CultureInfo(ConfigurationManager.AppSettings["region"].ToString());
            //string shortUkDateFormatString = pl.DateTimeFormat.ShortDatePattern;
            //string shortUkTimeFormatString = pl.DateTimeFormat.ShortTimePattern;
            //myCI.DateTimeFormat.ShortDatePattern = shortUkDateFormatString;
            //myCI.DateTimeFormat.ShortTimePattern = shortUkTimeFormatString;
            #endregion
            //System.Threading.Thread.CurrentThread.CurrentCulture = myCI;

            try
            {

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new detailsAdd());
        }

        
    }
}

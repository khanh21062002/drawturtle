using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DevExpress.XtraReports.Web.ClientControls;


public class CustomGlobalizationService : IGlobalizationService
{
    public void UpdateCultures(string[] userLanguagesFromClient)
    {
        if (userLanguagesFromClient.Length > 0)
        {
            string lang = userLanguagesFromClient[0];
            CultureInfo currentCulture = CultureInfo.GetCultureInfo(lang);
            Thread.CurrentThread.CurrentCulture = currentCulture;
            Thread.CurrentThread.CurrentUICulture = currentCulture;
        }

    }
}

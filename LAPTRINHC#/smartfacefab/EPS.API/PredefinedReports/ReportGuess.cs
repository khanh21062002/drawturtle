using System;
using DevExpress.XtraReports.UI;

namespace EPS.API.PredefinedReports
{
    public partial class ReportGuess
    {
        public ReportGuess()
        {
            InitializeComponent();
            DateTime date = DateTime.Now;
        }
        private void ReportRealTime_DataSourceDemanded(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(p_lang.Value.ToString()))
            {
                string lang = p_lang.Value.ToString();
                //String a
                if (lang.Trim() == "vi" || lang.Trim() == "en")
                {
                    this.ApplyLocalization(lang);


                }
                else
                {
                    this.ApplyLocalization("vi");
                }
            }
            else
            {
                this.ApplyLocalization("vi");
            }

        }
    }
}

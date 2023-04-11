using System;
using DevExpress.XtraReports.UI;

namespace EPS.API.PredefinedReports
{
    public partial class ReportInOutStudent
    {
        public ReportInOutStudent()
        {
            InitializeComponent();
            //DateTime date = DateTime.Now;
            //p_filterDateFrom.Value = DateTime.Today;
            //p_className.Value = "Tất cả";
            //p_gradesName.Value = "Tất cả";
        }

        private void ReportInOutStudent_DataSourceDemanded(object sender, EventArgs e)
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

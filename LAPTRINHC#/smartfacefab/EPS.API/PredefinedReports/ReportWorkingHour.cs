using System;
using System.Globalization;
using DevExpress.XtraReports.UI;

namespace EPS.API.PredefinedReports
{
    public partial class ReportWorkingHour
    {
        public ReportWorkingHour()
        {
            InitializeComponent();
            DateTime date = DateTime.Now;
            p_filterDateFrom.Value = new DateTime(date.Year, date.Month, 1);
            p_filterDateTo.Value = DateTime.Today.AddDays(-1);
        }

        private void ReportWorkingHour_DataSourceDemanded(object sender, EventArgs e)
        {
            date1.Text = String.Format("{0:dd/MM}", p_filterDateFrom.Value);
            date2.Text = String.Format("{0:dd/MM}", Convert.ToDateTime(p_filterDateFrom.Value, CultureInfo.InvariantCulture).AddDays(1));
            date3.Text = String.Format("{0:dd/MM}", Convert.ToDateTime(p_filterDateFrom.Value, CultureInfo.InvariantCulture).AddDays(2));
            date4.Text = String.Format("{0:dd/MM}", Convert.ToDateTime(p_filterDateFrom.Value, CultureInfo.InvariantCulture).AddDays(3));
            date5.Text = String.Format("{0:dd/MM}", Convert.ToDateTime(p_filterDateFrom.Value, CultureInfo.InvariantCulture).AddDays(4));
            date6.Text = String.Format("{0:dd/MM}", Convert.ToDateTime(p_filterDateFrom.Value, CultureInfo.InvariantCulture).AddDays(5));
            date7.Text = String.Format("{0:dd/MM}", Convert.ToDateTime(p_filterDateFrom.Value, CultureInfo.InvariantCulture).AddDays(6));
            date8.Text = String.Format("{0:dd/MM}", Convert.ToDateTime(p_filterDateFrom.Value, CultureInfo.InvariantCulture).AddDays(7));
            date9.Text = String.Format("{0:dd/MM}", Convert.ToDateTime(p_filterDateFrom.Value, CultureInfo.InvariantCulture).AddDays(8));
            date10.Text = String.Format("{0:dd/MM}", Convert.ToDateTime(p_filterDateFrom.Value, CultureInfo.InvariantCulture).AddDays(9));
            date11.Text = String.Format("{0:dd/MM}", Convert.ToDateTime(p_filterDateFrom.Value, CultureInfo.InvariantCulture).AddDays(10));
            date12.Text = String.Format("{0:dd/MM}", Convert.ToDateTime(p_filterDateFrom.Value, CultureInfo.InvariantCulture).AddDays(11));
            date13.Text = String.Format("{0:dd/MM}", Convert.ToDateTime(p_filterDateFrom.Value, CultureInfo.InvariantCulture).AddDays(12));
            date14.Text = String.Format("{0:dd/MM}", Convert.ToDateTime(p_filterDateFrom.Value, CultureInfo.InvariantCulture).AddDays(13));
            date15.Text = String.Format("{0:dd/MM}", Convert.ToDateTime(p_filterDateFrom.Value, CultureInfo.InvariantCulture).AddDays(14));
            date16.Text = String.Format("{0:dd/MM}", Convert.ToDateTime(p_filterDateFrom.Value, CultureInfo.InvariantCulture).AddDays(15));
            date17.Text = String.Format("{0:dd/MM}", Convert.ToDateTime(p_filterDateFrom.Value, CultureInfo.InvariantCulture).AddDays(16));
            date18.Text = String.Format("{0:dd/MM}", Convert.ToDateTime(p_filterDateFrom.Value, CultureInfo.InvariantCulture).AddDays(17));
            date19.Text = String.Format("{0:dd/MM}", Convert.ToDateTime(p_filterDateFrom.Value, CultureInfo.InvariantCulture).AddDays(18));
            date20.Text = String.Format("{0:dd/MM}", Convert.ToDateTime(p_filterDateFrom.Value, CultureInfo.InvariantCulture).AddDays(19));
            date21.Text = String.Format("{0:dd/MM}", Convert.ToDateTime(p_filterDateFrom.Value, CultureInfo.InvariantCulture).AddDays(20));
            date22.Text = String.Format("{0:dd/MM}", Convert.ToDateTime(p_filterDateFrom.Value, CultureInfo.InvariantCulture).AddDays(21));
            date23.Text = String.Format("{0:dd/MM}", Convert.ToDateTime(p_filterDateFrom.Value, CultureInfo.InvariantCulture).AddDays(22));
            date24.Text = String.Format("{0:dd/MM}", Convert.ToDateTime(p_filterDateFrom.Value, CultureInfo.InvariantCulture).AddDays(23));
            date25.Text = String.Format("{0:dd/MM}", Convert.ToDateTime(p_filterDateFrom.Value, CultureInfo.InvariantCulture).AddDays(24));
            date26.Text = String.Format("{0:dd/MM}", Convert.ToDateTime(p_filterDateFrom.Value, CultureInfo.InvariantCulture).AddDays(25));
            date27.Text = String.Format("{0:dd/MM}", Convert.ToDateTime(p_filterDateFrom.Value, CultureInfo.InvariantCulture).AddDays(26));
            date28.Text = String.Format("{0:dd/MM}", Convert.ToDateTime(p_filterDateFrom.Value, CultureInfo.InvariantCulture).AddDays(27));
            date29.Text = String.Format("{0:dd/MM}", Convert.ToDateTime(p_filterDateFrom.Value, CultureInfo.InvariantCulture).AddDays(28));
            date30.Text = String.Format("{0:dd/MM}", Convert.ToDateTime(p_filterDateFrom.Value, CultureInfo.InvariantCulture).AddDays(29));
            date31.Text = String.Format("{0:dd/MM}", Convert.ToDateTime(p_filterDateFrom.Value, CultureInfo.InvariantCulture).AddDays(30));

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

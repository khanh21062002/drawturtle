﻿using System;
using DevExpress.XtraReports.UI;

namespace EPS.API.PredefinedReports
{
    public partial class ReportWorkingHourEarly
    {
        public ReportWorkingHourEarly()
        {
            InitializeComponent();
            //DateTime date = DateTime.Now;
            //p_filterDateFrom.Value = new DateTime(date.Year, date.Month, 1);
            //p_filterDateTo.Value = DateTime.Today.AddDays(-1);
            //p_departmentName.Value = "Tất cả";
        }

        private void ReportWorkingHourEarly_DataSourceDemanded(object sender, EventArgs e)
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
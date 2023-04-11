using DevExpress.DataAccess.Json;
using DevExpress.XtraReports.UI;
using EPS.API.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace EPS.API.PredefinedReports
{
    public static class ReportsFactory
    {
        public static Dictionary<string, Func<XtraReport>> Reports = new Dictionary<string, Func<XtraReport>>()
        {
            ["ReportOverTime"] = () => new ReportOverTime(),
            ["ReportWorkingHour"] = () => new ReportWorkingHour(),
            ["ReportOverTimeHours"] = () => new ReportOverTimeHours(),
            ["ReportWorkingHourLate"] = () => new ReportWorkingHourLate(),
            ["ReportWorkingHourEarly"] = () => new ReportWorkingHourEarly(),
            ["ReportRealTime"] = () => new ReportRealTime(),
            ["ReportInOutStudent"] = () => new ReportInOutStudent(),
            ["ReportInOutEmployee"] = () => new ReportInOutEmployee(),
            ["SummaryOfArrivals"] = () => new SummaryOfArrivals(),
            ["ReportGuess"] = () => new ReportGuess(),

            ["ReportEmployeeInOut"] = () => new ReportEmployeeInOut(),
            ["ReportPersonInOut"] = () => new ReportPersonInOut(),
        };
    }
}

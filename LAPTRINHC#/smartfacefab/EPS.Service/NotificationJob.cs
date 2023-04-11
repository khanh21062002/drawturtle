using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Helpers;
using EPS.Service.Models;
using EPS.Utils.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Service
{
    public class NotificationJob : IJob
    {
        private readonly ILogger<NotificationJob> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private IOptions<AppSettings> _settings;

        public NotificationJob(ILogger<NotificationJob> logger, IServiceScopeFactory scopeFactory, IOptions<AppSettings> settings)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _settings = settings;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            // Thời gian quét DB
            const int TIME_CHECK = -30;
            const double DEFAULT_TEMP_CHECK = 37.5;
            _logger.LogInformation("Start Send Schedule Mail:");

            using (var scope = _scopeFactory.CreateScope())
            {
                var _dbContext = scope.ServiceProvider.GetRequiredService<EPSContext>();
                var _mailService = scope.ServiceProvider.GetRequiredService<MailService>();
                var lstNoti = await _dbContext.Set<Notification>().WhereMany(x => x.IsDelete == false).ToListAsync();
                foreach (var item in lstNoti)
                {
                    var id = item.ID;
                    var lstNotiDetails = await _dbContext.Set<NotificationDetail>().WhereMany(x => x.NotiId == id).ToListAsync();
                    var compId = item.CompId;
                    var type = item.Type;
                    var machineId = item.MachineId;
                    if (type.HasValue)
                    {
                        var scoreMatch = item.ScoreMatch;
                        MailRequest mail = new MailRequest();
                        //mail.ToEmail = notidetail.Email;
                        //set template
                        string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\NotificationMailTemplate.html";
                        StreamReader str = new StreamReader(FilePath);
                        string MailText = str.ReadToEnd();
                        str.Close();
                        bool sendMail = false;
                        if (type == 1)
                        {
                            // noti unknown person
                            DateTime current = DateTime.Now;
                            DateTime dateCompare = current.AddMinutes(TIME_CHECK);
                            var lstUnknown = await _dbContext.Set<Event>().WhereMany(x => x.CompId == compId && x.MachineId == machineId && x.Status == 0 && x.ScoreMatch <= scoreMatch && x.AccessTime.HasValue && DateTime.Compare(x.AccessTime.Value, dateCompare) > 0).OrderByDescending(x => x.AccessTime).ToListAsync();
                            var tableContent = "<thead>";
                            tableContent += "<th>Home.Table.GridOptions.avatar</th>";
                            tableContent += "<th>Warning.List.Table.Machine </th>";
                            tableContent += "<th>Categories.GroupAccess.List.Table.Timeseg </th>";
                            tableContent += "<th>Reporting.Event.List.Table.Temp</th>";
                            tableContent += "<th>GroupService.Message.Threshold</th>";
                            tableContent += "</thead>";

                            foreach (var eve in lstUnknown)
                            {
                                tableContent += "<tbody style = \"text-align: center; \">";
                                string faceURL = _settings.Value.ApiUrl + "event-img/" + eve.CompId + "/" + eve.AccessDate.Value.ToString("yyyy-MM-dd") + "/" + eve.EventId + ".jpg";
                                tableContent += "<td><img  src=\"" + faceURL + "\"  width =\"60\" style=\"max-width: 80px; max-height: 80px;\"></td>";
                                //tableContent += "<td>" + eve.Person.FullName + "</td>";
                                tableContent += "<td>" + eve.Machine.DeviceName + "</td>";
                                tableContent += "<td>" + FormatDate(eve.AccessTime) + "</td>";
                                tableContent += "<td>" + formatDouble(eve.Temperature) + "</td>";
                                tableContent += "<td>" + formatDouble(eve.ScoreMatch) + "</td>";
                                tableContent += "</tbody>";
                            }
                            MailText = MailText.Replace("[table-content]", tableContent);
                            MailText = MailText.Replace("[first-sentence]", "NotificationJod.Message.List");
                            MailText = MailText.Replace("[second-sentence]", "");
                            MailText = MailText.Replace("[footer]", "NotificationJod.Message.Thank");

                            mail.Subject = $"Warning.Create.Type.Stranger";
                            if (lstUnknown.Count > 0)
                            {
                                sendMail = true;
                            }
                        }
                        else if (type == 2)
                        {

                            // noti high temp
                            DateTime current = DateTime.Now;
                            DateTime dateCompare = current.AddMinutes(TIME_CHECK);
                            var lstHighTemp = await _dbContext.Set<Event>().WhereMany(x => x.CompId == compId && x.MachineId == machineId && (x.AccessTime.HasValue && DateTime.Compare(x.AccessTime.Value, dateCompare) > 0) && ((x.Temperature.HasValue && x.Temperature >= x.Machine.TemperatureThreshold) || (!x.Temperature.HasValue && x.Temperature >= DEFAULT_TEMP_CHECK))).OrderByDescending(x => x.AccessTime).ToListAsync();
                            var tableContent = "<thead>";
                            tableContent += "<th>NotificationJod.Message.FullName</th>";
                            tableContent += "<th>Home.Table.GridOptions.avatar</th>";
                            tableContent += "<th>Warning.List.Table.Machine </th>";
                            tableContent += "<th>Categories.GroupAccess.List.Table.Timeseg </th>";
                            tableContent += "<th>Reporting.Event.List.Table.Temp</th>";
                            tableContent += "<th>GroupService.Message.Threshold</th>";
                            tableContent += "</thead>";

                            foreach (var eve in lstHighTemp)
                            {
                                tableContent += "<tbody style = \"text-align: center; \">";
                                //tableContent += "<td>" + eve.Person.FullName + "</td>";
                                string faceURL = _settings.Value.ApiUrl + "event-img/" + eve.CompId + "/" + eve.AccessDate.Value.ToString("yyyy-MM-dd") + "/" + eve.EventId + ".jpg";
                                tableContent += "<td><img  src=\"" + faceURL + "\"  width =\"60\"  style=\"max-width: 80px; max-height: 80px;\"></td>";
                                //tableContent += "<td>" + eve.Person.FullName + "</td>";
                                tableContent += "<td>" + eve.Machine.DeviceName + "</td>";
                                tableContent += "<td>" + FormatDate(eve.AccessTime) + "</td>";
                                tableContent += "<td style=\"color: red \">" + formatDouble(eve.Temperature) + "</td>";
                                tableContent += "<td>" + formatDouble(eve.ScoreMatch) + "</td>";
                                tableContent += "</tbody>";
                            }
                            MailText = MailText.Replace("[table-content]", tableContent);
                            MailText = MailText.Replace("[first-sentence]", "NotificationJod.Message.ListTemp");
                            MailText = MailText.Replace("[second-sentence]", "");
                            MailText = MailText.Replace("[footer]", "NotificationJod.Message.Thank");
                            mail.Subject = $"Warning.Create.Type.Stranger";
                            if (lstHighTemp.Count > 0)
                            {
                                sendMail = true;
                            }
                        }
                        if (sendMail)
                        {
                            foreach (var notidetail in lstNotiDetails)
                            {
                                var name = notidetail.Person.FullName;
                                MailText = MailText.Replace("[name]", name);
                                var builder = new BodyBuilder();
                                builder.HtmlBody = MailText;
                                mail.Body = MailText;
                                mail.ToEmail = notidetail.Email;
                                _ = _mailService.SendEmailAsync(mail);

                            }
                        }
                    }



                }



            }

        }

        private static string formatDouble(double? number)
        {
            if (number.HasValue)
            {
                return string.Format("{0:0.00}", number);
            }
            return "";
        }

        private static string FormatDate(DateTime? date)
        {
            if (date.HasValue)
            {
                return date.Value.ToString("dd/MM/yyyy hh:mm:ss tt");
            }
            return "";

        }
        private static bool CheckHighTemp(double? temparature, double? temperatureThreshold)
        {
            if (temparature.HasValue)
            {
                //Ko có thì lấy môc 37.5
                double threshHold = temperatureThreshold.HasValue ? temperatureThreshold.Value : 37.5;
                if (temparature.Value > threshHold)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

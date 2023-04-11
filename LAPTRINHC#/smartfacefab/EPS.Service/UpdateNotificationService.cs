using EPS.Service.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EPS.Service
{
    public class UpdateNotificationService
    {
        private readonly AppSettings _appSettings;

        public UpdateNotificationService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public async Task<string> SyncData(string method, object data)
        {
            string status = "";
            try
            {
                string URL = _appSettings.SyncURL;
                string json = JsonSerializer.Serialize(data);
                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage
                    {
                        Method = CreateHttpMethod(method),
                        RequestUri = new Uri(URL),
                        Content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json)
                    };
                    client.Timeout = TimeSpan.FromMilliseconds(30000);
                    var response = await client.SendAsync(request).ConfigureAwait(false);
                    response.EnsureSuccessStatusCode();
                    status = "Success";
                    var result = response.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception ex)
            {
                status = ex.Message;
            }
            return status;
        }

        private HttpMethod CreateHttpMethod(string method)
        {
            switch (method)
            {
                case "POST":
                    return HttpMethod.Post;
                case "PUT":
                    return HttpMethod.Put;
                case "Delete":
                    return HttpMethod.Delete;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}

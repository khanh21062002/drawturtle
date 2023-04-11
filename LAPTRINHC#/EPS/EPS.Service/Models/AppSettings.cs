using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Models
{
    public class AppSettings
    {
        public string ApiUrl { get; set; }
        public string UploadPersonImgFolder { get; set; }
        public string DownloadFolder { get; set; }        
        public string ApiSearchFace { get; set; }
        public string ApiDetectFace { get; set; }
    }
}

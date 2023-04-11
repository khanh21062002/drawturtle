using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Models
{
    public class AppSettings
    {
        public string ApiUrl { get; set; }
        public string UploadPersonImgFolder { get; set; }
        public string Api { get; set; }
        public string MjpegServer { get; set; }
        public string ApiUrlPolygon { get; set; }
        public string ApiUrlPolygonCancel { get; set; }
        public string DownloadFolder { get; set; }        
        public string ApiSearchFace { get; set; }
        public string ApiSearchEvent { get; set; }
        public string ApiDetectFace { get; set; }
        public string UploadImgFolder { get; set; }
        public string SyncURL { get; set; }
        public string CameraLink { get; set; }
    }
}

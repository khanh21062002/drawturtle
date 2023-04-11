using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Device
{
    public class CheckImport
    {
        public string FileName { get; set; }
        public IFormFile FileData { get; set; }
    }
}

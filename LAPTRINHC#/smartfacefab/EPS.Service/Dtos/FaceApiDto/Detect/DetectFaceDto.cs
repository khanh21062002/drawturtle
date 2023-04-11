using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.FaceApiDto.Detect
{
    public class DetectFaceDto
    {
        public string img_url { get; set; }
        public string img_base64 { get; set; }
    }
}

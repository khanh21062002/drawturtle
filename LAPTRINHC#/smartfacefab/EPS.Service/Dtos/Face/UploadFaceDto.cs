using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Face
{
    public class UploadFaceDto
    {
        public string FilePath { get; set; }
        public string OriginalName { get; set; }
        public bool IsSuccess { get; set; }
    }
}

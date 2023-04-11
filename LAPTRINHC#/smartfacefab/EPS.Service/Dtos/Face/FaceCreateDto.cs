using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Face
{
    public class FaceCreateDto
    {
        public Guid PersonId { get; set; }
        public Guid FaceId { get; set; }
        public string FaceUrl { get; set; }
        public string FacePath { get; set; }
        public string FeaturePath { get; set; }
        public Nullable<int> FaceStatus { get; set; }
        public Nullable<int> Status { get; set; }
        public string File1 { get; set; }
        public string FileData { get; set; }
        public Nullable<int> WearMask { get; set; }
        public string ImageBase64 { get; set; }
    }
}

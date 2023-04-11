using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Face
{
    public class FaceUpdateDto
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
        public Nullable<DateTime> UpdateTime { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<DateTime> DeleteTime { get; set; }
        public string DeleteBy { get; set; }
    }
}

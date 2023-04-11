using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Text;

namespace EPS.Data.Entities
{
    public partial class Face
    {
        public Guid PersonId { get; set; }
        public Guid FaceId { get; set; }
        public string FaceUrl { get; set; }
        public string FacePath { get; set; }
        public string FeaturePath { get; set; }
        public Nullable<int>  FaceStatus { get; set; }
        public int Status { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public string UpdateBy { get; set; }
        public string ImageBase64 { get; set; }

        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }
    }
}

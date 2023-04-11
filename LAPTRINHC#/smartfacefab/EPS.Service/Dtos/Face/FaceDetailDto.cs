using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Face
{
    public class FaceDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> ComId { get; set; }
        public Nullable<int> Status { get; set; }
        public string Position { get; set; }
        public string Code { get; set; }
        public Nullable<int> ParentId { get; set; }

    }
}

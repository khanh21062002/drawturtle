using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Face
{
    public class FaceGridDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ComId { get; set; }
        public int Status { get; set; }
        public string Position { get; set; }
        public string Code { get; set; }
        public string Company { get; set; }
        public string Face { get; set; }
        public int ParentId { get; set; }


    }
}

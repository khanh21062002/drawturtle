using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcFaceService.Entity
{
    public class PersonUnit
    {
        public int groupId { get; set; }
        public string personId { get; set; }
        public string personCode { get; set; }
        public string personName { get; set; }
        public string passport { get; set; }
        public DateTime birthday { get; set; }
        public string phonenumber { get; set; }
        public string facePath { get; set; }
        public byte[] faceFeature { get; set; }
    }
}

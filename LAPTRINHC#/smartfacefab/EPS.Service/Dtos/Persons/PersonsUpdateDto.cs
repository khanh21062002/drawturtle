using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Persons
{
    public class PersonsUpdateDto
    {
        public Guid PersonId { get; set; }
        public Nullable<int> GroupId { get; set; }
        public string PersonCode { get; set; }
        public string PersonName { get; set; }
        public string FacePath { get; set; }
        public Nullable<int> Gender { get; set; }
        public Nullable<int> Age { get; set; }
        public Nullable<int> Liveness { get; set; }
        public Nullable<int> Mask { get; set; }
        public Nullable<int> LeftEyeClose { get; set; }
        public Nullable<int> RightEyeClose { get; set; }
        public Nullable<int> WearGlass { get; set; }
        public Nullable<float> Quality { get; set; }
        public Nullable<int> Orient { get; set; }
        public string Rect { get; set; }
        public string Landmark { get; set; }
        public string Angle { get; set; }
        public byte[] Facefeature { get; set; }
        public Nullable<DateTime> RegisterTime { get; set; }
        public Nullable<DateTime> DeleteTime { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<bool> IsDelete { get; set; }
    }
}

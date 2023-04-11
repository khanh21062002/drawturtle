using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.PersonQRUpdate
{
    public class PersonQRUpdateGridDto
    {
        public int Id { get; set; }
        public Nullable<DateTime> DateUpdate { get; set; }
        public string DateUpdateStr { get; set; }
        public Guid PersonId { get; set; }
        public string PersonCode { get; set; }
        public string PersonName { get; set; }
        public Nullable<int> DeptId { get; set; }
        public Nullable<int> CompId { get; set; }
        public string DeptName { get; set; }
        public string Gender { get; set; }
        public string ImageUpdate { get; set; }
        public Nullable<int> Status { get; set; }
        public string StatusStr { get; set; }
        public string Note { get; set; }
    }
}

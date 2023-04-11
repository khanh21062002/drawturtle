using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.PersonQRUpdate
{
    public class UpdateImageDto
    {
        public int Id { get; set; }
        public Guid PersonId { get; set; }
        public Nullable<DateTime> DateUpdate { get; set; }
        public string ImageUpdate { get; set; }
        public string FileData { get; set; }
        public Nullable<int> Status { get; set; }
        public string Note { get; set; }
    }
}

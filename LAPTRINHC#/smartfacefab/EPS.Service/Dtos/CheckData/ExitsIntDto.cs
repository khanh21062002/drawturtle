using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.CheckData
{
    public class ExitsIntDto
    {
        public int ID { get; set; }
        public Guid EventId { get; set; }
        public Guid PersonId { get; set; }
        public string PhoneNumber { get; set; }
        public int ComId { get; set; }
    }
}

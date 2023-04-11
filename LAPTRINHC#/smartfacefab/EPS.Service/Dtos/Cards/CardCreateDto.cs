using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Card
{
    public class CardCreateDto
    {
        public Guid CardId { get; set; }
        public Guid PersonId { get; set; }
        public string CardNo { get; set; }
        public Nullable<System.DateTime> RegisterTime { get; set; }
        public string RegisterBy { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<System.DateTime> DeleteTime { get; set; }
        public string DeleteBy { get; set; }
        public int Status { get; set; }
    }
}

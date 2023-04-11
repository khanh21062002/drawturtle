using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.PreOrder
{
    public class PreOrderDetailDto
    {
        public int Id { get; set; }
        public DateTime? DateOrder { get; set; }
        public DateTime? TimeOrder { get; set; }
        public Guid? IdGuest { get; set; }
        public string NameGuest { get; set; }
        public string PhoneNumber { get; set; }
        public int? AmountPeople { get; set; }
        public string RequestOrder { get; set; }
        public string AnotherOrder { get; set; }
        public decimal? DownPayment { get; set; }
        public string DownPaymentStr { get; set; }
        public int? CompId { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DateCreate { get; set; }
    }
}

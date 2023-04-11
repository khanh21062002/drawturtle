using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EPS.Data.Entities
{
    public class HomeCustomerComeStore
    {
        public int ID { get; set; }
        public Nullable<int> HourEvent { get; set; }
        public Nullable<int> CountFirst { get; set; }
        public Nullable<int> CountReturn { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Data.Entities
{
    public class HomePieCustomerByDay
    {
        public int ID { get; set; }
        public Nullable<int> CountVip { get; set; }
        public Nullable<int> CountBlacklist{ get; set; }
        public Nullable<int> CountGuest { get; set; }
    }
}

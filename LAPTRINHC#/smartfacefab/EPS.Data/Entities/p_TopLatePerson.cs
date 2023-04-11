using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Data.Entities
{
    public class p_TopLatePerson
    {
        public long ID { get; set; }
        public System.Guid PersonId { get; set; }
        public string DeptName { get; set; }
        public string FullName { get; set; }
        public string File1 { get; set; }
        public int Latedays { get; set; }

    }
}

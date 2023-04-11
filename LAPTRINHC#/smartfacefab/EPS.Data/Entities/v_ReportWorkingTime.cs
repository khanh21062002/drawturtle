using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPS.Data.Entities
{
    public partial class v_ReportWorkingTime
    {
        public int ID { get; set; }
        public System.Guid PersonId { get; set; }
        public string FullName { get; set; }
        public string PersonCode { get; set; }
        public Nullable<System.DateTime> Day { get; set; }
        public Nullable<System.DateTime> StartAccessTime { get; set; }
        public Nullable<System.DateTime> EndAccessTime { get; set; }
        public Nullable<int> DiffStartTime { get; set; }
        public Nullable<int> DiffEndTime { get; set; }
        public Nullable<double> TotalTime { get; set; }
        public string DeptName { get; set; }


    }
}

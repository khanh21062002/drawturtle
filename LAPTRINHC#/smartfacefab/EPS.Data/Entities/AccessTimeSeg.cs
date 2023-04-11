using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPS.Data.Entities
{
    public partial class AccessTimeSeg
    {
        public int Id { get; set; }
        public string TimeSegName { get; set; }
        public int CompId { get; set; }
        public int DeptId { get; set; }
        public int GroupId { get; set; }
        public  Nullable<System.TimeSpan> SundayStart1 { get; set; }
        public Nullable<System.TimeSpan>SundayEnd1 { get; set; }
        public Nullable<System.TimeSpan>SundayStart2 { get; set; }
        public Nullable<System.TimeSpan>SundayEnd2 { get; set; }
        public Nullable<System.TimeSpan>SundayStart3 { get; set; }
        public Nullable<System.TimeSpan>SundayEnd3 { get; set; }
        public Nullable<System.TimeSpan>SundayStart4 { get; set; }
        public Nullable<System.TimeSpan>SundayEnd4 { get; set; }
        public Nullable<System.TimeSpan>MondayStart1 { get; set; }
        public Nullable<System.TimeSpan>MondayEnd1 { get; set; }
        public Nullable<System.TimeSpan>MondayStart2 { get; set; }
        public Nullable<System.TimeSpan>MondayEnd2 { get; set; }
        public Nullable<System.TimeSpan>MondayStart3 { get; set; }
        public Nullable<System.TimeSpan>MondayEnd3 { get; set; }
        public Nullable<System.TimeSpan>MondayStart4 { get; set; }
        public Nullable<System.TimeSpan>MondayEnd4 { get; set; }
        public Nullable<System.TimeSpan>TuesdayStart1 { get; set; }
        public Nullable<System.TimeSpan>TuesdayEnd1 { get; set; }
        public Nullable<System.TimeSpan>TuesdayStart2 { get; set; }
        public Nullable<System.TimeSpan>TuesdayEnd2 { get; set; }
        public Nullable<System.TimeSpan>TuesdayStart3 { get; set; }
        public Nullable<System.TimeSpan>TuesdayEnd3 { get; set; }
        public Nullable<System.TimeSpan>TuesdayStart4 { get; set; }
        public Nullable<System.TimeSpan>TuesdayEnd4 { get; set; }
        public Nullable<System.TimeSpan>WednesdayStart1 { get; set; }
        public Nullable<System.TimeSpan>WednesdayEnd1 { get; set; }
        public Nullable<System.TimeSpan>WednesdayStart2 { get; set; }
        public Nullable<System.TimeSpan>WednesdayEnd2 { get; set; }
        public Nullable<System.TimeSpan>WednesdayStart3 { get; set; }
        public Nullable<System.TimeSpan>WednesdayEnd3 { get; set; }
        public Nullable<System.TimeSpan>WednesdayStart4 { get; set; }
        public Nullable<System.TimeSpan>WednesdayEnd4 { get; set; }
        public Nullable<System.TimeSpan>ThursdayStart1 { get; set; }
        public Nullable<System.TimeSpan>ThursdayEnd1 { get; set; }
        public Nullable<System.TimeSpan>ThursdayStart2 { get; set; }
        public Nullable<System.TimeSpan>ThursdayEnd2 { get; set; }
        public Nullable<System.TimeSpan>ThursdayStart3 { get; set; }
        public Nullable<System.TimeSpan>ThursdayEnd3 { get; set; }
        public Nullable<System.TimeSpan>ThursdayStart4 { get; set; }
        public Nullable<System.TimeSpan>ThursdayEnd4 { get; set; }
        public Nullable<System.TimeSpan>FridayStart1 { get; set; }
        public Nullable<System.TimeSpan>FridayEnd1 { get; set; }
        public Nullable<System.TimeSpan>FridayStart2 { get; set; }
        public Nullable<System.TimeSpan>FridayEnd2 { get; set; }
        public Nullable<System.TimeSpan>FridayStart3 { get; set; }
        public Nullable<System.TimeSpan>FridayEnd3 { get; set; }
        public Nullable<System.TimeSpan>FridayStart4 { get; set; }
        public Nullable<System.TimeSpan>FridayEnd4 { get; set; }
        public Nullable<System.TimeSpan>SaturdayStart1 { get; set; }
        public Nullable<System.TimeSpan>SaturdayEnd1 { get; set; }
        public Nullable<System.TimeSpan>SaturdayStart2 { get; set; }
        public Nullable<System.TimeSpan>SaturdayEnd2 { get; set; }
        public Nullable<System.TimeSpan>SaturdayStart3 { get; set; }
        public Nullable<System.TimeSpan>SaturdayEnd3 { get; set; }
        public Nullable<System.TimeSpan>SaturdayStart4 { get; set; }
        public Nullable<System.TimeSpan> SaturdayEnd4 { get; set; }
        public int Status { get; set; }
        [ForeignKey("CompId")]
        public virtual Company Company { get; set; }
     
        public bool IsDelete { get; set; }
    }
}

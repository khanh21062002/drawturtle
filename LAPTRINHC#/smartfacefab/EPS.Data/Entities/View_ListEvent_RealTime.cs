using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPS.Data.Entities
{
    public partial class View_ListEvent_RealTime
    {
        [Key]
        public System.Guid EventId { get; set; }
        public Nullable<System.Guid> PersonId { get; set; }
        public Nullable<System.Guid> FaceId { get; set; }
        public Nullable<System.Guid> FingerId { get; set; }
        public Nullable<System.Guid> CardId { get; set; }
        public string CardNo { get; set; }
        public string FacePath { get; set; }
        public string FeaturePath { get; set; }
        public Nullable<int> MachineId { get; set; }
        public Nullable<System.DateTime> AccessDate { get; set; }
        public Nullable<System.DateTime> AccessTime { get; set; }
        public string AccessType { get; set; }
        public Nullable<double> Temperature { get; set; }
        public Nullable<int> Gender { get; set; }
        public Nullable<int> WearMask { get; set; }
        public Nullable<int> Age { get; set; }
        public Nullable<double> ScoreMatch { get; set; }
        public Nullable<System.DateTime> SyncDatetime { get; set; }
        public string ErrorCode { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> CompId { get; set; }
        public Nullable<bool> IsCheckNoti { get; set; }
        public string PersonPosition { get; set; }
        public int? AreaId { get; set; }
        public Nullable<int> CompType { get; set; }
        public string Person { get; set; }
        public Nullable<int> DeptId { get; set; }
        public string PersonCode { get; set; }
        public int? PersonType { get; set; }
        public string PhoneNumber { get; set; }
        public int? NumberOfTimes { get; set; }
        public int? AmountPeople { get; set; }
        public DateTime? TimeOrder { get; set; }
        public string PhoneOrder { get; set; }
        public DateTime? DateCreate { get; set; }
        public string DepName { get; set; }
        public string DepCode { get; set; }

        [ForeignKey("MachineId")]
        public virtual Machine Machine { get; set; }
        public string Note { get; set; }
        [ForeignKey("AreaId")]
        public virtual Areas Areas { get; set; }
        [ForeignKey("CompId")]
        public virtual Company Company { get; set; }
    }
}

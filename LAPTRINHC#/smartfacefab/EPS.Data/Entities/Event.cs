using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPS.Data.Entities
{
    public partial class Event
    {
        public System.Guid EventId { get; set; }
        public Nullable<System.Guid> PersonId { get; set; }
        //public Nullable<System.Guid> SimilarPersonId { get; set; }
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
        public Nullable<double> Quality { get; set; }
        public Nullable<System.DateTime> SyncDatetime { get; set; }
        public string ErrorCode { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> CompId { get; set; }
        public int? AreaId { get; set; }
        public int? PersonType { get; set; }        
        public Nullable<bool> IsCheckNoti { get; set; }
        //[ForeignKey("PersonId")]
        //public virtual Person Person { get; set; }
        [ForeignKey("MachineId")]
        public virtual Machine Machine { get; set; }
        public string Note { get; set; }
        [ForeignKey("AreaId")]
        public virtual Areas Areas { get; set; }

    }
}

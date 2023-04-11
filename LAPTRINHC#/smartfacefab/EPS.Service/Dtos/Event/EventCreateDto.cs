using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Event
{
    public class EventCreateDto
    {
        public Guid EventId { get; set; }
        public Nullable<Guid> PersonId { get; set; }
        //public Nullable<Guid> SimilarPersonId { get; set; }
        //public Nullable<double> FaceImage { get; set; }
        public string FacePath { get; set; }
        public Nullable<int> DeptId { get; set; }
        public Nullable<int> AreaId { get; set; }
        public Nullable<int> CompId { get; set; }
        public string DeptName { get; set; }
        public string FeaturePath { get; set; }
        public Nullable<int> MachineId { get; set; }
        public Nullable<System.DateTime> AccessDate { get; set; }
        public Nullable<System.DateTime> AccessTime { get; set; }
        public string AccessDateDayFormat { get; set; }
        public string AccessDateTimeFormat { get; set; }
        public string AccessType { get; set; }
        public Nullable<double> Temperature { get; set; }
        public Nullable<double> TemperatureThreshold { get; set; }
        public string TemperatureFormat { get; set; }
        public Nullable<int> Gender { get; set; }
        public Nullable<int> Age { get; set; }
        public Nullable<int> WearMask { get; set; }
        public Nullable<double> ScoreMatch { get; set; }
        public Nullable<double> Quality { get; set; }
        public string ScoreMatchFormat { get; set; }
        public Nullable<System.DateTime> SyncDatetime { get; set; }
        public string ErrorCode { get; set; }
        public Nullable<int> Status { get; set; }
        public string Machine { get; set; }
        public string PersonCode { get; set; }
        public string PersonName { get; set; }
        public string FileData { get; set; }

        public Nullable<bool> IsCheckNoti { get; set; }

        public Nullable<int> PersonType { get; set; }

        public string Username { get; set; }
    }
}

using System;


namespace EPS.Service.Dtos.Event
{
    public class EventDetailDto
    {
        public System.Guid EventId { get; set; }
        public Nullable<System.Guid> PersonId { get; set; }
        public Nullable<System.Guid> FaceId { get; set; }
        public Nullable<System.Guid> FingerId { get; set; }
        public Nullable<System.Guid> CardId { get; set; }
        public string CardNo { get; set; }
        //public Nullable<double> FaceImage { get; set; }
        public string FacePath { get; set; }
        public string Address { get; set; }
        public Nullable<int> DeptId { get; set; }
        public string DepName { get; set; }
        public string FeaturePath { get; set; }
        public Nullable<int> MachineId { get; set; }
        public Nullable<System.DateTime> AccessDate { get; set; }
        public Nullable<System.DateTime> AccessTime { get; set; }
        public string AccessDateDayFormat { get; set; }
        public string AccessDateTimeFormat { get; set; }
        public string AccessType { get; set; }
        public string BirthdayStr { get; set; }
        public string GenderStr { get; set; }
        public Nullable<double> Temperature { get; set; }
        public Nullable<double> TemperatureThreshold { get; set; }
        public string TemperatureFormat { get; set; }
        public Nullable<int> Gender { get; set; }
        public Nullable<int> Age { get; set; }
        public Nullable<double> ScoreMatch { get; set; }
        public string ScoreMatchFormat { get; set; }
        public Nullable<System.DateTime> SyncDatetime { get; set; }
        public string ErrorCode { get; set; }
        public Nullable<int> Status { get; set; }
        public string Machine { get; set; }
        public string PersonCode { get; set; }
        public string PersonName { get; set; }
        public string StatusName { get; set; }
        public string ErrorCodeName { get; set; }
        public string WearMaskName { get; set; }
        public int CompId { get; set; }
        public string Value { get; set; }
        public string EmloyeeOther { get; set; }
        public Nullable<int> DeptIdOther { get; set; }
        public Nullable<int> WearMask { get; set; }
        public string PersonCodeOther { get; set; }
        public string PersonNameOther { get; set; }
        public string getFaceUrl { get; set; }

        public bool IsHighTemp { get; set; }

        public Nullable<bool> IsCheckNoti { get; set; }

        public Nullable<int> PersonType { get; set; }

        public string PersonTypeStr { get; set; }
        public string VaccineStr { get; set; }
        public Nullable<int> Vaccine { get; set; }
        public string PhoneNumber { get; set; }
        public string AreaName { get; set; }
        public DateTime? Birthday { get; set; }
        public string Email { get; set; }

        public string Note { get; set; }

        public PCCovidHTTInfo pcCovidInfo { get; set; }
        public Nullable<bool> isCheckPCCovid { get; set; }
        public Nullable<int> NumberOfTimes { get; set; }
    }
}

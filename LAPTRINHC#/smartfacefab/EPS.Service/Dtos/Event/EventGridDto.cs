using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Event
{
    public class EventGridDto
    {
        public System.Guid EventId { get; set; }
        public Nullable<System.Guid> PersonId { get; set; }
        public Nullable<System.Guid> FaceId { get; set; }
        public Nullable<System.Guid> FingerId { get; set; }
        public Nullable<int> DeptId { get; set; }
        public string DeptName { get; set; }
        public Nullable<int> CompId { get; set; }
        public Nullable<int> GroupId { get; set; }
        public string PersonCode { get; set; }
        public string ErrorCodeName { get; set; }
        public string Value { get; set; }
        public Nullable<Guid> PersonIdOther { get; set; }
        public Nullable<int> PersonTypeOther { get; set; }
        public string PersonCodeOther { get; set; }
        public string PersonNameOther { get; set; }
        public Nullable<System.Guid> CardId { get; set; }
        public string CardNo { get; set; }
        public string FacePath { get; set; }
        public string FeaturePath { get; set; }
        public Nullable<int> MachineId { get; set; }
        public Nullable<System.DateTime> AccessDate { get; set; }
        public Nullable<System.DateTime> AccessTime { get; set; }
        public string AccessDateDayFormat { get; set; }
        public string AccessDateTimeFormat { get; set; }
        public string AccessType { get; set; }
        public Nullable<double> Temperature { get; set; }
        public string TemperatureFormat { get; set; }
        public Nullable<int> Gender { get; set; }
        public string GenderStr { get; set; }
        public Nullable<int> Age { get; set; }
        public Nullable<double> ScoreMatch { get; set; }
        public Nullable<double> Quality { get; set; }
        public string ImageTemplate { get; set; }
        public string EmloyeeOther { get; set; }
        public Nullable<int> DeptIdOther { get; set; }
        public string ScoreMatchFormat { get; set; }
        public Nullable<System.DateTime> SyncDatetime { get; set; }
        public string ErrorCode { get; set; }
        public Nullable<int> Status { get; set; }
        public string Machine { get; set; }
        public string Person { get; set; }
        public string StatusName { get; set; }
        public string CompName { get; set; }
        public string AccessTypeName { get; set; }
        public Nullable<int> WearMask { get; set; }
        public string WearMaskName { get; set; }
        public string getFaceUrl { get; set; }
        public bool IsHighTemp { get; set; }
        public Nullable<double> TemperatureThreshold { get; set; }
        public Nullable<bool> IsCheckNoti { get; set; }
        public Nullable<int> PersonType { get; set; }
        public string PersonTypeStr { get; set; }
        public string PersonPosition { get; set; }
        public string VaccineStr { get; set; }
        public Nullable<int> Vaccine { get; set; }
        public Nullable<int> CompType { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
        public PCCovidHTTInfo pcCovidInfo;
        public int? AreaId { get; set; }
        public string AreaName { get; set; }
        public string HiddenParentField { get; set; }
        public string PersonPhoneNumber { get; set; }
        public Nullable<int> NumberOfTimes { get; set; }
        public string HiddenParentCompanyField { get; set; }
        public int? AmountPeople { get; set; }
        public DateTime? TimeOrder { get; set; }
        public string PhoneOrder { get; set; }
        public DateTime? DateCreate { get; set; }
        public string DepName { get; set; }
        public string DepCode { get; set; }
    }
}

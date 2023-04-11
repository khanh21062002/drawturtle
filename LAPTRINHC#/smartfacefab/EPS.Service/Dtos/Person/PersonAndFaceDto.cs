using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Person
{
    public class PersonAndFaceDto
    {
        public Guid PersonId { get; set; }
        public string PersonCode { get; set; }
        public string FullName { get; set; }
        public Nullable<int> CompId { get; set; }
        public Nullable<int> DeptId { get; set; }
        public string Position { get; set; }
        public string JobDuties { get; set; }
        public Nullable<int> PersonType { get; set; }
        public Nullable<int> CompType { get; set; }
        public Nullable<int> Status { get; set; }
        public bool IsDelete { get; set; }
        public string FaceUrl { get; set; }
        public string FacePath { get; set; }
        public string FeaturePath { get; set; }
        public string Passport { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Birthday { get; set; }

        public PersonAndFaceDto() { }

        public PersonAndFaceDto(Guid PersonId, string PersonCode, string FullName, int CompId, int DeptId, string Position,
            string JobDuties, int PersonType, int CompType, int Status, bool IsDelete, string FaceUrl, string FacePath, 
            string FeaturePath, string Passport, string PhoneNumber, DateTime Birthday)
        {
            this.PersonId = PersonId;
            this.PersonCode = PersonCode;
            this.FullName = FullName;
            this.CompId = CompId;
            this.DeptId = DeptId;
            this.Position = Position;
            this.JobDuties = JobDuties;
            this.PersonType = PersonType;
            this.CompType = CompType;
            this.Status = Status;
            this.IsDelete = IsDelete;
            this.FaceUrl = FaceUrl;
            this.FacePath = FacePath;
            this.FeaturePath = FeaturePath;
            this.FeaturePath = Passport;
            this.PhoneNumber = PhoneNumber;
            this.Birthday = Birthday;
        }

        public PersonAndFaceDto(Guid PersonId, string PersonCode, string FullName, int CompId, int PersonType, string FeaturePath)
        {
            this.PersonId = PersonId;
            this.PersonCode = PersonCode;
            this.FullName = FullName;
            this.CompId = CompId;
            this.PersonType = PersonType;
            this.FeaturePath = FeaturePath;
        }
    }
}

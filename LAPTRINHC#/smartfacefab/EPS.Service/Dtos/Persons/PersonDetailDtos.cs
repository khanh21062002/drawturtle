using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace EPS.Service.Dtos.Persons
{
    public class PersonDetailDtos
    {
        public Guid person_id { get; set; }
        public string person_code { get; set; }
        public string person_name { get; set; }
        [NonSerialized]
        public Nullable<DateTime> RegisterTime;
        [NonSerialized]
        public int GroupId;
        [NonSerialized]
        public string GroupName;
    }
}

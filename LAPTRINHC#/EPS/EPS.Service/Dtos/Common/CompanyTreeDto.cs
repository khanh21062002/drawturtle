using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace EPS.Service.Dtos.Common
{
    [DataContract]
    public class CompanyTreeDto
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Code { get; set; }
        [DataMember(Name = "label")]
        public string Name { get; set; }
        [DataMember]
        public int? ParentId { get; set; }
        [DataMember]
        public bool HasChildren { get; set; }
        [IgnoreDataMember]
        public CompanyTreeDto Parent { get; set; }
        [DataMember]
        public List<CompanyTreeDto> _children { get; set; }
        [DataMember]
        public List<CompanyTreeDto> children { get; set; }

        //public CompanyTreeDto()
        //{
        //    children = new List<CompanyTreeDto>();
        //    _children = new List<CompanyTreeDto>();
        //}
    }
}
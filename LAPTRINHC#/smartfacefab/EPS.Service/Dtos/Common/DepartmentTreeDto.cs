using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace EPS.Service.Dtos.Common
{
    [DataContract]
    public class DepartmentTreeDto
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
        public bool HasChildren { get { return Children.Any(); } }

        [DataMember]
        public bool IsDelete { get; set; }

        [DataMember]
        public string HiddenParentField { get; set; }

        public int? TreeCode { get; set; }
        public int? TreeLevelInt { get; set; }

        public int? ComId { get; set; }

        public int? Type { get; set; }

        [IgnoreDataMember]
        public DepartmentTreeDto Parent { get; set; }
        [DataMember]
        public List<DepartmentTreeDto> Children { get; set; }
        [DataMember]
        public List<DepartmentTreeDto> _children { get; set; }

        public DepartmentTreeDto()
        {
            Children = new List<DepartmentTreeDto>();
            _children = new List<DepartmentTreeDto>();
        }
    }
}
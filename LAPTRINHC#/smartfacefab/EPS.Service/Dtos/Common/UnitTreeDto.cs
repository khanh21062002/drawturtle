using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace EPS.Service.Dtos.Common
{
    [DataContract]
    public class UnitTreeDto
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
        [IgnoreDataMember]
        public UnitTreeDto Parent { get; set; }
        [DataMember]
        public List<UnitTreeDto> Children { get; set; }

        public UnitTreeDto()
        {
            Children = new List<UnitTreeDto>();
        }
    }
}
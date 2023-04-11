using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.FaceApiDto.RemovePerson
{
    public class RemovePersonDto
    {
        public string person_id { get; set; }
        public int collection_id { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Common
{
    public class SelectItemPersonId
    {
        public string PersonId { get; set; }
        public string Value { get { return PersonId; } set { PersonId = value; } }
        public string Text { get; set; }
    }
}

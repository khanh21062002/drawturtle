using EPS.Data.Entities;
using EPS.Service.Dtos.Face;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EPS.Service.Dtos.Person
{
    public class ShowImfomationDto
    {
        public Nullable<int> GroupId { get; set; }
        public string Time2 { get; set; }
        public string Time3 { get; set; }

        public string Time4 { get; set; }

        public string Time5 { get; set; }
        public string Time6 { get; set; }

        public string Time8 { get; set; }

        public string Time7 { get; set; }

        public string DeviceName { get; set; }
    }
}

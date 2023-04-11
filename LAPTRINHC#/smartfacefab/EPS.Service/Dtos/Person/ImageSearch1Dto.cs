using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Person
{
    public class ImageSearch1Dto
    {
        public int gender   { get; set; }
        public int age { get; set; }
        public int wearMask { get; set; }
        public int wearGlass    { get; set; }
        public int liveness { get; set; }

        public List<int> face_rectangle { get; set; }
    }
}

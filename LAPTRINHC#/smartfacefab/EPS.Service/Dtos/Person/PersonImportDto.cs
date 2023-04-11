using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Person
{
    public class PersonImportDto
    {
        public string FileName { get; set; }
        public string FileData { get; set; }
        public string Lang { get; set; }
    }
}

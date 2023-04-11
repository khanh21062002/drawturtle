using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EPS.Service.Dtos.Collection
{
    public class CollectionRequestCreateDto
    {
        public int collection_id { get; set; }
        [Required]
        [MaxLength(250)]
        public string collection_name { get; set; }
        [Required]
        [MaxLength(50)]
        public string collection_code { get; set; }
        public int status { get; set; }
        public bool is_delete { get; set; }
        public int? start { get; set; }
        public int? end { get; set; }
        public int userId { get; set; }
    }
}

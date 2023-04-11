using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Menu
{
    public class MenuCreateDto
    {
        public int Id { get; set; }
        public int? Type { get; set; }
        public string Name { get; set; }
        public string SetFoodId { get; set; }
        public decimal? Price { get; set; }
        public string Note { get; set; }
        public int? CompId { get; set; }
        public bool? IsDelete { get; set; }
    }
}

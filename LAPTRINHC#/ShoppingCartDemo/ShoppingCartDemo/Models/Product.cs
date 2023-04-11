using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCartDemo.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập một giá trị")]
        public string Name { get; set; }

        public string Slug { get; set; }

        [Required, MinLength(4,ErrorMessage = "Độ dài nhỏ nhất là 2")]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Vui lòng nhập một giá trị")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }

        public long CategoryID { get; set; }
        public Category Category { get; set; }
        public string Image { get; set; }
    }
}

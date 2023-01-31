using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Theo_Dõi_Chi_Phí.Models
{
    public class Category
    {
        [Key]
        public int Số_danh_mục { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "Title is required.")]
        public string Tiêu_đề { get; set; }

        [Column(TypeName = "nvarchar(5)")]
        public string Biểu_tượng { get; set; } = "";

        [Column(TypeName = "nvarchar(10)")]
        public string Loại { get; set; } = "Expense";

        [NotMapped]
        public string? TitleWithIcon
        {
            get
            {
                return this.Biểu_tượng + " " + this.Tiêu_đề;
            }
        }
    }
}

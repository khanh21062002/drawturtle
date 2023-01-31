using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Theo_Doi_Chi_Phi.Models
{
    public class Category
    {
        [Key]
        public int So_Danh_Muc { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Required(ErrorMessage = "Title is required.")]
        public string Tieu_De { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string Bieu_Tuong { get; set; } = "";

        [Column(TypeName = "nvarchar(20)")]
        public string Loai { get; set; } = "Chi phí";

        [NotMapped]
        public string? TitleWithIcon
        {
            get
            {
                return this.Bieu_Tuong + " " + this.Tieu_De;
            }
        }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Theo_Doi_Chi_Phi.Models
{
    public class Transaction
    {
        [Key]
        public int Id_Giao_Dich { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Hãy chọn một danh mục.")]
        public int So_Danh_Muc { get; set; }
        public Category? Category { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Số tiền phải lớn hơn 0.")]
        public int Gia_Tri { get; set; }

        [Column(TypeName = "nvarchar(75)")]
        public string? Ghi_Chu { get; set; }

        public DateTime Thoi_gian { get; set; } = DateTime.Now;

        [NotMapped]
        public string? CategoryTitleWithIcon
        {
            get
            {
                return Category == null ? "" : Category.Bieu_Tuong + " " + Category.Tieu_De;
            }
        }

        [NotMapped]
        public string? FormattedAmount
        {
            get
            {
                return ((Category == null || Category.Loai == "Chi phí") ? "- " : "+ ") + Gia_Tri.ToString("C0");
            }
        }

    }
}
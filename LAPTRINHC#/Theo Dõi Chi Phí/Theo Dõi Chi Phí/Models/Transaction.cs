using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Theo_Dõi_Chi_Phí.Models
{
    public class Transaction
    {
        [Key]
        public int Mã_giao_dịch { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Hãy chọn một danh mục.")]
        public int Số_danh_mục { get; set; }
        public Category? Category { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Số tiền phải lớn hơn 0.")]
        public int Giá_trị { get; set; }

        [Column(TypeName = "nvarchar(75)")]
        public string? Ghi_chú { get; set; }

        public DateTime Thời_gian { get; set; } = DateTime.Now;

        [NotMapped]
        public string? CategoryTitleWithIcon
        {
            get
            {
                return Category == null ? "" : Category.Biểu_tượng + " " + Category.Tiêu_đề;
            }
        }

        [NotMapped]
        public string? FormattedAmount
        {
            get
            {
                return ((Category == null || Category.Loại == "Expense") ? "- " : "+ ") + Giá_trị.ToString("C0");
            }
        }
    }
}

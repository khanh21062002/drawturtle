using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankTransactions.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Account Number")]
        [Required(ErrorMessage = "This field is required")]
        //[Required(ErrorMessage = " This field must enter a number. ")]
        [MaxLength(12,ErrorMessage ="Maximum 12 characters")]
        public string AccountNumber { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Beneficiary Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required.")]
        public string BeneficiaryName { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Bank Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required.")]
        public string BankName { get; set; }

        [Column(TypeName = "nvarchar(11)")]
        [DisplayName("SWIFTCode")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required.")]
        [MaxLength(11, ErrorMessage = "Maximum 11 characters")]
        public string SWIFTCode { get; set; }

        public int Amount { get; set; }
        public DateTime date { get; set; }
    }
}

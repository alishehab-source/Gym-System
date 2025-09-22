using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gym_System.Models
{
    public class Payment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Amount Is Required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount Must Be Greater Than Zero")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Payment Date Is Required")]
        [DataType(DataType.Date)]
        public DateTime PaymentDate { get; set; }

        public string? Note { get; set; }

        [Required(ErrorMessage = "Member Is Required")]
        public int MemberId { get; set; }
        public Member? Member { get; set; }
    }
}

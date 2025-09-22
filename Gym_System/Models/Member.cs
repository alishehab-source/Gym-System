using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gym_System.Models
{
    public class Member
    {


        public int Id { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        [StringLength(100)]
        public string FullName { get; set; }

        [StringLength(10)]
        public string? Gender { get; set; }

        [Phone(ErrorMessage = "Invalid phone number")]
        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "Subscription price is required")]
        [Range(0, double.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SubscriptionPrice { get; set; }

        [Required(ErrorMessage = "Amount paid is required")]
        [Range(0, double.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPaid { get; set; }

        [NotMapped]
        public decimal AmountDue => SubscriptionPrice - TotalPaid;

        public bool IsActive { get; set; } = true;

        [NotMapped]
        public int SessionsUsed => MemberSessions?.Count ?? 0;

        [NotMapped]
        public int SessionsRemaining => Subscription?.NumberOfSession - SessionsUsed ?? 0;

        public ICollection<Payment>? Payments { get; set; }

        public ICollection<MemberSession>? MemberSessions { get; set; } = new List<MemberSession>();

        [Required(ErrorMessage = "Subscription is required")]
        public int SubscriptionId { get; set; }
        public Subscription? Subscription { get; set; }


    }
}
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gym_System.Models
{
    public class Subscription
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Subscription Name Is Required")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Duration Is Required")]
        [Range(1, 365, ErrorMessage = "Duration Must Be At Least 1 Day.")]
        public int DurationInDay { get; set; }

        [Required(ErrorMessage = "Price Is Required")]
        [Range(0, double.MaxValue, ErrorMessage = "Price Must Be a Positive Value")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Range(0, 365, ErrorMessage = "Number Of Session must be Between 0 and 365")]
        public int? NumberOfSession { get; set; }

        public bool IsPrivate { get; set; }
        public bool InVitationRequired { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Member>? Members { get; set; } = new List<Member>();
    }
}

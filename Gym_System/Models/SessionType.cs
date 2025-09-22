using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gym_System.Models
{
    public class SessionType
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "The Price is The Postive Number.")]
        public decimal Price { get; set; }
        public ICollection<VisitorSession> VisitorSessions { get; set; }
    }
}

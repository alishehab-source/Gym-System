using System.ComponentModel.DataAnnotations;

namespace Gym_System.Models
{
    public class MemberSession
    {
        public int Id { get; set; }

        [Display(Name = "Session Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Session date is required.")]
        public DateTime SessionDate { get; set; }

        [Display(Name = "Notes")]
        [MaxLength(250, ErrorMessage = "Notes cannot exceed 250 characters.")]
        public string? Note { get; set; }

        [Display(Name = "Member (Optional)")]
        public int? MemberId { get; set; }

        public virtual Member? Member { get; set; }
    }
}

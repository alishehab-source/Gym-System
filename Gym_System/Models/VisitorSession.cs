using System.ComponentModel.DataAnnotations;

namespace Gym_System.Models
{
    public class VisitorSession
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "SessionData is Required")]
        public DateTime SessionDate { get; set; }
        public String? Note { get; set; }
        //[Required(ErrorMessage = "SessionType Is Required")]
        public int? SessionTypeId { get; set; }
        public SessionType SessionType { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Gym_System.Models.ViewModel
{
    public class ChangePasswordViewModel

    {

        [Required]

        [DataType(DataType.Password)]

        public  string CurrentPassword { get; set; }=string.Empty;



        [Required]

        [DataType(DataType.Password)]

        public string NewPassWord { get; set; } = string.Empty;



        [DataType(DataType.Password)]

        [Compare("NewPassWord", ErrorMessage = "Passwords do not match.")]

        public string ConfirmNewPassword { get; set; } = string.Empty;



    }
}

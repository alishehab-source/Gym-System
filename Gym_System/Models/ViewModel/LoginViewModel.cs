using System.ComponentModel.DataAnnotations;

namespace Gym_System.Models.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "UaserName is Required")]
        public String UserName { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}

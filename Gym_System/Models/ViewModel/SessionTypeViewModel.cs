using System.ComponentModel.DataAnnotations;

namespace Gym_System.Models.ViewModel
{
    public class SessionTypeViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "The price must be a positive number.")]
        public decimal Price { get; set; }
    }
}

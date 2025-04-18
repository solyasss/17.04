using System.ComponentModel.DataAnnotations;

namespace _17._04.Models
{
    public class register_model
    {
        [Required(ErrorMessage = "user name is required")]
        public string? name { get; set; }

        [Required(ErrorMessage = "login is required")]
        public string? login { get; set; }

        [Required(ErrorMessage = "password is required")]
        [DataType(DataType.Password)]
        public string? password { get; set; }

        [Required(ErrorMessage = "password confirmation is required")]
        [Compare("password", ErrorMessage = "passwords do not match")]
        [DataType(DataType.Password)]
        public string? confirm_password { get; set; }
    }
}
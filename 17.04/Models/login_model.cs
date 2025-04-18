using System.ComponentModel.DataAnnotations;

namespace _17._04.Models
{
    public class login_model
    {
        [Required(ErrorMessage = "login is required")]
        public string? login { get; set; }

        [Required(ErrorMessage = "password is required")]
        [DataType(DataType.Password)]
        public string? password { get; set; }
    }
}
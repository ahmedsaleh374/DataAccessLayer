using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.ViewModels
{
    public class RegisterVM
    {
        [Required]
        [MaxLength(50,ErrorMessage ="max is 50")]
        public string firstName { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "max is 50")]
        public string LastName { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "max is 50")]
        public string UserName { get; set; }
        
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public bool isAgree { get; set; }
    }
}

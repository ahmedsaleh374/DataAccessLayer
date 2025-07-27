using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.ViewModels
{
    public class ForgetPasswordVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

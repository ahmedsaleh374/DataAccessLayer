using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.ViewModels
{
    public class UpdateUserVM
    {
        public string Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "max is 50")]
        public string UserName { get; set; }
    }
}

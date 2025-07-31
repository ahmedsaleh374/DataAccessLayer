using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.ViewModels
{
    public class CreateEditRoleVM
    {
        public string? Id { get; set; }
        [Required(ErrorMessage = "Role name is required")]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Role name must contain only letters and spaces")]
        public string Name { get; set; }
    }
}

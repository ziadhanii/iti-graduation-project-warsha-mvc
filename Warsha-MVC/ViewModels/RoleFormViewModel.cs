using System.ComponentModel.DataAnnotations;

namespace Warsha_MVC.ViewModels
{
    public class RoleFormViewModel
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }
    }
}

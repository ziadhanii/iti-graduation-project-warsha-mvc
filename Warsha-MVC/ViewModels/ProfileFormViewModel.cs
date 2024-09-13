
namespace Warsha_MVC.ViewModels
{
    public class ProfileFormViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(100, ErrorMessage = "First Name must be between {2} and {1} characters long.", MinimumLength = 3)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty; 

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(100, ErrorMessage = "Last Name must be between {2} and {1} characters long.", MinimumLength = 3)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty; 

        [Required(ErrorMessage = "User name is required.")]
        [StringLength(100, ErrorMessage = "User Name must be between {2} and {1} characters long.", MinimumLength = 3)]
        [Display(Name = "User Name")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty; 
    }
}

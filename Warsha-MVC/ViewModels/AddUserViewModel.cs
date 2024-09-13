
namespace Warsha_MVC.ViewModels
{
    public class AddUserViewModel
    {
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

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, ErrorMessage = "Password must be between {2} and {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty; 

        [Required(ErrorMessage = "Confirm password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;


        [Display(Name = "Role")]
        public List<string> SelectedRoleIds { get; set; } = new List<string>();

        public IEnumerable<SelectListItem> Roles { get; set; } = Enumerable.Empty<SelectListItem>(); 
    }
}

namespace Warsha_MVC.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public string Username { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Required]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Display(Name = "Profile Picture")]
            public byte[] ProfilePicture { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            Username = await _userManager.GetUserNameAsync(user);

            Input = new InputModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = await _userManager.GetPhoneNumberAsync(user),
                ProfilePicture = user.ProfilePicture
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var updateResult = await UpdateUserAsync(user);
            if (!updateResult.Succeeded)
            {
                StatusMessage = $"Unexpected error when trying to update profile. Errors: {string.Join(", ", updateResult.Errors.Select(e => e.Description))}";
                return RedirectToPage();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

        private async Task<IdentityResult> UpdateUserAsync(ApplicationUser user)
        {
            IdentityResult result;

            if (Input.FirstName != user.FirstName)
            {
                user.FirstName = Input.FirstName;
                result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded) return result;
            }

            if (Input.LastName != user.LastName)
            {
                user.LastName = Input.LastName;
                result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded) return result;
            }

            if (Input.PhoneNumber != await _userManager.GetPhoneNumberAsync(user))
            {
                result = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!result.Succeeded) return result;
            }

            if (Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files.FirstOrDefault();
                if (file != null)
                {
                    if (!IsValidImage(file))
                    {
                        StatusMessage = "Invalid file type or size. Please upload a PNG or JPEG image with a maximum size of 5MB.";
                        return IdentityResult.Failed(new IdentityError { Description = StatusMessage });
                    }

                    using (var dataStream = new MemoryStream())
                    {
                        await file.CopyToAsync(dataStream);
                        user.ProfilePicture = dataStream.ToArray();
                    }

                    result = await _userManager.UpdateAsync(user);
                    if (!result.Succeeded) return result;
                }
            }

            return IdentityResult.Success;
        }

        private bool IsValidImage(IFormFile file)
        {
            const long maxFileSize = 5 * 1024 * 1024;
            if (file.Length > maxFileSize)
            {
                return false;
            }

            var allowedExtensions = new[] { ".png", ".jpg", ".jpeg" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(extension))
            {
                return false;
            }

            return true;
        }
    }
}

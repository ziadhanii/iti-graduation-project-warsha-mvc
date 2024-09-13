namespace Warsha_MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(RoleFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var roles = await _roleManager.Roles.ToListAsync();
                return View("Index", roles);
            }

            var roleExists = await _roleManager.RoleExistsAsync(model.Name);
            if (roleExists)
            {
                ModelState.AddModelError("Name", "Role already exists");
                var roles = await _roleManager.Roles.ToListAsync();
                return View("Index", roles);
            }

            await _roleManager.CreateAsync(new IdentityRole(model.Name.Trim()));

            TempData["SuccessMessage"] = "Role added successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}

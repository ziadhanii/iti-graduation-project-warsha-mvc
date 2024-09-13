namespace Warsha_MVC.Services
{
    public class UserServices : IUserServices
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserServices(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<SelectListItem>> GetRolesSelectList()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return roles.Select(role => new SelectListItem
            {
                Value = role.Id,
                Text = role.Name
            }).ToList();
        }
    }
}

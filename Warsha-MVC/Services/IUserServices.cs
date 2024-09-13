
namespace Warsha_MVC.Services
{
    public interface IUserServices
    {
        Task<IEnumerable<SelectListItem>> GetRolesSelectList();
    }
}

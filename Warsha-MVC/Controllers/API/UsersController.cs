
namespace Warsha_MVC.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UsersController> _logger;

        public UsersController(UserManager<ApplicationUser> userManager, ILogger<UsersController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning("Delete failed: User with ID {UserId} not found", userId);
                return NotFound(new { Message = "User not found" });
            }

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                _logger.LogError("Delete failed: Error deleting user with ID {UserId}. Errors: {Errors}", userId, string.Join(", ", result.Errors));
                return BadRequest(new { Message = "Error deleting user", Errors = result.Errors });
            }

            _logger.LogInformation("User with ID {UserId} deleted successfully", userId);
            return Ok(new { Message = "User deleted successfully" });
        }
    }
}

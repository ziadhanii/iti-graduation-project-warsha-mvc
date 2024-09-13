namespace Warsha_MVC.ViewModels
{
    public class UserViewModel
    {

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Role { get; set; }

    }
}

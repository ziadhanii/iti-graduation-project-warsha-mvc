namespace Warsha_MVC.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetStudentsSelectList()
        {
            return _context.Users
                .Select(u => new SelectListItem { Value = u.Id, Text = u.UserName })
                .OrderBy(u => u.Text)
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<SelectListItem> GetCoursesSelectList()
        {
            return _context.Courses
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Title })
                .OrderBy(c => c.Text)
                .AsNoTracking()
                .ToList();
        }

        public Course GetCourseById(int courseId)
        {
            return _context.Courses
                .AsNoTracking()  
                .FirstOrDefault(c => c.Id == courseId);
        }

    }
}

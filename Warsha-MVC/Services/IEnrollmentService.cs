namespace Warsha_MVC.Services
{
    public interface IEnrollmentService
    {
        IEnumerable<SelectListItem> GetStudentsSelectList();
        IEnumerable<SelectListItem> GetCoursesSelectList();
         Course GetCourseById(int courseId);
    }
}

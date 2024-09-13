
namespace Warsha_MVC.ViewModels
{
    public class EnrollmentFormViewModel
    {
        [Display(Name = "Course")]
        public int SelectedCourseId { get; set; } 

        public IEnumerable<SelectListItem> Courses { get; set; } = Enumerable.Empty<SelectListItem>();

        public Course? SelectedCourse { get; set; }

    }
}

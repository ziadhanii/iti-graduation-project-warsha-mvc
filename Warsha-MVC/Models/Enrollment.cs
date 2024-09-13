namespace Warsha_MVC.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public virtual ApplicationUser Student { get; set; }

        public virtual Course Course { get; set; }
    }
}

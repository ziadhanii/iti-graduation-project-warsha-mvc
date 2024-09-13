[Route("Enrollments")]
[ApiController]
public class EnrollmentsController : ControllerBase
{
    private readonly IEnrollmentService _enrollmentService;

    public EnrollmentsController(IEnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }

    [HttpGet("GetCourseDetails")]
    public IActionResult GetCourseDetails(int courseId)
    {
        var course = _enrollmentService.GetCourseById(courseId);
        if (course == null)
        {
            return NotFound();
        }

        var courseDetails = new
        {
            title = course.Title,
            description = course.Description,
            startDate = course.StartDate.ToString("yyyy-MM-dd"),
            endDate = course.EndDate.ToString("yyyy-MM-dd")
        };

        return Ok(courseDetails);
    }
}

namespace Warsha_MVC.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IEnrollmentService enrollmentService)
        {
            _context = context;
            _userManager = userManager;
            _enrollmentService = enrollmentService;

        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            if (isAdmin)
            {
                var enrollments = _context.Enrollments
                    .Include(e => e.Course)
                    .Include(e => e.Student)
                    .OrderBy(n => n.EnrollmentDate);
                return View(await enrollments.ToListAsync());
            }
            else
            {
                var userId = user.Id;
                var enrollments = _context.Enrollments
                    .Where(e => e.StudentId == userId)
                    .Include(e => e.Course)
                    .OrderBy(n => n.EnrollmentDate);
                return View(await enrollments.ToListAsync());
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        [HttpGet]
        public IActionResult Create()
        {
            EnrollmentFormViewModel viewModel = new()
            {
                Courses = _enrollmentService.GetCoursesSelectList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EnrollmentFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Courses = _enrollmentService.GetCoursesSelectList();
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            var studentId = user.Id;

            var enrollment = new Enrollment
            {
                EnrollmentDate = DateTime.UtcNow,
                StudentId = studentId,
                CourseId = model.SelectedCourseId
            };

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [Authorize (Roles ="Admin")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var enrollment = _context.Enrollments
                .Where(e => e.Id == id)
                .Include(e => e.Course) 
                .FirstOrDefault();

            if (enrollment == null)
                return NotFound();

            EditenrollmentFormViewModel viewModel = new()
            {
                SelectedCourseId = enrollment.CourseId,
                Courses = _enrollmentService.GetCoursesSelectList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditenrollmentFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Courses = _enrollmentService.GetCoursesSelectList();
                return View(model);
            }

            var enrollment = await _context.Enrollments.FindAsync(model.Id);

            if (enrollment == null)
                return NotFound();

            enrollment.CourseId = model.SelectedCourseId;

            _context.Enrollments.Update(enrollment);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }






        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (enrollment == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            if (isAdmin)
            {
                ViewBag.CanCancel = true; 
            }
            else
            {
                var courseStartDate = enrollment.Course.StartDate;
                ViewBag.CanCancel = courseStartDate >= DateTime.Now.AddDays(2);
            }

            return View(enrollment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);

            if (enrollment == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var user = await _userManager.GetUserAsync(User);
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            if (isAdmin)
            {
                _context.Enrollments.Remove(enrollment);
                await _context.SaveChangesAsync();
            }
            else
            {
                var courseStartDate = enrollment.Course.StartDate;
                var canCancel = courseStartDate >= DateTime.Now.AddDays(2);

                if (canCancel)
                {
                    _context.Enrollments.Remove(enrollment);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction(nameof(Index));
        }

        private bool EnrollmentExists(int id)
        {

            return _context.Enrollments.Any(e => e.Id == id);
        }



    }
}

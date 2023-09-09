using KorsatkoApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KorsatkoApp.Controllers {
	public class MyCoursesController : Controller {
		private readonly ApplicationDbContext _context;

		public MyCoursesController(ApplicationDbContext context) {
			_context = context;
		}
		public IActionResult Index() {
			var enrolls = _context.Enrollments
				.Include(e => e.student)
				.Include(e => e.session)
				.ThenInclude(e => e.course)
				.Include(e => e.session)
				.ThenInclude(e => e.instructor)
				.Where(e => e.student.Email == User.Identity.Name).ToList();
			return View(enrolls);
		}
		public async Task<IActionResult> Details(string id) {
			if (id == null || _context.Enrollments == null) {
				return NotFound();
			}
			var enrollment = await _context.Enrollments
			.FirstOrDefaultAsync(m => m.EnrollmentNumber == id);


			var session = _context.Sessions
				.Include(s => s.course)
                .Include(s => s.instructor)
                .Include(s => s.Enrollments)
                .ThenInclude(s => s.student)
                .Where(s => s.Enrollments.Contains(enrollment)).First();




			if (enrollment == null) {
				return NotFound();
			}

			return View(session);
		}
	}
}

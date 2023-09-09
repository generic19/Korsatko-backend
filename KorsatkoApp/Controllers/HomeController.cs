using System.Diagnostics;
using KorsatkoApp.Areas.Admin.Models;
using KorsatkoApp.Data;
using KorsatkoApp.Models;
using KorsatkoApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;

namespace KorsatkoApp.Controllers {
    public class HomeController : Controller {

		private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context) {
            _context = context;
        }
 
        public async Task<IActionResult> Index() {
            IEnumerable<Course> _courses=_context.Courses.ToList();
			IEnumerable<Instructor> _instructors=_context.Instructors.ToList();
            var model = new HomeViewModel() {
                courses = _courses,
                instructors = _instructors
			};
            return View(model);
        }
        public async Task<IActionResult> CourseDetails(int id) {
			if (id == null || _context.Instructors == null) {
				return NotFound();
			}
			var course = await _context.Courses
				.FirstOrDefaultAsync(m => m.Id == id);

			if (course == null) {
				return NotFound();
			}
			return View(course);
        }
        public async Task <IActionResult> InstructorDetails(int id) {
			if (id == null || _context.Instructors == null) {
				return NotFound();
			}
			var instructor = await _context.Instructors
				.FirstOrDefaultAsync(m => m.Id == id);

			if (instructor == null) {
				return NotFound();
			}
			return View(instructor);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
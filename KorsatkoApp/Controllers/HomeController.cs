using System.Diagnostics;
using KorsatkoApp.Data;
using KorsatkoApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace KorsatkoApp.Controllers {
    public class HomeController : Controller {

		private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context) {
            _context = context;
        }

        public IActionResult Index() {
            List<Course> courses=_context.Courses.ToList();
            return View(courses);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
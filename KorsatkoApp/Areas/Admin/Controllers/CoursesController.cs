using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KorsatkoApp.Data;
using KorsatkoApp.Models;
using NToastNotify;

namespace KorsatkoApp.Areas.Admin.Controllers {
    [Area("Admin")]
    public class CoursesController : Controller {
        private readonly ApplicationDbContext _context;
        private readonly IToastNotification toastNotification;
        public CoursesController(ApplicationDbContext context, IToastNotification toastNotification) {
            _context = context;
            this.toastNotification = toastNotification; //Marly: injecting toastNotification
        }

        // GET: Admin/Courses
        public async Task<IActionResult> Index() {
            return _context.Courses != null ?
                        View(await _context.Courses.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Courses'  is null.");
        }

        // GET: Admin/Courses/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null || _context.Courses == null) {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null) {
                return NotFound();
            }

            return View(course);
        }

        // GET: Admin/Courses/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Admin/Courses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Prerequisites,Picture,Price,AddedOn")] Course course) {
            if (ModelState.IsValid) {
                _context.Add(course);
                await _context.SaveChangesAsync();
                toastNotification.AddSuccessToastMessage("تمت اضافة الكورس بنجاح"); //Marly: notification
                return RedirectToAction(nameof(Index));
            }
            toastNotification.AddErrorToastMessage("برجاء التأكد من صحة البيانات");
            return View(course);
        }

        // GET: Admin/Courses/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null || _context.Courses == null) {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null) {
                return NotFound();
            }
            return View(course);
        }

        // POST: Admin/Courses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Prerequisites,Picture,Price,AddedOn")] Course course) {
            if (id != course.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                    toastNotification.AddSuccessToastMessage("تم تعديل الكورس بنجاح");//Marly: notification
                } catch (DbUpdateConcurrencyException) {
                    if (!CourseExists(course.Id)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Admin/Courses/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null || _context.Courses == null) {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null) {
                return NotFound();
            }

            return View(course);
        }

        // POST: Admin/Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (_context.Courses == null) {
                return Problem("Entity set 'ApplicationDbContext.Courses'  is null.");
            }
            var course = await _context.Courses.FindAsync(id);
            if (course != null) {
                _context.Courses.Remove(course);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id) {
            return (_context.Courses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

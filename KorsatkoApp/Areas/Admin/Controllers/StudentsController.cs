using ClosedXML.Excel;
using System.Data;
using KorsatkoApp.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace KorsatkoApp.Areas.Admin.Controllers {
	[Authorize(Roles = "Admin")]
	[Area("Admin")]
    public class StudentsController : Controller {
        private readonly UserManager<Student> _userManager;
        private readonly IToastNotification _toastNotification;

        public StudentsController(UserManager<Student> userManager, IToastNotification toastNotification) {
            _toastNotification = toastNotification;
            _userManager = userManager;
        }

        public IActionResult Index() {
            var Users = _userManager.Users.ToList();
            return View(Users);
        }    
      
        public async Task<IActionResult> Details(string? id) {
            if (id == null || _userManager.Users == null) {
                return NotFound();
            }
            var student = await _userManager.Users
                .FirstOrDefaultAsync(m => m.Id.Equals(id));
            if (student == null) {
                return NotFound();
            }
            return View(student);
        }
        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserName,UserPassword,BirthOfDate,Id,FullName,Gender,Email,PhoneNumber,NationalId,AddedOn")] Student student) {
            if (ModelState.IsValid) {
                await _userManager.CreateAsync(student);
                //   await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }
        public async Task<IActionResult> Edit(string? id) {
            if (id == null || _userManager.Users == null) {
                return NotFound();
            }

            var student = await _userManager.FindByIdAsync(id);
            if (student == null) {
                return NotFound();
            }
            return View(student);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserName,UserPassword,BirthOfDate,Id,FullName,Gender,Email,PhoneNumber,NationalId,AddedOn")] Student student) {
            if (!id.Equals(student.Id)) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    await _userManager.UpdateAsync(student);
                    //                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!StudentExists(student.Id)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }
        public async Task<IActionResult> Delete(string? id) {
            if (id == null || _userManager.Users == null) {
                return NotFound();
            }

            var student = await _userManager.Users
                .FirstOrDefaultAsync(m => m.Id.Equals(id));
            if (student == null) {
                return NotFound();
            }

            return View(student);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id) {
            if (_userManager.Users == null) {
                return Problem("Entity set 'ApplicationDbContext.Students'  is null.");
            }
            var student = await _userManager.FindByIdAsync(id);
            if (student != null) {
                await _userManager.DeleteAsync(student);
                //_context.Students.Remove(student);
            }

            //  await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

		public async Task<FileResult> ExportInExcel() {
			var students = await _userManager.Users.ToListAsync();
			var fileName = "الطلاب.xlsx";
			return GenerateExcel(fileName, students);
		}
		private FileResult GenerateExcel(string fileName, IEnumerable<Student> students) {
			DataTable dataTable = new DataTable("الطلاب");
			dataTable.Columns.AddRange(new DataColumn[] {
				new DataColumn("Id"),
				new DataColumn("الاسم بالكامل"),
				new DataColumn("النوع"),
				new DataColumn("تاريخ الميلاد"),
				new DataColumn("البريد الإلكتروني"),
				new DataColumn("الرقم القومي"),
				new DataColumn("تاريخ الإضافة")
			});
			foreach (var student in students) {
				dataTable.Rows.Add(student.Id, student.FullName
					, student.Gender, student.DateOfBirth, student.Email
				, student.NationalId, student.AddedOn);
			}
			using (XLWorkbook wb = new XLWorkbook()) {
				wb.Worksheets.Add(dataTable);
				using (MemoryStream stream = new MemoryStream()) {
					wb.SaveAs(stream);
					return File(stream.ToArray(),
					 "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
					 fileName);
				}
			}
		}

		private bool StudentExists(string id) {
            return (_userManager.Users?.Any(e => e.Id.Equals(id))).GetValueOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KorsatkoApp.Data;
using KorsatkoApp.Models;
using NToastNotify;
using KorsatkoApp.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using ClosedXML.Excel;

namespace KorsatkoApp.Areas.Admin.Controllers {
	[Authorize(Roles = "Admin")]
	[Area("Admin")]
	public class InstructorsController : Controller {

		private readonly ApplicationDbContext _context;
		private readonly IToastNotification toastNotification;// injecting toastNotification : basmalla & Rewan
		public InstructorsController(ApplicationDbContext context, IToastNotification toastNotification) {
			_context = context;
			this.toastNotification = toastNotification; // Rewan & Basmalla
		}

		// GET: Admin/Instructors
		public async Task<IActionResult> Index() {
			return _context.Instructors != null ?
						View(await _context.Instructors.ToListAsync()) :
						Problem("Entity set 'ApplicationDbContext.Instructors'  is null.");
		}

		// GET: Admin/Instructors/Details/5
		public async Task<IActionResult> Details(int? id) {
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

		// GET: Admin/Instructors/Create
		public IActionResult Create() {

			var model = new Instructor();
			model.Gender = 'm'; // Set default value if needed

			// Populate gender options
			ViewBag.GenderOptions = new SelectList(new[]
			{
		new { Value = "m", Text = "ذكر" },
		new { Value = "f", Text = "انثى" }
		}, "Value", "Text", model.Gender);

			return View(model);
		}

		// POST: Admin/Instructors/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("ExperienceYears,Qualifications,Id,FullName,Gender,Email,PhoneNumber,NationalId,AddedOn")] Instructor instructor) {
			if (ModelState.IsValid) {
				_context.Add(instructor);
				await _context.SaveChangesAsync();
				toastNotification.AddSuccessToastMessage("تم تسجيل البيانات بنجاح");//Basmalla & Rewan : notification 
				return RedirectToAction(nameof(Index));
			}
			toastNotification.AddErrorToastMessage("برجاء إدخال البيانات بشكل صحيح");// Basmalla & Rewan : notification
			return View(instructor);
		}

		// GET: Admin/Instructors/Edit/5
		public async Task<IActionResult> Edit(int? id) {	
			if (id == null || _context.Instructors == null) {
				return NotFound();
			}

			var instructor = await _context.Instructors.FindAsync(id);
			if (instructor == null) {
				return NotFound();
			}
			instructor.Gender = 'm'; // Set default value if needed

			// Populate gender options
			ViewBag.GenderOptions = new SelectList(new[]
			{
		new { Value = "m", Text = "ذكر" },
		new { Value = "f", Text = "انثى" }
		}, "Value", "Text", instructor.Gender);

			return View(instructor);
		}

		// POST: Admin/Instructors/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("ExperienceYears,Qualifications,Id,FullName,Gender,Email,PhoneNumber,NationalId,AddedOn")] Instructor instructor) {
			if (id != instructor.Id) {
				return NotFound();
			}

			if (ModelState.IsValid) {
				try {
					_context.Update(instructor);
					await _context.SaveChangesAsync();
					toastNotification.AddSuccessToastMessage("تم تعديل  البيانات بنجاح"); // Basmalla & Rewan : notification
				} catch (DbUpdateConcurrencyException) {
					if (!InstructorExists(instructor.Id)) {
						return NotFound();
					} else {
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(instructor);
		}

		// GET: Admin/Instructors/Delete/5
		public async Task<IActionResult> Delete(int? id) {
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

		// POST: Admin/Instructors/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id) {
			if (_context.Instructors == null) {
				return Problem("Entity set 'ApplicationDbContext.Instructors'  is null.");
			}
			var instructor = await _context.Instructors.FindAsync(id);
			if (instructor != null) {
				_context.Instructors.Remove(instructor);
			}

			await _context.SaveChangesAsync();
			toastNotification.AddSuccessToastMessage("تم حذف البيانات بنجاح");// Basmalla & Rewan : notification
			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<FileResult> ExportInExcel() {
			var instrctours = await _context.Instructors.ToListAsync();
			var fileName = "المدربين.xlsx";
			return GenerateExcel(fileName, instrctours);
		}
		private FileResult GenerateExcel(string fileName, IEnumerable<Instructor> instructors) {
			DataTable dataTable = new DataTable("المدربين");
			dataTable.Columns.AddRange(new DataColumn[] {
				new DataColumn("Id"),
				new DataColumn("الاسم بالكامل"),
				new DataColumn("البريد الإلكتروني"),
				new DataColumn("النوع"),
				new DataColumn("رقم الهاتف"),
				new DataColumn("المؤهلات"),
				new DataColumn("الرقم القومي"),
				new DataColumn("سنوات الخبرة")
			});
			foreach (var instructor in instructors) {
				dataTable.Rows.Add(instructor.Id, instructor.FullName
					, instructor.Email, instructor.Gender
					, instructor.PhoneNumber, instructor.Qualifications
					, instructor.NationalId, instructor.ExperienceYears);
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

		private bool InstructorExists(int id) {
			return (_context.Instructors?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}

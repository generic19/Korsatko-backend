using System.Linq;
using KorsatkoApp.Areas.Admin.Models;
using KorsatkoApp.Areas.Admin.ViewModels;
using KorsatkoApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClosedXML.Excel;

using NToastNotify;
using System.Data;

namespace KorsatkoApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class HomeController : Controller {
	private readonly IToastNotification _toastNotification;
	private readonly UserManager<Student> _userManager;
	private readonly ApplicationDbContext _context;

	public HomeController(IToastNotification toastNotification, UserManager<Student> userManager, ApplicationDbContext context) {
		_toastNotification = toastNotification;
		_userManager = userManager;
		_context = context;

	}
	[HttpGet]
	public async Task<IActionResult> Index() {
		var student = await _userManager.FindByEmailAsync(User.Identity?.Name!);
		ViewBag.studentCount = _context.Users.ToList().Count;
		ViewBag.courseCount = _context.Courses.ToList().Count;
		ViewBag.instructorsCount = _context.Instructors.ToList().Count;
		ViewBag.enrollmentCount = _context.Enrollments.ToList().Count;
		//Success
		_toastNotification.AddInfoToastMessage("مرحبا بك " + student.FullName);
		ViewBag.students = new SelectList(_userManager.Users, "Id", "FullName");
		ViewBag.courses = new SelectList(_context.Courses, "Id", "Name");
		//session
		var customValues = new List<SelectListItem>();
		var sessions = _context.Sessions.Include(e => e.course).Include(e => e.instructor).ToList();
		foreach (var s in sessions) {
			string sessionName = s.course.Name + " - " + s.instructor.FullName + " - " + s.Location;
			customValues.Add(new SelectListItem { Value = s.Id.ToString(), Text = sessionName });
		}
		var SessionselectList = new SelectList(customValues, "Value", "Text");
		ViewData["sessions"] = SessionselectList;
		//session

		return View();
	}
	[HttpPost]
	public async Task<IActionResult> Index(SearchViewModel searchModel) {
		//ViewBags========
		ViewBag.studentCount = _context.Users.ToList().Count;
		ViewBag.courseCount = _context.Courses.ToList().Count;
		ViewBag.instructorsCount = _context.Instructors.ToList().Count;
		ViewBag.enrollmentCount = _context.Enrollments.ToList().Count;

		ViewBag.students = new SelectList(_userManager.Users, "Id", "FullName");
		ViewBag.courses = new SelectList(_context.Courses, "Id", "Name");
		//session
		var customValues = new List<SelectListItem>();
		var sessions = _context.Sessions.Include(e => e.course).Include(e => e.instructor).ToList();
		foreach (var s in sessions) {
			string sessionName = s.course.Name + " - " + s.instructor.FullName + " - " + s.Location;
			customValues.Add(new SelectListItem { Value = s.Id.ToString(), Text = sessionName });
		}
		var SessionselectList = new SelectList(customValues, "Value", "Text");
		ViewData["sessions"] = SessionselectList;
		//ViewBags========

		List<Student> result = new();

		if (!String.IsNullOrWhiteSpace(searchModel.student)) {
			var student = await _context.Users.FirstOrDefaultAsync(x => x.Id == searchModel.student);
			result.Add(new Student() {
				FullName = student.FullName,
				Gender = student.Gender,
				PhoneNumber = student.PhoneNumber,
				NationalId = student.NationalId,
				DateOfBirth = student.DateOfBirth,
				Email = student.Email,
				AddedOn = student.AddedOn
			});
			ViewBag.studentResult = result;
			return View();
		}
		if (searchModel.course != null && searchModel.course != 0) {
			var enrollmentsInCourse = _context.Enrollments.Include(e => e.session).Include(e => e.student).Where(e => e.session.CourseId == searchModel.course);
			if (searchModel.gender == 'm') {
				enrollmentsInCourse = enrollmentsInCourse.Where(e => e.student.Gender == 'm');
			} else if (searchModel.gender == 'f') {
				enrollmentsInCourse = enrollmentsInCourse.Where(e => e.student.Gender == 'f');
			}
			if (searchModel.ageOlderThan != null && searchModel.ageOlderThan != 0) {
				enrollmentsInCourse = enrollmentsInCourse.Where(e => testAge(e.student, searchModel.ageOlderThan, searchModel.ageYoungerThan) == true);
			}
			foreach (var enroll in enrollmentsInCourse) {
				result.Add(new Student() {
					FullName = enroll.student.FullName,
					Gender = enroll.student.Gender,
					PhoneNumber = enroll.student.PhoneNumber,
					NationalId = enroll.student.NationalId,
					DateOfBirth = enroll.student.DateOfBirth,
					Email = enroll.student.Email,
					AddedOn = enroll.student.AddedOn
				});
			}
			ViewBag.studentResult = result;
			return View();
		}
		var res = _context.Users.ToList();
		if (searchModel.gender == 'm') res = res.Where(e => e.Gender == 'm').ToList();
		if (searchModel.gender == 'f') res = res.Where(e => e.Gender == 'f').ToList();
		if (searchModel.ageOlderThan != null && searchModel.ageYoungerThan != null && searchModel.ageOlderThan != 0 && searchModel.ageYoungerThan != 0) {
			res = res.Where(e => testAge(e, searchModel.ageOlderThan, searchModel.ageYoungerThan) == true).ToList();
		}
		ViewBag.studentResult = res;
		return View();
	}
	[HttpGet]
	public async Task<FileResult> ExportInExcel() {
		var courses = await _context.Courses.ToListAsync();
		var enrollments = await _context.Enrollments.ToListAsync();
		var instructors = await _context.Instructors.ToListAsync();
		var sessions = await _context.Sessions.ToListAsync();
		var students = await _context.Users.ToListAsync();


		var fileName = "قاعدة البيانات.xlsx";
		return GenerateExcel(fileName, instructors, courses, enrollments, students, sessions);
	}
	private FileResult GenerateExcel(string fileName,
		IEnumerable<Instructor> instructors, IEnumerable<Course> courses,
		IEnumerable<Enrollment> enrollments, IEnumerable<Student> students, IEnumerable<Session> sessions) {

		DataTable InstructorDataTable = new DataTable("المدربين");
		DataTable CoursesDataTable = new DataTable("الدورات");
		DataTable EnrollmentsDataTable = new DataTable("الإشتراكات");
		DataTable SessionsDataTable = new DataTable("المواعيد");
		DataTable StudentsDataTable = new DataTable("الطلاب");

		InstructorDataTable.Columns.AddRange(new DataColumn[] {
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
			InstructorDataTable.Rows.Add(instructor.Id, instructor.FullName
				, instructor.Email, instructor.Gender
				, instructor.PhoneNumber, instructor.Qualifications
				, instructor.NationalId, instructor.ExperienceYears);
		}

		CoursesDataTable.Columns.AddRange(new DataColumn[] {
				new DataColumn("Id"),
				new DataColumn("اسم الكورس"),
				new DataColumn("محتوى الكورس"),
				new DataColumn("متطلبات الكورس"),
				new DataColumn("سعر الكورس"),
				new DataColumn("تاريخ الإضافة")
			});
		foreach (var course in courses) {
			CoursesDataTable.Rows.Add(course.Id, course.Name
				, course.Description, course.Prerequisites, course.Price
			, course.AddedOn);
		}

		EnrollmentsDataTable.Columns.AddRange(new DataColumn[] {
				new DataColumn("Id"),
				new DataColumn("الطالب"),
				new DataColumn("الميعاد"),
				new DataColumn("حالة الدفع"),
				new DataColumn("تاريخ الإضافة")
			});
		foreach (var enrollment in enrollments) {
			var session = _context.Sessions.FirstOrDefault(e => e.Id == enrollment.SessionId);
			string sessionName = session.course.Name + " - "
				+ session.instructor.FullName + " - " + session.Location;
			EnrollmentsDataTable.Rows.Add(enrollment.EnrollmentNumber, enrollment.student.FullName
				, sessionName, enrollment.PaymentStatus
				, enrollment.AddedOn);
		}

		SessionsDataTable.Columns.AddRange(new DataColumn[] {
				new DataColumn("Id"),
				new DataColumn("المدرب"),
				new DataColumn("الكورس"),
				new DataColumn("المكان"),
				new DataColumn("تاريخ البداية"),
				new DataColumn("تاريخ النهاية"),
				new DataColumn ("معدل السعر"),
				new DataColumn ("الحد الأقصى"),
				new DataColumn("ميعاد البداية"),
				new DataColumn("ميعاد النهاية"),
				new DataColumn("هل متاح"),
				new DataColumn("تاريخ الإضافة")
			});
		foreach (var session in sessions) {
			SessionsDataTable.Rows.Add(session.Id, session.instructor.FullName, session.course.Name,
				session.Location, session.StartDate, session.EndDate, session.PriceRate,
				session.Limit, session.startTime, session.EndTime, session.IsAvailable,
				session.AddedOn);
		}

		StudentsDataTable.Columns.AddRange(new DataColumn[] {
				new DataColumn("Id"),
				new DataColumn("الاسم بالكامل"),
				new DataColumn("النوع"),
				new DataColumn("تاريخ الميلاد"),
				new DataColumn("البريد الإلكتروني"),
				new DataColumn("الرقم القومي"),
				new DataColumn("تاريخ الإضافة")
			});
		foreach (var student in students) {
			StudentsDataTable.Rows.Add(student.Id, student.FullName
				, student.Gender, student.DateOfBirth, student.Email
			, student.NationalId, student.AddedOn);
		}

		using (XLWorkbook wb = new XLWorkbook()) {
			wb.Worksheets.Add(StudentsDataTable);
			wb.Worksheets.Add(InstructorDataTable);
			wb.Worksheets.Add(EnrollmentsDataTable);
			wb.Worksheets.Add(SessionsDataTable);
			wb.Worksheets.Add(CoursesDataTable);

			using (MemoryStream stream = new MemoryStream()) {
				wb.SaveAs(stream);
				return File(stream.ToArray(),
				 "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
				 fileName);
			}
		}
	}
	public async Task<IActionResult> Reports() {
		return View();
	}



		public bool testAge(Student student, int Older, int Younger) {
		TimeSpan tsAge = DateTime.Now.Subtract(student.DateOfBirth);
		var age = new DateTime(tsAge.Ticks).Year - 1;
		if (age > Older && age <= Younger) {
			return true;
		}
		return false;
	}
}

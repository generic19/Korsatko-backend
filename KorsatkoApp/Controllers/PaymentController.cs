using KorsatkoApp.Areas.Admin.Models;
using KorsatkoApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NToastNotify;

namespace KorsatkoApp.Controllers {
	[Authorize]
	public class PaymentController : Controller {
		private readonly IToastNotification _toastNotification;
		private readonly UserManager<Student> _UserManager;
		private readonly ApplicationDbContext _context;

		public PaymentController(IToastNotification toastNotification, UserManager<Student> userManager, ApplicationDbContext context) {
			_toastNotification = toastNotification;
			_UserManager = userManager;
			_context = context;
		}

		public IActionResult Index() {
           string sessionid = HttpContext.Request.Query["sessionid"];
           string courseid = HttpContext.Request.Query["courseid"];

            ViewBag.sessionid = sessionid;
            ViewBag.courseid = courseid;
            return View();
		}


        public async Task<IActionResult> Confirm() {
           string sessionid = HttpContext.Request.Query["sessionid"];
           string courseid = HttpContext.Request.Query["courseid"];
			var student = await _UserManager.FindByEmailAsync(User.Identity.Name);
			var studentid = student.Id;
			Enrollment enrollment = new Enrollment() {
				PaymentStatus="تم الدفع",
				SessionId=int.Parse(sessionid),
				StudentId= studentid,
				EnrollmentNumber = Guid.NewGuid().ToString(),
				AddedOn = DateTime.Now
		};


			_context.Add(enrollment);
			await _context.SaveChangesAsync();
			_toastNotification.AddSuccessToastMessage("تمت اضافة اللإشتراك بنجاح");

			return RedirectToAction("Index", "MyCourses");
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KorsatkoApp.Controllers {
	[Authorize]
	public class PaymentController : Controller {
		public IActionResult Index() {
			return View();
		}
	}
}

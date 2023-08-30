using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace KorsatkoApp.Areas.Admin.Controllers {

    [Area("Admin")]
    public class HomeController : Controller {
        private readonly IToastNotification _toastNotification;
        public HomeController(IToastNotification toastNotification) {
            _toastNotification = toastNotification;
        }
        public IActionResult Index() {
            //Success
            _toastNotification.AddSuccessToastMessage("Welcome in arabic :D");
            return View();
        }

    }
}

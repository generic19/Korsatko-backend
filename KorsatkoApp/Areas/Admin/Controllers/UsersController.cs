using KorsatkoApp.Areas.Admin.Models;
using KorsatkoApp.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace KorsatkoApp.Areas.Admin.Controllers;
[Area("Admin")]
public class UsersController : Controller {
    private readonly UserManager<Student> _userManager;
    private readonly IToastNotification _toastNotification;

    public UsersController(UserManager<Student> userManager, IToastNotification toastNotification) {
        _userManager = userManager;
        _toastNotification = toastNotification;

    }
    public async Task<IActionResult> Index() {
        List<UsersViewModel> users = new List<UsersViewModel>();
        _userManager.Users.ToList().ForEach(u => {
            UsersViewModel user = new UsersViewModel();
            var roles = _userManager.GetRolesAsync(u).Result;
            string StringRoles = "";
            foreach (var role in roles) {
                StringRoles += role + " ";
            }
            user.FullName = u.FullName;
            user.Email = u.Email;
            user.Role = StringRoles;
            users.Add(user);
        });
        return View(users);
    }



    [HttpPost]
    public async Task<IActionResult> MakeAdmin(string Email) {
        var user = await _userManager.FindByEmailAsync(Email);
        if(await _userManager.IsInRoleAsync(user, "Admin")) {
            _toastNotification.AddErrorToastMessage($"{user.FullName} Is Already an Admin !");
        } else {
            await _userManager.AddToRoleAsync(user, "Admin");
            _toastNotification.AddSuccessToastMessage($"{user.FullName} Is Now an Admin ");
        }
        return RedirectToAction("Index");
    }
}

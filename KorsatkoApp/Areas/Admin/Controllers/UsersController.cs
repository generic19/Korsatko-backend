using ClosedXML.Excel;
using KorsatkoApp.Areas.Admin.Models;
using KorsatkoApp.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Data;

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


    public async Task<FileResult> ExportInExcel() {
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
        var students = await _userManager.Users.ToListAsync();
        var fileName = "المستخدمين.xlsx";
        return GenerateExcel(fileName, users);
    }
    private FileResult GenerateExcel(string fileName, List<UsersViewModel> users) {
        DataTable dataTable = new DataTable("الطلاب");
        dataTable.Columns.AddRange(new DataColumn[] {
                new DataColumn("الاسم بالكامل"),
                new DataColumn("البريد الإلكتروني"),
                new DataColumn("الدور في النظام"),
            });
        foreach (var user in users) {
            dataTable.Rows.Add(
                user.FullName,
                user.Email,
                user.Role
            );
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
}

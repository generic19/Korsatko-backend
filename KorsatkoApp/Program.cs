using KorsatkoApp.Areas.Admin.Models;
using KorsatkoApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
builder.Services.AddControllersWithViews().AddNToastNotifyNoty(new NToastNotify.NotyOptions() {
    ProgressBar = true,  // show the progress bar
    Timeout = 3500,      // notification will be disapear after 3500ms,
    Theme = "mint"      // Notify.Js theme name 
}); //Marly:configuring NToastNotify services
;
builder.Services.AddRazorPages();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");



builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    });


builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddDefaultIdentity<Student>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

//builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseMigrationsEndPoint();
} else {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "MyArea",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

// add roles
using (var scope = app.Services.CreateScope()) {
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "User" };
    foreach (var role in roles) {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }
}

// add admin accounts
using (var scope = app.Services.CreateScope()) {
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Student>>();
    string email = "mohamed@korsatko.com";
    string email2 = "mai@korsatko.com";
    string password = "Admin123#";
    string password2 = "Admin1234#";

    if (await userManager.FindByEmailAsync(email) == null) {
        var user = new Student();
        user.UserName = email;
        user.FullName = "محمد عيد";
        user.NationalId = "1234525255";
        user.Gender = 'm';
        user.DateOfBirth = DateTime.Now;
        user.PhoneNumber = "01242411345";
        user.Email = email;

        await userManager.CreateAsync(user, password);
        await userManager.AddToRoleAsync(user, "Admin");
    }
    if (await userManager.FindByEmailAsync(email2) == null) {
        var user = new Student();
        user.FullName = "مي جمال";
        user.UserName = email2;
        user.NationalId = "1234524214";
        user.Gender = 'f';
        user.DateOfBirth = DateTime.Now;
        user.PhoneNumber = "01124212345";
        user.Email = email2;

        await userManager.CreateAsync(user, password2);
        await userManager.AddToRoleAsync(user, "Admin");
    }
}

app.Run();

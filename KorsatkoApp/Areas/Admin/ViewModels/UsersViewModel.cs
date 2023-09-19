using System.ComponentModel.DataAnnotations;

namespace KorsatkoApp.Areas.Admin.ViewModels {
	public class UsersViewModel {
        [Key]
        [Display(Name ="الاسم بالكامل")]
        public string FullName { get; set; }
        [Display(Name ="البريد الإلكتروني")]
        public string Email { get; set; }
        public string Role { get; set; }
    }
}

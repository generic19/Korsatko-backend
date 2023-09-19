using System.ComponentModel.DataAnnotations;
using KorsatkoApp.Areas.Admin.Models;

namespace KorsatkoApp.Areas.Admin.ViewModels {
	public class SearchViewModel {
        [Display(Name="اسم الطالب")]
        public String student { get; set; }
        [Display(Name="النوع")]
        public char gender { get; set; }
        [Key]
        [Display(Name="اكبر من")]
        public int ageOlderThan { get; set; }
        [Display(Name="اصغر من")]
        public int ageYoungerThan { get; set; }
        [Display(Name="الكورسات المسجل بها")]
        public int course { get; set; }
        [Display(Name="المواعيد المسجل بها")]
        public int session { get; set; }
    }
}

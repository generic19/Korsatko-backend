using System.ComponentModel.DataAnnotations;
namespace KorsatkoApp.Models {
    public class Student : Person {
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public DateTime BirthOfDate { get; set; }
        public List<Enrolment> Enrolments { get; set; } = new();
    }
}

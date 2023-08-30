using System.ComponentModel.DataAnnotations;

namespace KorsatkoApp.Models {
    public class Instructor : Person {
        public int ExperienceYears { get; set; }
        public string? Qualifications { get; set; }
        public List<Session> Sessions { get; set; } = new();
    }
}


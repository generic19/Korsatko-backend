using System.ComponentModel.DataAnnotations;

namespace KorsatkoApp.Models {
    public class Enrolment {
        [Key]
        public int StudentId { get; set; }
        [Key]
        public int SessionId { get; set; }
        public String PaymentStatus { get; set; }
        public DateTime AddedOn { get; set; } = DateTime.Now;
        public Session session { get; set; } = null!;  //  Reference navigation property
        public Student student { get; set; } = null!; // Reference navigation property

    }
}

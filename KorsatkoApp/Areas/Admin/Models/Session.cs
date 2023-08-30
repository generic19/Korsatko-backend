using System.ComponentModel.DataAnnotations;

namespace KorsatkoApp.Models {
    public class Session {
        [Key]
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime startTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Location { get; set; }
        public int Limit { get; set; }
        public bool IsAvailable { get; set; }
        public float PriceRate { get; set; }
        public int CourseId { get; set; }
        public int InstructorId { get; set; } 
        public DateTime AddedOn { get; set; } = DateTime.Now;
        public List<Enrolment> Enrolments { get; set; } = new(); //Reference Navigation property
        public Course course { get; set; } = null!;//Reference Navigation property
        public Instructor instructor { set; get; } = null!; //Reference Navigation property


    }



}

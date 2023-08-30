using System.ComponentModel.DataAnnotations;

namespace KorsatkoApp.Models {
    public class Person {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "The FullName is Required")]
        public string FullName { get; set; }
        public char Gender { get; set; }
        [Required]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "National ID  is required")]
        public string NationalId { get; set; }
        public DateTime AddedOn { get; set; } = DateTime.Now;
    }
}

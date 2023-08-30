using System.ComponentModel.DataAnnotations;
namespace KorsatkoApp.Models;

public class Course {
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Prerequisites { get; set; }
    public string? Picture { get; set; }
    public double Price { get; set; }
    public List<Session> Sessions { get; } = new();
    public DateTime AddedOn { get; set; } = DateTime.Now;
}

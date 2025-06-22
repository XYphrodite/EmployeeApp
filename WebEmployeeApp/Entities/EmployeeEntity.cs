using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeApp.Server.Models;
public class Employee
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Firstname { get; set; }

    [Required]
    public string Surname { get; set; }

    public string? Lastname { get; set; }

    public DateTime? Birthday { get; set; }

    [Required]
    public short PositionId { get; set; }

    [ForeignKey("PositionId")]
    public Position Position { get; set; }

    [Required]
    public int Salary { get; set; }

    [Required]
    public bool IsActive { get; set; }
}





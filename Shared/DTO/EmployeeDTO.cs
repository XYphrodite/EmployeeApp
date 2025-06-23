
namespace Shared.DTO;

public class EmployeeDto
{
    public int Id { get; set; }
    public string Firstname { get; set; }
    public string Surname { get; set; }
    public string? Lastname { get; set; }
    public DateTime? Birthday { get; set; }
    public short PositionId { get; set; }
    public string? PositionName { get; set; }
    public int Salary { get; set; }
    public bool IsActive { get; set; }
}

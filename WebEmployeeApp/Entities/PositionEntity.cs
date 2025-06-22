
using System.ComponentModel.DataAnnotations;

public class Position
{
    [Key]
    public short Id { get; set; }

    [Required]
    public string PositionName { get; set; }
}
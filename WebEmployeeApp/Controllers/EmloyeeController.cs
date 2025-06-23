using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeApp.Server.Models;
using Shared.DTO;
using WebEmployeeApp.Services;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly AppDbContext _context;
    public EmployeeController(AppDbContext context) => _context = context;

    [HttpGet]
    public async Task<IEnumerable<EmployeeDto>> GetAll()
        => await _context.Employees.Include(e => e.Position)
            .Select(e => new EmployeeDto
            {
                Id = e.Id,
                Firstname = e.Firstname,
                Surname = e.Surname,
                PositionId = e.Position.Id,
                PositionName = e.Position.PositionName,
                IsActive = e.IsActive
            }).ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeDto>> Get(int id)
    {
        var emp = await _context.Employees.FindAsync(id);
        if (emp == null) return NotFound();
        return new EmployeeDto
        {
            Id = emp.Id,
            Firstname = emp.Firstname,
            Surname = emp.Surname,
            Lastname = emp.Lastname,
            Birthday = emp.Birthday,
            PositionId = emp.PositionId,
            Salary = emp.Salary,
            IsActive = emp.IsActive
        };
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] EmployeeDto dto)
    {
        var e = new Employee
        {
            Firstname = dto.Firstname,
            Surname = dto.Surname,
            Lastname = dto.Lastname,
            Birthday = dto.Birthday,
            PositionId = dto.PositionId,
            Salary = dto.Salary,
            IsActive = dto.IsActive
        };
        _context.Employees.Add(e);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = e.Id }, e);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, EmployeeDto dto)
    {
        if (id != dto.Id) return BadRequest();
        var e = await _context.Employees.FindAsync(id);
        if (e == null) return NotFound();
        e.Firstname = dto.Firstname;
        e.Surname = dto.Surname;
        e.Lastname = dto.Lastname;
        e.Birthday = dto.Birthday;
        e.PositionId = dto.PositionId;
        e.Salary = dto.Salary;
        e.IsActive = dto.IsActive;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var e = await _context.Employees.FindAsync(id);
        if (e == null) return NotFound();
        _context.Employees.Remove(e);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}

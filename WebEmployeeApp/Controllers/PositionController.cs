using EmployeeApp.Server.Data;
using EmployeeApp.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class PositionController : ControllerBase
{
    private readonly AppDbContext _context;
    public PositionController(AppDbContext context) => _context = context;

    [HttpGet]
    public async Task<IEnumerable<Position>> GetAll()
        => await _context.Positions.ToListAsync();
}

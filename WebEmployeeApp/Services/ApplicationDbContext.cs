using System.Collections.Generic;
using EmployeeApp.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace WebEmployeeApp.Services;

public class AppDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Position> Positions { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }
}

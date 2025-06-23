using EmployeeApp.Server.Data;
using EmployeeApp.Server.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WebEmployeeApp.Services;

public class AppDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Position> Positions { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();

        DbInitializer.Initialize(this);
    }
}

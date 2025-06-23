using System.Linq;
using EmployeeApp.Server.Models;
using WebEmployeeApp.Services;

namespace EmployeeApp.Server.Data;

public static class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        context.Database.EnsureCreated();

        if (!context.Positions.Any())
        {
            context.Positions.AddRange(
                new Position { PositionName = "Программист" },
                new Position { PositionName = "Юрист" },
                new Position { PositionName = "Бухгалтер" },
                new Position { PositionName = "Менеджер" },
                new Position { PositionName = "Директор" }
            );
            context.SaveChanges();
        }
    }
}

namespace WebEmployeeApp.Services;

public class GenericRepo
{
    private readonly AppDbContext context;

    public GenericRepo(AppDbContext context)
    {
        this.context = context;
    }
}

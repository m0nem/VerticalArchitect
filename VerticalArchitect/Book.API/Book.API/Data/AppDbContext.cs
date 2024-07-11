using Microsoft.EntityFrameworkCore;
namespace Book.API.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        public DbSet<Entities.Book> books => Set<Entities.Book>();
    }
}

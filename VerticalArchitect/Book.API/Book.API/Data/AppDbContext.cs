using Microsoft.EntityFrameworkCore;
namespace Book.API.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Entities.Book> Books => Set<Entities.Book>();
    }
}

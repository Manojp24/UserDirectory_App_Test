using Microsoft.EntityFrameworkCore;
using UserDirectory_app.Model;

namespace UserDirectory_app.EF
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
    }
}

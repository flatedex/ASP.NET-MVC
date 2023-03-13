using Microsoft.EntityFrameworkCore;
using WebArchiver.Models;

namespace WebArchiver.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            
        }
        public DbSet<User> User { get; set; }
        public DbSet<LastFile> LastFile { get; set; }
    }
}

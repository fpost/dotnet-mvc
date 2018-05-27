using Microsoft.EntityFrameworkCore;

namespace LoginApp.Models
{
    public class LoginAppDbContext : DbContext
    {
        public LoginAppDbContext (DbContextOptions<LoginAppDbContext> options)
            : base(options)
        {
        }

        public DbSet<LoginApp.Models.User> User { get; set; }
        public DbSet<LoginApp.Models.Regex> Regex { get; set; }
    }
}
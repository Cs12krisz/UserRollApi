using Microsoft.EntityFrameworkCore;

namespace UserRollAPI.Models
{
    public class UserRoleDbContext : DbContext
    {
        public UserRoleDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> User { get; set; }

        public DbSet<Role> Role { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;UID=root;database=userroles;password=''");
        }
    }
}

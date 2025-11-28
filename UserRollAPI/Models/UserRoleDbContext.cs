using Microsoft.EntityFrameworkCore;

namespace UserRollAPI.Models
{
    public class UserRoleDbContext : DbContext
    {
        public UserRoleDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> User { get; set; }

        public DbSet<Role> Role { get; set; }

        public DbSet<RoleUser> RoleUser { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        modelBuilder.Entity<RoleUser>()
            .HasKey(ru => new { ru.UsersId, ru.RolesId });

        modelBuilder.Entity<RoleUser>()
            .HasOne(ru => ru.User)
            .WithMany(u => u.RoleUsers)
            .HasForeignKey(ru => ru.UsersId);

        modelBuilder.Entity<RoleUser>()
            .HasOne(ru => ru.Role)
            .WithMany(r => r.RoleUsers)
            .HasForeignKey(ru => ru.RolesId);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;UID=root;database=userroles;password=''");
        }
    }
}

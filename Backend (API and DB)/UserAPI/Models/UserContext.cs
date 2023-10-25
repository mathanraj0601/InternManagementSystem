using Microsoft.EntityFrameworkCore;

namespace UserAPI.Models
{
    public class UserContext : DbContext
    {
        public UserContext()
        {
            
        }
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Intern>().Property(i=>i.InternId).ValueGeneratedNever();
        }
        public  virtual DbSet<User>? Users { get; set; }
        public  virtual DbSet<Intern>? Interns { get; set; }
    }
}

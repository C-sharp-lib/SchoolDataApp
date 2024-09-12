using Microsoft.EntityFrameworkCore;

namespace SchoolMVCApp.Models
{
    public class SchoolMVCAppDBContext : DbContext
    {
        public SchoolMVCAppDBContext(DbContextOptions<SchoolMVCAppDBContext> options) : base(options)
        {
        }
        public DbSet<Students> Students { get; set; }
        public DbSet<Classes> Classes { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Students>()
            .HasMany(s => s.Classes)
            .WithMany(c => c.Students)
            .UsingEntity<Dictionary<string, object>>(
                "StudentClasses", // This is the join table name
                j => j.HasOne<Classes>().WithMany().HasForeignKey("ClassId"),
                j => j.HasOne<Students>().WithMany().HasForeignKey("StudentId"));
            modelBuilder.Entity<Teacher>()
            .HasMany(t => t.Classes)
            .WithOne(c => c.Teacher)
            .HasForeignKey(c => c.Teacher);
            base.OnModelCreating(modelBuilder);
        }
    }
}

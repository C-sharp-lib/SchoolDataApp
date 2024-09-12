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
        public DbSet<StudentClasses> StudentClasses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Teacher>().HasKey(t => t.TeacherId);
            modelBuilder.Entity<Students>().HasKey(s => s.StudentId);
            modelBuilder.Entity<Classes>().HasKey(c => c.ClassId);
            modelBuilder.Entity<StudentClasses>().HasKey(sc => new { sc.StudentId, sc.ClassId });
            modelBuilder.Entity<StudentClasses>().HasOne(sc => sc.Student).WithMany(s => s.StudentClasses).HasForeignKey(sc => sc.StudentId);
            modelBuilder.Entity<StudentClasses>().HasOne(sc => sc.Clas).WithMany(c => c.StudentClasses).HasForeignKey(sc => sc.ClassId);
            modelBuilder.Entity<Teacher>().HasMany(t => t.Clas).WithOne(c => c.Teacher).HasForeignKey(c => c.TeacherId);
        }
    }
}

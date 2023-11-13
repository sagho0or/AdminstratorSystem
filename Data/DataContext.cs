using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace AdministratorSystem.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        //public DbSet<Student> Students { get; set; }
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Course> Course => Set<Course>();
        public DbSet<Module> Module => Set<Module>();
        public DbSet<Assessment> Assessment => Set<Assessment>();
        public DbSet<StudentAssesment> StudentAssesments => Set<StudentAssesment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
            .HasKey(s => s.StudentId);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Course)
                .WithMany(p => p.Students)
                .HasForeignKey(s => s.CourseId);

            modelBuilder.Entity<Course>()
                .HasKey(p => p.CourseId);

            modelBuilder.Entity<Course>()
                .HasMany(p => p.Modules)
                .WithOne(m => m.Course)
                .HasForeignKey(m => m.CourseId);

            modelBuilder.Entity<Module>()
                .HasKey(m => m.ModuleCode);

            modelBuilder.Entity<Module>()
                .HasMany(m => m.Assessments)
                .WithOne(a => a.Module)
                .HasForeignKey(a => a.ModuleCode);

            modelBuilder.Entity<Assessment>()
                .HasKey(a => a.AssessmentId);

            modelBuilder.Entity<Cohort>()
                .HasKey(c => c.CohortId);

            modelBuilder.Entity<Cohort>()
                .HasOne(c => c.Course)
                .WithMany(p => p.Cohorts)
                .HasForeignKey(c => c.CourseId);
        }


    }

}

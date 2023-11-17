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
        public DbSet<Cohort> Cohort => Set<Cohort>();
        public DbSet<StudentAssesment> StudentAssesments => Set<StudentAssesment>();
        public DbSet<StudentModule> StudentModule => Set<StudentModule>();
        public DbSet<StudentCourse> StudentCourse => Set<StudentCourse>();
        public DbSet<CourseModule> CourseModule => Set<CourseModule>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cohort>()
            .HasKey(s => s.CohortId);

            modelBuilder.Entity<Student>()
            .HasKey(s => s.StudentId);

            modelBuilder.Entity<Cohort>()
                .HasMany(s => s.Students)
                .WithOne(p => p.Cohort)
                .HasForeignKey(s => s.CohortId);

            modelBuilder.Entity<Module>()
                .HasKey(m => m.ModuleId);

            modelBuilder.Entity<Module>()
                .Property(m => m.ModuleCode)
                .HasMaxLength(5); // Assuming the module code is a string of length 5

            modelBuilder.Entity<Course>()
                .HasKey(p => p.CourseId);

            modelBuilder.Entity<Course>()
                .Property(p => p.CourseIdentifier)
                .HasMaxLength(6); // Assuming the Course identifier is a string of length 6

            modelBuilder.Entity<CourseModule>()
                .HasKey(pm => new { pm.CourseId, pm.ModuleId });

            modelBuilder.Entity<CourseModule>()
                .HasOne(pm => pm.Course)
                .WithMany(p => p.CourseModules)
                .HasForeignKey(pm => pm.CourseId);

            modelBuilder.Entity<CourseModule>()
                .HasOne(pm => pm.Module)
                .WithMany(m => m.CourseModules)
                .HasForeignKey(pm => pm.ModuleId);

            modelBuilder.Entity<Cohort>()
            .HasKey(c => c.CohortId);

            modelBuilder.Entity<Cohort>()
            .HasOne(c => c.Course)
            .WithMany(course => course.Cohorts)
            .HasForeignKey(c => c.CourseId)
            .IsRequired(); // This enforces the constraint that a Cohort must have a CourseId

            modelBuilder.Entity<Module>()
                .HasMany(m => m.Assessments)
                .WithOne(a => a.Module)
                .HasForeignKey(a => a.ModuleId);

            modelBuilder.Entity<Assessment>()
                .HasKey(a => a.AssessmentId);
        }


    }

}

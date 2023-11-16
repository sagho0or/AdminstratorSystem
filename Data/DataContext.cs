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

            modelBuilder.Entity<Course>()
                .HasKey(p => p.CourseId);

            modelBuilder.Entity<Course>()
                .HasMany(p => p.Modules)
                .WithOne(m => m.Course)
                .HasForeignKey(m => m.CourseId);

            modelBuilder.Entity<Course>()
                .HasMany(p => p.Cohorts)
                .WithOne(m => m.Course)
                .HasForeignKey(m => m.CourseId);

            modelBuilder.Entity<Module>()
                .HasKey(m => m.ModuleId);

            modelBuilder.Entity<Module>()
                .HasMany(m => m.Assessments)
                .WithOne(a => a.Module)
                .HasForeignKey(a => a.ModuleId);

            modelBuilder.Entity<Assessment>()
                .HasKey(a => a.AssessmentId);
        }


    }

}

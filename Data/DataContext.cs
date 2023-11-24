using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace AdministratorSystem.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Course> Course => Set<Course>();
        public DbSet<Module> Module => Set<Module>();
        public DbSet<Assessment> Assessment => Set<Assessment>();
        public DbSet<Cohort> Cohort => Set<Cohort>();
        public DbSet<StudentAssessment> StudentAssessments => Set<StudentAssessment>();
        public DbSet<StudentModule> StudentModule => Set<StudentModule>();
        public DbSet<StudentCourse> StudentCourse => Set<StudentCourse>();
        public DbSet<CourseModule> CourseModule => Set<CourseModule>();
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<StudentModule> StudentModules { get; set; }
        public DbSet<ModuleAssessment> ModuleAssessment => Set<ModuleAssessment>();
        

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

            modelBuilder.Entity<ModuleAssessment>()
                .HasKey(pm => new { pm.ModuleId, pm.AssessmentId });

            modelBuilder.Entity<ModuleAssessment>()
                .HasOne(pm => pm.Module)
                .WithMany(p => p.ModuleAssessments)
                .HasForeignKey(pm => pm.ModuleId);

            modelBuilder.Entity<ModuleAssessment>()
                .HasOne(pm => pm.Assessment)
                .WithMany(m => m.ModuleAssessments)
                .HasForeignKey(pm => pm.AssessmentId);


            modelBuilder.Entity<Assessment>()
                .HasKey(a => a.AssessmentId);
            modelBuilder.Entity<StudentCourse>()
           .HasKey(sc => sc.Id);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.StudentId);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(sc => sc.CourseId);

            modelBuilder.Entity<StudentModule>()
                .HasKey(sm => sm.Id);

            modelBuilder.Entity<StudentModule>()
                .HasOne(sm => sm.Student)
                .WithMany(s => s.StudentModules)
                .HasForeignKey(sm => sm.StudentId);

            modelBuilder.Entity<StudentModule>()
                .HasOne(sm => sm.Module)
                .WithMany(m => m.StudentModules)
                .HasForeignKey(sm => sm.ModuleId);
        }
    }

}

﻿// <auto-generated />
using System;
using AdministratorSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AdministratorSystem.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231128122732_optionalModule")]
    partial class optionalModule
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.13");

            modelBuilder.Entity("AdministratorSystem.Assessment", b =>
                {
                    b.Property<int>("AssessmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("ModuleId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("AssessmentId");

                    b.HasIndex("ModuleId");

                    b.ToTable("Assessment");
                });

            modelBuilder.Entity("AdministratorSystem.Cohort", b =>
                {
                    b.Property<int>("CohortId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AcademicYear")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("CourseId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CohortId");

                    b.HasIndex("CourseId");

                    b.ToTable("Cohort");
                });

            modelBuilder.Entity("AdministratorSystem.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DurationInYears")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("CourseId");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("AdministratorSystem.CourseModule", b =>
                {
                    b.Property<int>("CourseId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ModuleId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsRequired")
                        .HasColumnType("INTEGER");

                    b.HasKey("CourseId", "ModuleId");

                    b.HasIndex("ModuleId");

                    b.ToTable("CourseModule");
                });

            modelBuilder.Entity("AdministratorSystem.Module", b =>
                {
                    b.Property<int>("ModuleId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(5)
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AssessmentId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CourseId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsRequired")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ModuleId");

                    b.HasIndex("AssessmentId");

                    b.HasIndex("CourseId");

                    b.ToTable("Module");
                });

            modelBuilder.Entity("AdministratorSystem.ModuleAssessment", b =>
                {
                    b.Property<int>("ModuleId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("AssessmentId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MaxMark")
                        .HasColumnType("INTEGER");

                    b.HasKey("ModuleId", "AssessmentId");

                    b.HasIndex("AssessmentId");

                    b.ToTable("ModuleAssessment");
                });

            modelBuilder.Entity("AdministratorSystem.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CohortId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("StudentId");

                    b.HasIndex("CohortId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("AdministratorSystem.StudentAssessment", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AssessmentId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Mark")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ModuleId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StudentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.HasIndex("AssessmentId");

                    b.HasIndex("ModuleId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentAssessments");
                });

            modelBuilder.Entity("AdministratorSystem.StudentCourse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CourseId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Mark")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Result")
                        .HasColumnType("TEXT");

                    b.Property<int>("StudentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentCourse");
                });

            modelBuilder.Entity("AdministratorSystem.StudentModule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsSelected")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Mark")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ModuleId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Result")
                        .HasColumnType("TEXT");

                    b.Property<int>("StudentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ModuleId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentModule");
                });

            modelBuilder.Entity("AdministratorSystem.Assessment", b =>
                {
                    b.HasOne("AdministratorSystem.Module", "Module")
                        .WithMany()
                        .HasForeignKey("ModuleId");

                    b.Navigation("Module");
                });

            modelBuilder.Entity("AdministratorSystem.Cohort", b =>
                {
                    b.HasOne("AdministratorSystem.Course", "Course")
                        .WithMany("Cohorts")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("AdministratorSystem.CourseModule", b =>
                {
                    b.HasOne("AdministratorSystem.Course", "Course")
                        .WithMany("CourseModules")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AdministratorSystem.Module", "Module")
                        .WithMany("CourseModules")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Module");
                });

            modelBuilder.Entity("AdministratorSystem.Module", b =>
                {
                    b.HasOne("AdministratorSystem.Assessment", null)
                        .WithMany("Modules")
                        .HasForeignKey("AssessmentId");

                    b.HasOne("AdministratorSystem.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId");

                    b.Navigation("Course");
                });

            modelBuilder.Entity("AdministratorSystem.ModuleAssessment", b =>
                {
                    b.HasOne("AdministratorSystem.Assessment", "Assessment")
                        .WithMany("ModuleAssessments")
                        .HasForeignKey("AssessmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AdministratorSystem.Module", "Module")
                        .WithMany("ModuleAssessments")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assessment");

                    b.Navigation("Module");
                });

            modelBuilder.Entity("AdministratorSystem.Student", b =>
                {
                    b.HasOne("AdministratorSystem.Cohort", "Cohort")
                        .WithMany("Students")
                        .HasForeignKey("CohortId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cohort");
                });

            modelBuilder.Entity("AdministratorSystem.StudentAssessment", b =>
                {
                    b.HasOne("AdministratorSystem.Assessment", "Assessment")
                        .WithMany("StudentAssessments")
                        .HasForeignKey("AssessmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AdministratorSystem.Module", "Module")
                        .WithMany()
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AdministratorSystem.Student", "Student")
                        .WithMany("StudentAssessments")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assessment");

                    b.Navigation("Module");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("AdministratorSystem.StudentCourse", b =>
                {
                    b.HasOne("AdministratorSystem.Course", "Course")
                        .WithMany("StudentCourses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AdministratorSystem.Student", "Student")
                        .WithMany("StudentCourses")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("AdministratorSystem.StudentModule", b =>
                {
                    b.HasOne("AdministratorSystem.Module", "Module")
                        .WithMany("StudentModules")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AdministratorSystem.Student", "Student")
                        .WithMany("StudentModules")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Module");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("AdministratorSystem.Assessment", b =>
                {
                    b.Navigation("ModuleAssessments");

                    b.Navigation("Modules");

                    b.Navigation("StudentAssessments");
                });

            modelBuilder.Entity("AdministratorSystem.Cohort", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("AdministratorSystem.Course", b =>
                {
                    b.Navigation("Cohorts");

                    b.Navigation("CourseModules");

                    b.Navigation("StudentCourses");
                });

            modelBuilder.Entity("AdministratorSystem.Module", b =>
                {
                    b.Navigation("CourseModules");

                    b.Navigation("ModuleAssessments");

                    b.Navigation("StudentModules");
                });

            modelBuilder.Entity("AdministratorSystem.Student", b =>
                {
                    b.Navigation("StudentAssessments");

                    b.Navigation("StudentCourses");

                    b.Navigation("StudentModules");
                });
#pragma warning restore 612, 618
        }
    }
}

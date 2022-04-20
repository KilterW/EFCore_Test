using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace EFCore_DBFirst
{
    public partial class DBFContext : DbContext
    {
        public DBFContext()
        {
        }

        public DBFContext(DbContextOptions<DBFContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ChooseCourse> ChooseCourses { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!; 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfiguration config = new ConfigurationBuilder()
                     .SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json")
                     .Build();
                string ConnectStr = config.GetConnectionString("NpgsqlDbConn_DBFirst");
                optionsBuilder.UseNpgsql(ConnectStr);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChooseCourse>(entity =>
            {
                entity.ToTable("choose_course");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.CourseId)
                    .HasColumnType("character varying")
                    .HasColumnName("Course_ID");

                entity.Property(e => e.StuNum)
                    .HasColumnType("character varying")
                    .HasColumnName("Stu_Num");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.ChooseCourses)
                    .HasPrincipalKey(p => p.CourseId)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("choose_course_Course_ID_fkey");

                entity.HasOne(d => d.StuNumNavigation)
                    .WithMany(p => p.ChooseCourses)
                    .HasPrincipalKey(p => p.StuNum)
                    .HasForeignKey(d => d.StuNum)
                    .HasConstraintName("choose_course_Stu_Num_fkey");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("course");

                entity.HasIndex(e => e.CourseId, "course_Course_ID_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.CourseId)
                    .HasColumnType("character varying")
                    .HasColumnName("Course_ID");

                entity.Property(e => e.Name).HasColumnType("character varying");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("student");

                entity.HasIndex(e => e.StuNum, "student_Stu_Num_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Gender).HasColumnType("character varying");

                entity.Property(e => e.Name).HasColumnType("character varying");

                entity.Property(e => e.Phone).HasColumnType("character varying");

                entity.Property(e => e.StuNum)
                    .HasColumnType("character varying")
                    .HasColumnName("Stu_Num");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

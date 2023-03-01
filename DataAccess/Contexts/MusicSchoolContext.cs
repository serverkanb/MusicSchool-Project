using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contexts
{
    public class MusicSchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<StudentTeacherLessonClassroom> StudentTeacherLessonClassrooms { get; set; }
        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<TeacherContact> TeacherContacts { get; set; }
        public DbSet<StudentContact> StudentContacts { get; set; }

        public MusicSchoolContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentTeacherLessonClassroom>().HasKey(stlc => new
            {
                stlc.StudentId,
                stlc.TeacherId,
                stlc.LessonId,
                stlc.ClassroomId
            });
            modelBuilder.Entity<StudentContact>()
                .HasOne(sc => sc.Student)
                .WithOne(s => s.StudentContact)
                .HasForeignKey<StudentContact>(sc => sc.StudentId)
                .OnDelete(DeleteBehavior.NoAction); // Principal entity silinmesini engellemek için NoAction tanımladık

            modelBuilder.Entity<Student>()
                .HasOne(s => s.StudentContact)
                .WithOne(sc => sc.Student)
                .HasForeignKey<StudentContact>(sc => sc.StudentId)
                .OnDelete(DeleteBehavior.Cascade); // Dependent entity tabloları silebilmek için cascade tanımladık 

            modelBuilder.Entity<TeacherContact>()
                 .HasOne(tc => tc.Teacher)
                 .WithOne(t => t.TeacherContact)
                 .HasForeignKey<TeacherContact>(tc => tc.TeacherId)
                 .OnDelete(DeleteBehavior.NoAction); // Principal entity silinmesini engellemek için NoAction tanımladık

            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.TeacherContact)
                .WithOne(tc => tc.Teacher)
                .HasForeignKey<TeacherContact>(tc => tc.TeacherId)
                .OnDelete(DeleteBehavior.Cascade); // Dependent entity tabloları silebilmek için cascade tanımladık 

            modelBuilder.Entity<StudentContact>()
                .HasIndex(sc => sc.Email).IsUnique(true);

            modelBuilder.Entity<TeacherContact>()
                .HasIndex(tc => tc.Email).IsUnique(true);

            modelBuilder.Entity<City>()
               .HasOne(ci => ci.Country)
               .WithMany(co => co.Cities)
               .HasForeignKey(ci => ci.CountryId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StudentContact>()
               .HasOne(sc => sc.Country)
               .WithMany(co => co.StudentContacts)
               .HasForeignKey(sc => sc.CountryId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StudentContact>()
               .HasOne(sc => sc.City)
               .WithMany(ci => ci.StudentContacts)
               .HasForeignKey(sc => sc.CityId)
               .OnDelete(DeleteBehavior.NoAction);

        }
    }
}

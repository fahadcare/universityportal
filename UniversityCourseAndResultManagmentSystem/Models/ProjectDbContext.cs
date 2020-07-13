using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultManagmentSystem.Models
{
    public class ProjectDbContext : DbContext
    {
        public DbSet<Department> Departments { set; get; }
        public DbSet<Course> Courses { set; get; }
        public DbSet<Teacher> Teachers { set; get; }
        public DbSet<Designation> Designations { set; get; }
        public DbSet<Student> Students { set; get; }
        public DbSet<AssignCourse> AssignCourses { get; set; }
        public DbSet<Grade> Grades { set; get; }
        public DbSet<Room> Rooms { set; get; }
        public DbSet<Day> Days { get; set; }
        public DbSet<RoomAllocation> RoomAllocations { get; set; }
        public DbSet<EnrollCourse> EnrollCourses { get; set; }

        public System.Data.Entity.DbSet<UniversityCourseAndResultManagmentSystem.Models.Semester> Semesters { get; set; }
    }
}
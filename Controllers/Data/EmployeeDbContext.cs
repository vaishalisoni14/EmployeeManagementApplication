using EmployeeManagementApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System.Xml;

namespace EmployeeManagementApplication.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Gender> Genders{ get; set; }
        public DbSet<User> Users { get; set; }
       
        



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeView>().HasNoKey();

            modelBuilder.Entity<UserView>().HasNoKey();

            modelBuilder.Entity<TotalRecords>().HasNoKey();

            modelBuilder.Entity<Department>().HasData(
                new Department
                {
                    DepartmentId = 1,
                    DepartmentName = "IT"
                },
                new Department
                {
                    DepartmentId = 2,
                    DepartmentName = "HR"
                },
                new Department
                {
                    DepartmentId = 3,
                    DepartmentName = "BDE"
                }
                );



          
            modelBuilder.Entity<Gender>().HasData(
                new Gender
                {
                    GenderId = 1,
                    GenderName = "Female"
                },
                new Gender
                {
                    GenderId = 2,
                    GenderName = "Male"
                },
                new Gender
                {
                    GenderId = 3,
                    GenderName = "Other"
                }
                );




            base.OnModelCreating(modelBuilder);
        }
    }
}
    
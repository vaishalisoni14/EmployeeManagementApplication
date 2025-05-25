using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementApplication.Models
{
    public class Employee
    {
        [Key]
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }


        [Required]
        public DateOnly DateOfBirth { get; set; }


        [Range(18, 60)]
        [Required]
        public int Age { get; set; }

        [Required]
        [MaxLength(50)]
        public string? City { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Country { get; set; }

        [Required]
        [EmailAddress]
        public string? EmailId { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [Range(10000, 100000)]
        public double Salary { get; set; }

        [Required]
        public string WorkAddress { get; set; }
        [Required]
        public string HomeAddress { get; set; }
        [Required]
        public string PermanentAddress { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Hobbies { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public int DepartmentId { get; set; }
  
        public Department ? Department { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        

        public int GenderId { get; set; }
  
        public Gender? Gender { get; set; }

    }
}
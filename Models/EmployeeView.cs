using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementApplication.Models
{
    public class EmployeeView
    {
        public int EmployeeId { get; set; }

        public string? FirstName { get; set; }

        
        public string? LastName { get; set; }


        
        public DateOnly ? DateOfBirth { get; set; }

        
        public int ? Age { get; set; }

        
        public string? City { get; set; }

        public string? Country { get; set; }

        
        public string? EmailId { get; set; }

        
        public string ? PhoneNumber { get; set; }

        
        public double ? Salary { get; set; }

        
        public string WorkAddress { get; set; }
        
        public string ? HomeAddress { get; set; }
        
        public string ? PermanentAddress { get; set; }

        
        public string? Hobbies { get; set; }

        public int ? DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public int ? GenderId { get; set; }

        public string? GenderName { get; set; }



    }
}
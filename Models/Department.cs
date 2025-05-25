using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementApplication.Models
    {
        public class Department
        {
        [Key]
        [Required]
        public int DepartmentId { get; set; }
        [Required]
        public  string ?DepartmentName { get; set; }       
        public  ICollection<Employee> Employees { get; set; } =new List<Employee>();
    }
    }

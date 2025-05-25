using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementApplication.Models
{
    public class Gender
    {
        [Key]
        [Required]
        public int GenderId { get; set; }

        [Required]
        public string GenderName { get; set; }

    }
}

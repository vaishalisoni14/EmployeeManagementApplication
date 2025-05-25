using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementApplication.Models
{
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public string  FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}

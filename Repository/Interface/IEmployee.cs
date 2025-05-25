using EmployeeManagementApplication.Models;

namespace EmployeeManagementApplication.Repository.Interface
{
    public interface IEmployee
    {
        Task<IEnumerable<EmployeeView>> GetAllAsync
            (
             string searchKeyword = "",
            string sortColumn = "name",
            string sortDirection = "asc",
            int page = 1,
            int pageSize = 5,
            string genderIds = null,
            string departmentIds = null,
            int minAge = 18,
            int maxAge = 60,
            int minSalary = 10000,
            int maxSalary = 150000
            );
        Task<int> GetCount
            (
            string searchKeyword = "",
            string genderIds = null,
            string departmentIds = null,
            int minAge = 18,
            int maxAge = 60,
            int minSalary = 10000,
            int maxSalary = 150000

            );
        Task<Employee> GetById(int ? id);
        Task<IEnumerable<EmployeeView>> getId(int ?id);


    }
}

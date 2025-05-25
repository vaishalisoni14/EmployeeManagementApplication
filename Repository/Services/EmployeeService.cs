using EmployeeManagementApplication.Data;
using EmployeeManagementApplication.Models;
using EmployeeManagementApplication.Repository.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Dynamic.Core;
using static System.Net.Mime.MediaTypeNames;

namespace EmployeeManagementApplication.Repository.Services
{
    public class EmployeeService : IEmployee
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public EmployeeService(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }


        public async Task<IEnumerable<EmployeeView>> GetAllAsync
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
            )
        {
            var parameter = new[]
            {
                new SqlParameter("@SearchKeyword" ,SqlDbType.NVarChar){Value =searchKeyword},
                new SqlParameter("@SortColumn" , SqlDbType.NVarChar){Value=sortColumn},
                new SqlParameter("@SortDirection" , SqlDbType.NVarChar){Value=sortDirection},
                new SqlParameter("@Page" , SqlDbType.Int){Value=page},
                new SqlParameter("@PageSize" ,SqlDbType.Int){Value=pageSize},
                new SqlParameter("@genderIds" , SqlDbType.NVarChar){Value=genderIds == null ? DBNull.Value : genderIds},
                new SqlParameter("@departmentIds" , SqlDbType.NVarChar){Value=departmentIds  == null ? DBNull.Value : departmentIds},
                new SqlParameter("@minAge" , SqlDbType.Int){Value=minAge},
                new SqlParameter("@maxAge" , SqlDbType.Int){Value=maxAge},
                new SqlParameter("@minSalary" , SqlDbType.Int){Value=minSalary},
                new SqlParameter("@maxSalary" , SqlDbType.Int){Value=maxSalary}

            };
            //try
            //{

            //var employee1 = _employeeDbContext.Set<Employee>().FromSqlRaw($"EXEC sp_GetAllEmployeeList @SearchKeyword , @SortColumn , @SortDirection , @Page, @PageSize , @genderIds , @departmentIds, @minAge , @maxAge , @minSalary , @maxSalary",parameter).IgnoreQueryFilters().ToList();
            //}
            //catch (Exception ex)
            //{

            //}
            var employee = _employeeDbContext.Set<EmployeeView>().FromSqlRaw($"EXEC sp_GetAllEmployeeList @SearchKeyword , @SortColumn , @SortDirection , @Page, @PageSize , @genderIds , @departmentIds, @minAge , @maxAge , @minSalary , @maxSalary",parameter).IgnoreQueryFilters();
            return  employee;

        }

        public async Task<int> GetCount
            (
            string searchKeyword = "",
            string genderIds = null,
            string departmentIds = null,
            int minAge = 18,
            int maxAge = 60,
            int minSalary = 10000,
            int maxSalary = 150000
            )
        {
            var parameter = new[]
            {
                new SqlParameter("@SearchKeyword" ,SqlDbType.NVarChar){Value =searchKeyword},
                new SqlParameter("@genderIds" , SqlDbType.NVarChar){Value=genderIds == null ? DBNull.Value : genderIds},
                new SqlParameter("@departmentIds" , SqlDbType.NVarChar){Value=departmentIds  == null ? DBNull.Value : departmentIds},
                new SqlParameter("@minAge" , SqlDbType.Int){Value=minAge},
                new SqlParameter("@maxAge" , SqlDbType.Int){Value=maxAge},
                new SqlParameter("@minSalary" , SqlDbType.Int){Value=minSalary},
                new SqlParameter("@maxSalary" , SqlDbType.Int){Value=maxSalary}
            };

            
             var employee = await _employeeDbContext.Set<TotalRecords>().FromSqlRaw($"EXEC sp_GetEmployeeCount @SearchKeyword ,  @genderIds , @departmentIds, @minAge , @maxAge , @minSalary , @maxSalary", parameter).IgnoreQueryFilters().ToListAsync();
            var test = Convert.ToInt32(employee[0].TotalRecordsCount);   
            return test;
            
        }

        
        public async Task<Employee> GetById(int ? id)
        {
            var emp =await _employeeDbContext.Employees.FindAsync(id);
            return emp;
        }



        public async Task<IEnumerable<EmployeeView>> getId(int? id)
        {
            var test = await _employeeDbContext.Set<EmployeeView>().FromSqlRaw($"EXEC spDetailEmployee @employeeId", new SqlParameter("@employeeId", id)).ToListAsync();
            return  test;
        }
    }


}

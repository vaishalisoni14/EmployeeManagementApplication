using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using EmployeeManagementApplication.Data;
using EmployeeManagementApplication.Models;
using EmployeeManagementApplication.Repository.Interface;
using EmployeeManagementApplication.Repository.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using static System.Net.Mime.MediaTypeNames;

namespace EmployeeManagementApplication.Controllers
{
    public class HomeController : Controller
    {
        private IDbConnection _context;

        private readonly IEmployee _employeeService;

        private readonly EmployeeDbContext _employeeDbContext;

        public HomeController(EmployeeDbContext employeeDbContext, IEmployee employeeService)
        {
            _employeeDbContext = employeeDbContext;
            _employeeService = employeeService;
        }

        //public HomeController(EmployeeService employeeService)
        //{
        //   _employeeService = employeeService;
        //}


        [Authorize]
        public IActionResult Index
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

            var employees = _employeeDbContext.Set<EmployeeView>()
          .FromSql($"EXEC sp_GetAllEmployeeList @SearchKeyword = {searchKeyword}, @SortColumn = {sortColumn}, @SortDirection = {sortDirection}, @Page = {page}, @PageSize = {pageSize} , @genderIds={genderIds} , @departmentIds={departmentIds} , @minAge={minAge} , @maxAge={maxAge} , @minSalary={minSalary} , @maxSalary={maxSalary}").IgnoreQueryFilters();


            var TotalRecords = Convert.ToInt32(_employeeDbContext.Set<TotalRecords>().FromSql($"EXEC sp_GetEmployeeCount @SearchKeyword={searchKeyword},@genderIds={genderIds}, @departmentIds={departmentIds},@minAge={minAge}, @maxAge={maxAge} , @minSalary={minSalary} , @maxSalary={maxSalary} ").IgnoreQueryFilters().AsEnumerable().FirstOrDefault().TotalRecordsCount);

            var totalPages = (int)Math.Ceiling((float)TotalRecords / (float)pageSize);



            ViewBag.sortColumn = sortColumn;
            ViewBag.sortDirection = sortDirection;
            ViewBag.searchKeyword = searchKeyword;
            ViewBag.page = page;
            ViewBag.pageSize = pageSize;
            ViewBag.GenderIds = genderIds;
            ViewBag.DepartmentIds = departmentIds;
            ViewBag.minAge = minAge;
            ViewBag.maxAge = maxAge;
            ViewBag.minSalary = minSalary;
            ViewBag.maxSalary = maxSalary;
            ViewBag.totalPages = totalPages;


            return View(employees);
        }


        public IActionResult Create()
        {

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {

            if (ModelState.IsValid)
            {
                _employeeDbContext.Employees.AddAsync(employee);
                await _employeeDbContext.SaveChangesAsync();
                return RedirectToAction("Demo");
            }
            return View(employee);

        }




        public async Task<IActionResult> Details(int id)
        {

            var test = await _employeeService.getId(id);
            return View(test);
        }





        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _employeeDbContext.Employees == null)
            {
                return NotFound();
            }
            var employeeData = await _employeeService.GetById(id);
           
            if (_employeeDbContext == null)
            {
                return NotFound();
            }
            return View(employeeData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _employeeDbContext.Employees.Update(employee);
                await _employeeDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(employee);

        }




        public async Task<IActionResult> Delete(int? id)
        {
            var employeeData = await _employeeService.GetById(id);
            

            return View(employeeData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? id, Employee employees)
        {
         

            _employeeDbContext.Database.ExecuteSqlRaw("spDeleteEmployees @employeeId ",
            new SqlParameter("employeeId", id));
            _employeeDbContext.SaveChanges();
            return RedirectToAction("Demo");
        }


        [Authorize]
        public async Task<IActionResult> Demo
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
            var employees = await _employeeService.GetAllAsync(searchKeyword, sortColumn, sortDirection, page, pageSize, genderIds, departmentIds, minAge, maxAge, minSalary, maxSalary);

            var TotalRecords = await _employeeService.GetCount(searchKeyword, genderIds, departmentIds, minAge, maxAge, minSalary, maxSalary);

            var totalPages = (int)Math.Ceiling((float)TotalRecords / (float)pageSize);


            ViewBag.sortColumn = sortColumn;
            ViewBag.sortDirection = sortDirection;
            ViewBag.searchKeyword = searchKeyword;
            ViewBag.page = page;
            ViewBag.pageSize = pageSize;
            ViewBag.GenderIds = genderIds;
            ViewBag.DepartmentIds = departmentIds;
            ViewBag.minAge = minAge;
            ViewBag.maxAge = maxAge;
            ViewBag.minSalary = minSalary;
            ViewBag.maxSalary = maxSalary;
            ViewBag.totalPages = totalPages;
            ViewBag.totalRecords = TotalRecords;

            return View(employees);
        }

        public IActionResult MainDashBoard()
        {
            ViewBag.total = Convert.ToInt32(_employeeDbContext.Set<TotalRecords>().FromSql($"EXEC sp_GetEmployeeCount").IgnoreQueryFilters().AsEnumerable().FirstOrDefault().TotalRecordsCount);

            return View();
        }


        public IActionResult store()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}





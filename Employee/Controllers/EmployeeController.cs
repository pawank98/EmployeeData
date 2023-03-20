using EmployeeData.DataAccessLayer;
using EmployeeData.Models;
using EmployeeData.Models.DbModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EmployeeData.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeDbContext _context;

        //initialize EmployeeDbCOntext
        public EmployeeController(EmployeeDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            var employees = _context.employees.ToList();
            List<EmployeeViewModel> employeelist = new List<EmployeeViewModel>();

            if (employees != null)
            {
                foreach (var employee in employees)
                {
                    var EmployeeViewModel = new EmployeeViewModel()
                    {
                        Id = employee.Id,
                        FirstName = employee.FristName,
                        LastName = employee.LastName,
                        DateOfBirth = employee.DateOfBirth,
                        Email = employee.Email,
                        Salary = employee.Salary
                    };
                    employeelist.Add(EmployeeViewModel);

                }
                return View(employeelist);
            }
            return View(employeelist);


        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeedata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var employee = new Employees()
                    {
                        FristName = employeedata.FirstName,
                        LastName = employeedata.LastName,
                        DateOfBirth = employeedata.DateOfBirth,
                        Email = employeedata.Email,
                        Salary = employeedata.Salary

                    };
                    _context.employees.Add(employee);
                    //_context.employees.Add(employee);
                    _context.SaveChanges();
                    TempData["successMessage"] = "Success";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = "Model not valid!!";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"]= ex.Message;
                return View();

           
            }
        }

    }
}


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
                    TempData["successMessage"] = "Employee Cerated";
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
                TempData["errorMessage"] = ex.Message;
                return View();

            }
        }



        /* public IActionResult Edit()
         {
             return View();
         }*/

        [HttpGet]
        [Route("Edit/{id}")]
        public IActionResult Edit(int Id)
        {
            try
            {
                var employee = _context.employees.SingleOrDefault(x => x.Id == Id);
                if (employee != null)
                {
                    var employeeView = new EmployeeViewModel()
                    {
                        Id = employee.Id,
                        FirstName = employee.FristName,
                        LastName = employee.LastName,
                        DateOfBirth = employee.DateOfBirth,
                        Email = employee.Email,
                        Salary = employee.Salary

                    };
                    return View(employeeView);
                }
                else
                {
                    TempData["errorMessage"] = "Data not available with this Id";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");

            }

        }

        [HttpPost]
        [Route("Edit/{id}")]

        public IActionResult Edit(EmployeeViewModel employeeEdit)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var employee = new Employees()
                    {
                        Id = employeeEdit.Id,
                        FristName = employeeEdit.FirstName,
                        LastName = employeeEdit.LastName,
                        DateOfBirth = employeeEdit.DateOfBirth,
                        Email = employeeEdit.Email,
                        Salary = employeeEdit.Salary

                    };
                    _context.employees.Update(employee);
                    _context.SaveChanges();
                    TempData["successMessage"] = "Data Updated Successfully.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessgae"] = "Model is Invalid";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        /* private IActionResult RedirectTOAction(string v)
         {
             throw new NotImplementedException();
         }*/
        [HttpGet]
        [Route("Delete/{id}")]
        public IActionResult Delete(int Id)
        {
            try
            {
                var employee = _context.employees.SingleOrDefault(x => x.Id == Id);
                if (employee != null)
                {
                    var viewEmployee = new EmployeeViewModel()
                    {
                        Id = employee.Id,
                        FirstName = employee.FristName,
                        LastName = employee.LastName,
                        DateOfBirth = employee.DateOfBirth,
                        Email = employee.Email,
                        Salary = employee.Salary

                    };
                    return View(viewEmployee);
                }
                else
                {
                    TempData["errorMessage"] = "Data not available with this Id";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");

            }

        }

        [HttpPost]
        [Route("Delete/{id}")]
        public IActionResult Delete(EmployeeViewModel deleteemp)
        {
            try
            {
                var employee = _context.employees.FirstOrDefault(x => x.Id == deleteemp.Id);

                if (employee != null)
                {
                    _context.employees.Remove(employee);
                    _context.SaveChanges();
                    TempData["successMessage"] = "Deleted Employee Data";

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessgae"] = "Model is Invalid";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();


            }
        }
    }
}


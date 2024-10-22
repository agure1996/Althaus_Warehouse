using Microsoft.AspNetCore.Mvc;
using Althaus_Warehouse.Models.DTO.EmployeeDTOs;
using Althaus_Warehouse.Services;
using Althaus_Warehouse.Models.Entities;
using Althaus_Warehouse.Models;

namespace Althaus_Warehouse.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
        }

        // GET: Employees/Index
        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return View(employees); // Assuming you have a view to display the list of employees
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewBag.EmployeeTypes = Enum.GetValues(typeof(EmployeeType))
                .Cast<EmployeeType>()
                .Select(e => new
                {
                    Value = e.ToString(),
                    Text = e.ToString()
                }).ToList();

            return View();
        }

        // GET: Employees/GetById/{id}
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound(); // Return a 404 if the employee is not found
            }

            return View(employee); // Return a view displaying the employee details
        }

        // GET: Employees/GetById/{id}
        public IActionResult GetEmployeeById()
        {
            return View(); // This will return the view for entering the employee ID
        }



        // POST: Employees/Create
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeDTO employeeDTO)
        {
            if (ModelState.IsValid)
            {
                // Map DTO to Employee entity
                var employee = new Employee
                {
                    FirstName = employeeDTO.FirstName,
                    LastName = employeeDTO.LastName,
                    Email = employeeDTO.Email,
                    EmployeeType = Enum.Parse<EmployeeType>(employeeDTO.EmployeeType),
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(employeeDTO.Password)
                };

                await _employeeService.CreateEmployeeAsync(employee);
                return RedirectToAction(nameof(Index)); // Adjust as necessary
            }

            // If model state is invalid, return the view with the current model to show validation errors
            ViewBag.EmployeeTypes = Enum.GetValues(typeof(EmployeeType))
                .Cast<EmployeeType>()
                .Select(e => new
                {
                    Value = e.ToString(),
                    Text = e.ToString()
                }).ToList();

            return View(employeeDTO);
        }

        // GET: Employees/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee); // Assuming you have a view for confirming deletion
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _employeeService.DeleteEmployeeAsync(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Log error or show an error message to the user
                return View("Error", new ErrorViewModel { RequestId = ex.Message });
            }
        }

        // GET: Employees/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee); // Make sure you create a Details view for this
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            var employeeDTO = new EditEmployeeDTO
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                EmployeeType = employee.EmployeeType.ToString() // Convert Enum to string for the dropdown
            };

            ViewBag.EmployeeTypes = Enum.GetValues(typeof(EmployeeType))
                .Cast<EmployeeType>()
                .Select(e => new { Value = e.ToString(), Text = e.ToString() })
                .ToList();

            return View(employeeDTO);
        }





        [HttpPost]
        public async Task<IActionResult> Edit(EditEmployeeDTO employeeDTO)
        {
            if (ModelState.IsValid)
            {
                await _employeeService.UpdateEmployeeAsync(employeeDTO.Id, employeeDTO);
                return RedirectToAction("Index");
            }

            // Repopulate ViewBag if model state is invalid
            ViewBag.EmployeeTypes = Enum.GetValues(typeof(EmployeeType))
                .Cast<EmployeeType>()
                .Select(e => new { Value = e.ToString(), Text = e.ToString() })
                .ToList();

            return View(employeeDTO);
        }



    }
}

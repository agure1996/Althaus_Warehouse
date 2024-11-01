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
        // Controller Action to Render the Search View
        // GET: Employees/GetEmployeeById
        [HttpGet]
        public IActionResult SearchEmployeeById()
        {
            return View(); 
        }

        // Controller Action to Fetch Employee by ID
        // GET: Employees/GetEmployeeById/{id}
        [HttpGet]

        public async Task<IActionResult> GetEmployeeById(int id)
        {
            if (id <= 0) // Handle case when id is 0 or negative
            {
                return BadRequest(new { Message = "Invalid Employee ID." });
            }

            var employee = await _employeeService.GetEmployeeByIdAsync(id);

            if (employee == null)
            {
                return NotFound(new { Message = $"Employee with ID {id} does not exist." });
            }

            return Ok(employee);

        }


        // POST: Employees/Create
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeDTO employeeDTO)
        {
            if (!ModelState.IsValid)
            {
                // Log validation errors for debugging
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                return BadRequest(new { Errors = errors }); // Return 400 with validation errors
            }

            // Create the employee object
            var employee = new Employee
            {
                FirstName = employeeDTO.FirstName,
                LastName = employeeDTO.LastName,
                Email = employeeDTO.Email,
                EmployeeType = Enum.Parse<EmployeeType>(employeeDTO.EmployeeType),
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(employeeDTO.Password) // Ensure Password is part of DTO
            };

            // Call service to create employee
            await _employeeService.CreateEmployeeAsync(employee);

            // Return success response with redirect URL
            return Ok(new { redirectUrl = Url.Action(nameof(Index), "Employees") });
        }


        // GET: Employees/Delete/{id}

        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound(); // Employee not found, handle appropriately
            }

            await _employeeService.DeleteEmployeeAsync(id);

            // Redirect to the index action after successful deletion
            return View(employee);
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
            return View(employee);
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
                EmployeeType = employee.EmployeeType.ToString() // Converted to Enum to string for the dropdown
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

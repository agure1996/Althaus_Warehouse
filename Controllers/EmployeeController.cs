using Asp.Versioning;
using AutoMapper;
using Althaus_Warehouse.Models;
using Althaus_Warehouse.Models.DTO.EmployeeDTOs;
using Althaus_Warehouse.Services; // Assuming you have a service/repository for employee operations
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Althaus_Warehouse.Models.Entities;
using Althaus_Warehouse.Services.Repositories;

namespace Althaus_Warehouse.Controllers
{
    /// <summary>
    /// Controller for handling Employee-related operations, providing API access to manage employees.
    /// Supports API versioning and uses a repository pattern to fetch data.
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/v{version:apiVersion}/employees")]
    [ApiVersion(1)]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository; // Assuming this is the repository for employee operations
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for EmployeesController.
        /// </summary>
        /// <param name="employeeRepository">Repository for employee-related database operations.</param>
        /// <param name="mapper">AutoMapper instance to map entities to DTOs.</param>
        /// <exception cref="ArgumentNullException">Thrown when employeeRepository or mapper is null.</exception>
        public EmployeesController(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Retrieves a list of employees.
        /// </summary>
        /// <response code="200">Returns a list of employees.</response>
        /// <response code="404">No employees found.</response>
        /// <returns>A list of employees in the form of EmployeeDTO.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EmployeeDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployees()
        {
            var employeeEntities = await _employeeRepository.GetEmployeesAsync();
            if (employeeEntities == null || !employeeEntities.Any())
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<EmployeeDTO>>(employeeEntities));
        }

        /// <summary>
        /// Retrieves a specific employee by their ID.
        /// </summary>
        /// <param name="employeeId">ID of the employee to retrieve.</param>
        /// <response code="200">Returns the requested employee information.</response>
        /// <response code="404">Employee not found.</response>
        /// <returns>Returns the employee information.</returns>
        [HttpGet("{employeeId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEmployeeById(int employeeId)
        {
            var employeeEntity = await _employeeRepository.GetEmployeeAsync(employeeId);

            if (employeeEntity == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<EmployeeDTO>(employeeEntity));
        }

        /// <summary>
        /// Creates a new employee.
        /// </summary>
        /// <param name="createEmployeeDto">DTO containing the employee's data.</param>
        /// <response code="201">Employee created successfully.</response>
        /// <response code="400">Invalid employee data provided.</response>
        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")] // Only admins can create employees
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(EmployeeDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDTO createEmployeeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employeeEntity = _mapper.Map<Employee>(createEmployeeDto);
            await _employeeRepository.CreateEmployeeAsync(employeeEntity);

            return CreatedAtAction(nameof(GetEmployeeById), new { employeeId = employeeEntity.Id }, _mapper.Map<EmployeeDTO>(employeeEntity));
        }

        /// <summary>
        /// Updates an existing employee.
        /// </summary>
        /// <param name="employeeId">ID of the employee to update.</param>
        /// <param name="updateEmployeeDto">DTO containing the updated employee's data.</param>
        /// <response code="204">Employee updated successfully.</response>
        /// <response code="404">Employee not found.</response>
        [HttpPut("{employeeId}")]
        [Authorize(Policy = "RequireAdminRole")] // Only admins can update employees
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateEmployee(int employeeId, [FromBody] UpdateEmployeeDTO updateEmployeeDto)
        {
            if (!ModelState.IsValid || employeeId != updateEmployeeDto.Id)
            {
                return BadRequest(ModelState);
            }

            var existingEmployee = await _employeeRepository.GetEmployeeAsync(employeeId);
            if (existingEmployee == null)
            {
                return NotFound();
            }

            var employeeEntity = _mapper.Map<Employee>(updateEmployeeDto);
            await _employeeRepository.UpdateEmployeeAsync(employeeEntity);

            return NoContent();
        }

        /// <summary>
        /// Deletes an employee by their ID.
        /// </summary>
        /// <param name="employeeId">ID of the employee to delete.</param>
        /// <response code="204">Employee deleted successfully.</response>
        /// <response code="404">Employee not found.</response>
        [HttpDelete("{employeeId}")]
        [Authorize(Policy = "RequireAdminRole")] // Only admins can delete employees
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEmployee(int employeeId)
        {
            var employeeEntity = await _employeeRepository.GetEmployeeAsync(employeeId);
            if (employeeEntity == null)
            {
                return NotFound();
            }

            await _employeeRepository.DeleteEmployeeAsync(employeeEntity);
            return NoContent();
        }
    }
}

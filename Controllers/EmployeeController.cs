using Althaus_Warehouse.Models.DTO.EmployeeDTOs; // Ensure this is correct
using Althaus_Warehouse.Models.Entities;
using Althaus_Warehouse.Services.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace Althaus_Warehouse.Controllers
{
    /// <summary>
    /// Controller to manage operations related to employees in the warehouse.
    /// </summary>
    [Route("api/v{version:apiVersion}/employees")]
    [Authorize(Roles = "Admin")]
    [Asp.Versioning.ApiVersion(1.0)]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Retrieves a list of all employees in the warehouse.
        /// </summary>
        /// <returns>A list of employees as <see cref="EmployeeDTO"/>.</returns>
        /// <response code="200">Returns the list of employees.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetAllEmployees()
        {
            try
            {
                var employees = await _employeeRepository.GetAllEmployeesAsync();
                var employeeDTOs = _mapper.Map<IEnumerable<EmployeeDTO>>(employees);
                return Ok(employeeDTOs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all employees.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Retrieves an employee by their ID.
        /// </summary>
        /// <param name="id">The ID of the employee to retrieve.</param>
        /// <returns>The employee as <see cref="EmployeeDTO"/> if found, or a 404 status if not found.</returns>
        /// <response code="200">Returns the employee if found.</response>
        /// <response code="404">Returns Not Found if the employee doesn't exist.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmployeeDTO>> GetEmployeeById(int id)
        {
            try
            {
                var employeeEntity = await _employeeRepository.GetEmployeeByIdAsync(id);
                if (employeeEntity == null)
                {
                    _logger.LogWarning("Employee with ID {Id} not found.", id);
                    return NotFound($"Employee with ID {id} was not found.");
                }

                var employeeDTO = _mapper.Map<EmployeeDTO>(employeeEntity);
                return Ok(employeeDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving employee with ID {Id}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Creates a new employee in the warehouse.
        /// </summary>
        /// <param name="createEmployeeDTO">The employee data to create.</param>
        /// <returns>The created employee as <see cref="EmployeeDTO"/>.</returns>
        /// <response code="201">If the employee was created successfully.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EmployeeDTO>> CreateEmployee([FromBody] CreateEmployeeDTO createEmployeeDTO)
        {
            if (createEmployeeDTO == null)
            {
                return BadRequest("Employee data is null.");
            }

            try
            {
                var employee = _mapper.Map<Employee>(createEmployeeDTO);

                await _employeeRepository.AddEmployeeAsync(employee);
                await _employeeRepository.SaveChangesAsync();

                var createdEmployee = _mapper.Map<EmployeeDTO>(employee);
                _logger.LogInformation("Employee with ID {Id} created successfully.", createdEmployee.Id);
                return CreatedAtAction(nameof(GetEmployeeById), new { id = createdEmployee.Id }, createdEmployee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating a new employee.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Updates an existing employee in the warehouse.
        /// </summary>
        /// <param name="id">The ID of the employee to update.</param>
        /// <param name="updateEmployeeDTO">The updated employee data.</param>
        /// <response code="204">If the employee was updated successfully.</response>
        /// <response code="404">If the employee is not found.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateEmployee(int id, [FromBody] UpdateEmployeeDTO updateEmployeeDTO)
        {
            if (updateEmployeeDTO == null)
            {
                return BadRequest("Employee data is null.");
            }

            try
            {
                var employeeEntity = await _employeeRepository.GetEmployeeByIdAsync(id);
                if (employeeEntity == null)
                {
                    _logger.LogWarning("Employee with ID {Id} not found for update.", id);
                    return NotFound($"Employee with ID {id} was not found.");
                }

                _mapper.Map(updateEmployeeDTO, employeeEntity);
                await _employeeRepository.SaveChangesAsync();

                _logger.LogInformation("Employee with ID {Id} updated successfully.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating employee with ID {Id}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Deletes an employee from the warehouse.
        /// </summary>
        /// <param name="id">The ID of the employee to delete.</param>
        /// <response code="204">If the employee was deleted successfully.</response>
        /// <response code="404">If the employee is not found.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            try
            {
                var employeeEntity = await _employeeRepository.GetEmployeeByIdAsync(id);
                if (employeeEntity == null)
                {
                    _logger.LogWarning("Employee with ID {Id} not found for deletion.", id);
                    return NotFound($"Employee with ID {id} was not found.");
                }

                await _employeeRepository.DeleteEmployeeAsync(id);
                await _employeeRepository.SaveChangesAsync();

                _logger.LogInformation("Employee with ID {Id} deleted successfully.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting employee with ID {Id}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}

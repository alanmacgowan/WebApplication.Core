using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Core.Data.Interfaces;
using WebApplication.Core.Domain;

namespace WebApplication.Core.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // GET: api/employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            var results = await _employeeRepository.GetAllEmployeesAsync();

            return results.ToList();
        }

        // GET: api/employees/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var result = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (result == null) return NotFound();

            return result;
        }

        // POST: api/employees
        [HttpPost]
        public async Task<ActionResult<Employee>> Post(Employee employee)
        {
            var result = await _employeeRepository.CreateEmployeeAsync(employee);
            if (result != null)
            {
                return Created("employee", result);
            }
            else
            {
                return BadRequest("Failed to save new Employee");
            }
        }

        // PUT: api/employees
        [HttpPut]
        public async Task<ActionResult<Employee>> Put(Employee employee)
        {
            await _employeeRepository.EditEmployeeAsync(employee);

            return NoContent();
        }

        // DELETE: api/employees/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var oldEmployee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (oldEmployee == null) return NotFound();

            await _employeeRepository.DeleteEmployeeAsync(oldEmployee);

            return NoContent();
        }


    }
}

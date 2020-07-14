using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Core.Data;
using WebApplication.Core.Domain;

namespace WebApplication.Core.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly WebApplicationContext _dataContext;

        public EmployeesController(WebApplicationContext dataContext)
        {
            _dataContext = dataContext;
        }

        // GET: api/employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            var results = await _dataContext.Employees.AsNoTracking().ToListAsync();

            return results.ToList();
        }

        // GET: api/employees/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var result = await _dataContext.Employees.SingleOrDefaultAsync(x => x.Id == id);
            if (result == null) return NotFound();

            return result;
        }

        // POST: api/employees
        [HttpPost]
        public async Task<ActionResult<Employee>> Post(Employee employee)
        {
            var result = await _dataContext.AddAsync(employee);
            await _dataContext.SaveChangesAsync();
            if (result != null)
            {
                return Created("employee", result.Entity);
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
            _dataContext.Update(employee);
            await _dataContext.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/employees/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _dataContext.Employees.SingleOrDefaultAsync(x => x.Id == id);
            if (result == null) return NotFound();

            _dataContext.Remove(result);
            await _dataContext.SaveChangesAsync();

            return NoContent();
        }


    }
}

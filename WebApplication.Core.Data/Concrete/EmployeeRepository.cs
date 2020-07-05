using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Core.Data.Interfaces;
using WebApplication.Core.Domain;

namespace WebApplication.Core.Data.Concrete
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly WebApplicationContext _dataContext;

        public EmployeeRepository(WebApplicationContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            return await InsertAsync(employee);
        }

        public async Task<Employee> EditEmployeeAsync(Employee employee)
        {
            return await UpdateAsync(employee);
        }

        public async Task DeleteEmployeeAsync(Employee employee)
        {
            await DeleteAsync(employee);
        }
    }
}

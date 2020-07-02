using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Core.UI.Entities;

namespace WebApplication.Core.UI.Data.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<Employee> CreateEmployeeAsync(Employee employee);
        Task<Employee> EditEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(Employee employee);
    }
}

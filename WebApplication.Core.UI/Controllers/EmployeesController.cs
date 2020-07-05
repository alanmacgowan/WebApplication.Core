using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication.Core.Domain;
using WebApplication.Core.UI.Infrastructure.Services;
using WebApplication.Core.UI.Models;

namespace WebApplication.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        // GET: Employees
        public async Task<ActionResult> Index()
        {
            var results = await _employeeService.GetAllEmployeesAsync();

            return View(_mapper.Map<EmployeeViewModel[]>(results));
        }

        // GET: Employees/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var result = await _employeeService.GetEmployeeByIdAsync(id);
            if (result == null) return NotFound();

            return View(_mapper.Map<EmployeeViewModel>(result));
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
                await _employeeService.CreateEmployeeAsync(_mapper.Map<Employee>(employee));
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var result = await _employeeService.GetEmployeeByIdAsync(id);
            if (result == null) return NotFound();

            return View(_mapper.Map<EmployeeViewModel>(result));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
                var result = await _employeeService.GetEmployeeByIdAsync(employee.Id);
                _mapper.Map(employee, result);
                await _employeeService.EditEmployeeAsync(result);

                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _employeeService.GetEmployeeByIdAsync(id);
            if (result == null) return NotFound();

            return View(_mapper.Map<EmployeeViewModel>(result));
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var result = await _employeeService.GetEmployeeByIdAsync(id);
            await _employeeService.DeleteEmployeeAsync(result);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}

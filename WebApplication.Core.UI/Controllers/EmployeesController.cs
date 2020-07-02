using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using WebApplication.Core.UI.Data.Interfaces;
using WebApplication.Core.UI.Entities;
using WebApplication.Core.UI.Models;

namespace WebApplication.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        // GET: Employees
        public async Task<ActionResult> Index()
        {
            var results = await _employeeRepository.GetAllEmployeesAsync();

            return View(_mapper.Map<EmployeeViewModel[]>(results));
        }

        // GET: Employees/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var result = await _employeeRepository.GetEmployeeByIdAsync(id);
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
                await _employeeRepository.CreateEmployeeAsync(_mapper.Map<Employee>(employee));
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var result = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (result == null) return NotFound();

            return View(_mapper.Map<EmployeeViewModel>(result));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
                var result = await _employeeRepository.GetEmployeeByIdAsync(employee.Id);
                _mapper.Map(employee, result);
                await _employeeRepository.EditEmployeeAsync(result);

                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (result == null) return NotFound();

            return View(_mapper.Map<EmployeeViewModel>(result));
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var result = await _employeeRepository.GetEmployeeByIdAsync(id);
            await _employeeRepository.DeleteEmployeeAsync(result);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}

using AutoMapper;
using FootballStats.API.Infrastructure.Profiles;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using WebApplication.Controllers;
using WebApplication.Core.Domain;
using WebApplication.Core.UI.Infrastructure.Services;
using WebApplication.Core.UI.Models;
using Xunit;

namespace WebApplication.Core.Tests.Unit
{
    public class EmployeesControllerTests
    {

        private Mock<IEmployeeService> _employeeService;
        private EmployeesController _employeesController;
        private MapperConfiguration _mapperConfig;

        public EmployeesControllerTests()
        {
            _employeeService = new Mock<IEmployeeService>();
            _mapperConfig = new MapperConfiguration(cfg => { cfg.AddProfile(new MapperProfile()); });
            _employeesController = new EmployeesController(_employeeService.Object, _mapperConfig.CreateMapper());
        }

        [Fact]
        public void Details_Test()
        {
            //Arrange
            var employee = new Employee() { Id = 1, FirstName = "Juan", LastName = "Perez" };
            _employeeService.Setup(x => x.GetEmployeeByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(employee));

            //Act
            var result = _employeesController.Details(1) as Task<ActionResult>;
            var resultValue = result.Result as ViewResult;

            //Assert
            Assert.Equal((resultValue.Model as EmployeeViewModel).FirstName, employee.FirstName);
        }

    }
}

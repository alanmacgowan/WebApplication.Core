using AutoMapper;
using FootballStats.API.Infrastructure.Profiles;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using WebApplication.Controllers;
using WebApplication.Core.UI.Data.Interfaces;
using WebApplication.Core.UI.Entities;
using WebApplication.Core.UI.Models;
using Xunit;

namespace WebApplication.Core.Tests.Unit
{
    public class EmployeesControllerTests
    {

        private Mock<IEmployeeRepository> _employeeRepository;
        private EmployeesController _employeesController;
        private MapperConfiguration _mapperConfig;

        public EmployeesControllerTests()
        {
            _employeeRepository = new Mock<IEmployeeRepository>();
            _mapperConfig = new MapperConfiguration(cfg => { cfg.AddProfile(new MapperProfile()); });
            _employeesController = new EmployeesController(_employeeRepository.Object, _mapperConfig.CreateMapper());
        }

        [Fact]
        public void Details_Test()
        {
            //Arrange
            var employee = new Employee() { Id = 1, FirstName = "Juan", LastName = "Perez" };
            _employeeRepository.Setup(x => x.GetEmployeeByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(employee));

            //Act
            var result = _employeesController.Details(1) as Task<ActionResult>;
            var resultValue = result.Result as ViewResult;

            //Assert
            Assert.Equal((resultValue.Model as EmployeeViewModel).FirstName, employee.FirstName);
        }

    }
}

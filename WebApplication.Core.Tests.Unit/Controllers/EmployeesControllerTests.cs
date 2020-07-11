namespace WebApplication.Core.Tests.Unit.Controllers
{
    using WebApplication.Controllers;
    using System;
    using Xunit;
    using Moq;
    using WebApplication.Core.UI.Infrastructure.Services;
    using AutoMapper;
    using WebApplication.Core.UI.Models;
    using System.Threading.Tasks;
    using FootballStats.API.Infrastructure.Profiles;
    using WebApplication.Core.Domain;
    using Microsoft.AspNetCore.Mvc;

    public class EmployeesControllerTests
    {
        private EmployeesController _testClass;
        private Mock<IEmployeeService> _employeeService;
        private IMapper _mapper;

        public EmployeesControllerTests()
        {
            _employeeService = new Mock<IEmployeeService>();
            _mapper = new MapperConfiguration(cfg => { cfg.AddProfile(new MapperProfile()); }).CreateMapper();
            _testClass = new EmployeesController(_employeeService.Object, _mapper);
        }

        [Fact]
        public void Details_Test()
        {
            //Arrange
            var employee = new Employee() { Id = 1, FirstName = "Juan", LastName = "Perez" };
            _employeeService.Setup(x => x.GetEmployeeByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(employee));

            //Act
            var result = _testClass.Details(1) as Task<ActionResult>;
            var resultValue = result.Result as ViewResult;

            //Assert
            Assert.Equal((resultValue.Model as EmployeeViewModel).FirstName, employee.FirstName);
        }

        [Fact]
        public void CanConstruct()
        {
            var instance = new EmployeesController(_employeeService.Object, _mapper);
            Assert.NotNull(instance);
        }

        [Fact]
        public void CannotCallCreateWithEmployeeViewModelWithNullEmployee()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => _testClass.Create(default(EmployeeViewModel)));
        }

        [Fact]
        public void CannotCallEditWithEmployeeViewModelWithNullEmployee()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => _testClass.Edit(default(EmployeeViewModel)));
        }

    }
}
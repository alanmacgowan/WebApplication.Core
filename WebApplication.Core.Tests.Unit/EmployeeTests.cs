namespace WebApplication.Core.Tests.Unit
{
    using WebApplication.Core.Domain;
    using System;
    using Xunit;

    public class EmployeeTests
    {
        private Employee _testClass;

        public EmployeeTests()
        {
            _testClass = new Employee();
        }

        [Fact]
        public void CanConstruct()
        {
            var instance = new Employee();
            Assert.NotNull(instance);
        }

        [Fact]
        public void CanSetAndGetFirstName()
        {
            var testValue = "TestValue69997117";
            _testClass.FirstName = testValue;
            Assert.Equal(testValue, _testClass.FirstName);
        }

        [Fact]
        public void CanSetAndGetLastName()
        {
            var testValue = "TestValue614069338";
            _testClass.LastName = testValue;
            Assert.Equal(testValue, _testClass.LastName);
        }

        [Fact]
        public void CanSetAndGetBirthDate()
        {
            var testValue = new DateTime(1026705144);
            _testClass.BirthDate = testValue;
            Assert.Equal(testValue, _testClass.BirthDate);
        }

        [Fact]
        public void CanSetAndGetCountry()
        {
            var testValue = "TestValue749357912";
            _testClass.Country = testValue;
            Assert.Equal(testValue, _testClass.Country);
        }
    }
}
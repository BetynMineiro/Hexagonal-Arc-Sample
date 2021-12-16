using System;
using Application.AwardsUserCases;
using Application.AwardsUserCases.Dto;
using Domain.Entites;
using Domain.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace VestingProblem.Test.ApplicationCore.Application.AwardsUserCases
{
    public class AwardsUserCasesTest
    {
        private readonly Mock<IEmployeeAwardRepository> _Repository = new Mock<IEmployeeAwardRepository>();

        [Fact]
        [Trait("AwardsUserCases", nameof(AwardsUserCasesTest))]
        public void Should_Create_Instance_A_VestingAwardUserCase_And_Executing_Venting()
        {
            // Arrange
            var userCase = new VestingAwardUserCase(_Repository.Object);
            var id = "E03";
            var name = "Robert C. Martin";
            var award = "ISO-03";
            var date = "09-03-2011";
            var quantity = 100;


            _Repository.Setup(c => c.ApplyEmployeeChanges(It.IsAny<Employee>())).Returns(() =>
            {
                var employee = new Employee(id, name);
                return employee;
            });

            var input = new VestingAwardOperationInput(new string[] { "VEST", id, name, award, date, quantity.ToString() });

            // Act
             var output = userCase.Execute(input, DateTime.Now, 0);

            // Assert
            _Repository.Verify(c => c.ApplyEmployeeChanges(It.IsAny<Employee>()), Times.Once);
            _Repository.Verify(c => c.GetEmployee(It.IsAny<string>()), Times.Once);
            output.Should().BeAssignableTo(typeof(VestingAwardOperationOutput));
            output.AwardId.Should().Be(award);
            output.EmployeeId.Should().Be(id);
            output.EmployeeName.Should().Be(name);
            output.Quantity.Should().Be(quantity);

        }

        [Fact]
        [Trait("AwardsUserCases", nameof(AwardsUserCasesTest))]
        public void Should_Create_Instance_A_CancelAwardUserCase_And_Executing_Cancel()
        {
            // Arrange
            var userCase = new CancelAwardUserCase(_Repository.Object);
            var id = "E03";
            var name = "Robert C. Martin";
            var award = "ISO-03";
            var date = "09-03-2011";
            var quantity = 100;
            var expectedQuantity = 900;

            _Repository.Setup(c => c.GetEmployee(It.IsAny<string>())).Returns(() =>
            {
                var employee = new Employee(id, name);
                employee.VestingAward(award, 1000, 0);
                return employee;
            });

            _Repository.Setup(c => c.ApplyEmployeeChanges(It.IsAny<Employee>())).Returns(() =>
            {
                var employee = new Employee(id, name);
                return employee;
            });

            var input = new VestingAwardOperationInput(new string[] { "CANCEL", id, name, award, date, quantity.ToString() });

            // Act
            var output = userCase.Execute(input, DateTime.Now, 0);

            // Assert
            _Repository.Verify(c => c.ApplyEmployeeChanges(It.IsAny<Employee>()), Times.Once);
            _Repository.Verify(c => c.GetEmployee(It.IsAny<string>()), Times.Once);
            output.Should().BeAssignableTo(typeof(VestingAwardOperationOutput));
            output.AwardId.Should().Be(award);
            output.EmployeeId.Should().Be(id);
            output.EmployeeName.Should().Be(name);
            output.Quantity.Should().Be(expectedQuantity);

        }
    }
}

using System;
using Application.AwardsUserCases;
using Application.AwardsUserCases.Dto;
using Application.Factories.Awards;
using Domain.Entites;
using Domain.Enuns;
using Domain.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace VestingProblem.Test.ApplicationCore.Application.Factories.Awards
{
    public class AwardUserCaseStrategyFactoryTest
    {
        private readonly Mock<IEmployeeAwardRepository> _Repository = new Mock<IEmployeeAwardRepository>();

        [Fact]
        [Trait("Factories", nameof(AwardUserCaseStrategyFactoryTest))]
        public void Employee_Should_Create_Instance_And_Return_a_VestingAwardUserCase_In_Strategy()
        {
            // Arrange
            var factory = new AwardUserCaseStrategyFactory(_Repository.Object);
            var input = new VestingAwardOperationInput(new string[] { "VEST", "E03", "Robert C. Martin", "ISO-03", "09-03-2011", "100" });

            // Act
            var strategy = factory.CreateStrategy(input);

            // Assert
            strategy.Should().NotBeNull();
            strategy.Should().BeAssignableTo(typeof(VestingAwardUserCase));

        }

        [Fact]
        [Trait("Factories", nameof(AwardUserCaseStrategyFactoryTest))]
        public void Employee_Should_Create_Instance_And_Return_a_CancelAwardUserCase_In_Strategy()
        {
            // Arrange
            var factory = new AwardUserCaseStrategyFactory(_Repository.Object);
            var input = new VestingAwardOperationInput(new string[] { "CANCEL", "E03", "Robert C. Martin", "ISO-03", "09-03-2011", "100" });

            // Act
            var strategy = factory.CreateStrategy(input);

            // Assert
            strategy.Should().NotBeNull();
            strategy.Should().BeAssignableTo(typeof(CancelAwardUserCase));

        }

        [Fact]
        [Trait("Factories", nameof(AwardUserCaseStrategyFactoryTest))]
        public void Employee_Should_Create_Instance_And_Return_a_VestingAwardUserCase_In_Strategy_And_Executing_Venting()
        {
            // Arrange
            var factory = new AwardUserCaseStrategyFactory(_Repository.Object);
            var id = "E03";
            var name = "Robert C. Martin";
            var award = "ISO-03";
            var date = "09-03-2011";
            var quantity = 100;



            _Repository.Setup(c => c.ApplyEmployeeChanges(It.IsAny<Employee>())).Returns(() =>
            {
                var employee = new Employee(id, name);
                employee.VestingAward(award, quantity,0);
                return employee;
            });

            var input = new VestingAwardOperationInput(new string[] { "VEST", id, name, award, date, quantity.ToString() });

            // Act
            var strategy = factory.CreateStrategy(input);
            var output = strategy.Execute(input,DateTime.Now,0);

            // Assert
            strategy.Should().NotBeNull();
            strategy.Should().BeAssignableTo(typeof(VestingAwardUserCase));
            _Repository.Verify(c => c.ApplyEmployeeChanges(It.IsAny<Employee>()), Times.Once);
            _Repository.Verify(c => c.GetEmployee(It.IsAny<string>()), Times.Once);
            output.Should().BeAssignableTo(typeof(VestingAwardOperationOutput));
            output.AwardId.Should().Be(award);
            output.EmployeeId.Should().Be(id);
            output.EmployeeName.Should().Be(name);
            output.Quantity.Should().Be(quantity);

        }

        [Fact]
        [Trait("Factories", nameof(AwardUserCaseStrategyFactoryTest))]
        public void Employee_Should_Create_Instance_And_Return_a_VestingAwardUserCase_In_Strategy_Execute_Vesting_Out_Off_Date_And_Quantity_Equals_0()
        {
            // Arrange
            var factory = new AwardUserCaseStrategyFactory(_Repository.Object);
            var id = "E03";
            var name = "Robert C. Martin";
            var award = "ISO-03";
            var date = "09-03-2011";
            var quantity = 100;



            _Repository.Setup(c => c.ApplyEmployeeChanges(It.IsAny<Employee>())).Returns(() =>
            {
                var employee = new Employee(id, name);
                employee.VestingAward(award, quantity,0);
                return employee;
            });

            var input = new VestingAwardOperationInput(new string[] { "VEST", id, name, award, date, quantity.ToString() });
            input.OperationDate = DateTime.Now;

            // Act
            var strategy = factory.CreateStrategy(input);
            var output = strategy.Execute(input, DateTime.Now.AddDays(-10),0);

            // Assert
            strategy.Should().NotBeNull();
            strategy.Should().BeAssignableTo(typeof(VestingAwardUserCase));
            _Repository.Verify(c => c.ApplyEmployeeChanges(It.IsAny<Employee>()), Times.Once);
            _Repository.Verify(c => c.GetEmployee(It.IsAny<string>()), Times.Once);
            output.Should().BeAssignableTo(typeof(VestingAwardOperationOutput));
            output.AwardId.Should().Be(award);
            output.EmployeeId.Should().Be(id);
            output.EmployeeName.Should().Be(name);
            output.Quantity.Should().Be(decimal.Zero);

        }

        [Fact]
        [Trait("Factories", nameof(AwardUserCaseStrategyFactoryTest))]
        public void Employee_Should_Create_Instance_And_Return_a_CancelAwardUserCase_In_Strategy_Execute_Cancel_Out_Off_Date_And_Quantity_Equals_0()
        {
            // Arrange
            var factory = new AwardUserCaseStrategyFactory(_Repository.Object);
            var id = "E03";
            var name = "Robert C. Martin";
            var award = "ISO-03";
            var date = "09-03-2011";
            var quantity = 100;



            _Repository.Setup(c => c.ApplyEmployeeChanges(It.IsAny<Employee>())).Returns(() =>
            {
                var employee = new Employee(id, name);
                employee.VestingAward(award, quantity,0);
                return employee;
            });

            var input = new VestingAwardOperationInput(new string[] { "CANCEL", id, name, award, date, quantity.ToString() });
            input.OperationDate = DateTime.Now;

            // Act
            var strategy = factory.CreateStrategy(input);
            var output = strategy.Execute(input, DateTime.Now.AddDays(-10),0);

            // Assert
            strategy.Should().NotBeNull();
            strategy.Should().BeAssignableTo(typeof(CancelAwardUserCase));
            _Repository.Verify(c => c.ApplyEmployeeChanges(It.IsAny<Employee>()), Times.Once);
            _Repository.Verify(c => c.GetEmployee(It.IsAny<string>()), Times.Once);
            output.Should().BeAssignableTo(typeof(VestingAwardOperationOutput));
            output.AwardId.Should().Be(award);
            output.EmployeeId.Should().Be(id);
            output.EmployeeName.Should().Be(name);
            output.Quantity.Should().Be(decimal.Zero);

        }

        [Fact]
        [Trait("Factories", nameof(AwardUserCaseStrategyFactoryTest))]
        public void Employee_Should_Create_Instance_And_Return_a_VestingAwardUserCase_In_Strategy_And_Executing_Vesting_Increment_Position()
        {
            // Arrange
            var factory = new AwardUserCaseStrategyFactory(_Repository.Object);
            var id = "E03";
            var name = "Robert C. Martin";
            var award = "ISO-03";
            var quantity = 100;
            var date = "09-03-2011";
            var expectedQuantity = 200;

            _Repository.Setup(c => c.GetEmployee(It.IsAny<string>())).Returns(() =>
            {
                var employee = new Employee(id, name);
                employee.VestingAward(award, quantity,0);
                return employee;
            });

            _Repository.Setup(c => c.ApplyEmployeeChanges(It.IsAny<Employee>())).Returns(() =>
            {
                var employee = new Employee(id, name);
                return employee;
            });

            var input = new VestingAwardOperationInput(new string[] { "VEST", id, name, award, date, quantity.ToString() });

            // Act
            var strategy = factory.CreateStrategy(input);
            var output = strategy.Execute(input, DateTime.Now,0);

            // Assert
            strategy.Should().NotBeNull();
            strategy.Should().BeAssignableTo(typeof(VestingAwardUserCase));
            _Repository.Verify(c => c.ApplyEmployeeChanges(It.IsAny<Employee>()), Times.Once);
            _Repository.Verify(c => c.GetEmployee(It.IsAny<string>()), Times.Once);
            output.Should().BeAssignableTo(typeof(VestingAwardOperationOutput));
            output.AwardId.Should().Be(award);
            output.EmployeeId.Should().Be(id);
            output.EmployeeName.Should().Be(name);
            output.Quantity.Should().Be(expectedQuantity);

        }

        [Fact]
        [Trait("Factories", nameof(AwardUserCaseStrategyFactoryTest))]
        public void Employee_Should_Create_Instance_And_Return_a_CancelAwardUserCase_In_Strategy_And_Executing_Cancel()
        {
            // Arrange
            var factory = new AwardUserCaseStrategyFactory(_Repository.Object);
            var id = "E03";
            var name = "Robert C. Martin";
            var award = "ISO-03";
            var quantity = 100;
            var date = "09-03-2011";


            _Repository.Setup(c => c.ApplyEmployeeChanges(It.IsAny<Employee>())).Returns(() =>
            {
                var employee = new Employee(id, name);
                return employee;
            });

            var input = new VestingAwardOperationInput(new string[] { "CANCEL", id, name, award, date, quantity.ToString() });

            // Act
            var strategy = factory.CreateStrategy(input);
            var output = strategy.Execute(input, DateTime.Now,0);

            // Assert
            strategy.Should().NotBeNull();
            strategy.Should().BeAssignableTo(typeof(CancelAwardUserCase));
            _Repository.Verify(c => c.ApplyEmployeeChanges(It.IsAny<Employee>()), Times.Once);
            _Repository.Verify(c => c.GetEmployee(It.IsAny<string>()), Times.Once);
            output.Should().BeAssignableTo(typeof(VestingAwardOperationOutput));
            output.AwardId.Should().Be(award);
            output.EmployeeId.Should().Be(id);
            output.EmployeeName.Should().Be(name);
            output.Quantity.Should().Be(decimal.Zero);

        }

        [Fact]
        [Trait("Factories", nameof(AwardUserCaseStrategyFactoryTest))]
        public void Employee_Should_Create_Instance_And_Return_a_CancelAwardUserCase_In_Strategy_And_Executing_Cancel_Decrement_Position()
        {
            // Arrange
            var factory = new AwardUserCaseStrategyFactory(_Repository.Object);
            var id = "E03";
            var name = "Robert C. Martin";
            var award = "ISO-03";
            var quantity = 1000;
            var decrementQuantity = 800;
            var expectedQuantity = 200;
            var date = "09-03-2011";

            _Repository.Setup(c => c.GetEmployee(It.IsAny<string>())).Returns(() =>
            {
                var employee = new Employee(id, name);
                employee.VestingAward(award, quantity,0);
                return employee;
            });

            _Repository.Setup(c => c.ApplyEmployeeChanges(It.IsAny<Employee>())).Returns(() =>
            {
                var employee = new Employee(id, name);
                return employee;
            });

            var input = new VestingAwardOperationInput(new string[] { "CANCEL", id, name, award, date, decrementQuantity.ToString() });

            // Act
            var strategy = factory.CreateStrategy(input);
            var output = strategy.Execute(input, DateTime.Now,0);

            // Assert
            strategy.Should().NotBeNull();
            strategy.Should().BeAssignableTo(typeof(CancelAwardUserCase));
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

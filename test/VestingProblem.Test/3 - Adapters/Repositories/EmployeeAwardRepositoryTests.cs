using System;
using Domain.Entites;
using FluentAssertions;
using SimpleStorage.Repositories;
using Xunit;

namespace VestingProblem.Test.Adapters.Repositories
{
    public class EmployeeAwardRepositoryTests
    {
        [Fact]
        [Trait("Repositories", nameof(EmployeeAwardRepositoryTests))]
        public void Repository_Should_Create_Instance()
        {
            // Arrange
            var repository = new EmployeeAwardRepository();
            var id = "E002";
            var name = "Martin Fowler";
            var award = "ISO-02";
            var quantity = 1000;
            var employee = new Employee(id, name);

            // Act
            employee.VestingAward(award, quantity,0);
            var assert = repository.ApplyEmployeeChanges(employee);

            // Assert
            assert.Id.Should().Be(id);
            assert.Name.Should().Be(name);
            assert.Awards.Should().NotBeEmpty();
            assert.Awards.Should().OnlyContain(c => c.Key == award);
            assert.Awards[award].Quantity.Should().Be(quantity);
        }

        [Fact]
        [Trait("Repositories", nameof(EmployeeAwardRepositoryTests))]
        public void Repository_Should_Create_Instance_And_Add_A_Single_Award_And_Increment_The_Quantity_This_Award()
        {
            // Arrange
            var repository = new EmployeeAwardRepository();
            var id = "E002";
            var name = "Martin Fowler";
            var award = "ISO-02";
            var quantity = 80.20M;
            var expectedQuantity = 500.65M;
            var employee = new Employee(id, name);


            // Act
            employee.VestingAward(award, quantity,2);
            repository.ApplyEmployeeChanges(employee);
            employee.VestingAward(award, 420.45M,2);
            var assert = repository.ApplyEmployeeChanges(employee);

            // Assert
            assert.Id.Should().Be(id);
            assert.Name.Should().Be(name);
            assert.Awards.Should().NotBeEmpty();
            assert.Awards.Should().OnlyContain(c => c.Key == award);
            assert.Awards[award].Quantity.Should().Be(expectedQuantity);
        }

        [Fact]
        [Trait("Repositories", nameof(EmployeeAwardRepositoryTests))]
        public void Repository_Should_Create_Instance_And_Try_Cancel_A_Nonexistent_Award()
        {
            // Arrange
            var repository = new EmployeeAwardRepository();
            var id = "E002";
            var name = "Martin Fowler";
            var award = "ISO-02";
            var quantity = 100;
            var expectedQuantity = 0;
            var employee = new Employee(id, name);

            // Act
            employee.VestingCancel(award, quantity,0);
            var assert = repository.ApplyEmployeeChanges(employee);

            // Assert
            assert.Id.Should().Be(id);
            assert.Name.Should().Be(name);
            assert.Awards.Should().NotBeEmpty();
            assert.Awards.Should().OnlyContain(c => c.Key == award);
            assert.Awards[award].Quantity.Should().Be(expectedQuantity);

        }

        [Fact]
        [Trait("Repositories", nameof(EmployeeAwardRepositoryTests))]
        public void Employee_Should_Create_Instance_And_Add_A_Many_Awards_And_Increment_The_Quantity_This_Awards()
        {
            // Arrange
            var repository = new EmployeeAwardRepository();
            var id = "E002";
            var name = "Martin Fowler";
            var award_1 = "ISO-01";
            var award_2 = "ISO-02";
            var award_3 = "ISO-03";
            var quantity_1 = 100;
            var quantity_2 = 200;
            var quantity_3 = 300;

            var expectedQuantity_1 = 500.62M;
            var expectedQuantity_2 = 500.65M;
            var expectedQuantity_3 = 500.70M;
            var employee = new Employee(id, name);
            // Act

            employee.VestingAward(award_1, quantity_1,2);
            repository.ApplyEmployeeChanges(employee);

            employee.VestingAward(award_2, quantity_2,2);
            repository.ApplyEmployeeChanges(employee);

            employee.VestingAward(award_3, quantity_3,2);
            repository.ApplyEmployeeChanges(employee);

            employee.VestingAward(award_1, 400.62M,2);
            repository.ApplyEmployeeChanges(employee);

            employee.VestingAward(award_2, 300.65M,2);
            repository.ApplyEmployeeChanges(employee);

            employee.VestingAward(award_3, 200.7M,2);
            repository.ApplyEmployeeChanges(employee);

            var assert = repository.ApplyEmployeeChanges(employee);
            // Assert
            assert.Id.Should().Be(id);
            assert.Name.Should().Be(name);
            assert.Awards.Should().NotBeEmpty();
            assert.Awards.Should().ContainKeys(award_1, award_2, award_3);

            assert.Awards[award_1].Quantity.Should().Be(expectedQuantity_1);
            assert.Awards[award_2].Quantity.Should().Be(expectedQuantity_2);
            assert.Awards[award_3].Quantity.Should().Be(expectedQuantity_3);

        }

        [Fact]
        [Trait("Repositories", nameof(EmployeeAwardRepositoryTests))]
        public void Repository_Should_Return_A_Null_Instance_Employee()
        {
            // Arrange
            var repository = new EmployeeAwardRepository();
            var id = "E002";

            // Act
            var assert = repository.GetEmployee(id);

            // Assert
            assert.Should().BeNull();

        }

        [Fact]
        [Trait("Repositories", nameof(EmployeeAwardRepositoryTests))]
        public void Repository_Should_Return_A_Instance_Employee_ID_E002()
        {
            // Arrange
            var repository = new EmployeeAwardRepository();
            var id = "E002";
            var name = "Martin Fowler";
            var employee = new Employee(id, name);

            // Act
            repository.ApplyEmployeeChanges(employee);
            var assert = repository.GetEmployee(id);
            // Assert
            assert.Should().NotBeNull();
            assert.Should().BeAssignableTo(typeof(Employee));
            assert.Id.Should().Be(id);
            assert.Name.Should().Be(name);
          
        }
    }
}

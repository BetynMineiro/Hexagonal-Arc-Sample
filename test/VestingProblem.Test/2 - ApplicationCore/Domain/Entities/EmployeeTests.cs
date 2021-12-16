using System;
using Domain.Entites;
using FluentAssertions;
using Xunit;

namespace VestingProblem.Test.ApplicationCore.Domain.Entities
{
    public class EmployeeTests
    {

        [Fact]
        [Trait("Entities", nameof(EmployeeTests))]
        public void Employee_Should_Create_Instance()
        {
            // Arrange
            var id = "E001";
            var name = "Voughn Vernon";

            // Act
            var employee = new Employee(id, name);

            // Assert
            employee.Id.Should().Be(id);
            employee.Name.Should().Be(name);
            employee.Awards.Should().BeEmpty();

        }

        [Fact]
        [Trait("Entities", nameof(EmployeeTests))]
        public void Employee_Should_Create_Instance_And_Add_A_Single_Award()
        {
            // Arrange
            var id = "E001";
            var name = "Voughn Vernon";
            var award = "ISO-01";
            var quantity = 100;

            // Act
            var employee = new Employee(id, name);
            employee.VestingAward(award, quantity,0);

            // Assert
            employee.Id.Should().Be(id);
            employee.Name.Should().Be(name);
            employee.Awards.Should().NotBeEmpty();
            employee.Awards.Should().OnlyContain(c => c.Key == award);
            employee.Awards[award].Quantity.Should().Be(quantity);

        }


        [Fact]
        [Trait("Entities", nameof(EmployeeTests))]
        public void Employee_Should_Create_Instance_And_Add_A_Single_Award_And_Increment_The_Quantity_This_Award()
        {
            // Arrange
            var id = "E001";
            var name = "Voughn Vernon";
            var award = "ISO-01";
            var quantity = 80.20M;
            var expectedQuantity = 500.65M;

            // Act
            var employee = new Employee(id, name);
            employee.VestingAward(award, quantity,2);
            employee.VestingAward(award, 420.45M,2);

            // Assert
            employee.Id.Should().Be(id);
            employee.Name.Should().Be(name);
            employee.Awards.Should().NotBeEmpty();
            employee.Awards.Should().OnlyContain(c => c.Key == award);
            employee.Awards[award].Quantity.Should().Be(expectedQuantity);
        }

        [Fact]
        [Trait("Entities", nameof(EmployeeTests))]
        public void Employee_Should_Create_Instance_And_Add_A_Many_Awards()
        {
            // Arrange
            var id = "E001";
            var name = "Voughn Vernon";
            var award_1 = "ISO-01";
            var award_2 = "ISO-02";
            var award_3 = "ISO-03";
            var quantity_1 = 100;
            var quantity_2 = 200;
            var quantity_3 = 300;

            // Act
            var employee = new Employee(id, name);
            employee.VestingAward(award_1, quantity_1,0);
            employee.VestingAward(award_2, quantity_2,0);
            employee.VestingAward(award_3, quantity_3,0);

            // Assert
            employee.Id.Should().Be(id);
            employee.Name.Should().Be(name);
            employee.Awards.Should().NotBeEmpty();
            employee.Awards.Should().ContainKeys(award_1, award_2, award_3);

            employee.Awards[award_1].Quantity.Should().Be(quantity_1);
            employee.Awards[award_2].Quantity.Should().Be(quantity_2);
            employee.Awards[award_3].Quantity.Should().Be(quantity_3);

        }

        [Fact]
        [Trait("Entities", nameof(EmployeeTests))]
        public void Employee_Should_Create_Instance_And_Add_A_Many_Awards_And_Increment_The_Quantity_This_Awards()
        {
            // Arrange
            var id = "E001";
            var name = "Voughn Vernon";
            var award_1 = "ISO-01";
            var award_2 = "ISO-02";
            var award_3 = "ISO-03";
            var quantity_1 = 100;
            var quantity_2 = 200;
            var quantity_3 = 300;

            var expectedQuantity_1 = 500.62M;
            var expectedQuantity_2 = 500.65M;
            var expectedQuantity_3 = 500.70M;
            // Act
            var employee = new Employee(id, name);
            employee.VestingAward(award_1, quantity_1,2);
            employee.VestingAward(award_2, quantity_2,2);
            employee.VestingAward(award_3, quantity_3,2);

            employee.VestingAward(award_1, 400.62M,2);
            employee.VestingAward(award_2, 300.65M,2);
            employee.VestingAward(award_3, 200.7M,2);

            // Assert
            employee.Id.Should().Be(id);
            employee.Name.Should().Be(name);
            employee.Awards.Should().NotBeEmpty();
            employee.Awards.Should().ContainKeys(award_1, award_2, award_3);

            employee.Awards[award_1].Quantity.Should().Be(expectedQuantity_1);
            employee.Awards[award_2].Quantity.Should().Be(expectedQuantity_2);
            employee.Awards[award_3].Quantity.Should().Be(expectedQuantity_3);

        }

        [Fact]
        [Trait("Entities", nameof(EmployeeTests))]
        public void Employee_Should_Create_Instance_And_Try_Cancel_A_Nonexistent_Award()
        {
            // Arrange
            var id = "E001";
            var name = "Voughn Vernon";
            var award = "ISO-01";
            var quantity = 100;
            var expectedQuantity = 0;

            // Act
            var employee = new Employee(id, name);
            employee.VestingCancel(award, quantity,0);

            // Assert
            employee.Id.Should().Be(id);
            employee.Name.Should().Be(name);
            employee.Awards.Should().NotBeEmpty();
            employee.Awards.Should().OnlyContain(c => c.Key == award);
            employee.Awards[award].Quantity.Should().Be(expectedQuantity);

        }

        [Fact]
        [Trait("Entities", nameof(EmployeeTests))]
        public void Employee_Should_Create_Instance_And_Try_Cancel_A_Partial_Quantity_Award()
        {
            // Arrange
            var id = "E001";
            var name = "Voughn Vernon";
            var award = "ISO-01";
            var quantity = 100;
            var expectedQuantity = 80;

            // Act
            var employee = new Employee(id, name);
            employee.VestingAward(award, quantity,0);
            employee.VestingCancel(award, 20,0);

            // Assert
            employee.Id.Should().Be(id);
            employee.Name.Should().Be(name);
            employee.Awards.Should().NotBeEmpty();
            employee.Awards.Should().OnlyContain(c => c.Key == award);
            employee.Awards[award].Quantity.Should().Be(expectedQuantity);

        }

        [Fact]
        [Trait("Entities", nameof(EmployeeTests))]
        public void Employee_Should_Create_Instance_And_Try_Cancel_Amount_More_Than_Due_In_Award()
        {
            // Arrange
            var id = "E001";
            var name = "Voughn Vernon";
            var award = "ISO-01";
            var quantity = 100;
            var expectedQuantity = 0;

            // Act
            var employee = new Employee(id, name);
            employee.VestingAward(award, quantity,0);
            employee.VestingCancel(award, 800,0);

            // Assert
            employee.Id.Should().Be(id);
            employee.Name.Should().Be(name);
            employee.Awards.Should().NotBeEmpty();
            employee.Awards.Should().OnlyContain(c => c.Key == award);
            employee.Awards[award].Quantity.Should().Be(expectedQuantity);

        }
    }
}

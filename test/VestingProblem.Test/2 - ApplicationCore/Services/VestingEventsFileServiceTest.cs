using System;
using System.Collections.Generic;
using System.IO;
using Application.AwardsUserCases;
using Application.AwardsUserCases.Dto;
using Application.Factories.Awards.Interfaces;
using Application.Services;
using CrossCutting.Services.Interfaces;
using Domain.Entites;
using Domain.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace VestingProblem.Test.ApplicationCore.Services
{
    public class VestingEventsFileServiceTest
    {
        private readonly Mock<IAwardUserCaseStrategyFactory> _awardUserCaseStrategyFactory = new Mock<IAwardUserCaseStrategyFactory>();
        private readonly Mock<IEmployeeAwardRepository> _Repository = new Mock<IEmployeeAwardRepository>();
        private readonly Mock<IFileService> _fileService = new Mock<IFileService>();

        [Fact]
        [Trait("Service", nameof(VestingEventsFileServiceTest))]
        public void Service_Should_Execute_A_VestingAwardUserCase_In_Strategy()
        {
            // Arrange
            var service = new VestingEventsFileService(_awardUserCaseStrategyFactory.Object, _fileService.Object);
            var inputs = new string[] { "testFile.csv", "2021-12-31", "0" };
            _fileService.Setup(c => c.ReadInputsFromCsvFile(It.IsAny<string>(), It.IsAny<string>())).Returns(new List<string[]> { new string[] { "VEST", "E002", "Bobby Jones", "ISO-002", "2020-01-01", "234" } });
            _awardUserCaseStrategyFactory.Setup(c => c.CreateStrategy(It.IsAny<VestingAwardOperationInput>())).Returns(new VestingAwardUserCase(_Repository.Object));
            var expectedOutput = new VestingAwardOperationOutput("E002", "Bobby Jones", "ISO-002", 234);
            // Act
            var (sucess, output) = service.ProcessFile(inputs, Directory.GetCurrentDirectory());

            // Assert
            sucess.Should().BeTrue();
            _Repository.Verify(c => c.GetEmployee(It.IsAny<string>()), Times.Once);
            _Repository.Verify(c => c.ApplyEmployeeChanges(It.IsAny<Employee>()), Times.Once);
            output.Should().BeAssignableTo(typeof(IList<VestingAwardOperationOutput>));
            output.Should().ContainEquivalentOf(expectedOutput);
        }

        [Fact]
        [Trait("Service", nameof(VestingEventsFileServiceTest))]
        public void Service_Should_Execute_A_CancelAwardUserCase_In_Strategy()
        {
            // Arrange
            var service = new VestingEventsFileService(_awardUserCaseStrategyFactory.Object, _fileService.Object);
            var inputs = new string[] { "testFile.csv", "2021-12-31", "0" };
            _fileService.Setup(c => c.ReadInputsFromCsvFile(It.IsAny<string>(), It.IsAny<string>())).Returns(new List<string[]> { new string[] { "CANCEL", "E002", "Bobby Jones", "ISO-002", "2020-01-01", "234" } });
            _awardUserCaseStrategyFactory.Setup(c => c.CreateStrategy(It.IsAny<VestingAwardOperationInput>())).Returns(new CancelAwardUserCase(_Repository.Object));
            var expectedOutput = new VestingAwardOperationOutput("E002", "Bobby Jones", "ISO-002", 0);
            // Act
            var (sucess, output) = service.ProcessFile(inputs, Directory.GetCurrentDirectory());

            // Assert
            sucess.Should().BeTrue();
            _Repository.Verify(c => c.GetEmployee(It.IsAny<string>()), Times.Once);
            _Repository.Verify(c => c.ApplyEmployeeChanges(It.IsAny<Employee>()), Times.Once);
            output.Should().BeAssignableTo(typeof(IList<VestingAwardOperationOutput>));
            output.Should().ContainEquivalentOf(expectedOutput);
        }
    }
}

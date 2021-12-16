using System;
using System.Collections.Generic;
using System.IO;
using CrossCutting.Services;
using FluentAssertions;
using Xunit;

namespace VestingProblem.Test.Commom.Services
{
    public class FileServicesTest
    {
        [Fact]
        [Trait("Service", nameof(FileServicesTest))]
        public void Service_Should_Read_SingleLine_In_File()
        {
            // Arrange
            var service = new FileService();
            var file = "test.csv";
            var expectedOutput = new List<string[]> { new string[] { "VEST", "E002", "Bobby Jones", "ISO-002", "2020-01-01", "234" } };
            // Act
            var output = service.ReadInputsFromCsvFile(file, Directory.GetCurrentDirectory());

            // Assert
            output.Should().BeAssignableTo(typeof(IList<string[]>));
            output.Should().BeEquivalentTo(expectedOutput);
        }
    }
}

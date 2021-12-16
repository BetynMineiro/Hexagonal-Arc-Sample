using System;
using CrossCutting.Extensions;
using FluentAssertions;
using Xunit;

namespace VestingProblem.Test.Commom.Extensions
{
    public class StringFormatExtensionTest
    {
        [Fact]
        [Trait("Extension", nameof(StringFormatExtensionTest))]
        public void Extension_Should_Return_True_For_Valid_Date_Format_US_String()
        {
            // Arrange
            var dateFormat = "MM-dd-yyyy";
            var date = "09-03-2011";


            // Act

            var isValid = date.IsValidDateFormat(dateFormat);

            // Assert
            isValid.Should().BeTrue();

        }

        [Fact]
        [Trait("Extension", nameof(StringFormatExtensionTest))]
        public void Extension_Should_Return_True_For_Valid_Date_Whith_Default_Format_US_String()
        {
            // Arrange
            var date = "2011-09-03";


            // Act

            var isValid = date.IsValidDateFormat();

            // Assert
            isValid.Should().BeTrue();

        }

        [Fact]
        [Trait("Extension", nameof(StringFormatExtensionTest))]
        public void Extension_Should_Return_True_For_Valid_Date_Format_BR_String()
        {
            // Arrange
            var dateFormat = "dd/MM/yyyy";
            var date = "01/02/2016";


            // Act

            var isValid = date.IsValidDateFormat(dateFormat);

            // Assert
            isValid.Should().BeTrue();

        }


        [Fact]
        [Trait("Extension", nameof(StringFormatExtensionTest))]
        public void Extension_Should_Return_False_For_InValid_Date_Format_String()
        {
            // Arrange
            var dateFormat = "MM-dd-xxyy";
            var date = "09-03-2011";


            // Act

            var isValid = date.IsValidDateFormat(dateFormat);

            // Assert
            isValid.Should().BeFalse();

        }
    }
}

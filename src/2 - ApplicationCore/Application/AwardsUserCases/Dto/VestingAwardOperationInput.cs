using System;
using CrossCutting.Extensions;
using Domain.Enuns;

namespace Application.AwardsUserCases.Dto
{
    public class VestingAwardOperationInput
    {

        public OperationType OperationType { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string AwardId { get; set; }
        public DateTime OperationDate { get; set; }
        public decimal Quantity { get; set; }


        public VestingAwardOperationInput(string[] input)
        {
            OperationType = GetOperationType(input[0]);
            EmployeeId = string.IsNullOrWhiteSpace(input[1]) ? string.Empty : input[1];
            EmployeeName = string.IsNullOrWhiteSpace(input[2]) ? string.Empty : input[2];
            AwardId = string.IsNullOrWhiteSpace(input[3]) ? string.Empty : input[3];
            OperationDate = input[4] is null || input[4].IsValidDateFormat() ? Convert.ToDateTime(input[4]) : DateTime.MinValue;
            Quantity = string.IsNullOrWhiteSpace(input[5]) ? decimal.MinusOne : Convert.ToDecimal(input[5]);
        }

        private OperationType GetOperationType(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) { return OperationType.Undefined; }
            return input.Equals("VEST", StringComparison.CurrentCultureIgnoreCase) ? OperationType.Vest : OperationType.Cancel;
        }
    }
}

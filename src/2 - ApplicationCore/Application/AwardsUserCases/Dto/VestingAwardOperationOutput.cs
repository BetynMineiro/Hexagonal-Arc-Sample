using System;
using Domain.Entites;

namespace Application.AwardsUserCases.Dto
{
    public class VestingAwardOperationOutput
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string AwardId { get; set; }
        public decimal Quantity { get; set; }

        public VestingAwardOperationOutput(string employeeId, string employeeName, string awardId, decimal quantity)
        {
            EmployeeId = employeeId;
            EmployeeName = employeeName;
            AwardId = awardId;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return string.Join(',',EmployeeId,EmployeeName,AwardId,Quantity);
        }

    }
}

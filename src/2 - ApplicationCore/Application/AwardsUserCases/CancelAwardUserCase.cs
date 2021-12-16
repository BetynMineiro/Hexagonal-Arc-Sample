using System;
using Application.AwardsUserCases.Dto;
using Application.Factories.Awards.Interfaces;
using Domain.Entites;
using Domain.Repositories;

namespace Application.AwardsUserCases
{
    public class CancelAwardUserCase : IAwardUserCaseStrategy
    {
        private readonly IEmployeeAwardRepository _EmployeeAwardRepository;

        public CancelAwardUserCase(IEmployeeAwardRepository employeeAwardRepository)
        {
            _EmployeeAwardRepository = employeeAwardRepository;
        }

        public VestingAwardOperationOutput Execute(VestingAwardOperationInput input, DateTime targetDate, int precision)
        {
            var employee = _EmployeeAwardRepository.GetEmployee(input.EmployeeId);

            if (employee is null)
            {
                employee = new Employee(input.EmployeeId, input.EmployeeName);
            }

            if (input.OperationDate.Date <= targetDate.Date)
            {
                employee.VestingCancel(input.AwardId, input.Quantity, precision);
            }
            else { employee.VestingCancel(input.AwardId, decimal.Zero, precision); }

            _EmployeeAwardRepository.ApplyEmployeeChanges(employee);
            return new VestingAwardOperationOutput(employee.Id, employee.Name, employee.Awards[input.AwardId].Id, employee.Awards[input.AwardId].Quantity);

        }
    }
}

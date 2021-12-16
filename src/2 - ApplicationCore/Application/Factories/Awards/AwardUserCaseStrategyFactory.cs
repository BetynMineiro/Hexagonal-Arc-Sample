using System;
using System.Collections.Generic;
using Application.AwardsUserCases;
using Application.AwardsUserCases.Dto;
using Application.Factories.Awards.Interfaces;
using Domain.Enuns;
using Domain.Repositories;

namespace Application.Factories.Awards
{
    public  class AwardUserCaseStrategyFactory : IAwardUserCaseStrategyFactory
    {
        
        private  readonly IEmployeeAwardRepository _EmployeeAwardRepository;

        public AwardUserCaseStrategyFactory(IEmployeeAwardRepository employeeAwardRepository)
        {
            _EmployeeAwardRepository = employeeAwardRepository;
        }

        public IAwardUserCaseStrategy CreateStrategy(VestingAwardOperationInput input) {

            switch (input.OperationType)
            {
                case OperationType.Vest:
                    return new VestingAwardUserCase(_EmployeeAwardRepository);
                  
                case OperationType.Cancel:
                    return new CancelAwardUserCase(_EmployeeAwardRepository);
  
            }
            return null;
        }
    }
}

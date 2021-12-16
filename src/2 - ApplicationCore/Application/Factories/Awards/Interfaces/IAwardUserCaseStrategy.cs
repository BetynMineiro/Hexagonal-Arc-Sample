using System;
using Application.AwardsUserCases.Dto;

namespace Application.Factories.Awards.Interfaces
{
    public interface IAwardUserCaseStrategy
    {
        VestingAwardOperationOutput Execute(VestingAwardOperationInput input, DateTime targetDate, int precision);
    }
}

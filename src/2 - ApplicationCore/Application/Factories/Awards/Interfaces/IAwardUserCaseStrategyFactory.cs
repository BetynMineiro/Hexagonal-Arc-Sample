using System;
using Application.AwardsUserCases.Dto;

namespace Application.Factories.Awards.Interfaces
{
    public interface IAwardUserCaseStrategyFactory
    {
        IAwardUserCaseStrategy CreateStrategy(VestingAwardOperationInput input);
    }
}

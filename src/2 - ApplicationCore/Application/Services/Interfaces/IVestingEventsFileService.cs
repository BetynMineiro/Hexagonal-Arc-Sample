using System;
using System.Collections.Generic;
using Application.AwardsUserCases.Dto;

namespace Application.Services.Interfaces
{
    public interface IVestingEventsFileService
    {
        (bool sucess, IList<VestingAwardOperationOutput> output) ProcessFile(string[] input, string dir);
    }
}

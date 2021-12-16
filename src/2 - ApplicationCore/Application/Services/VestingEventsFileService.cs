using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Application.AwardsUserCases.Dto;
using Application.Factories.Awards.Interfaces;
using Application.Services.Dto;
using Application.Services.Interfaces;
using CrossCutting.Services.Interfaces;

namespace Application.Services
{
    public class VestingEventsFileService : IVestingEventsFileService
    {
        private readonly IAwardUserCaseStrategyFactory _awardUserCaseStrategyFactory;
        private readonly IFileService _fileService;
        
        public VestingEventsFileService(IAwardUserCaseStrategyFactory awardUserCaseStrategyFactory, IFileService fileService)
        {
            _awardUserCaseStrategyFactory = awardUserCaseStrategyFactory;
            _fileService = fileService;
        }

        public (bool sucess, IList<VestingAwardOperationOutput> output) ProcessFile(string[] input, string dir)
        {
            var outputInfo = new Dictionary<string,VestingAwardOperationOutput>();
            var vestingEventsFileInfoInput = new VestingEventsFileInfoInput(input);

            foreach (var operationLine in _fileService.ReadInputsFromCsvFile(vestingEventsFileInfoInput.File, dir))
            {
                var operation = new VestingAwardOperationInput(operationLine);
                var strategy = _awardUserCaseStrategyFactory.CreateStrategy(operation);
                var response = strategy.Execute(operation, vestingEventsFileInfoInput.TargetDate, vestingEventsFileInfoInput.Precision);
                var key = $"{response.EmployeeId}-{response.AwardId}";

                VestingAwardOperationOutput current;
                if (outputInfo.TryGetValue(key, out current))
                {
                    outputInfo[key] = response;
                }
                else { outputInfo.Add(key, response); }

            }


            return (true, outputInfo.Values.OrderBy(c=> c.EmployeeId).ThenBy(c=>c.AwardId).ToList());

        }

    }
}

using System;
using CrossCutting.Extensions;

namespace Application.Services.Dto
{
    public class VestingEventsFileInfoInput
    {
        public string File { get; set; }
        public DateTime TargetDate { get; set; }
        public int Precision { get; set; }

        public VestingEventsFileInfoInput(string[] input)
        {
            File = input[0] is null ? string.Empty : input[0];
            TargetDate = input[0] is null || input[1].IsValidDateFormat() ? Convert.ToDateTime(input[1]) : DateTime.MinValue;
            Precision = string.IsNullOrWhiteSpace(input[2]) ? 0 : Convert.ToInt16(input[2]) > 6 ? 6 : Convert.ToInt16(input[2]);

        }
    }
}

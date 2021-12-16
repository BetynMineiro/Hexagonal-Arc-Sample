using System;
using System.Collections.Generic;

namespace CrossCutting.Services.Interfaces
{
    public interface IFileService
    {
        IList<string[]> ReadInputsFromCsvFile(string fileName, string dir);
    }
}

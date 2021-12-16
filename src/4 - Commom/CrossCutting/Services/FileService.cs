using System;
using System.Collections.Generic;
using System.IO;
using CrossCutting.Services.Interfaces;

namespace CrossCutting.Services
{
    public class FileService : IFileService
    {
        public FileService()
        {
        }

        public IList<string[]> ReadInputsFromCsvFile(string fileName, string dir)
        {
            var path = Path.Combine(dir, fileName);

            var listOutput = new List<string[]>() { };
            if (File.Exists(path))
            {
                using (StreamReader file = new StreamReader(path))
                {
                    while (!file.EndOfStream)
                    {
                       listOutput.Add(file.ReadLine().Split(','));

                    }
                }
            }

            return listOutput;
        }
    }
}

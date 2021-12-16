using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Application;
using Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Terminal
{
    class Program
    {
        private static readonly CancellationTokenSource canToken = new CancellationTokenSource();
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.ConfigureLayerServices();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var vestingEventsFile = serviceProvider.GetService<IVestingEventsFileService>();

            Console.WriteLine("Application has started. Ctrl-C to end");

            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                Console.WriteLine("Cancel event triggered");
                canToken.Cancel();
                eventArgs.Cancel = true;
                Environment.Exit(0);
            };

            Worker(vestingEventsFile);


        }

        static void Worker(IVestingEventsFileService vestingEventsFile)
        {
            while (!canToken.IsCancellationRequested)
            {
                var inputArgs = Console.ReadLine().Split(' ');
                var (sucess, outPutlist) =  vestingEventsFile.ProcessFile(inputArgs, Directory.GetCurrentDirectory());
                if (sucess)
                {
                    foreach (var item in outPutlist)
                    {
                        Console.WriteLine(item.ToString());
                    }
                }
            }
        }
    }
}

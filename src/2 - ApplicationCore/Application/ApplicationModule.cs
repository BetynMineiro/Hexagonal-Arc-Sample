using System;
using Application.Factories.Awards;
using Application.Factories.Awards.Interfaces;
using Application.Services;
using Application.Services.Interfaces;
using CrossCutting.Services;
using CrossCutting.Services.Interfaces;
using Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using SimpleStorage.Repositories;

namespace Application
{
    public static class ApplicationModule
    {

        public static void ConfigureLayerServices(this IServiceCollection services)
        {
            services.AddSingleton<IAwardUserCaseStrategyFactory, AwardUserCaseStrategyFactory>();
            services.AddSingleton<IEmployeeAwardRepository, EmployeeAwardRepository>();
            services.AddSingleton<IVestingEventsFileService, VestingEventsFileService>();
            services.AddSingleton<IFileService, FileService>();
  
        }
    }
}

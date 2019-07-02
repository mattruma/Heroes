using Heroes.Data;
using Heroes.Data.Models;
using Heroes.Domain.Models;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(Heroes.FuncApp.Startup))]
namespace Heroes.FuncApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var connectionString = 
                Environment.GetEnvironmentVariable("SqlServer:ConnectionString");

            var services =
                builder.Services;

            services.AddDbContext<EntitiesDbContext>(
                options => SqlServerDbContextOptionsExtensions.UseSqlServer(
                    options, connectionString));

            services.AddTransient<IHeroEntityStore, HeroEntityStore>();
            services.AddTransient<IHeroService, HeroService>();
        }
    }
}
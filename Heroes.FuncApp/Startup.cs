using Heroes.Data.Models;
using Heroes.Domain.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(Heroes.FuncApp.Startup))]
namespace Heroes.FuncApp
{
    public class Startup: FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var cosmosClient =
                new CosmosClient(
                    Environment.GetEnvironmentVariable("AzureCosmosDocumentStoreOptions:ConnectionString"));
            var cosmosDatabase =
                cosmosClient.Databases["heroes"];

            var services =
                builder.Services;

            services.AddSingleton(cosmosDatabase);

            services.AddTransient<IHeroDocumentStore, HeroDocumentStore>();
            services.AddTransient<IHeroService, HeroService>();
        }
    }
}
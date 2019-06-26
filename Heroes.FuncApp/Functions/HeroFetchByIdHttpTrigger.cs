using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Heroes.Domain.Models;

namespace Heroes.FuncApp.Functions
{
    public class HeroFetchByIdHttpTrigger
    {
        private readonly IHeroService _heroService;

        public HeroFetchByIdHttpTrigger(
            IHeroService heroService)
        {
            _heroService = heroService;
        }

        [FunctionName("HeroFetchByIdHttpTrigger")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "heroes/{id}")] HttpRequest req,
            string id,
            ILogger log)
        {
            var hero =
               await _heroService.FetchByIdAsync(
                   new Guid(id));

            if (hero == null)
            {
                return new NotFoundObjectResult(id);
            }

            return new OkObjectResult(hero);
        }
    }
}

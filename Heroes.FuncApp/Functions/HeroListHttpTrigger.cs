using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Heroes.Domain.Models;

namespace Heroes.FuncApp.Functions
{
    public class HeroListHttpTrigger
    {
        private readonly IHeroService _heroService;

        public HeroListHttpTrigger(
            IHeroService heroService)
        {
            _heroService = heroService;
        }

        [FunctionName("HeroListHttpTrigger")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "heroes")] HttpRequest req,
            ILogger log)
        {
            var heroList =
                await _heroService.ListAsync();

            return new OkObjectResult(heroList);
        }
    }
}

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
    public class HeroDeleteByIdHttpTrigger
    {
        private readonly IHeroService _heroService;

        public HeroDeleteByIdHttpTrigger(
            IHeroService heroService)
        {
            _heroService = heroService;
        }

        [FunctionName("HeroDeleteByIdHttpTrigger")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "heroes/{id}")] HttpRequest req,
            string id,
            ILogger log)
        {
            await _heroService.DeleteByIdAsync(
                new Guid(id));

            return new NoContentResult();
        }
    }
}

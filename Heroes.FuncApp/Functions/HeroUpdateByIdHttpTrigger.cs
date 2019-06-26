using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using System;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Heroes.Domain.Models;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Heroes.FuncApp.Functions
{
    public class HeroUpdateByIdHttpTrigger
    {
        private readonly IHeroService _heroService;

        public HeroUpdateByIdHttpTrigger(
            IHeroService heroService)
        {
            _heroService = heroService;
        }

        [FunctionName("HeroUpdateByIdHttpTrigger")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "heroes/{id}")] HttpRequest req,
            string id,
            ILogger log)
        {
            var heroUpdateOptions = 
                JsonConvert.DeserializeObject<HeroUpdateOptions>(
                    await new StreamReader(req.Body).ReadToEndAsync());

            var hero =
               await _heroService.UpdateByIdAsync(
                   new Guid(id),
                   heroUpdateOptions);

            return new OkObjectResult(hero);
        }
    }
}

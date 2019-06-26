using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Heroes.Domain.Models;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Heroes.FuncApp.Functions
{
    public class HeroAddHttpTrigger
    {
        private readonly IHeroService _heroService;

        public HeroAddHttpTrigger(
            IHeroService heroService)
        {
            _heroService = heroService;
        }

        [FunctionName("HeroAddHttpTrigger")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "heroes")] HttpRequest req,
            ILogger log)
        {
            var heroAddOptions = 
                JsonConvert.DeserializeObject<HeroAddOptions>(
                    await new StreamReader(req.Body).ReadToEndAsync());

            var hero =
               await _heroService.AddAsync(
                   heroAddOptions);

            return new CreatedResult("HeroFetchById", hero);
        }
    }
}

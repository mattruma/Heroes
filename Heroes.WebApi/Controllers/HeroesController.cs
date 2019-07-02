using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using Heroes.Domain.Models;

namespace Heroes.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/heroes")]
    [ApiController]
    public class HerosController : ControllerBase
    {
        private readonly IHeroService _heroService;

        public HerosController(
            IHeroService heroService)
        {
            _heroService = heroService;
        }


        /// <summary>
        /// Returns a list of heroes. The heroes are returned sorted by name.
        /// </summary>
        [HttpGet(Name = "HeroList")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns an array of hero objects.", typeof(IEnumerable<Hero>))]
        public async Task<IActionResult> ListAsync()
        {
            var heroList =
                await _heroService.ListAsync();

            return Ok(heroList);
        }

        /// <summary>
        /// Retrieves the details of an existing hero. 
        /// </summary>
        /// <param name="id">The identifier of the hero to be retrieved.</param>
        [HttpGet("{id}", Name = "HeroFetchById")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns an hero object if a valid identifier was provided.", typeof(Hero))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Returns an error if the hero was not found.")]
        public async Task<IActionResult> FetchByIdAsync(
            [FromRoute] Guid id)
        {
            var hero =
               await _heroService.FetchByIdAsync(
                   id);

            if (hero == null)
            {
                return NotFound();
            }

            return Ok(hero);
        }

        /// <summary>
        /// Creates a new hero object.
        /// </summary>
        [HttpPost(Name = "HeroAdd")]
        [SwaggerResponse(StatusCodes.Status201Created, "Returns an hero object.", typeof(Hero))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Returns an error if add parameters are invalid.")]
        public async Task<IActionResult> AddAsync(
            [SwaggerParameter(Required = true)] HeroAddOptions heroAddOptions)
        {
            var hero =
                await _heroService.AddAsync(
                    heroAddOptions);

            return Created("HeroFetchById", hero);
        }

        /// <summary>
        /// Updates the specified hero by setting the values of the parameters passed.
        /// </summary>
        /// <param name="id">The identifier of the hero to be updated.</param>
        [HttpPut("{id}", Name = "HeroUpdateById")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns the hero object.", typeof(Hero))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Returns an error if unable to update the hero object.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Returns an error if the hero was not found.")]
        public async Task<IActionResult> UpdateByIdAsync(
            [FromRoute] Guid id,
            [SwaggerParameter(Required = true)] HeroUpdateOptions heroUpdateOptions)
        {
            var hero =
                await _heroService.UpdateByIdAsync(
                    id,
                    heroUpdateOptions);

            if (hero == null)
            {
                return NotFound();
            }

            return Ok(hero);
        }

        /// <summary>
        /// Permanently deletes a hero. It cannot be undone. 
        /// </summary>
        /// <param name="id">The identifier of the hero to be deleted.</param>
        [HttpDelete("{id}", Name = "HeroDeleteById")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns an object with a deleted parameter on success.", typeof(Hero))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Returns an error if unable to delete the hero object.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Returns an error if the hero was not found.")]
        public async Task<IActionResult> DeleteByIdAsync(
            [FromRoute] Guid id)
        {
            await _heroService.DeleteByIdAsync(
                id);

            return NoContent();
        }
    }
}
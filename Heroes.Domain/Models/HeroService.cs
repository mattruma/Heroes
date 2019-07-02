using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Heroes.Data.Models;

namespace Heroes.Domain.Models
{
    public class HeroService : IHeroService
    {
        private readonly IHeroEntityStore _heroEntityStore;

        public HeroService(
            IHeroEntityStore heroEntityStore)
        {
            _heroEntityStore = heroEntityStore;
        }

        public async Task<Hero> AddAsync(
            HeroAddOptions heroAddOptions)
        {
            var heroEntity =
                new HeroEntity
                {
                    Name = heroAddOptions.Name,
                    Gender = heroAddOptions.Gender,
                    Notes = heroAddOptions.Notes
                };

            if (heroAddOptions.Affiliations != null)
            {
                heroEntity.Affiliations = string.Join(",", heroAddOptions.Affiliations);
            }

            if (heroAddOptions.Powers != null)
            {
                heroEntity.Powers = string.Join(",", heroAddOptions.Powers);
            }

            heroEntity =
                await _heroEntityStore.AddAsync(
                    heroEntity);

            var hero =
               new Hero(heroEntity);

            return hero;
        }

        public async Task<Hero> UpdateByIdAsync(
            Guid id,
            HeroUpdateOptions heroUpdateOptions)
        {
            var heroEntity =
                await _heroEntityStore.FetchByIdAsync(
                    id);

            if (heroEntity == null)
            {
                throw new NullReferenceException("Hero does not exist.");
            }

            heroEntity.Name = heroUpdateOptions.Name;
            heroEntity.Gender = heroUpdateOptions.Gender;
            heroEntity.Notes = heroUpdateOptions.Notes;

            if (heroUpdateOptions.Affiliations != null)
            {
                heroEntity.Affiliations = string.Join(",", heroUpdateOptions.Affiliations);
            }
            else
            {
                heroEntity.Affiliations = null;
            }

            if (heroUpdateOptions.Powers != null)
            {
                heroEntity.Powers = string.Join(",", heroUpdateOptions.Powers);
            }
            else
            {
                heroEntity.Powers = null;
            }

            heroEntity =
                await _heroEntityStore.UpdateByIdAsync(
                    id,
                    heroEntity);

            var hero =
               new Hero(heroEntity);

            return hero;
        }

        public async Task DeleteByIdAsync(
            Guid id)
        {
            var heroEntity =
                await _heroEntityStore.FetchByIdAsync(
                    id);

            if (heroEntity == null)
            {
                throw new NullReferenceException("Hero does not exist.");
            }

            await _heroEntityStore.DeleteByIdAsync(
                id);
        }

        public async Task<Hero> FetchByIdAsync(
            Guid id)
        {
            var heroEntity =
                await _heroEntityStore.FetchByIdAsync(
                    id);

            if (heroEntity == null)
            {
                throw new NullReferenceException("Hero does not exist.");
            }

            var hero =
               new Hero(heroEntity);

            return hero;
        }

        public async Task<IEnumerable<Hero>> ListAsync()
        {
            var heroEntityList =
                await _heroEntityStore.ListAsync();

            var heroList =
                heroEntityList.Select(x => new Hero(x));

            return heroList;
        }
    }
}

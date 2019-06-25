using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Heroes.Data.Models;

namespace Heroes.Domain.Models
{
    public class HeroService : IHeroService
    {
        private readonly IHeroDocumentStore _heroDocumentStore;

        public HeroService(
            IHeroDocumentStore heroDocumentStore)
        {
            _heroDocumentStore = heroDocumentStore;
        }

        public async Task<Hero> AddAsync(
            HeroAddOptions heroAddOptions)
        {
            var heroDocument =
                new HeroDocument
                {
                    Name = heroAddOptions.Name,
                    Gender = heroAddOptions.Gender,
                    Affiliations = heroAddOptions.Affiliations,
                    Powers = heroAddOptions.Powers,
                    Notes = heroAddOptions.Notes
                };

            heroDocument =
                await _heroDocumentStore.AddAsync(
                    heroDocument);

            var hero =
               new Hero(heroDocument);

            return hero;
        }

        public async Task<Hero> UpdateByIdAsync(
            Guid id,
            HeroUpdateOptions heroUpdateOptions)
        {
            var heroDocument =
                await _heroDocumentStore.FetchByIdAsync(
                    id);

            if (heroDocument == null)
            {
                throw new NullReferenceException("Hero does not exist.");
            }

            heroDocument.Name = heroUpdateOptions.Name;
            heroDocument.Gender = heroUpdateOptions.Gender;
            heroDocument.Affiliations = heroUpdateOptions.Affiliations;
            heroDocument.Powers = heroUpdateOptions.Powers;
            heroDocument.Notes = heroUpdateOptions.Notes;

            heroDocument =
                await _heroDocumentStore.UpdateByIdAsync(
                    id,
                    heroDocument);

            var hero =
               new Hero(heroDocument);

            return hero;
        }

        public async Task DeleteByIdAsync(
            Guid id)
        {
            var heroDocument =
                await _heroDocumentStore.FetchByIdAsync(
                    id);

            if (heroDocument == null)
            {
                throw new NullReferenceException("Hero does not exist.");
            }

            await _heroDocumentStore.DeleteByIdAsync(
                id);
        }

        public async Task<Hero> FetchByIdAsync(
            Guid id)
        {
            var heroDocument =
                await _heroDocumentStore.FetchByIdAsync(
                    id);

            if (heroDocument == null)
            {
                throw new NullReferenceException("Hero does not exist.");
            }

            var hero =
               new Hero(heroDocument);

            return hero;
        }

        public async Task<IEnumerable<Hero>> ListAsync()
        {
            var heroDocumentList =
                await _heroDocumentStore.ListAsync();

            var heroList =
                heroDocumentList.Select(x => new Hero(x));

            return heroList;
        }
    }
}

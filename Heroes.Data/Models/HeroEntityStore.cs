using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Heroes.Data.Models
{
    public class HeroEntityStore : BaseEntityStore, IHeroEntityStore
    {
        public HeroEntityStore(
            EntitiesDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<HeroEntity> AddAsync(
            HeroEntity heroEntity)
        {
            _dbContext.Heroes.Add(heroEntity);

            await _dbContext.SaveChangesAsync();

            return heroEntity;
        }

        public async Task DeleteByIdAsync(
            Guid id)
        {
            var heroEntity =
                await _dbContext.Heroes.FindAsync(id);

            if (heroEntity != null)
            {
                _dbContext.Heroes.Remove(heroEntity);

                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<HeroEntity> FetchByIdAsync(
            Guid id)
        {
            var heroEntity =
                await _dbContext.Heroes.FindAsync(id);

            return heroEntity;
        }

        public async Task<IEnumerable<HeroEntity>> ListAsync()
        {
            var heroEntityList =
                await _dbContext.Heroes.ToListAsync();

            return heroEntityList;
        }

        public async Task<HeroEntity> UpdateByIdAsync(
            Guid id,
            HeroEntity heroEntity)
        {
            var heroEntityToUpdate =
                await _dbContext.Heroes.FindAsync(id);

            heroEntityToUpdate.Name = heroEntity.Name;
            heroEntityToUpdate.Gender = heroEntity.Gender;
            heroEntityToUpdate.Powers = heroEntity.Powers;
            heroEntityToUpdate.Affiliations = heroEntity.Affiliations;
            heroEntityToUpdate.Notes = heroEntity.Notes;

            await _dbContext.SaveChangesAsync();

            return heroEntityToUpdate;
        }
    }
}

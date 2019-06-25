using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Heroes.Domain.Models
{
    public interface IHeroService
    { 
        Task<Hero> AddAsync(
            HeroAddOptions heroAddOptions);

        Task<Hero> UpdateByIdAsync(
            Guid id,
            HeroUpdateOptions heroUpdateOptions);

        Task DeleteByIdAsync(
            Guid id);

        Task<Hero> FetchByIdAsync(
            Guid id);

        Task<IEnumerable<Hero>> ListAsync();
    }
}

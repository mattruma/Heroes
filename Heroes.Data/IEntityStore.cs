using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Heroes.Data
{
    public interface IEntityStore<TEntity> where TEntity : IEntity
    {
        Task<TEntity> AddAsync(
            TEntity document);

        Task DeleteByIdAsync(
            Guid id);

        Task<TEntity> FetchByIdAsync(
            Guid id);

        Task<IEnumerable<TEntity>> ListAsync();

        Task<TEntity> UpdateByIdAsync(
            Guid id,
            TEntity document);
    }
}

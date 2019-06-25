using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Heroes.Data.Models
{
    public interface IDocumentStore<TDocument> where TDocument : IDocument
    {
        Task<HeroDocument> AddAsync(
            TDocument document);

        Task DeleteByIdAsync(
            Guid id);

        Task<HeroDocument> FetchByIdAsync(
            Guid id);

        Task<IEnumerable<TDocument>> ListAsync();

        Task<TDocument> UpdateByIdAsync(
            Guid id,
            TDocument document);
    }
}

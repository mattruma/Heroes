using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Heroes.Data.Models
{
    public class HeroDocumentStore : BaseDocumentStore, IHeroDocumentStore
    {
        private const string _containerId = "heroes";

        public HeroDocumentStore(
            CosmosDatabase cosmosDatabase) : base(cosmosDatabase)
        {
        }

        public async Task<HeroDocument> AddAsync(
            HeroDocument heroDocument)
        {
            var heroContainer =
               _cosmosDatabase.Containers[_containerId];

            heroDocument.HeroId = 
                heroDocument.Id;

            var heroDocumentResponse =
                await heroContainer.Items.CreateItemAsync(
                    heroDocument.Id.ToString(),
                    heroDocument);

            return heroDocumentResponse.Resource;
        }

        public async Task DeleteByIdAsync(
            Guid id)
        {
            var heroContainer =
               _cosmosDatabase.Containers[_containerId];

            await heroContainer.Items.DeleteItemAsync<HeroDocument>(
                id.ToString(),
                id.ToString());
        }

        public async Task<HeroDocument> FetchByIdAsync(
            Guid id)
        {
            var heroContainer =
               _cosmosDatabase.Containers[_containerId];

            var heroDocumentResponse =
                await heroContainer.Items.ReadItemAsync<HeroDocument>(
                    id.ToString(),
                    id.ToString());

            return heroDocumentResponse.Resource;
        }

        public async Task<IEnumerable<HeroDocument>> ListAsync()
        {
            var heroContainer =
               _cosmosDatabase.Containers[_containerId];

            var sqlQuery =
                "SELECT * FROM t1 WHERE t1.object = @object";

            var sqlQueryDefinition =
                new CosmosSqlQueryDefinition(sqlQuery);

            sqlQueryDefinition.UseParameter(
                "@object", "Hero");

            var heroDocumentIterator =
                heroContainer.Items.CreateItemQuery<HeroDocument>(
                    sqlQueryDefinition,
                    maxConcurrency: MAXIMUM_CONCURRENCY);

            var heroDocumentList = new List<HeroDocument>();

            while (heroDocumentIterator.HasMoreResults)
            {
                heroDocumentList.AddRange(
                    await heroDocumentIterator.FetchNextSetAsync());
            };

            return heroDocumentList;
        }

        public async Task<HeroDocument> UpdateByIdAsync(
            Guid id,
            HeroDocument heroDocument)
        {
            var heroContainer =
               _cosmosDatabase.Containers[_containerId];

            var heroDocumentResponse =
                 await heroContainer.Items.ReplaceItemAsync<HeroDocument>(
                     id.ToString(),
                     id.ToString(),
                     heroDocument);

            return heroDocumentResponse.Resource;
        }
    }
}

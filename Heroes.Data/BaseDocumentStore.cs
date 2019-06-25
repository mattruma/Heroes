using Microsoft.Azure.Cosmos;

namespace Heroes.Data
{
    public abstract class BaseDocumentStore
    {
        public const int MAXIMUM_CONCURRENCY = -1;

        protected readonly CosmosDatabase _cosmosDatabase;

        public BaseDocumentStore(
            CosmosDatabase cosmosDatabase)
        {
            _cosmosDatabase = cosmosDatabase;
        }
    }
}

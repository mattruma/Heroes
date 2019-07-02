namespace Heroes.Data
{
    public abstract class BaseEntityStore
    {
        protected readonly EntitiesDbContext _dbContext;

        public BaseEntityStore(
            EntitiesDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}

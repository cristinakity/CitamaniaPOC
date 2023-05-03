namespace CitamaniaPOC.Data.Repositories.Core
{
    public interface IGenericRepository<TEntity>
    where TEntity : class
    {
        Task<IEnumerable<TEntity>?> GetAll(bool? active);

        Task<TEntity?> GetByPk(params long[] keys);

        Task Create(TEntity entity, string user);

        Task Update(TEntity entity, string user, params long[] keys);

        Task Delete(string user, params long[] keys);
    }
}
namespace CitamaniaPOC.Business.Services.Core
{
    public interface IGenericService<TResponse, TPayload>
    {
        Task<List<TResponse>?> GetAll(bool? active);

        Task<TResponse?> GetByPk(params long[] keys);

        Task Create(TPayload entity, string user);

        Task Update(TPayload entity, string user, params long[] keys);

        Task Delete(string user, params long[] keys);
    }
}
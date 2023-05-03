namespace CitamaniaPOC.Business.Services.Core
{
    public class GenericService<TRepository, TEntity, TResponse, TPayload> : IGenericService<TResponse, TPayload>
    where TRepository : IGenericRepository<TEntity>
    where TResponse : class
    where TEntity : class
    where TPayload : class
    {
        protected readonly TRepository _repository;
        protected readonly IMapper _mapper;

        public GenericService(TRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public virtual Task Delete(string user, params long[] keyValues)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<List<TResponse>?> GetAll(bool? active)
        {
            var queryResult = await _repository.GetAll(active);
            return _mapper.Map<List<TResponse>>(queryResult);
        }

        public virtual async Task<TResponse?> GetByPk(params long[] keys)
        {
            var queryResult = await _repository.GetByPk(keys);
            return _mapper.Map<TResponse>(queryResult);
        }

        public virtual async Task Create(TPayload entity, string user)
        {
            var modelEntity = _mapper.Map<TEntity>(entity);
            await _repository.Create(modelEntity, user);
        }

        public virtual async Task Update(TPayload entity, string user, params long[] keyValues)
        {
            var modelEntity = _mapper.Map<TEntity>(entity);
            await _repository.Update(modelEntity, user, keyValues);
        }
    }
}
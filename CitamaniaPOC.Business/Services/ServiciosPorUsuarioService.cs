namespace CitamaniaPOC.Business.Services
{
    public class ServiciosPorUsuarioService : GenericService<IServiciosPorUsuarioRepository, ServiciosPorUsuario, ServiciosPorUsuarioResponse, ServiciosPorUsuarioPayload>, IServiciosPorUsuarioService
    {
        public ServiciosPorUsuarioService(IServiciosPorUsuarioRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
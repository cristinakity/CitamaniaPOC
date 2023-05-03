namespace CitamaniaPOC.Business.Services
{
    public class UsuarioService : GenericService<IUsuarioRepository, Usuario, UsuarioResponse, UsuarioPayload>, IUsuarioService
    {
        public UsuarioService(IUsuarioRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
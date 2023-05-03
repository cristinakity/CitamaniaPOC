namespace CitamaniaPOC.API.Controllers
{
    public class UsuarioController : GenericController<IUsuarioService, UsuarioController, UsuarioResponse, UsuarioPayload>
    {
        public UsuarioController(ILogger<UsuarioController> logger, IUsuarioService service) : base(logger, service)
        {
        }
    }
}
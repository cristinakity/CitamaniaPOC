namespace CitamaniaPOC.API.Controllers
{
   public class ServiciosPorUsuarioController : GenericController<IServiciosPorUsuarioService, ServiciosPorUsuarioController, ServiciosPorUsuarioResponse, ServiciosPorUsuarioPayload>
   {
		public ServiciosPorUsuarioController(ILogger<ServiciosPorUsuarioController> logger, IServiciosPorUsuarioService service) : base(logger, service)
		{
		}
   }
}
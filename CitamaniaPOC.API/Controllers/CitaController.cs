namespace CitamaniaPOC.API.Controllers
{
   public class CitaController : GenericController<ICitaService, CitaController, CitaResponse, CitaPayload>
   {
		public CitaController(ILogger<CitaController> logger, ICitaService service) : base(logger, service)
		{
		}
   }
}
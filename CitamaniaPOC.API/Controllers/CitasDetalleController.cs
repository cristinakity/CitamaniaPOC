namespace CitamaniaPOC.API.Controllers
{
    public class CitasDetalleController : GenericController<ICitasDetalleService, CitasDetalleController, CitasDetalleResponse, CitasDetallePayload>
    {
        public CitasDetalleController(ILogger<CitasDetalleController> logger, ICitasDetalleService service) : base(logger, service)
        {
        }
    }
}
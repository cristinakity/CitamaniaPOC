namespace CitamaniaPOC.Business.Services
{
    public class CitasDetalleService : GenericService<ICitasDetalleRepository, CitasDetalle, CitasDetalleResponse, CitasDetallePayload>, ICitasDetalleService
    {
        public CitasDetalleService(ICitasDetalleRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
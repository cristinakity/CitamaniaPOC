namespace CitamaniaPOC.Business.Services
{
    public class CitaService : GenericService<ICitaRepository, Cita, CitaResponse, CitaPayload>, ICitaService
    {
        public CitaService(ICitaRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
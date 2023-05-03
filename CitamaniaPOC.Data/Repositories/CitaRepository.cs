namespace CitamaniaPOC.Data.Repositories
{
    public class CitaRepository : GenericRepository<Cita>, ICitaRepository
    {
        public CitaRepository(IConfiguration config, TelemetryConfiguration telemetryConfig) : base(config, telemetryConfig, tableName: "Citas")
        {
        }
    }
}
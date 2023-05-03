namespace CitamaniaPOC.Data.Repositories
{
    public class CitasDetalleRepository : GenericRepository<CitasDetalle>, ICitasDetalleRepository
    {
        public CitasDetalleRepository(IConfiguration config, TelemetryConfiguration telemetryConfig) : base(config, telemetryConfig, tableName: "CitasDetalles")
        {
        }
    }
}
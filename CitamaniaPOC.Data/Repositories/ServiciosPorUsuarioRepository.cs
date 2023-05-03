namespace CitamaniaPOC.Data.Repositories
{
    public class ServiciosPorUsuarioRepository : GenericRepository<ServiciosPorUsuario>, IServiciosPorUsuarioRepository
    {
        public ServiciosPorUsuarioRepository(IConfiguration config, TelemetryConfiguration telemetryConfig) : base(config, telemetryConfig, tableName: "ServiciosPorUsuario")
        {
        }
    }
}
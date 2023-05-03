namespace CitamaniaPOC.Data.Repositories
{
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(IConfiguration config, TelemetryConfiguration telemetryConfig) : base(config, telemetryConfig, tableName: "Usuarios")
        {
        }
    }
}
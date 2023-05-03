using CitamaniaPOC.Data.Repositories;
using CitamaniaPOC.Data.Repositories.Core;

namespace CitamaniaPOC.API
{
    public class Configuration
    {
        public static void Configure(IServiceCollection services, ConfigurationManager configuration)
        {
            var connectionString = configuration.GetConnectionString("CitamaniaPOC");
            services.AddSingleton<DbConnectionFactory>(x => new DbConnectionFactory(connectionString));
            services.AddAutoMapper(typeof(MappingProfile));
            AddDataRepositoriesServices(services);
            AddBusinessServices(services);
        }

        public static void AddDataRepositoriesServices(IServiceCollection services)
        {
            services.AddTransient<ICitaRepository, CitaRepository>();
            services.AddTransient<ICitasDetalleRepository, CitasDetalleRepository>();
            services.AddTransient<IServiciosPorUsuarioRepository, ServiciosPorUsuarioRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
        }

        public static void AddBusinessServices(IServiceCollection services)
        {
            services.AddTransient<ICitaService, CitaService>();
            services.AddTransient<ICitasDetalleService, CitasDetalleService>();
            services.AddTransient<IServiciosPorUsuarioService, ServiciosPorUsuarioService>();
            services.AddTransient<IUsuarioService, UsuarioService>();
        }
    }
}
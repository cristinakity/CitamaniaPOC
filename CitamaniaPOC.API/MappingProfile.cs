namespace CitamaniaPOC.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Map();
        }

        private void Map()
        {
            CreateMap<Cita, CitaResponse>();
            CreateMap<CitasDetalle, CitasDetalleResponse>();
            CreateMap<ServiciosPorUsuario, ServiciosPorUsuarioResponse>();
            CreateMap<Usuario, UsuarioResponse>();

            CreateMap<CitaPayload, Cita>();
            CreateMap<CitasDetallePayload, CitasDetalle>();
            CreateMap<ServiciosPorUsuarioPayload, ServiciosPorUsuario>();
            CreateMap<UsuarioPayload, Usuario>();
        }
    }
}
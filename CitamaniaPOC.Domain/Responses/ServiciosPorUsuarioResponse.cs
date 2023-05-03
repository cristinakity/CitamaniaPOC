namespace CitamaniaPOC.Domain.Responses
{
    public class ServiciosPorUsuarioResponse
    {
                [Key]
        [Required]

        public int ServicioId { get; set; }
                [Required]
        [MaxLength(400)]

        public string Descripcion { get; set; }
                [Required]

        public decimal Precio { get; set; }
                [Required]
        [MaxLength(100)]

        public string Servicio { get; set; }
                [Required]

        public int UsuarioId { get; set; }

    }
}
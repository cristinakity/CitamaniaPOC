namespace CitamaniaPOC.Domain.Responses
{
    public class CitasDetalleResponse
    {
                [Key]
        [Required]

        public int CitaId { get; set; }
        [Key]
        [Required]

        public int ServicioId { get; set; }
                [Required]

        public int Cantidad { get; set; }
                [Required]

        public decimal Precio { get; set; }

    }
}
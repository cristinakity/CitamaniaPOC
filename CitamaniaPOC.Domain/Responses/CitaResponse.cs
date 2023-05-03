namespace CitamaniaPOC.Domain.Responses
{
    public class CitaResponse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int CitaId { get; set; }

        [Required]
        public int ClienteId { get; set; }

        [Required]
        public Guid CodigoUnico { get; set; }

        [Required]
        [MaxLength(50)]
        public string Estatus { get; set; }

        public DateTime? FechaAprobacion { get; set; }

        public DateTime? FechaCancelacion { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        [Required]
        public DateTime FechaDeCita { get; set; }

        public DateTime? FechaModificacion { get; set; }

        [MaxLength(400)]
        public string? MotivoDeCancelacion { get; set; }

        [MaxLength(500)]
        public string? Notas { get; set; }

        [Required]
        public int PrestadorDeServicioId { get; set; }

        [Required]
        public int SolicitanteId { get; set; }

        public int? UsuarioAprobacionId { get; set; }

        public int? UsuarioCancelacionId { get; set; }
    }
}
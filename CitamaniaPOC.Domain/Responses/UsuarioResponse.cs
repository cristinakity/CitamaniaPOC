namespace CitamaniaPOC.Domain.Responses
{
    public class UsuarioResponse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int UsuarioId { get; set; }

        [Required]
        [MaxLength(250)]
        public string Email { get; set; }

        [Required]
        [MaxLength(400)]
        public string HasPassword { get; set; }

        [Required]
        [MaxLength(250)]
        public string Nombre { get; set; }

        [Required]
        public bool PrestaServicio { get; set; }

        [Required]
        [MaxLength(400)]
        public string SaltStrig { get; set; }

        [MaxLength(100)]
        public string? TokenDeRecuperacion { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace Bibliotecaper.Dtos
{
    public class CrearLibroDto
    {
        [Required]
        public string Titulo { get; set; }
        [Required]
        public Guid AutorId { get; set; }
        [Required]
        public Guid GeneroId { get; set; }

        [MaxLength(300)]
        public string?Descripcion { get; set; }

        public string? Estado { get; set; } = "Por leer";

        public int? calificacion { get; set; }

        public int? NumPaginas { get; set; }

        public DateTime? FechaPublicacion { get; set; }


    }
}

using System.ComponentModel.DataAnnotations;

namespace Bibliotecaper.Dtos
{
    public class ActualizarLibroDto
    {
        public string? Titulo { get; set; }
       
        
        public Guid? GeneroId { get; set; }

        [MaxLength(300)]
        public string? Descripcion { get; set; }

        public string? Estado { get; set; } = "Por leer";

        public int? calificacion { get; set; }

        public int? NumeroPaginas { get; set; }

        public DateTime? FechaPublicacion { get; set; }
    }
}

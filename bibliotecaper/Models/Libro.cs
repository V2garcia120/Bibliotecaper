using System.ComponentModel.DataAnnotations;

namespace Bibliotecaper.Models
{
    public class Libro
    {
        [Required(ErrorMessage = "id requerido")]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required(ErrorMessage = "Título requerido")]s
        [MaxLength(100, ErrorMessage = "El título no puede exceder los 100 caracteres")]
        public string Titulo { get; set; }
        [MaxLength(500, ErrorMessage = "La descripccion no puede exeder los 500 caracteres")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "Usuario requerido")]
        public Guid UsuarioId { get; set; }
        [Required(ErrorMessage = "Autor requerido")]
        public Guid AutorId { get; set; }
        [Required(ErrorMessage = "Género requerido")]
        public Guid GeneroId { get; set; }

        public int? calificacion 
        {get
            {
                return calificacion;
            } 
            set 
            {
                if (value < 0 || value > 5)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "La calificación debe estar entre 0 y 5.");
                }
                calificacion = value;
            } 
        }
        
        public string Estado { get; set; } = "Disponible";
        public int? NumeroPaginas { get; set; } 
        public DateTime? FechaPublicacion { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Autor Autor { get; set; }
        public virtual Generos Genero { get; set; }

    }
}

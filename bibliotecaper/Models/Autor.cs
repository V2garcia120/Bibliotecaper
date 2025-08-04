using System.ComponentModel.DataAnnotations;

namespace Bibliotecaper.Models
{
    public class Autor
    {
        [Required(ErrorMessage = "Id requerido")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Nombre requerido")]
        [MaxLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres")]
        public string Nombre { get; set; }
        [MaxLength(500, ErrorMessage = "La biografía no puede exceder los 500 caracteres")]
        public string Biografia { get; set; }
        [Required(ErrorMessage = "Fecha de nacimiento requerida")]
        public DateTime FechaNacimiento { get; set; }

        public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>(); 

    }
}

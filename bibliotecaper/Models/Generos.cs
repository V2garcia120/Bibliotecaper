using System.ComponentModel.DataAnnotations;

namespace Bibliotecaper.Models
{
    public class Generos
    {
        [Required(ErrorMessage = "id requerido")]
        public Guid id { get; set; }
        [Required(ErrorMessage = "nombre requerido")]
        [MinLength(5, ErrorMessage = "El nombre debe tener minimo 5 caracteres")]
        [MaxLength(100, ErrorMessage = "No puede exceder los 100 Caracteres")]
        public string Nombre { get; set; } = string.Empty;
        [MaxLength(300, ErrorMessage = "La descripcion no puede exceder los 300 caracteres")]
        public string? Descripcion { get; set; }

        public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
    }
}

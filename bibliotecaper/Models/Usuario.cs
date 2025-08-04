using System.ComponentModel.DataAnnotations;

namespace Bibliotecaper.Models
{
    public class Usuario
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Nombre requerido")]
        [MaxLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Emalil requerido")]
        [EmailAddress(ErrorMessage = "Formato de email no válido")]
        [MaxLength(100, ErrorMessage = "El email no puede exceder los 100 caracteres")]
        public string Email { get; set; }

        [MaxLength(15, ErrorMessage = "El teléfono no puede exceder los 15 caracteres")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Contraseña requerida")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        [MaxLength(100, ErrorMessage = "La contraseña no puede exceder los 100 caracteres")]
        public string Contraseña { get; set; }
        public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();

    }
}

using System.ComponentModel.DataAnnotations;

namespace Bibliotecaper.Dtos
{
    public class RegistroDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres")]
        public string Nombre { get; set; } = string.Empty;
        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        [MaxLength(100, ErrorMessage = "El email no puede exceder los 100 caracteres")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [MaxLength(15, ErrorMessage = "El teléfono no puede exceder los 15 caracteres")]
        public string Telefono { get; set; } = string.Empty;
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        [MaxLength(100, ErrorMessage = "La contraseña no puede exceder los 100 caracteres")]
        public string Contraseña { get; set; } = string.Empty;
        [Required(ErrorMessage = "La confirmación de contraseña es obligatoria")]
        [Compare("Contraseña", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmarContraseña { get; set; } = string.Empty;


    }
}

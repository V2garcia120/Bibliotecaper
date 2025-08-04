using System.ComponentModel.DataAnnotations;
namespace Bibliotecaper.Dtos
{
    public class LoginResponseDto
    {
      
        public string Token { get; set; } = string.Empty;
    
        public Guid UsuarioId { get; set; }
       
        public string NombreUsuario { get; set; } = string.Empty;
      
        public string Email { get; set; } = string.Empty;
        public DateTime FechaExpiracion { get; set; }
    }
}

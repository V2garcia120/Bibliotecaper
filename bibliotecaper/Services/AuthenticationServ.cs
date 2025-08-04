
using Bibliotecaper.Data;
using Bibliotecaper.Dtos;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Bibliotecaper.Models;

namespace Bibliotecaper.Services
{
    public class AuthenticationServ
    {
       private readonly BibliotecaContext _context;
       private readonly IConfiguration _configuration;

        public AuthenticationServ(BibliotecaContext context, IConfiguration configuration)
         {
              _context = context;
              _configuration = configuration;
        }

        public async Task RegisterAsync(RegistroDto registerDto)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Email == registerDto.Email))
            {
                throw new InvalidOperationException("El email ya está en uso");
            }
            var usuario = new Models.Usuario
            {
                Nombre = registerDto.Nombre,
                Email = registerDto.Email,
                Telefono = registerDto.Telefono,
                Contraseña = BCrypt.Net.BCrypt.HashPassword(registerDto.Contraseña)
            };
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            GenerarJwtToken(usuario);

        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(loginDto.Contraseña, usuario.Contraseña))
            {
                throw new UnauthorizedAccessException("Credenciales inválidas");
            }
            var token = GenerarJwtToken(usuario);
            if (string.IsNullOrEmpty(token))
            {
                throw new InvalidOperationException("Error al generar el token JWT");
            }
            return new LoginResponseDto
            {
                Token = token,
                UsuarioId = usuario.Id,
                NombreUsuario = usuario.Nombre,
                Email = usuario.Email,
                FechaExpiracion = DateTime.UtcNow.AddHours(1) 
            };

        }
        private string GenerarJwtToken(Usuario usuario) 
        {
            var SecretKey = _configuration["JwtSettings:SecretKey"];
            var issuer = _configuration["JwtSettigns:Issuer"];
            var audience = _configuration["JwtSettigns:Audience"];
            var expiration = _configuration.GetValue<int>("JwtSettigns:ExpirationInHours");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,  usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim("UsuarioId", usuario.Id.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(expiration),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

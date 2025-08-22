using Bibliotecaper.Data;
using Bibliotecaper.Dtos;
using Bibliotecaper.Models;
using Microsoft.EntityFrameworkCore;


namespace Bibliotecaper.Services
{
    public class LibroServ
    {
        private readonly BibliotecaContext _context;
        private readonly IConfiguration _configuration;
        public LibroServ(BibliotecaContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<List<Models.Libro>> GetLibrosAsync(Guid usuarioId, FiltrosDtos? filtros = null)
        {
            var query = _context.Libros
                .Include(l => l.Usuario)
                .Include(l => l.Autor)
                .Include(l => l.Genero)
                .Where(l => l.UsuarioId == usuarioId);
            if (filtros != null) 
            {
                if (filtros.AutorId != null) 
                {
                    query = query.Where(l => l.AutorId == filtros.AutorId);
                }
                if (!string.IsNullOrEmpty(filtros.TituloBusqueda))
                {
                    query = query.Where(l => l.Titulo.Contains(filtros.TituloBusqueda));
                }
                if (!string.IsNullOrEmpty(filtros.Estado))
                {
                    query = query.Where(l => l.Estado == filtros.Estado);
                }
                if (filtros.GeneroId != null)
                {
                    query = query.Where(l => l.GeneroId == filtros.GeneroId); 
                }
                
            } 
            return await query.ToListAsync();
        }
        public async Task<Models.Libro> GetLibroByIdAsync(Guid id, Guid usuarioId)
        {
            return await _context.Libros
                .Include(l => l.Usuario)
                .Include(l => l.Autor)
                .Include(l => l.Genero)
                .Where(l => l.UsuarioId == usuarioId)
                .FirstOrDefaultAsync(l => l.Id == id);
        }
        public async Task<Models.Libro> AddLibroAsync( CrearLibroDto dto, Guid usuarioId)
        {

            var libro = new Models.Libro
            {
                Titulo = dto.Titulo,
                AutorId = dto.AutorId,
                Descripcion = dto.Descripcion,
                calificacion = dto.Calificacion,
                Estado = dto.Estado,
                GeneroId = dto.GeneroId,
                NumeroPaginas = dto.NumPaginas,
                FechaPublicacion = dto.FechaPublicacion,



            };
            libro.UsuarioId = usuarioId;
            _context.Libros.Add(libro);
            await _context.SaveChangesAsync();
            return libro;

        }
        public async Task<Models.Libro> UpdateLibroAsync(Guid id, ActualizarLibroDto dto, Guid usuarioId)
        {
            var existingLibro = await _context.Libros.FindAsync(id);
            if (existingLibro == null)
            {
                throw new Exception("El libro no existe");
            }
            if (usuarioId == existingLibro.UsuarioId)
            {
                if (!string.IsNullOrEmpty(dto.Titulo))
                {
                    existingLibro.Titulo = dto.Titulo;
                }
                if (dto.Descripcion != null)
                {
                    existingLibro.Descripcion = dto.Descripcion;
                }
                if (dto.GeneroId != null)
                {
                    existingLibro.GeneroId = dto.GeneroId.Value;
                }
                if (dto.Calificacion != null)
                {
                    existingLibro.calificacion = dto.Calificacion;
                }
                if (dto.Estado != null)
                {
                    existingLibro.Estado = dto.Estado;
                }
                if (dto.NumeroPaginas != null)
                {
                    existingLibro.NumeroPaginas = dto.NumeroPaginas;
                }
                if (dto.FechaPublicacion != null)
                {
                    existingLibro.FechaPublicacion = dto.FechaPublicacion;
                }
                _context.Libros.Update(existingLibro);
                await _context.SaveChangesAsync();
                return existingLibro;
            }
            throw new Exception("no tienes permisos para modificar este libro");
                   
        }
        public async Task<string> DeleteLibroAsync(Guid id, Guid usuarioId ) 
        {
            var existingLibro = await _context.Libros.FindAsync(id);
            if (existingLibro == null) 
            {
                return "Libro no encontrado";
            }
            if (existingLibro.UsuarioId == usuarioId)
            {
                _context.Libros.Remove(existingLibro);
                await _context.SaveChangesAsync();
                return "Libro Eliminado";
            }
            return "No tienes permisos para modificar este libro";
        }
    }
}

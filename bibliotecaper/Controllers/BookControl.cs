using Microsoft.AspNetCore.Mvc;
using bibliotecaper.Services;
using bibliotecaper.Dtos;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;




namespace bibliotecaper.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class BookControl : ControllerBase
    {
        private readonly LibroServ _librosserv;
        public BookControl(LibroServ librosserv)
        {
            _librosserv = librosserv;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CrearLibro([FromBody] CrearLibroDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var usuarioid = GetGuifromToken();
                await _librosserv.AddLibroAsync(dto, usuarioid);
                return Created("", new { message = "libro creado" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> ActualizarLibro(Guid id, [FromBody] ActualizarLibroDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var usuariid = GetGuifromToken();
                var libro = await _librosserv.UpdateLibroAsync(id, dto, usuariid);
                return Ok(libro);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
        [HttpGet]
        [Authorize]

        public async Task<IActionResult> GetLibros([FromQuery] FiltrosDtos? filtros)
        {
            try
            {
                var usuario = GetGuifromToken();
                var libros = await _librosserv.GetLibrosAsync(usuario);
                return Ok(libros);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpDelete("{id}")]
        [Authorize]

        public async Task<IActionResult> DeleteLibro([FromBody] Guid id) 
        {
            try
            {
                var usuarioid = GetGuifromToken();
                var resultado = await _librosserv.DeleteLibroAsync(id, usuarioid);
                if (resultado == "Libro no encotrado")
                {
                    return NotFound(new {message = resultado});
                }
                return Ok(resultado);


            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to delete {ex.Message}");
            }
        }
        [HttpGet]
        [Authorize]

        public async Task<IActionResult> GetById([FromBody] Guid id)
        {
            try
            {
                var usuarioid = GetGuifromToken();
                var libro = await _librosserv.GetLibroByIdAsync(id, usuarioid);
                return Ok(libro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        private Guid GetGuifromToken()
        {
            var userIdclaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.Parse(userIdclaim);

        }

    }
}

namespace Bibliotecaper.Models
{
    public class BookProgress
    {
        public Guid Id { get; set; }

        public DateTime FechaInicoLectura { get; set; }

        public DateTime? FechaFinalLectura { get; set; }

        public Guid UsuarioId { get; set; }

        public Guid LibroId { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual Libro Libro { get; set; }

    }
}

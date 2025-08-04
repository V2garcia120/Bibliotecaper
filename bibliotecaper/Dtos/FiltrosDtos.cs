namespace Bibliotecaper.Dtos
{
    public class FiltrosDtos
    {
        public Guid? LibroId { get; set; }
        public Guid? AutorId { get; set; }
        public string? TituloBusqueda { get; set; }
        public string? Estado { get; set; }

        public Guid? GeneroId { get; set; }
    }
}

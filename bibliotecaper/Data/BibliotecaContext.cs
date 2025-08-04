using Microsoft.EntityFrameworkCore;
using Bibliotecaper.Models;

namespace Bibliotecaper.Data
{
    public class BibliotecaContext : DbContext
    {
        public BibliotecaContext(DbContextOptions<BibliotecaContext> options) : base(options)
        {
        }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Generos> Generos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();
            modelBuilder.Entity<Libro>()
                .HasOne(l => l.Autor)
                .WithMany(a => a.Libros)
                .HasForeignKey(l => l.AutorId);
            modelBuilder.Entity<Libro>()
                .HasOne(l => l.Genero)
                .WithMany(g => g.Libros)
                .HasForeignKey(l => l.GeneroId);
            modelBuilder.Entity<Libro>()
                .HasOne(l => l.Usuario)
                .WithMany(u => u.Libros)
                .HasForeignKey(l => l.UsuarioId);
        }   
    }
    
}

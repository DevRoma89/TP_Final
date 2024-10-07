
using Microsoft.EntityFrameworkCore;
using ProyectoWeb.Shared.Entidades;

namespace ProyectoWeb.Server

{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Autor> Autores { get; set; }
        public DbSet<Editorial> Editoriales { get; set; }
        public DbSet<Categoria> Categorias{ get; set; }
        public DbSet<Libro> Libros { get; set; }

    }
}

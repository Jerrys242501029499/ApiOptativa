

using dominio.Modelo;
using Microsoft.EntityFrameworkCore;

namespace MajoApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions <ApplicationDbContext> options) : base(options) { }

        public DbSet<Personaje> personajes { get; set; }
   
        public DbSet<Genero> generos { get; set; }
    }
}

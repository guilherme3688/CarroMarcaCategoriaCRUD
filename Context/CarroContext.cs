using Microsoft.EntityFrameworkCore;

namespace ProjetoCarro.Context {
    public class CarroContext : DbContext {
        public CarroContext(DbContextOptions<CarroContext> options) : base(options) { }

        public DbSet<Carro> Carros { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }
}

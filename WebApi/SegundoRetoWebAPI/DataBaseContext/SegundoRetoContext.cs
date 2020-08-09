using SegundoRetoWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SegundoRetoWebAPI.DataBaseCotext
{
    public class SegundoRetoContext : DbContext
    {
        public SegundoRetoContext(DbContextOptions<SegundoRetoContext> options) : base(options)
        {
        }

        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Producto> Productos { get; set; }
    }
}
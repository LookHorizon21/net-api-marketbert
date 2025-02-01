using ApiMarketBert.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiMarketBert.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        //Modelos
        public DbSet<CategoriaMd> Categorias { get; set; }
        public DbSet<ProductoMd> Productos { get; set; }
        public DbSet<UsuarioMd> Usuarios { get; set; }
    }
}

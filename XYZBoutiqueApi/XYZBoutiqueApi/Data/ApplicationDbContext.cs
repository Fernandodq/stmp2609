using Microsoft.EntityFrameworkCore;
using XYZBoutiqueApi.Models;

namespace XYZBoutiqueApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Pedido> Pedidos { get; set; }
        // Agrega aqu� otras DbSets para otras entidades si es necesario
    }
}

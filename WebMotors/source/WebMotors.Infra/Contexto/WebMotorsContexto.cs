using Microsoft.EntityFrameworkCore;
using WebMotors.Core.Entidades;

namespace WebMotors.Infra.Contexto
{
    public class WebMotorsContexto : DbContext
    {
        public WebMotorsContexto(DbContextOptions options)
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Anuncio> Anuncios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WebMotorsContexto).Assembly);
        }
    }
}

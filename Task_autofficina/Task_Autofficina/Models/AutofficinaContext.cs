using Microsoft.EntityFrameworkCore;

namespace Task_Autofficina.Models
{
    public class AutofficinaContext : DbContext
    {
        public AutofficinaContext(DbContextOptions<AutofficinaContext> options) : base(options)
        {
        }
        public DbSet<Cliente> Clienti { get; set; }
        public DbSet<Veicolo> Veicoli { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Veicolo>()
                .Property(v => v.PrezzoIntervento)
                .HasColumnType("decimal(8, 2)");
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Reservas.Models;
namespace Reservas.Data
{
    public class ReservasContext : DbContext
    {
        public ReservasContext(DbContextOptions<ReservasContext> options) : base(options)
        {
        }

        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Participante> Participantes { get; set; }
        public DbSet<EventoParticipante> EventoParticipantes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relacionamento N:N entre Evento e Participante
            modelBuilder.Entity<EventoParticipante>()
                .HasKey(ep => new { ep.EventoId, ep.ParticipanteId });

            modelBuilder.Entity<EventoParticipante>()
                .HasOne(ep => ep.Evento)
                .WithMany(e => e.EventoParticipantes)
                .HasForeignKey(ep => ep.EventoId);

            modelBuilder.Entity<EventoParticipante>()
                .HasOne(ep => ep.Participante)
                .WithMany(p => p.EventoParticipantes)
                .HasForeignKey(ep => ep.ParticipanteId);

            // Configuração explícita do relacionamento Evento -> Local
            modelBuilder.Entity<Evento>()
                .HasOne(e => e.Local)
                .WithMany(l => l.Eventos)  // Assume que Local tem uma coleção de Eventos
                .HasForeignKey(e => e.LocalId)  // Chave estrangeira
                .OnDelete(DeleteBehavior.Cascade);  // Defina o comportamento de exclusão, caso necessário

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Reservas.Models.Local> Local { get; set; } = default!;
    }
}

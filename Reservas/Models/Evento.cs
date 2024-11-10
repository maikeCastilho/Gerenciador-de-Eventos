using Reservas.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reservas.Models
{
    public class Evento
    {
        [Key]
        public int EventoId { get; set; }
        [Required] 
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataHora { get; set; }
        public double PrecoIngresso { get; set; }
        [Required]
        public int LocalId { get; set; }
        public Local Local { get; set; }
        public ICollection<Reserva> Reservas { get; set; }
        public ICollection<EventoParticipante> EventoParticipantes { get; set; }
    }
}

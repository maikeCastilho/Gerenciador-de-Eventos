using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Reservas.Models
{
    public class Reserva
    {
        [Key]
        public int ReservaId { get; set; }
        [ForeignKey("Evento")]
        public int EventoId { get; set; }
        public Evento Evento { get; set; }
        [ForeignKey("Participante")]
        public int ParticipanteId { get; set; }
        public Participante Participante { get; set; }
    }
}

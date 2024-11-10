using System.ComponentModel.DataAnnotations.Schema;

    namespace Reservas.Models
    {
        public class EventoParticipante
        {
            [ForeignKey("Evento")]
            public int EventoId { get; set; }
            public Evento Evento { get; set; }

            [ForeignKey("Participante")]
            public int ParticipanteId { get; set; }
            public Participante Participante { get; set; }
        }
    }


using Reservas.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Reservas.Models
{
    public class Participante
    {
        [Key]
        public int IdParticipante { get; set; }
        [Required]
        public string Nome { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public TipoParticipante Tipo { get; set; }
        public ICollection<Reserva> Reservas { get; set; }
        public ICollection<EventoParticipante> EventoParticipantes { get; set; }
    }
}

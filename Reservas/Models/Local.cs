using System.ComponentModel.DataAnnotations;

namespace Reservas.Models
{
    public class Local
    {
        [Key]
        public int IdLocal { get; set; }
        [Required]
        public string Cidade { get; set; }
        [Required]
        public string Bairro { get; set; }
        [Required]
        public bool Acessibilidade { get; set; }
        public string EstacionamentoDisponivel { get; set; }
        public int Cep { get; set; }
        public ICollection<Evento> Eventos { get; set; }
    }
}

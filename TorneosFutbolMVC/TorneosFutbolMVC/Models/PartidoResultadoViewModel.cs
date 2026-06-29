using System.ComponentModel.DataAnnotations;

namespace TorneosFutbolMVC.Models
{
    public class PartidoResultadoViewModel
    {
        public long PartidoId { get; set; }

        [Required]
        [Range(0, 100)]
        public int GolesLocal { get; set; }

        [Required]
        [Range(0, 100)]
        public int GolesVisitante { get; set; }
    }
}
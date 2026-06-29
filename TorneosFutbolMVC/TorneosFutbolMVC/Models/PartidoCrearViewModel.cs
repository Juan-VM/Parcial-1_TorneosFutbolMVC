using System.ComponentModel.DataAnnotations;

namespace TorneosFutbolMVC.Models
{
    public class PartidoCrearViewModel
    {
        public long TorneoId { get; set; }

        [Required]
        public long EquipoLocalId { get; set; }

        [Required]
        public long EquipoVisitanteId { get; set; }

        [Required]
        public DateTime FechaPartido { get; set; }
    }
}
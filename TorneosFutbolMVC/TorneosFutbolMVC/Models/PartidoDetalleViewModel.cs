namespace TorneosFutbolMVC.Models
{
    public class PartidoDetalleViewModel
    {
        public long Id { get; set; }

        public string EquipoLocal { get; set; }

        public string EquipoVisitante { get; set; }

        public DateTime FechaPartido { get; set; }

        public bool Jugado { get; set; }

        public int GolesLocal { get; set; }

        public int GolesVisitante { get; set; }
    }
}

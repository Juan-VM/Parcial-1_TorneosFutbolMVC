namespace TorneosFutbolMVC.Models
{
    public class TorneoDetalleViewModel
    {
        public Torneo Torneo { get; set; }

        public List<Equipo> Equipos { get; set; }

        public List<PartidoDetalleViewModel> Partidos { get; set; }
    }
}
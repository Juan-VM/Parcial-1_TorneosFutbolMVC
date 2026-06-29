using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System.ComponentModel.DataAnnotations;

namespace TorneosFutbolMVC.Models
{
    [Table("partidos")]
    public class Partido : BaseModel
    {
        [PrimaryKey("id", false)]
        public long Id { get; set; }

        [Column("torneo_id")]
        public long TorneoId { get; set; }

        [Column("equipo_local_id")]
        public long EquipoLocalId { get; set; }

        [Column("equipo_visitante_id")]
        public long EquipoVisitanteId { get; set; }

        [Range(0, 100)]
        [Column("goles_local")]
        public int GolesLocal { get; set; }

        [Range(0, 100)]
        [Column("goles_visitante")]
        public int GolesVisitante { get; set; }

        [Column("fecha_partido")]
        public DateTime FechaPartido { get; set; }

        [Column("jugado")]
        public bool Jugado { get; set; }
    }
}

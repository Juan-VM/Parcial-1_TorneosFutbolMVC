using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System.ComponentModel.DataAnnotations;

namespace TorneosFutbolMVC.Models
{
    [Table("equipos")]
    public class Equipo : BaseModel
    {
        [PrimaryKey("id", false)]
        public long Id { get; set; }

        [Column("torneo_id")]
        public long TorneoId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Column("nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La ciudad es obligatoria")]
        [Column("ciudad")]
        public string Ciudad { get; set; }
    }
}
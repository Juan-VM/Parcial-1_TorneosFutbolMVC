using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System.ComponentModel.DataAnnotations;

namespace TorneosFutbolMVC.Models
{
    [Table("torneos")]
    public class Torneo : BaseModel
    {
        [PrimaryKey("id", false)]
        public long Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Column("nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La edición es obligatoria")]
        [Range(1900, 2100)]
        [Column("edicion")]
        public int Edicion { get; set; }

        [Column("activo")]
        public bool Activo { get; set; }

        [Column("creado_en")]
        public DateTime CreadoEn { get; set; }
    }
}
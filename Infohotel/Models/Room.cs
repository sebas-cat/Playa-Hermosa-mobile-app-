using System;
using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;

namespace Infohotel.Models
{
    // Modelo para la tabla de habitaciones
    [Table("tb_Habitacion")]
    public class Room : BaseModel
    {
        [PrimaryKey("id_habitacion")]
        public int id_habitacion { get; set; }

        [Column("numero")]
        public int numero { get; set; }

        [Column("nombre")]
        public string nombre { get; set; }

        [Column("descripcion")]
        public string descripcion { get; set; }

        [Column("precio_noche")]
        public decimal precio_noche { get; set; }

        [Column("capacidad")]
        public int capacidad { get; set; }

        [Column("imagen_url")]
        public string imagen_url { get; set; }

        [Column("disponible")]
        public bool disponible { get; set; }
    }
}


using System;
using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;

namespace Infohotel.Models
{
    // Modelo para la tabla de reservas
    [Table("tb_Reserva")]
    public class Reserva : BaseModel
    {
        [PrimaryKey("id_reserva")]
        public int id_reserva { get; set; }

        [Column("id_Usuario")]
        public int id_Usuario { get; set; }

        [Column("id_habitacion")]
        public int id_habitacion { get; set; }

        [Column("fecha_inicio")]
        public DateTime fecha_inicio { get; set; }

        [Column("fecha_fin")]
        public DateTime fecha_fin { get; set; }

        [Column("precio_noche")]
        public decimal precio_noche { get; set; }

        [Column("noches")]
        public int noches { get; set; }

        [Column("total")]
        public decimal total { get; set; }

        [Column("estado")]
        public string estado { get; set; }

        [Column("fecha_creacion")]
        public DateTime fecha_creacion { get; set; }
    }
}


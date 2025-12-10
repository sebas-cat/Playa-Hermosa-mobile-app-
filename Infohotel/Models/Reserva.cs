using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;

namespace Infohotel.Models
{
    [Table("tb_Reserva")]
    public class Reserva : BaseModel
    {
        [PrimaryKey("id_Reserva")]
        public int Id { get; set; }

        [Column("room_id")]
        public string RoomId { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("apellido")]
        public string Apellido { get; set; }

        [Column("correo")]
        public string Correo { get; set; }

        [Column("telefono")]
        public string Telefono { get; set; }

        [Column("fecha_reserva")]
        public DateTime FechaReserva { get; set; }
    }
}


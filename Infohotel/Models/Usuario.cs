using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;

namespace Infohotel.Models
{
    //Modelo para la tabla de usuarios para que C# y Supabase muevan datos 
    [Table("tb_Usuario")]
    public class Usuario : BaseModel
    {
        [PrimaryKey("id_Usuario")]
        public int id_Usuario { get; set; }

        [Column("nombre")]
        public string nombre { get; set; }

        [Column("apellido")]
        public string apellido { get; set; }

        [Column("correo")]
        public string correo { get; set; }

        [Column("contrasenha")]
        public string contrasenha { get; set; }

        [Column("edad")]
        public int edad {  get; set; }

        [Column("domicilio")]
        public string domicilio { get; set; }

        [Column("fecha_registro")]
        public DateTime fecha_registro { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supabase;

namespace Infohotel
{
    public static class SupabaseClientService {
        //Variable que guarda la instancia del cliente Supabase (en _client va supabase)
        private static Client _client;

        //Publica para que se pueda acceder a la instancia
        public static Client Client {
            get {
                if (_client == null) {
                    _client = new Client(
                    //URL del proyecto para crearlo 
                    "https://zaldcnbaufpxdlpjxzve.supabase.co",
                    //API KEY Publica);
                    "sb_publishable_zYRUjQ0kfc4gxwxTs4XGzg_uk7qvaaH",
                    //Opciones de configuracion
                    new SupabaseOptions
                    {
                        //Desactivamos la conexion en tiempo real
                        AutoConnectRealtime = false
                    });

                }
                //Se devuelve el mismo cliente que conectamos
                return _client;
            }
        }

    }
}

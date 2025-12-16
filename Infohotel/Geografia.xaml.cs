using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace Infohotel;

public partial class Geografia : ContentPage
{
	public Geografia()
	{
		InitializeComponent();
        CargarMapa();// Llama al método que se encarga de configurar y cargar el mapa
    }

    // Método encargado de configurar el mapa y mostrar la ubicación del hotel
    private void CargarMapa()
    {
        var ubicacion = new Location(10.572118733917682, -85.6972728693201); // Se crea una ubicación usando coordenadas de latitud y longitud

        HotelMapa.MoveToRegion(             // Mueve el mapa para que se centre en la ubicación indicada y define un radio de visualización de 1 kilómetro
            MapSpan.FromCenterAndRadius(
                ubicacion,
                Distance.FromKilometers(1))
        );


        // Se crea un pin (marcador) que aparecerá en el mapa

        var pin = new Pin  
        {
            Label = "Waldorf Astoria", // Texto que se mostrará como título del pin
            Address = "Guanacaste", // Dirección o zona que se mostrará como información adicional
            Location = ubicacion, // Ubicación exacta donde se colocará el pin
            Type = PinType.Place // Tipo de pin (lugar de interés)
        };

        // Se agrega el pin al mapa para que sea visible
        HotelMapa.Pins.Add(pin);
    }
}
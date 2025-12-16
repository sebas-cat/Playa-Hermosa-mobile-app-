using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace Infohotel;

public partial class Geografia : ContentPage
{
	public Geografia()
	{
		InitializeComponent();
        CargarMapa();
	}

    private void CargarMapa()
    {
        var ubicacion = new Location(10.572118733917682, -85.6972728693201); 

        HotelMapa.MoveToRegion(
            MapSpan.FromCenterAndRadius(
                ubicacion,
                Distance.FromKilometers(1))
        );

        var pin = new Pin
        {
            Label = "Waldorf Astoria",
            Address = "Guanacaste",
            Location = ubicacion,
            Type = PinType.Place
        };

        HotelMapa.Pins.Add(pin);
    }
}
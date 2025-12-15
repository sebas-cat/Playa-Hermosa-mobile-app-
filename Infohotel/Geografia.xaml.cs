using Microsoft.Maui.Controls.Maps;  // Para el control Map
using Microsoft.Maui.Maps;          // Para Location, MapSpan, Distance
using MAUIApplicationModel = Microsoft.Maui.ApplicationModel; // Alias para evitar conflicto

namespace Infohotel;

public partial class Geografia : ContentPage
{
    // Coordenadas del Hotel Waldorf Astoria Costa Rica
    private readonly Location _hotelLocation = new(10.5560, -85.6895);

    public Geografia()
    {
        InitializeComponent();

        // Configurar el mapa al cargar la página
        InitializeMap();
    }

    private void InitializeMap()
    {
        try
        {
            // Centrar el mapa en la ubicación del hotel
            mapaHotel.MoveToRegion(MapSpan.FromCenterAndRadius(
                _hotelLocation,
                Distance.FromKilometers(3))); // Zoom de 3km alrededor

            // Asegurar que el pin sea visible
            mapaHotel.Pins.Clear();
            var pin = new Pin
            {
                Label = "Waldorf Astoria Costa Rica",
                Address = "Punta Cacique, Playa Hermosa, Guanacaste",
                Type = PinType.Place,
                Location = _hotelLocation
            };

            mapaHotel.Pins.Add(pin);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error inicializando mapa: {ex.Message}");
        }
    }

    // Evento: Click en botón para centrar mapa
    private void OnCenterMapClicked(object sender, EventArgs e)
    {
        try
        {
            mapaHotel.MoveToRegion(MapSpan.FromCenterAndRadius(
                _hotelLocation,
                Distance.FromKilometers(1))); // Zoom más cercano
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error centrando mapa: {ex.Message}");
        }
    }

    // Evento: Abrir en Google Maps/Apple Maps nativo
    private async void OnOpenMapsClicked(object sender, EventArgs e)
    {
        try
        {
            var options = new MAUIApplicationModel.MapLaunchOptions
            {
                Name = "Waldorf Astoria Costa Rica",
                NavigationMode = MAUIApplicationModel.NavigationMode.None // Solo mostrar, no navegar
            };

            // Usamos el Map de ApplicationModel con el alias
            await MAUIApplicationModel.Map.OpenAsync(_hotelLocation, options);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error",
                $"No se pudo abrir Maps: {ex.Message}",
                "OK");
        }
    }

    // Evento: Cómo llegar (con navegación)
    private async void OnGetDirectionsClicked(object sender, EventArgs e)
    {
        try
        {
            var options = new MAUIApplicationModel.MapLaunchOptions
            {
                Name = "Waldorf Astoria Costa Rica",
                NavigationMode = MAUIApplicationModel.NavigationMode.Driving // Modo conducción
            };

            await MAUIApplicationModel.Map.OpenAsync(_hotelLocation, options);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error",
                $"No se pudo obtener direcciones: {ex.Message}",
                "OK");
        }
    }

    // Evento cuando la página aparece (por si hay que actualizar)
    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Si el mapa se perdió, reinicializar
        if (mapaHotel.Pins.Count == 0)
        {
            InitializeMap();
        }
    }
}
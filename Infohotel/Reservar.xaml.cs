using Infohotel.Models;
using System.Collections.ObjectModel;
namespace Infohotel;


// Página encargada de mostrar las habitaciones disponibles y permitir al usuario seleccionar una para reservar
public partial class Reservar : ContentPage
{
    public ObservableCollection<Room> Rooms { get; set; } = new();

    public Reservar()
    {
        InitializeComponent();
        BindingContext = this;
    }

    // Método del ciclo de vida de la página, Se ejecuta cada vez que la página aparece en pantalla
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadRoomsFromDatabase();
    }


    // Método que consulta la base de datos y llena la colección de habitaciones
    private async Task LoadRoomsFromDatabase()
    {
        try
        {
            Rooms.Clear();

            var response = await SupabaseClientService.Client
                .From<Room>()
                .Get();

            foreach (var room in response.Models)
                Rooms.Add(room);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "No se pudieron cargar las habitaciones.\n" + ex.Message, "OK");
        }
    }

    // Evento que se ejecuta cuando el usuario presiona el botón para ver el detalle de una habitación
    private async void OnViewRoomClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;// Obtiene el botón presionado
        var room = (Room)button.BindingContext;// Recupera la habitación asociada al botón mediante BindingContext
        await Navigation.PushAsync(new HabitacionDetalle(room));// Navega a la pantalla de detalle de la habitación seleccionada
    }
}

using Infohotel.Models;
using System.Collections.ObjectModel;

namespace Infohotel;

public partial class Reservas : ContentPage
{
    public ObservableCollection<Reserva> ListaReservas { get; set; } = new();  // Permite que la UI se actualice automáticamente al agregar o eliminar reservas

    public Reservas()
    {
        InitializeComponent();
        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CargarReservas();
    }

    private async Task CargarReservas() // Método encargado de cargar las reservas del usuario desde la base de datos
    {
        try
        {
            ListaReservas.Clear();// Limpia la lista antes de volver a llenarla

            int userId = Preferences.Get("user_logeado", 0);// Obtiene el ID del usuario logueado desde preferencias

            var reservasResponse = await SupabaseClientService.Client // Consulta las reservas asociadas al usuario actual
                .From<Reserva>()
                .Filter("id_Usuario", Supabase.Postgrest.Constants.Operator.Equals, userId)
                .Get();

            
            var roomsResponse = await SupabaseClientService.Client // Crea un diccionario para acceder rápido al nombre de la habitación por ID
            .From<Room>()
            .Get();

            var roomsDict = roomsResponse.Models.ToDictionary(r => r.id_habitacion, r => r.nombre); // Recorre cada reserva y asigna el nombre de la habitación correspondiente

            foreach (var reserva in reservasResponse.Models)
            {
                
                reserva.RoomName = roomsDict.TryGetValue(reserva.id_habitacion, out var name) ? name : "Desconocida";
                ListaReservas.Add(reserva); // Agrega la reserva a la lista observable
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Hubo un problema al cargar las reservas.\n" + ex.Message, "OK");
        }
    }

    private async void OnDeleteReservaClicked(object sender, EventArgs e)  // Evento que se ejecuta al presionar el botón "Eliminar Reserva"
    {
        if (sender is Button btn && btn.CommandParameter is Reserva reserva)
        {
            bool confirm = await DisplayAlert("Eliminar reserva", "¿Estás seguro de que deseas eliminar esta reserva?", "Sí", "No");
            if (!confirm) return;

            try
            {
                await SupabaseClientService.Client // Elimina la reserva de la base de datos usando su ID
                    .From<Reserva>()
                    .Where(x => x.id_reserva == reserva.id_reserva)
                    .Delete();

                ListaReservas.Remove(reserva); // Elimina la reserva de la lista para reflejar el cambio en la UI
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "No se pudo eliminar la reserva.\n" + ex.Message, "OK"); // Manejo de errores si falla la eliminación
            }
        }
    }
}



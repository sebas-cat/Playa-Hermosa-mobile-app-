using Infohotel.Models;
using System.Collections.ObjectModel;

namespace Infohotel;

public partial class Reservas : ContentPage
{
    public ObservableCollection<Reserva> ListaReservas { get; set; } = new();

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

    private async Task CargarReservas()
    {
        try
        {
            ListaReservas.Clear();
            int userId = Preferences.Get("user_logeado", 0);

            var reservasResponse = await SupabaseClientService.Client
                .From<Reserva>()
                .Filter("id_Usuario", Supabase.Postgrest.Constants.Operator.Equals, userId)
                .Get();

            
            var roomsResponse = await SupabaseClientService.Client
            .From<Room>()
            .Get();

            var roomsDict = roomsResponse.Models.ToDictionary(r => r.id_habitacion, r => r.nombre);

            foreach (var reserva in reservasResponse.Models)
            {
                
                reserva.RoomName = roomsDict.TryGetValue(reserva.id_habitacion, out var name) ? name : "Desconocida";
                ListaReservas.Add(reserva);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Hubo un problema al cargar las reservas.\n" + ex.Message, "OK");
        }
    }

    private async void OnDeleteReservaClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is Reserva reserva)
        {
            bool confirm = await DisplayAlert("Eliminar reserva", "¿Estás seguro de que deseas eliminar esta reserva?", "Sí", "No");
            if (!confirm) return;

            try
            {
                await SupabaseClientService.Client
                    .From<Reserva>()
                    .Where(x => x.id_reserva == reserva.id_reserva)
                    .Delete();

                ListaReservas.Remove(reserva);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "No se pudo eliminar la reserva.\n" + ex.Message, "OK");
            }
        }
    }
}



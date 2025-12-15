using Infohotel.Models;
using System.Collections.ObjectModel;

namespace Infohotel;

public partial class Reservar : ContentPage
{
    public ObservableCollection<Room> Rooms { get; set; } = new();

    public Reservar()
    {
        InitializeComponent();
        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadRoomsFromDatabase();
    }

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

    private async void OnViewRoomClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var room = (Room)button.BindingContext;
        await Navigation.PushAsync(new HabitacionDetalle(room));
    }
}

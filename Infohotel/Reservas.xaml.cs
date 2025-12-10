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

            var response = await SupabaseClientService.Client
                .From<Reserva>()
                .Get();

            foreach (var r in response.Models)
                ListaReservas.Add(r);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error",
                "Hubo un problema al cargar las reservas.\n" + ex.Message,
                "OK");
        }
    }
}



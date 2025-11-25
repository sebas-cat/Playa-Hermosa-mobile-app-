using Infohotel.Models;

namespace Infohotel;

public partial class ReservaPopup : ContentPage
{
    // Se mantiene la información original de la habitación seleccionada.
    private readonly Room _room;

    // Fechas seleccionadas en la pantalla anterior.
    private readonly DateTime _checkIn;
    private readonly DateTime _checkOut;

    // Cantidad de huéspedes seleccionados.
    private readonly int _guests;

    public ReservaPopup(Room room, DateTime checkIn, DateTime checkOut, int guests)
    {
        InitializeComponent();

        // Se guardan los valores para mostrarlos y calcular el precio.
        _room = room;
        _checkIn = checkIn;
        _checkOut = checkOut;
        _guests = guests;

        // Genera el resumen visible al usuario.
        FillSummary();
    }

    private void FillSummary()
    {
        RoomNameLabel.Text = _room.Name;

        // Se formatean las fechas para mostrarlas de manera legible.
        DatesLabel.Text = $"Entrada: {_checkIn:dddd dd MMMM yyyy}\nSalida: {_checkOut:dddd dd MMMM yyyy}";

        GuestsSummaryLabel.Text = _guests == 1 ? "1 huésped" : $"{_guests} huéspedes";

        // Se calcula el número de noches; si las fechas son iguales, se cuenta 1 noche.
        int nights = (_checkOut - _checkIn).Days;
        if (nights < 1) nights = 1;

        // Estimación del costo total según la tarifa por noche.
        decimal total = _room.PricePerNight * nights;

        PriceSummaryLabel.Text = $"{nights} noches · Total estimado: $ {total:N0}";
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        // Cierra el popup sin realizar ninguna acción adicional.
        await Navigation.PopModalAsync();
    }

    private async void OnConfirmClicked(object sender, EventArgs e)
    {
        // Validación mínima de campos obligatorios.
        if (string.IsNullOrWhiteSpace(FirstNameEntry.Text) ||
            string.IsNullOrWhiteSpace(LastNameEntry.Text) ||
            string.IsNullOrWhiteSpace(EmailEntry.Text))
        {
            await DisplayAlert("Error", "Por favor completa los campos obligatorios.", "OK");
            return;
        }

        // Confirmación simbólica de la reserva.
        await DisplayAlert("Reserva realizada",
            "Tu reserva ha sido registrada. Recibirás un correo con los detalles.",
            "OK");

        // Cierra el popup después de confirmar.
        await Navigation.PopModalAsync();
    }
}
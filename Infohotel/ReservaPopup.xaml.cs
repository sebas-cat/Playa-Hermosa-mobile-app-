using Infohotel.Models;

namespace Infohotel;

public partial class ReservaPopup : ContentPage
{
    private Room _room;

    // NUEVOS CAMPOS PARA RECIBIR LOS DATOS
    private DateTime _checkIn;
    private DateTime _checkOut;
    private int _guests;

    // 🔹 Constructor original (puedes dejarlo si lo necesitas)
    public ReservaPopup(Room room)
    {
        InitializeComponent();
        _room = room;
        LoadRoomInfo();
    }

    // 🔹 Constructor que SÍ COINCIDE con la llamada desde HabitacionDetalle
    public ReservaPopup(Room room, DateTime checkIn, DateTime checkOut, int guests)
    {
        InitializeComponent();

        _room = room;
        _checkIn = checkIn;
        _checkOut = checkOut;
        _guests = guests;

        LoadRoomInfoWithDates();
    }

    // 🔸 Vista simple sin fechas (del constructor viejo)
    private void LoadRoomInfo()
    {
        RoomNameLabel.Text = _room.Name;
        PriceSummaryLabel.Text = $"$ {_room.PricePerNight:N0} / noche";

        DatesLabel.Text = $"Reserva para hoy ({DateTime.Now:dd/MM/yyyy})";
        GuestsSummaryLabel.Text = $"Máximo {_room.MaxGuests} huéspedes";
    }

    // 🔸 Nueva versión que muestra fechas reales y huéspedes
    private void LoadRoomInfoWithDates()
    {
        RoomNameLabel.Text = _room.Name;
        PriceSummaryLabel.Text = $"$ {_room.PricePerNight:N0} / noche";

        DatesLabel.Text =
            $"{_checkIn:dd/MM/yyyy} → {_checkOut:dd/MM/yyyy} " +
            $"({(_checkOut - _checkIn).Days} noches)";

        GuestsSummaryLabel.Text = $"{_guests} huésped(es)";
    }

    // -----------------------------
    // BOTONES
    // -----------------------------

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private async void OnConfirmClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(FirstNameEntry.Text) ||
            string.IsNullOrWhiteSpace(LastNameEntry.Text) ||
            string.IsNullOrWhiteSpace(EmailEntry.Text))
        {
            await DisplayAlert("Campos requeridos",
                "Por favor completa nombre, apellido y correo.",
                "OK");
            return;
        }

        try
        {
            var reserva = new Reserva
            {
                RoomId = _room.Id,
                Nombre = FirstNameEntry.Text.Trim(),
                Apellido = LastNameEntry.Text.Trim(),
                Correo = EmailEntry.Text.Trim(),
                Telefono = PhoneEntry.Text?.Trim() ?? "",
                FechaReserva = DateTime.Now
            };

            await SupabaseClientService.Client
                .From<Reserva>()
                .Insert(reserva);

            await DisplayAlert("Reserva confirmada",
                "Tu reserva fue realizada con éxito.",
                "OK");

            await Navigation.PopModalAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error",
                "No se pudo registrar la reserva: " + ex.Message,
                "OK");
        }
    }
}


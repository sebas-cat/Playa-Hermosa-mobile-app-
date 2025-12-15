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
        RoomNameLabel.Text = _room.nombre;
        PriceSummaryLabel.Text = $"$ {_room.precio_noche:N0} / noche";

        DatesLabel.Text = $"Reserva para hoy ({DateTime.Now:dd/MM/yyyy})";
        GuestsSummaryLabel.Text = $"Máximo {_room.capacidad} huéspedes";
    }

    // 🔸 Nueva versión que muestra fechas reales y huéspedes
    private void LoadRoomInfoWithDates()
    {
        RoomNameLabel.Text = _room.nombre;
        PriceSummaryLabel.Text = $"$ {_room.precio_noche:N0} / noche";

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
                id_Usuario = Preferences.Get("user_logeado", 0),
                id_habitacion = _room.id_habitacion,
                fecha_inicio = _checkIn,
                fecha_fin = _checkOut,
                precio_noche = _room.precio_noche,
                noches = (_checkOut - _checkIn).Days,
                total = _room.precio_noche * (_checkOut - _checkIn).Days,
                estado = "Pendiente",
                fecha_creacion = DateTime.Now
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


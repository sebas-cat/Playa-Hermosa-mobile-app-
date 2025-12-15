using Infohotel.Models;

namespace Infohotel;

public partial class HabitacionDetalle : ContentPage
{
    // Se almacena la habitación seleccionada para mostrar sus detalles.
    private readonly Room _room;

    // Fechas seleccionadas con el popup personalizado.
    private DateTime _checkIn;
    private DateTime _checkOut;

    // Indica si el popup está seleccionando la fecha de entrada o de salida.
    private bool _selectingCheckIn;

    public HabitacionDetalle(Room room)
    {
        InitializeComponent();
        _room = room;

        // Información principal mostrada en la vista de detalle.
        RoomNameHeader.Text = room.nombre;
        RoomImage.Source = room.imagen_url;
        RoomDescription.Text = room.descripcion;

        // Se muestra el precio formateado en moneda.
        RoomPrice.Text = $"Precio aproximado: $ {room.precio_noche:N0} por noche";

        // Indica la capacidad total permitida según el modelo.
        RoomCapacity.Text = $"Capacidad máxima: {room.capacidad} personas";

        // Valores iniciales sugeridos.
        _checkIn = DateTime.Today.AddDays(1);
        _checkOut = DateTime.Today.AddDays(2);

        UpdateDateButtons();

        // Inicializa los pickers del popup con rangos válidos.
        InitializeDatePickers();
    }

    private void UpdateDateButtons()
    {
        // Actualiza el texto de los botones con las fechas seleccionadas.
        CheckInButton.Text = _checkIn.ToString("dd/MM/yyyy");
        CheckOutButton.Text = _checkOut.ToString("dd/MM/yyyy");
    }

    private void InitializeDatePickers()
    {
        // Se define un rango de años (ejemplo: desde este año hasta +2).
        int startYear = DateTime.Today.Year;
        int endYear = startYear + 2;

        YearPicker.Items.Clear();
        for (int year = startYear; year <= endYear; year++)
            YearPicker.Items.Add(year.ToString());

        // Meses en formato numérico o con nombre, según preferencia.
        MonthPicker.Items.Clear();
        for (int m = 1; m <= 12; m++)
            MonthPicker.Items.Add(m.ToString("00"));

        // Valores iniciales: se usan los de la fecha de entrada.
        SetPickersFromDate(_checkIn);
    }

    private void SetPickersFromDate(DateTime date)
    {
        // Ajusta el año y mes en los pickers según la fecha recibida.
        YearPicker.SelectedIndex = YearPicker.Items.IndexOf(date.Year.ToString());
        MonthPicker.SelectedIndex = MonthPicker.Items.IndexOf(date.Month.ToString("00"));

        // Vuelve a calcular la lista de días para ese año/mes.
        FillDaysForSelectedMonthYear(date.Day);
    }

    private void FillDaysForSelectedMonthYear(int? dayToSelect = null)
    {
        if (YearPicker.SelectedIndex < 0 || MonthPicker.SelectedIndex < 0)
            return;

        int year = int.Parse(YearPicker.Items[YearPicker.SelectedIndex]);
        int month = int.Parse(MonthPicker.Items[MonthPicker.SelectedIndex]);

        int daysInMonth = DateTime.DaysInMonth(year, month);

        DayPicker.Items.Clear();
        for (int d = 1; d <= daysInMonth; d++)
            DayPicker.Items.Add(d.ToString("00"));

        // Selecciona un día válido, si se indicó uno previamente.
        if (dayToSelect.HasValue && dayToSelect.Value <= daysInMonth)
        {
            string dayStr = dayToSelect.Value.ToString("00");
            DayPicker.SelectedIndex = DayPicker.Items.IndexOf(dayStr);
        }
        else
        {
            DayPicker.SelectedIndex = 0;
        }
    }

    // Se ejecuta cuando cambia el mes o el año en el popup,
    // para recalcular el número de días disponibles.
    private void OnMonthOrYearChanged(object sender, EventArgs e)
    {
        FillDaysForSelectedMonthYear();
    }

    private void OnGuestsChanged(object sender, ValueChangedEventArgs e)
    {
        // Actualiza dinámicamente el texto según el número de huéspedes.
        int guests = (int)e.NewValue;
        GuestsLabel.Text = guests == 1 ? "1 persona" : $"{guests} personas";
    }

    private void OnSelectCheckInClicked(object sender, EventArgs e)
    {
        _selectingCheckIn = true;
        DatePopupTitle.Text = "Selecciona la fecha de entrada";

        // Sincroniza los pickers con la fecha actual de entrada.
        SetPickersFromDate(_checkIn);

        DatePopup.IsVisible = true;
    }

    private void OnSelectCheckOutClicked(object sender, EventArgs e)
    {
        _selectingCheckIn = false;
        DatePopupTitle.Text = "Selecciona la fecha de salida";

        // Sincroniza los pickers con la fecha actual de salida.
        SetPickersFromDate(_checkOut);

        DatePopup.IsVisible = true;
    }

    private void OnDatePopupCancelClicked(object sender, EventArgs e)
    {
        // Cierra el popup sin aplicar cambios.
        DatePopup.IsVisible = false;
    }

    private async void OnDatePopupOkClicked(object sender, EventArgs e)
    {
        if (YearPicker.SelectedIndex < 0 ||
            MonthPicker.SelectedIndex < 0 ||
            DayPicker.SelectedIndex < 0)
        {
            DatePopup.IsVisible = false;
            return;
        }

        int year = int.Parse(YearPicker.Items[YearPicker.SelectedIndex]);
        int month = int.Parse(MonthPicker.Items[MonthPicker.SelectedIndex]);
        int day = int.Parse(DayPicker.Items[DayPicker.SelectedIndex]);

        DateTime selectedDate = new(year, month, day);

        if (_selectingCheckIn)
        {
            // La fecha de entrada no puede ser posterior a la fecha de salida actual.
            if (selectedDate > _checkOut)
            {
                await DisplayAlert("Rango no válido",
                    "La fecha de entrada no puede ser posterior a la fecha de salida.",
                    "OK");
                return;
            }

            _checkIn = selectedDate;
        }
        else
        {
            // La fecha de salida no puede ser anterior a la fecha de entrada.
            if (selectedDate < _checkIn)
            {
                await DisplayAlert("Rango no válido",
                    "La fecha de salida no puede ser anterior a la fecha de entrada.",
                    "OK");
                return;
            }

            _checkOut = selectedDate;
        }

        // Actualiza el texto de los botones con las fechas válidas.
        UpdateDateButtons();

        DatePopup.IsVisible = false;
    }


    private async void OnReservarClicked(object sender, EventArgs e)
    {
        // Obtiene los valores seleccionados por el usuario antes de continuar.
        int guests = (int)GuestsStepper.Value;
        DateTime checkIn = _checkIn;
        DateTime checkOut = _checkOut;

        // Abre la ventana modal que permite completar la reserva.
        await Navigation.PushModalAsync(
            new ReservaPopup(_room, checkIn, checkOut, guests));
    }
}
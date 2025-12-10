namespace Infohotel;

public partial class Perfil : ContentPage
{
    public Perfil()
    {
        InitializeComponent();
        SetupEvents();
    }

    private void SetupEvents()
    {
        // Botón atrás
        var backTap = new TapGestureRecognizer();
        backTap.Tapped += async (s, e) =>
        {
            await Navigation.PopAsync();
        };
        BackLabel.GestureRecognizers.Add(backTap);

        // Botón configuración
        var configTap = new TapGestureRecognizer();
        configTap.Tapped += async (s, e) =>
        {
            await DisplayAlert("Configuración",
                "Aquí irán las opciones de configuración del usuario.",
                "OK");
        };
        SettingsLabel.GestureRecognizers.Add(configTap);

        // Botón editar perfil
        EditButton.Clicked += OnEditProfileClicked;
    }

    private async void OnEditProfileClicked(object sender, EventArgs e)
    {
        await DisplayAlert(
            "Editar perfil",
            "Aquí podrás editar tus datos personales cuando agreguemos esa pantalla.",
            "OK"
        );
    }
}

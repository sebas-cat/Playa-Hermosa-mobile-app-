using Supabase.Gotrue;

namespace Infohotel;

public partial class ActualizarPassword : ContentPage
{
    public ActualizarPassword()
    {
        InitializeComponent();
    }

    private async void OnGuardarClicked(object sender, EventArgs e)
    {
        var pass1 = NuevaEntry.Text?.Trim() ?? "";
        var pass2 = ConfirmarEntry.Text?.Trim() ?? "";

        // 1) Validaciones primero
        if (string.IsNullOrWhiteSpace(pass1) || string.IsNullOrWhiteSpace(pass2))
        {
            await DisplayAlert("Error", "Completá ambos campos.", "OK");
            return;
        }

        if (pass1 != pass2)
        {
            await DisplayAlert("Error", "No coinciden.", "OK");
            return;
        }

        try
        {
            await SupabaseClientService.Client.InitializeAsync();

            // 2) Cambiar password en SUPABASE AUTH
            var attrs = new UserAttributes { Password = pass1 };
            await SupabaseClientService.Client.Auth.Update(attrs);

            // 3) Sincronizar tu tabla tb_Usuario (para que tu login funcione)
            var currentUser = SupabaseClientService.Client.Auth.CurrentUser;
            var email = currentUser?.Email;

            if (!string.IsNullOrEmpty(email))
            {
                var supabase = SupabaseClientService.Client;

                var result = await supabase
                    .From<Usuario>()
                    .Where(u => u.correo == email)
                    .Get();

                var row = result.Models.FirstOrDefault();
                if (row != null)
                {
                    row.contrasenha = pass1;
                    await supabase.From<Usuario>().Update(row);
                }
            }

            await DisplayAlert("Listo", "Contraseña actualizada.", "OK");
            Application.Current.MainPage = new NavigationPage(new Login());
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}

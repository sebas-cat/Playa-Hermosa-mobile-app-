using Infohotel.Models;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace Infohotel;

public partial class CambiarPassword : ContentPage
{
    public CambiarPassword()
    {
        InitializeComponent();
    }

    private async void OnEnviarLinkClicked(object sender, EventArgs e)
    {
        var email = CorreoEntry.Text?.Trim();

        if (string.IsNullOrWhiteSpace(email))
        {
            await DisplayAlert("Error", "Ingresá tu correo.", "OK");
            return;
        }

        try
        {
            await SupabaseClientService.Client.InitializeAsync();
            var supabase = SupabaseClientService.Client;

            // 1) Verificar que exista en tu tabla (tb_Usuario)
            var result = await supabase
                .From<Usuario>()
                .Select("id_Usuario, correo")
                .Where(u => u.correo == email)
                .Get();

            var userRow = result.Models.FirstOrDefault();

            if (userRow == null)
            {
                await DisplayAlert("Error", "Ese correo no está registrado.", "OK");
                return;
            }

            // 2) Asegurar que exista en SUPABASE AUTH (sin tocar tu registro)
            //    Si ya existe, esto va a fallar con "User already registered" y lo ignoramos.
            try
            {
                var tempPass = GenerateTempPassword(16);
                await supabase.Auth.SignUp(email, tempPass);
            }
            catch
            {
                // Si ya existe en Auth, perfecto. Si falló por otra razón, igual intentamos el reset abajo.
            }

            // 3) Enviar correo de recuperación
            await supabase.Auth.ResetPasswordForEmail(email);

            await DisplayAlert("Listo", "Revisá tu correo (y Spam). Te enviamos el enlace para cambiar la contraseña.", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private static string GenerateTempPassword(int length)
    {
        // password temporal aleatoria (no se muestra al usuario)
        const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnpqrstuvwxyz23456789!@$%*#?";
        var data = new byte[length];
        RandomNumberGenerator.Fill(data);

        var result = new char[length];
        for (int i = 0; i < length; i++)
            result[i] = chars[data[i] % chars.Length];

        return new string(result);
    }
}

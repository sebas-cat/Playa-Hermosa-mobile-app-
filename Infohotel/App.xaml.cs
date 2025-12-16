using System;
using System.Collections.Generic;
using System.Web;

namespace Infohotel
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Flujo normal de la app
            if (Preferences.ContainsKey("user_logeado"))
            {
                MainPage = new AppShell();
            }
            else
            {
                MainPage = new NavigationPage(new Login());
            }
        }

        // 🔹 ESTE MÉTODO ES CLAVE PARA EL CORREO DE RECUPERACIÓN
        protected override async void OnAppLinkRequestReceived(Uri uri)
        {
            base.OnAppLinkRequestReceived(uri);

            if (uri == null)
                return;

            // Esperamos algo como:
            // infohotel://reset-password#access_token=...&refresh_token=...&type=recovery
            if (uri.Host == "reset-password")
            {
                try
                {
                    var tokens = ParseFragment(uri.Fragment);

                    if (!tokens.ContainsKey("access_token") ||
                        !tokens.ContainsKey("refresh_token"))
                    {
                        await MainPage.DisplayAlert(
                            "Error",
                            "El enlace no es válido o ya expiró.",
                            "OK");
                        return;
                    }

                    await SupabaseClientService.Client.InitializeAsync();

                    // Establecer sesión temporal con los tokens del correo
                    await SupabaseClientService.Client.Auth.SetSession(
                        tokens["access_token"],
                        tokens["refresh_token"],
                        true);

                    // Forzamos navegación a la pantalla de nueva contraseña
                    MainPage = new NavigationPage(new ActualizarPassword());
                }
                catch (Exception ex)
                {
                    await MainPage.DisplayAlert("Error", ex.Message, "OK");
                }
            }
        }

        // Utilidad para leer los tokens del fragment (#)
        private Dictionary<string, string> ParseFragment(string fragment)
        {
            var dict = new Dictionary<string, string>();

            if (string.IsNullOrWhiteSpace(fragment))
                return dict;

            var clean = fragment.StartsWith("#")
                ? fragment.Substring(1)
                : fragment;

            var values = HttpUtility.ParseQueryString(clean);

            foreach (string key in values.AllKeys)
            {
                if (key != null)
                    dict[key] = values[key];
            }

            return dict;
        }
        public async void HandleRecoveryLink(string link)
        {
            try
            {
                var uri = new Uri(link);

                // Esperamos: infohotel://reset-password#access_token=...&refresh_token=...&type=recovery
                if (uri.Host != "reset-password")
                    return;

                var tokens = ParseFragmentSimple(uri.Fragment);

                if (!tokens.TryGetValue("access_token", out var access) ||
                    !tokens.TryGetValue("refresh_token", out var refresh))
                {
                    await MainPage.DisplayAlert("Error", "El enlace no es válido o ya expiró.", "OK");
                    return;
                }

                await SupabaseClientService.Client.InitializeAsync();

                await SupabaseClientService.Client.Auth.SetSession(access, refresh, true);

                MainPage = new NavigationPage(new ActualizarPassword());
            }
            catch (Exception ex)
            {
                await MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private Dictionary<string, string> ParseFragmentSimple(string fragment)
        {
            var dict = new Dictionary<string, string>();

            if (string.IsNullOrWhiteSpace(fragment))
                return dict;

            var clean = fragment.StartsWith("#") ? fragment.Substring(1) : fragment;
            var pairs = clean.Split('&', StringSplitOptions.RemoveEmptyEntries);

            foreach (var p in pairs)
            {
                var kv = p.Split('=', 2);
                if (kv.Length == 2)
                    dict[Uri.UnescapeDataString(kv[0])] = Uri.UnescapeDataString(kv[1]);
            }

            return dict;
        }

    }
}

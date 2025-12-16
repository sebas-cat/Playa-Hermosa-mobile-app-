

namespace Infohotel
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }


        // Método que se ejecuta cuando el usuario presiona el botón para ir a la imagen anterior del carrusel
        void OnPrevClicked(object sender, EventArgs e)
        {
            try
            {

                // Lógica de navegación hacia atrás en el carrusel:
                // - Si no está en la primera imagen, retrocede una posición
                // - Si ya está en la primera, vuelve a la última imagen

                if (carousel.Position > 0)
                    carousel.Position--;
                else
                    carousel.Position = GetItemsCount() - 1;
            }
            catch { }
        }


        // Método que se ejecuta cuando el usuario presione el botón para avanzar a la siguiente imagen del carrusel
        void OnNextClicked(object sender, EventArgs e)
        {
            try
            {
                int count = GetItemsCount();
                if (carousel.Position < count - 1)
                    carousel.Position++;
                else
                    carousel.Position = 0;
            }
            catch { }
        }

        // Método auxiliar que calcula cuántos elementos tiene el ItemsSource del carrusel
        int GetItemsCount()
        {
  
            if (carousel.ItemsSource is System.Collections.IEnumerable en)
            {
                int c = 0;
                foreach (var _ in en) c++;
                return c;
            }
            return 0;
        }

        private async void logout(object sender, EventArgs e) {
            try
            {
                if (Preferences.ContainsKey("user_logeado"))
                    Preferences.Remove("user_logeado");
                if (Preferences.ContainsKey("auth_token"))
                    Preferences.Remove("auth_token");

                Microsoft.Maui.ApplicationModel.MainThread.BeginInvokeOnMainThread(() =>
                {
                    Application.Current.MainPage = new NavigationPage(new Login());
                });
            }
            catch (Exception ex) {
                await DisplayAlert("No se pudo cerrar sesion", ex.Message, "OK");
            }
        }
    }

}

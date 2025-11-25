namespace Infohotel
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Verificamos si existe un usuario guardado en Preferences
            if (Preferences.ContainsKey("user_id"))
            {
                // Si existe sesion ir directamente al Shell
                MainPage = new AppShell();
            }
            else
            {
                // Si NO hay sesion -> mostrar pagina de Login
                MainPage = new NavigationPage(new Login());
            }
        }
    }
}
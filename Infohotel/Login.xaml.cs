using Infohotel.Models;
using Supabase;

namespace Infohotel;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
	}

    //Metodo que juega a partir de tocar el BtnIr
    private async void OnLoginClicked(object sender, EventArgs e) { 

        //leer los valores primero
        //sintaxis es, si entry no es nulo, que haga el trim
        var email = UsernameEntry.Text?.Trim();
        var pass = PasswordEntry.Text?.Trim();

        //una validacion extra que funciona como errohandler pa decirle al usuario que llene lo que falta
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pass)) {
            await DisplayAlert("Error", "Debes llenar ambos campos", "OK");
            return;
        }

        //todo el trabajo de validar con la BD
        try
        {
            //obtenemos el cliente que se crea en la clase
            var supabase = SupabaseClientService.Client;

            // vamos a hacer una consulta a tb_usuarios filtrando por email
            var result = await supabase
                //esta parte interactua con el modelo, no con la BD como tal 
                .From<Usuario>()
                .Select("*")
                .Where(u => u.correo == email)
                .Get();

            Console.WriteLine("Rows: " + result.Models.Count);

            //Se agarra el primer usuario encontrado que exista
            var user = result.Models.FirstOrDefault();

            //si no se encuentra ninguno con ese email
            if (user == null)
            {
                await DisplayAlert("Error", "El usuario no existe", "OK");
                return; //para salir del metodo
            }

            //aca va la validacion de la contrasenha
            if (user.contrasenha != pass)
            {
                await DisplayAlert("Error", "Contraseña Incorrecta", "OK");
                return;
            }

            //Esta parte esta pensada para escalar, porque es para
            //mantener la sesion iniciada
            Preferences.Set("user_logeado", user.id_Usuario);
            Preferences.Set("user_email", user.correo);

            //Redireccion al resto de la app
            Application.Current.MainPage = new AppShell();
        }
        catch (Exception ex) { 
            //la parte de errores
            await DisplayAlert("Error", ex.Message, "OK");
        }


    }

    //Esto es lo de que al tocar registrar lo dirige a esa pag
    private async void OnRegisterTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Registro());
    }

}
using Infohotel.Models;
using Supabase;
using static Supabase.Postgrest.Constants;
namespace Infohotel;


public partial class Registro : ContentPage
{
	public Registro()
	{
		InitializeComponent();
	}

    private async void OnRegistroClick(object sender, EventArgs e) {

        var nombre = NombreEntry.Text?.Trim();
        var apellido = ApellidoEntry.Text?.Trim();
        var correo = CorreoEntry.Text?.Trim();
        var contrasenha = ContrasenhaEntry.Text?.Trim();
        var edadTexto = EdadEntry.Text?.Trim();
        var domicilio = DomicilioEntry.Text?.Trim();

        //La misma validacion para que llene todo 
        if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido)
            || string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contrasenha)) {
            await DisplayAlert("Error", "Debes llenar todos los campos", "OK");
            return;
        }

        if (!int.TryParse(edadTexto, out int edad))
        {
            await DisplayAlert("Error", "La edad debe ser un número válido.", "OK");
            return;
        }

        //validacion en la BD
        try
        {
            var supabase = SupabaseClientService.Client;

            // Verificar si ya existe un usuario con ese correo
            var existingUser = await supabase
                .From<Usuario>()
                .Select("*")
                .Filter("correo", Operator.ILike, correo)
                .Get();

            if (existingUser.Models.Count > 0)
            {
                await DisplayAlert("Error", "Ya existe un usuario con este correo.", "OK");
                return;
            }

            // Crear un nuevo usuario
            var nuevoUsuario = new Usuario
            {
                nombre = nombre,
                apellido = apellido,
                correo = correo,
                contrasenha = contrasenha, // (luego deberías encriptarla)
                edad = edad,
                domicilio = domicilio,
                fecha_registro = DateTime.UtcNow
            };

            // Insertar en Supabase
            var insertResult = await supabase
                .From<Usuario>()
                .Insert(nuevoUsuario);

            var userInserted = insertResult.Models.FirstOrDefault();

            if (userInserted == null)
            {
                await DisplayAlert("Error", "No se pudo crear la cuenta.", "OK");
                return;
            }

            // Guardar sesión
            Preferences.Set("user_logeado", userInserted.id_Usuario);
            Preferences.Set("user_email", userInserted.correo);

            // Redirigir al shell
            Application.Current.MainPage = new AppShell();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
    
}
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

namespace Infohotel
{
    [Activity(
        Theme = "@style/Maui.SplashTheme",
        MainLauncher = true,
        LaunchMode = LaunchMode.SingleTop,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
                               ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    [IntentFilter(
        new[] { Intent.ActionView },
        Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
        DataScheme = "infohotel",
        DataHost = "reset-password")]
    public class MainActivity : MauiAppCompatActivity
    {
    


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            HandleDeepLink(Intent);
        }

        protected override void OnNewIntent(Android.Content.Intent intent)
        {
            base.OnNewIntent(intent);
            HandleDeepLink(intent);
        }

        void HandleDeepLink(Android.Content.Intent intent)
        {
            var link = intent?.DataString;
            if (!string.IsNullOrEmpty(link))
            {
                Microsoft.Maui.ApplicationModel.MainThread.BeginInvokeOnMainThread(() =>
                {
                    if (Application.Current is App app)
                        app.HandleRecoveryLink(link);
                });
            }
        }
    }
}
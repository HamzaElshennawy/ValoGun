using Android.App;
using Android.Content.PM;
using Android.OS;

namespace ValoGun
{
	[Activity(Theme = "@style/Maui.SplashTheme", NoHistory = true, MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
	public class MainActivity : MauiAppCompatActivity
	{
		override protected void OnCreate(Bundle savedInstanceState) => base.OnCreate(savedInstanceState);
	}
}

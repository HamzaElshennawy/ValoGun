using ValoGun.Pages;

namespace ValoGun
{
	public partial class AppShell : Shell
	{
		public AppShell()
		{
			InitializeComponent();
			Routing.RegisterRoute(nameof(WeaponStatusPage), typeof(WeaponStatusPage));
		}
	}
}

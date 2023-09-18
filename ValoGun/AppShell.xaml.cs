using ValoGun.Pages;

namespace ValoGun
{
	public partial class AppShell : Shell
	{
		public AppShell()
		{
			InitializeComponent();
			Routing.RegisterRoute(nameof(WeaponSkinsPage), typeof(WeaponSkinsPage));
			Routing.RegisterRoute(nameof(WeaponsPage), typeof(WeaponsPage));
			Routing.RegisterRoute(nameof(AgentsPage), typeof(AgentsPage));
		}
	}
}

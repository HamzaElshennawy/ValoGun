using ValoGun.Pages;

namespace ValoGun
{
	public partial class AppShell : Shell
	{
		public AppShell()
		{
			InitializeComponent();
			Routing.RegisterRoute($"{nameof(HomePage)}", typeof(HomePage));
			Routing.RegisterRoute(nameof(AgentsPage), typeof(AgentsPage));
			Routing.RegisterRoute($"{nameof( WeaponSkinsPage )}", typeof(WeaponSkinsPage));
			Routing.RegisterRoute(nameof(WeaponsPage), typeof(WeaponsPage));
			Routing.RegisterRoute(nameof(AgentDetailsPage), typeof(AgentDetailsPage));
			Routing.RegisterRoute(nameof(WeaponDetailsPage), typeof(WeaponDetailsPage));
			Routing.RegisterRoute(nameof(PlayerCardsPage), typeof(PlayerCardsPage));
			Routing.RegisterRoute(nameof(PlayerCardsDetailsPage), typeof(PlayerCardsDetailsPage));
			Routing.RegisterRoute(nameof(SplashLoadingPage), typeof(SplashLoadingPage));
			Routing.RegisterRoute(nameof(MapsPage), typeof(MapsPage));
			Routing.RegisterRoute(nameof(MapPage), typeof(MapPage));
		}
	}
}

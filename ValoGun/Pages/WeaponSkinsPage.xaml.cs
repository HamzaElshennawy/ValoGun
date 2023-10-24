using ValoGun.Models;
using ValoGun.ViewModels;

namespace ValoGun.Pages;

public partial class WeaponSkinsPage : ContentPage
{
	public WeaponSkinsPage()
	{
		InitializeComponent();
		BindingContext = new WeaponsViewModel();
	}
	protected override void OnNavigatedTo(NavigatedToEventArgs args) => base.OnNavigatedTo(args);
}

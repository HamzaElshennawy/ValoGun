using ValoGun.Models;
using ValoGun.ViewModels;

namespace ValoGun.Pages;

public partial class WeaponStatusPage : ContentPage
{
	public WeaponStatusPage()
	{
		InitializeComponent();
		BindingContext = new WeaponStatusViewModel();
	}
	protected override void OnNavigatedTo(NavigatedToEventArgs args) => base.OnNavigatedTo(args);
}

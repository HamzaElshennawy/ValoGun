namespace ValoGun.Pages;

public partial class HomePage : ContentPage
{
	public HomePage() => InitializeComponent();
	//empty the navigation stack
	protected override void OnAppearing() => base.OnAppearing();
}

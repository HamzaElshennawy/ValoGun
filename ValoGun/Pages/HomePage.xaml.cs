namespace ValoGun.Pages;

public partial class HomePage : ContentPage
{
	public HomePage() => InitializeComponent();

	protected override bool OnBackButtonPressed()
	{
		return true;
	}

}

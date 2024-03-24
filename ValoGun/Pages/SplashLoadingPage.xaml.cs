namespace ValoGun.Pages;

public partial class SplashLoadingPage : ContentPage
{
	public SplashLoadingPage()
	{
		InitializeComponent();
		Thread navigationThread = new( async () =>
		{
			await Task.Delay( 2000 );
			await MainThread.InvokeOnMainThreadAsync(async ()=> await Shell.Current.GoToAsync( $"{nameof( HomePage )}" ));
		} );
		navigationThread.Start();
	}
}

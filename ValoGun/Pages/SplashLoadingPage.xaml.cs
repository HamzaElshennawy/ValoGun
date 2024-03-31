using ValoGun.ViewModels;

namespace ValoGun.Pages;

public partial class SplashLoadingPage : ContentPage
{
	public SplashLoadingPage()
	{
		InitializeComponent();
		Thread navigationThread = new( async () =>
		{
			//await MainThread.InvokeOnMainThreadAsync( ()=> loadingText.Text = "Loading Agents..."  );
			await AgentsPageViewModel.LoadData();
			await MainThread.InvokeOnMainThreadAsync(async ()=> await Shell.Current.GoToAsync( $"{nameof( HomePage )}" ));
		} );
		navigationThread.Start();
	}
}

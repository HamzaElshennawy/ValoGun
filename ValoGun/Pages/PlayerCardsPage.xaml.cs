using ValoGun.ViewModels;

namespace ValoGun.Pages;

public partial class PlayerCardsPage : ContentPage
{
	public PlayerCardsPage() => InitializeComponent();

	private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
	{
		var searchResult = PlayerCardsViewModel.Search(((SearchBar)sender).Text);
		CardsCV.ItemsSource = searchResult.Result;
		if(((SearchBar)sender).Text == "")
		{
			CardsCV.ItemsSource = PlayerCardsViewModel.PlayerCardsListUI;
		}
	}
}

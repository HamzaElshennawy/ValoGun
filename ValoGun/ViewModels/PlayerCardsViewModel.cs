using MvvmHelpers;
using Newtonsoft.Json;
using ValoGun.Models.Player_Cards;

namespace ValoGun.ViewModels;

public partial class PlayerCardsViewModel : BaseViewModel
{

	public string SearchText { get; set; }

	public static ObservableRangeCollection<Data> PlayerCards { get; set; }
	public static ObservableRangeCollection<PlayerCardUI> PlayerCardsListUI { get; set; }

	public PlayerCardsViewModel()
	{
		PlayerCards = [];
		PlayerCardsListUI = [];

		Thread DataThread = new(async () => await LoadPlayerCards());
		DataThread.Start();
	}

	public static Task<ObservableRangeCollection<PlayerCardUI>> Search(string SearchText = "")
	{
		if(string.IsNullOrEmpty(SearchText))
		{
			return null;
		}

		//var SearchResults = new ObservableRangeCollection<Data>();
		//SearchResults.AddRange(PlayerCards.Where(x => x.displayName.Contains(SearchText, StringComparison.CurrentCultureIgnoreCase)));
		var SearchResultsListUI = new ObservableRangeCollection<PlayerCardUI>();
		SearchResultsListUI.AddRange(PlayerCardsListUI.Where(x => x.displayName.Contains(SearchText, StringComparison.CurrentCultureIgnoreCase)));
		return Task.FromResult(SearchResultsListUI);
	}
	private async Task LoadPlayerCards()
	{
		using var stream = await FileSystem.OpenAppPackageFileAsync("PlayerCards.json");
		using var reader = new StreamReader(stream);
		var data = reader.ReadToEnd();

		var Cards = JsonConvert.DeserializeObject<PlayerCards>(data);

		foreach(var card in Cards.data)
		{
			//remove "Card" from the end of the name
			card.displayName = card.displayName.Replace("Card", "");
			await MainThread.InvokeOnMainThreadAsync(() => PlayerCards.Add(card));
			var PlayerCard = new PlayerCardUI
			{
				uuid = card.uuid,
				displayName = card.displayName,
				wideArt = card.wideArt,
				largeArt = card.largeArt
			};
			await MainThread.InvokeOnMainThreadAsync(() => PlayerCardsListUI.Add(PlayerCard));
			await MainThread.InvokeOnMainThreadAsync(() => OnPropertyChanged(nameof(PlayerCardsListUI)));
		}
		//await MainThread.InvokeOnMainThreadAsync(async () => await Shell.Current.DisplayAlert("Finished", "Finished", "OK"));

	}
}

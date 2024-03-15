using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoGun.Models.Player_Cards;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Storage;
using CommunityToolkit.Mvvm.Input;

namespace ValoGun.ViewModels
{
	public partial class PlayerCardsViewModel : BaseViewModel
	{

		public string SearchText { get; set; }

		public static ObservableRangeCollection<Data> PlayerCards { get; set; }

		public PlayerCardsViewModel()
		{
			PlayerCards = [];

			Thread DataThread = new(async () => await LoadPlayerCards());
			DataThread.Start();
		}
		
		public static Task<ObservableRangeCollection<Data>> Search(string SearchText = "")
        {
            if(string.IsNullOrEmpty(SearchText))
            {
                return null;
            }

            var SearchResults = new ObservableRangeCollection<Data>();
            SearchResults.AddRange(PlayerCards.Where(x => x.displayName.Contains(SearchText, StringComparison.CurrentCultureIgnoreCase)));

            return Task.FromResult(SearchResults);
        }
		private async Task LoadPlayerCards()
		{
			using var stream = await FileSystem.OpenAppPackageFileAsync("PlayerCards.json");
			using var reader = new StreamReader(stream);
			var data = reader.ReadToEnd();

			var Cards = JsonConvert.DeserializeObject<PlayerCards>(data);

			foreach(var card in Cards.data)
			{
				await MainThread.InvokeOnMainThreadAsync(() => PlayerCards.Add(card));
				await MainThread.InvokeOnMainThreadAsync(() => OnPropertyChanged(nameof(PlayerCards)));
			}
			//await MainThread.InvokeOnMainThreadAsync(async () => await Shell.Current.DisplayAlert("Finished", "Finished", "OK"));

		}
	}
}

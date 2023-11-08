using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoGun.Models.Player_Cards;

namespace ValoGun.ViewModels
{
	public class PlayerCardsViewModel : BaseViewModel
	{


		public ObservableRangeCollection<Data> _PlayerCards { get; set; }

		public PlayerCardsViewModel()
		{
			_PlayerCards = [];

			Thread DataThread = new Thread(async () => await LoadPlayerCards());
			DataThread.Start();
		}

		private async Task LoadPlayerCards()
		{
			using var stream = await FileSystem.OpenAppPackageFileAsync("PlayerCards.json");
			using var reader = new StreamReader(stream);
			var data = reader.ReadToEnd();

			var Cards = JsonConvert.DeserializeObject<PlayerCards>(data);

			foreach(var card in Cards.data)
			{
				await MainThread.InvokeOnMainThreadAsync(() => _PlayerCards.Add(card));
				await MainThread.InvokeOnMainThreadAsync(() => OnPropertyChanged(nameof(_PlayerCards)));
			}
			await MainThread.InvokeOnMainThreadAsync(async () => { await Shell.Current.DisplayAlert("Finished", "Finished", "OK"); });

		}
	}
}

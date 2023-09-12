using MvvmHelpers;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using ValoGun.Models;

namespace ValoGun.ViewModels
{
	public class HomePageViewModel : BaseViewModel
	{
		public static Weapons selectedWeapon { get; set; } = null!;
		public ObservableRangeCollection<Datum> Weapons { get; set; }
		public ObservableRangeCollection<Grouping<string, Datum>> GWeapons { get; set; } = new();
		public ObservableRangeCollection<Skin> Skins { get; set; }


		public AsyncCommand NavigateToStatus => new AsyncCommand(async () =>
		{
            await MainThread.InvokeOnMainThreadAsync(async () =>
			{
				await GoToPage(selectedWeapon);
            });
        });


		private Thread DataThread { get; set; }

		public HomePageViewModel()
		{
			Weapons = new();
			Skins = new();

			DataThread = new Thread(async () =>
			{
				await ReadData();
			});
			DataThread.Start();
		}

		private async Task<Task> ReadData()
		{

			try
			{
				//string filePath = "Vandal.json";
				//string filePath2 = "Odin.json";
				string filePath = "weapons.json";
				var stream = await FileSystem.OpenAppPackageFileAsync(filePath);
				var reader = new StreamReader(stream);


				string data = reader.ReadToEnd();

				// Deserialize the JSON data into an object
				var jsonData = JsonConvert.DeserializeObject<Weapons>(data);
				foreach (var weapon in jsonData.data)
				{
					Weapons.Add(weapon);
					OnPropertyChanged(nameof(Weapons));
				}

				Weapons.OrderBy(c => c.shopData.cost);


				GWeapons.Add(new Grouping<string, Datum>("Heavy", Weapons.Where(c => c.category == "Heavy")));
				GWeapons.Add(new Grouping<string, Datum>("Sniper", Weapons.Where(c => c.category == "Sniper")));
				GWeapons.Add(new Grouping<string, Datum>("Rifles", Weapons.Where(c => c.category == "Rifle")));
				GWeapons.Add(new Grouping<string, Datum>("Shotgun", Weapons.Where(c => c.category == "Shotgun")));
				GWeapons.Add(new Grouping<string, Datum>("SMG", Weapons.Where(c => c.category == "SMG")));
				GWeapons.Add(new Grouping<string, Datum>("Sidearm", Weapons.Where(c => c.category == "Sidearm")));
				GWeapons.Add(new Grouping<string, Datum>("Melee", Weapons.Where(c => c.category == "Melee")));
				//sort GWeapons list by shop price
				//for some reason this loop doesnt work
				//why?

				//foreach (var gWeapon in GWeapons)
				//{
				//	gWeapon.OrderBy(c => c.shopData.cost);
				//	OnPropertyChanged(nameof(GWeapons));
				//}
				////sort GWeapons list by category
				////GWeapons.OrderBy(c => c.Category);
				//OnPropertyChanged(nameof(GWeapons));



			}
			catch (Exception e)
			{

				await MainThread.InvokeOnMainThreadAsync(async () =>
				{
					await Application.Current.MainPage.DisplayAlert("Error", e.Message, "Ok");
				});
			}
			// Read the JSON file content



			return Task.CompletedTask;
		}
		private async Task GoToPage(Weapons weapon)
		{
            await Shell.Current.GoToAsync($"{nameof(WeaponStatusViewModel)}?MainWeapon = {weapon}");
        }
	}
}

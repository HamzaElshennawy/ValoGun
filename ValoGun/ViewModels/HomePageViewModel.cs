using MvvmHelpers;
using Newtonsoft.Json;
using ValoGun.Models;

namespace ValoGun.ViewModels
{
	public class HomePageViewModel : BaseViewModel
	{

		public ObservableRangeCollection<Datum> Weapons { get; set; }
		public List<GroupedWeapons> GWeapons{ get; set; } = new List<GroupedWeapons>();
		public ObservableRangeCollection<Skin> Skins { get; set; }


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
				//var dict = (Weapons.OrderBy(x => x.category)).GroupBy(o => o.category.Substring(0, 1)).ToDictionary(g => g.Key, g => g.ToList());

				//foreach (KeyValuePair<string, List<Datum>> item in dict)
				//{

				//	GWeapons.Add(new GroupedWeapons(item.Key, new List<Datum>(item.Value)));
				//	OnPropertyChanged(nameof(GWeapons));
				//}


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
	}
}

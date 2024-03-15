using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using ValoGun.Models.Weapons;
using ValoGun.Pages;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Storage;

namespace ValoGun.ViewModels
{
	public partial class WeaponsViewModel : BaseViewModel
	{
		
		private Thread DataThread { get; set; }

		Weapons SelectedWeapon;
		public Weapons selectedWeapon
		{
			get => SelectedWeapon;
			set
			{
				if(SelectedWeapon != value)
				{
					SelectedWeapon = value;
					OnPropertyChanged(nameof(SelectedWeapon));
					//GoToPage();
				}
			}
		}
		public ObservableRangeCollection<Datum> Weapons { get; set; }
		public ObservableRangeCollection<Grouping<string, Datum>> GWeapons { get; set; } = new();
		public ObservableRangeCollection<Skin> Skins { get; set; }


		
		public WeaponsViewModel()
		{
			
			Weapons = [];
			Skins = [];

			DataThread = new Thread(async () => await ReadData());
			DataThread.Start();
			
		}

		

		private async Task<Task> ReadData()
		{
			IsBusy = true;
			try
			{
				string filePath = "weapons.json";
				var stream = await FileSystem.OpenAppPackageFileAsync(filePath);
				var reader = new StreamReader(stream);


				string data = reader.ReadToEnd();
				await MainThread.InvokeOnMainThreadAsync(() => Weapons.Clear());
				// Deserialize the JSON data into an object
				var jsonData = JsonConvert.DeserializeObject<Weapons>(data);
				foreach(var weapon in jsonData.data)
				{
					Weapons.Add(weapon);
					//OnPropertyChanged(nameof(Weapons));

				}

				


				await MainThread.InvokeOnMainThreadAsync(() => GWeapons.Add(new Grouping<string, Datum>("Heavy", Weapons.Where(c => c.category == "Heavy").OrderBy((w) => w.shopData.cost))));
				await MainThread.InvokeOnMainThreadAsync(() => GWeapons.Add(new Grouping<string, Datum>("Sniper", Weapons.Where(c => c.category == "Sniper").OrderBy((w) => w.shopData.cost))));
				await MainThread.InvokeOnMainThreadAsync(() => GWeapons.Add(new Grouping<string, Datum>("Rifles", Weapons.Where(c => c.category == "Rifle").OrderBy((w) => w.shopData.cost))));
				await MainThread.InvokeOnMainThreadAsync(() => GWeapons.Add(new Grouping<string, Datum>("Shotgun", Weapons.Where(c => c.category == "Shotgun").OrderBy((w) => w.shopData.cost))));
				await MainThread.InvokeOnMainThreadAsync(() => GWeapons.Add(new Grouping<string, Datum>("SMG", Weapons.Where(c => c.category == "SMG").OrderBy((w) => w.shopData.cost))));
				await MainThread.InvokeOnMainThreadAsync(() => GWeapons.Add(new Grouping<string, Datum>("Sidearm", Weapons.Where(c => c.category == "Sidearm").OrderBy((w) => w.shopData.cost))));
				await MainThread.InvokeOnMainThreadAsync(() => GWeapons.Add(new Grouping<string, Datum>("Knife", Weapons.Where(c => c.category == "Melee"))));

				await MainThread.InvokeOnMainThreadAsync(() => OnPropertyChanged(nameof(GWeapons)));


			}
			catch(Exception e)
			{

				await MainThread.InvokeOnMainThreadAsync(async () => await Application.Current.MainPage.DisplayAlert("Error", e.Message, "Ok"));
			}
			// Read the JSON file content


			IsBusy = false;
			return Task.CompletedTask;
		}

		[RelayCommand]
		public async Task GoToWeaponPage(Datum datum)
		{
			WeaponDetailsViewModel.MainWeapon = datum;
			await Shell.Current.GoToAsync($"{nameof(WeaponDetailsPage)}", true);
		}
	}
}

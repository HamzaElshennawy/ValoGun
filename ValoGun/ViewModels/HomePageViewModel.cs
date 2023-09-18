using MvvmHelpers;
using CommunityToolkit.Maui;
using Newtonsoft.Json;
using ValoGun.Models;
using ValoGun.Pages;
using CommunityToolkit.Mvvm.Input;
using ValoGun.Models.Weapons;

namespace ValoGun.ViewModels
{
    public partial class HomePageViewModel : BaseViewModel
    {
        Weapons SelectedWeapon;
        public Weapons selectedWeapon
        {
            get => SelectedWeapon;
            set
            {
                if (SelectedWeapon != value)
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


        //public AsyncCommand NavigateToStatus => new(GoToPage);


        private Thread DataThread { get; set; }

        public HomePageViewModel()
        {
            Weapons = [];
            Skins = [];

            DataThread = new Thread(async () => await ReadData());
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


                GWeapons.Add(new Grouping<string, Datum>("Heavy", Weapons.Where(c => c.category == "Heavy").OrderBy((w) => w.shopData.cost)));
                GWeapons.Add(new Grouping<string, Datum>("Sniper", Weapons.Where(c => c.category == "Sniper").OrderBy((w) => w.shopData.cost)));
                GWeapons.Add(new Grouping<string, Datum>("Rifles", Weapons.Where(c => c.category == "Rifle").OrderBy((w) => w.shopData.cost)));
                GWeapons.Add(new Grouping<string, Datum>("Shotgun", Weapons.Where(c => c.category == "Shotgun").OrderBy((w) => w.shopData.cost)));
                GWeapons.Add(new Grouping<string, Datum>("SMG", Weapons.Where(c => c.category == "SMG").OrderBy((w) => w.shopData.cost)));
                GWeapons.Add(new Grouping<string, Datum>("Sidearm", Weapons.Where(c => c.category == "Sidearm").OrderBy((w) => w.shopData.cost)));
                GWeapons.Add(new Grouping<string, Datum>("Knife", Weapons.Where(c => c.category == "Melee")));

            }
            catch (Exception e)
            {

                await MainThread.InvokeOnMainThreadAsync(async () => await Application.Current.MainPage.DisplayAlert("Error", e.Message, "Ok"));
            }
            // Read the JSON file content



            return Task.CompletedTask;
        }
        [RelayCommand]
        private async Task GoToPage(Datum _selectedWeapon)
        {
            //await Shell.Current.DisplayAlert("Alert", "You have selected " + _selectedWeapon.displayName, "Ok");
            WeaponStatusViewModel.MainWeapon = _selectedWeapon;
            await Shell.Current.GoToAsync(nameof(WeaponSkinsPage), true);
        }

        [RelayCommand]
        public async Task GoToWeaponsPage()
        {
            await Shell.Current.GoToAsync(nameof(WeaponsPage), true);
        }

		[RelayCommand]
		public async Task GoToAgentsPage()
		{
			await Shell.Current.GoToAsync(nameof(AgentsPage), true);
		}
    }
}

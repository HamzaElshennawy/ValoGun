using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using Newtonsoft.Json;
using ValoGun.Models.Maps;
using ValoGun.Pages;
namespace ValoGun.ViewModels
{
	public partial class MapsPageViewModel : BaseViewModel
	{

		public ObservableRangeCollection<ValoGun.Models.Maps.Data> Maps { get; set; } = [];

		public MapsPageViewModel()
		{
			Thread MapsThread = new(async () => await LoadMaps());
			MapsThread.Start();
		}


		[RelayCommand]
		private async Task GoToMapPage(Data map)
		{
			MapViewModel.Map = map;
			await Shell.Current.GoToAsync("MapPage", true);
		}
		private async Task LoadMaps()
		{
			using var stream = await FileSystem.OpenAppPackageFileAsync("maps.json");
			using var reader = new StreamReader(stream);
			var data = reader.ReadToEnd();
			var jsonData = JsonConvert.DeserializeObject<ValoGun.Models.Maps.Map>(data);
			Maps.Clear();
			foreach(var map in jsonData!.data)
			{
				Maps.Add(map!);
			}
			await MainThread.InvokeOnMainThreadAsync(() => OnPropertyChanged(nameof(Maps)));
		}
	}
}

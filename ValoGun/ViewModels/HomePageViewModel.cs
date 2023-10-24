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
        


        //public AsyncCommand NavigateToStatus => new(GoToPage);


        

        public HomePageViewModel()
        {
            
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

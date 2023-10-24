using MvvmHelpers;
using ValoGun.Models.Weapons;


namespace ValoGun.ViewModels
{
	public class WeaponSkinsViewModel : BaseViewModel
	{

		public ObservableRangeCollection<Skin> Skins { get; set; }

		public static Datum MainWeapon { get; set; }

		public WeaponSkinsViewModel()
		{
			Thread SkinsThread = new(async () => await LoadSkins());
			SkinsThread.Start();
		}
		public async Task<Task> LoadSkins()
		{
			IsBusy = true;
			await MainThread.InvokeOnMainThreadAsync(() => Skins.Clear());
			
			foreach(var skin in MainWeapon.skins)
			{
				Skins.Add(skin);
			}
			IsBusy = false;
			await MainThread.InvokeOnMainThreadAsync(() => OnPropertyChanged(nameof(Skins)));
			return Task.CompletedTask;
		}
	}
}

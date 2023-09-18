using MvvmHelpers;
using MvvmHelpers.Commands;
using ValoGun.Models.Weapons;

namespace ValoGun.ViewModels
{
	public class WeaponStatusViewModel : BaseViewModel
	{
		public static Datum MainWeapon { get; set; }

		public ObservableRangeCollection<Skin> Skins { get; set; } = new();



		public AsyncCommand RefreshCommand { get; set; }

		public float totalProgress { get; set; }
		public float progress { get; set; }
		public WeaponStatusViewModel()
		{
			Title = MainWeapon.displayName;
			Thread SkinsThread = new(async () => await LoadSkins());
			SkinsThread.Start();
			RefreshCommand = new AsyncCommand(LoadSkins);
		}

		public async Task<Task> LoadSkins()
		{
			IsBusy = true;
			float Counter = 0;
			await MainThread.InvokeOnMainThreadAsync(() => Skins.Clear());
			totalProgress = MainWeapon.skins.Length;
			foreach (var skin in MainWeapon.skins)
			{
				Skins.Add(skin);
				progress = Counter / totalProgress;
				Counter++;
				await MainThread.InvokeOnMainThreadAsync(() => OnPropertyChanged(nameof(progress)));
				await MainThread.InvokeOnMainThreadAsync(() => OnPropertyChanged(nameof(Skins)));
			}
			IsBusy = false;
			return Task.CompletedTask;
		}
	}
}

using CommunityToolkit.Mvvm.ComponentModel;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ValoGun.Models;

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
			OnPropertyChanged(nameof(MainWeapon));
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
			//for(int i = 0; i<totalProgress; i++)
			//{
			//	SkinsToShow _skin = new();
			//	Skins.Add(_skin);
			//	progress = Counter / totalProgress;
			//	Counter++;
			//	await MainThread.InvokeOnMainThreadAsync(() => OnPropertyChanged(nameof(progress)));
			//	await MainThread.InvokeOnMainThreadAsync(() => OnPropertyChanged(nameof(Skins)));
			//}
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

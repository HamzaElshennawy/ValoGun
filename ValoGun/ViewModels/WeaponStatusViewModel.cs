using CommunityToolkit.Mvvm.ComponentModel;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoGun.Models;

namespace ValoGun.ViewModels
{
    public class WeaponStatusViewModel : BaseViewModel
    {
		public static Datum MainWeapon {get; set;}

		public ObservableRangeCollection<Skin> Skins { get; set; } = new();
		
		public WeaponStatusViewModel()
		{
			Title = MainWeapon.displayName;
			OnPropertyChanged(nameof(MainWeapon));
			Task.Run(LoadSkins);
		}

		public Task LoadSkins()
		{
			Skins.Clear();
			foreach (var skin in MainWeapon.skins)
			{
				Skins.Add(skin);
				OnPropertyChanged(nameof(Skins));
			}
			return Task.CompletedTask;
		}
		
	}
}

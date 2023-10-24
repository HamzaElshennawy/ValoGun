using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoGun.Models.Weapons;

namespace ValoGun.ViewModels
{
	public class WeaponDetailsViewModel : BaseViewModel
	{
		public static Datum MainWeapon { get; set; }

		public Datum SelectedWeapon { get; set; }
		public WeaponDetailsViewModel()
		{
			SelectedWeapon = MainWeapon;
		}
	}
}

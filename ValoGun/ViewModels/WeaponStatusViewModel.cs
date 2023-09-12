using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoGun.Models;

namespace ValoGun.ViewModels
{
    [QueryProperty(nameof(MainWeapon), nameof(MainWeapon))]
    public class WeaponStatusViewModel : BaseViewModel
    {
        
        public Weapons MainWeapon { get; set; } = new();


    }
}

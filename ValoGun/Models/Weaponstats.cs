using static ValoGun.Models.Weapons;

namespace ValoGun.Models
{

	public class Weaponstats
	{
		public float fireRate { get; set; }
		public int magazineSize { get; set; }
		public float runSpeedMultiplier { get; set; }
		public float equipTimeSeconds { get; set; }
		public float reloadTimeSeconds { get; set; }
		public float firstBulletAccuracy { get; set; }
		public int shotgunPelletCount { get; set; }
		public string wallPenetration { get; set; }
		public string feature { get; set; }
		public string fireMode { get; set; }
		public string altFireType { get; set; }
		public Adsstats adsStats { get; set; }
		public Altshotgunstats altShotgunStats { get; set; }
		public Airburststats airBurstStats { get; set; }
		public Damagerange[] damageRanges { get; set; }
	}

}
